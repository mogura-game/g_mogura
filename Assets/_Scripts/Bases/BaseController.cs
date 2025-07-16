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

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("List of all available States this Entity can execute.")]
        [SerializeField] private BaseState[] entityStates;
        [Tooltip("Player Rigidbody linear velocity property shorthand acting as a Getter function.")]
        public Vector2 Velocity => this.rb.linearVelocity;
        [Tooltip("Defines whether the Player is facing right or not.")]
        public bool facingRight = true;

        // * INTERNAL
        protected BaseStateMachine stateMachine;
        public bool actionsUnlocked = true;
        public bool movementUnlocked = true;

    // ? BASE METHODS===============================================================================================================================
        protected virtual void Awake() {
            this.ValidateStates();
            this.BuildStatesDictionary();
        }

        protected virtual void Start() {
            this.stateMachine?.Initialize();
        }

        protected virtual void Update() {
            this.transform.localScale = new Vector3(this.facingRight ? 1 : -1, this.transform.localScale.y, this.transform.localScale.z);
        }

        protected virtual void FixedUpdate() {
            if (this.Velocity.x < 0.0f) this.facingRight = false;
            else if (this.Velocity.x > 0.0f) this.facingRight = true;

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

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Function called from StateMachine to ask for dynamic animation clip per State.
        /// Inherit to handle custom naming logic and sending concatenated strings.
        /// </summary>
        public abstract void UpdateStateAnimation(EntityState id);
    }
}