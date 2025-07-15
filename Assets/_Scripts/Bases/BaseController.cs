using System.Collections.Generic;
using UnityEngine;

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
        [Tooltip("Reference to the BaseStateMachine inherited class controlling this Entity.")]
        [SerializeField] protected BaseStateMachine stateMachine;
        //[Tooltip("Reference to the Animator attached to this Entity.")]
        [SerializeField] public BaseAnimator baseAnimator;

        // * ATTRIBUTES
        //[Tooltip("List of all available States this BaseStateMachine inherited class can execute.")]
        [SerializeField] public List<BaseState> entityStatesList = new List<BaseState>();

        // * INTERNAL
        protected EntityStates currentState => this.stateMachine.currentState.id;

    // ? BASE METHODS===============================================================================================================================
        protected virtual void Awake() {}

        protected virtual void Start() {
            this.stateMachine?.Initialize();

            if (this.stateMachine is null) if (DEBUG) Debug.Log("[EC] Entity SM initialized");
            else Debug.LogError("[EC] No StateMachine assigned!");
        }

        protected virtual void FixedUpdate() {
            this.stateMachine?.Execute();
        }

        protected virtual void Update() {}

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================

    }
}