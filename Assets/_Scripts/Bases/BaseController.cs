using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace App.Game.Entities {
    /// <summary>
    /// Generic implementation for handling logic and behaviors.
    /// Inherit from this class to define specific logic and behaviours per Entity.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    [RequireComponent(typeof(BaseAnimator))]
    //[RequireComponent(typeof(BaseSounder))]
    public abstract class BaseController : MonoBehaviour {
    // ? DEBUG======================================================================================================================================
        [SerializeField] protected bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Reference to the Rigidbody2D attached to the Player.")]
        [SerializeField] protected Rigidbody2D rb;
        [Tooltip("Reference to the Animator script attached to this Entity.")]
        [SerializeField] protected BaseAnimator baseAnimator;
        [Tooltip("Property Player Rigidbody2D get linear velocity.")]
        public Vector2 GetCurrentLinearVelocity => this.rb.linearVelocity;
        [Tooltip("Reference to the StateMachine script created for this Entity.")]
        protected BaseStateMachine stateMachine;

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("List of all available States this Entity can execute.")]
        [SerializeField] private BaseState[] entityStates;
        [Tooltip("Vector which determines Entity max velocity per axis.")]
        [SerializeField] private Vector2 maxVelocity = Vector2.one;
        [Tooltip("Determines the current Entity speed value. (Ranging from 0 to 1)")]
        [SerializeField, Range(0, 5)] protected float speed = 1.0f;
        [Tooltip("Defines whether the Player is facing right or not.")]
        [SerializeField] protected bool facingRight = true;

        // * INTERNAL
        [HideInInspector] public bool actionsLocked = false;
        [HideInInspector] public bool movementLocked = false;

    // ? BASE METHODS===============================================================================================================================
        protected virtual void Awake() {
            this.ValidateStates();
            this.BuildStatesDictionary();
        }

        protected virtual void Start() {
            this.stateMachine?.Initialize();
        }

        protected virtual void Update() {
            if (this.GetCurrentLinearVelocity.x < -0.1f) this.facingRight = false;
            else if (this.GetCurrentLinearVelocity.x > 0.1f) this.facingRight = true;

            this.SetFacingDirection();
        }

        public void SetFacingDirection() {
            this.transform.localScale = new Vector3(this.facingRight ? 1 : -1, this.transform.localScale.y, this.transform.localScale.z);
        }

        protected virtual void FixedUpdate() {
            Vector2 velocity = this.GetCurrentLinearVelocity;

            velocity.x = Mathf.Clamp(velocity.x, -this.maxVelocity.x, this.maxVelocity.x);
            velocity.y = Mathf.Clamp(velocity.y, -this.maxVelocity.y, this.maxVelocity.y);

            this.rb.linearVelocity = velocity;

            this.stateMachine?.Execute();
        }

        private void OnValidate() {
            this.ValidateStates();
        }

    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// Validates that all EntityStates enum States are present in the entityStates array.
        /// Sends error logs for each missing or null State references.
        /// </summary>
        private void ValidateStates() {
            for (int i = 0; i < this.entityStates.Count(); i++) {
                if (!this.entityStates.ElementAt(i)) {
                    Debug.LogError($"[BC] Null State entry at: {i}");
                    return;
                }
            }

            List<EntityState> enumValues = Enum.GetValues(typeof(EntityState)).Cast<EntityState>().ToList(); //Listed States
            List<EntityState> assignedValues = this.entityStates.Where(s => s != null).Select(s => s.id).ToList(); //Assigned States

            List<EntityState> missing = enumValues.Except(assignedValues).ToList();
            
            if (missing.Any()) Debug.LogError($"[BC] Missing State impletations: {string.Join(", ", missing)}");
        }

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Method called from StateMachine to ask for dynamic animation clip per State.
        /// Inherit to handle custom naming logic and sending concatenated strings.
        /// </summary>
        public abstract void UpdateStateAnimation(EntityState id);
        
        /// <summary>
        /// Returns a Dictionary using EntityStates as Key with its respective BaseState value from entityStates array.
        /// Called fron BaseStateMachine to receive the actual and validated State list ready to use.
        /// </summary>
        public Dictionary<EntityState, BaseState> BuildStatesDictionary() {
            Dictionary<EntityState, BaseState> dictionary = new Dictionary<EntityState, BaseState>();

            foreach (BaseState state in this.entityStates) {
                if (state is null) {
                    Debug.LogError("[BC] Null State in entityStates array.");
                    continue;
                } else {
                    dictionary.Add(state.id, state);
                }
            }

            if (DEBUG) Debug.Log($"[BC] Dictionary built with {dictionary.Count} States.");
            return dictionary;
        }
        
        /// <summary>
        /// Method called from StateMachine to handle custom gravity changes per State.
        /// </summary>
        public void SetGravityScale(float scale) => this.rb.gravityScale = scale;
    
        /// <summary>
        /// Method to set custom velocities directly to this Entity Rigidbody2D.
        /// </summary>
        /// <param name="movement">Amount of movement to set.</param>
        public virtual void SetVelocity(Vector2 movement) => this.rb.linearVelocity = movement;
        
        /// <summary>
        /// Method to add forces to this Entity Rigidbody2D using ForceMode2D.Force.
        /// </summary>
        /// <param name="movement"></param>
        public virtual void SetMoveForce(Vector2 movement) => this.rb.AddForce(movement * this.speed, ForceMode2D.Force);
    }
}