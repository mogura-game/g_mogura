using System.Collections.Generic;
using UnityEngine;
using System;

namespace App.Game.Entities {
    /// <summary>
    /// Base class for implementing StateMachines from ScriptableObject States.
    /// Inherit from this class to define specific behaviors for each State Machine.
    /// </summary>
    [Serializable]
    public abstract class BaseStateMachine {
    // ? DEBUG======================================================================================================================================
        [Tooltip("Enables debugging logs for this object.")]
        [SerializeField] protected bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Reference to the Controller script this machine is assigned to.")]
        [SerializeField] public BaseController baseController;

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Default State for this StateMachine to execute.")]
        [SerializeField] protected EntityState initialState;
        [Tooltip("Displays currently executing State.")]
        [SerializeField] public BaseState currentState;
        [Tooltip("Property to get Entity Rigidbody2D current linear velocity.")]
        public Vector2 GetEntityVelocity => this.baseController?.GetCurrentLinearVelocity ?? Vector2.zero;
        [Tooltip("Property to get Entity movement lock value.")]
        public bool MovementLocked => this.baseController?.movementLocked ?? false;
        [Tooltip("Property to get Entity actions lock value.")]
        public bool ActionsLocked => this.baseController?.actionsLocked ?? false;

        // * INTERNAL
        private Dictionary<EntityState, BaseState> states;

    // ? BASE METHODS===============================================================================================================================
        /// <summary>
        /// Initializes this Entity States dictionary and enters initial State.
        /// </summary>
        public virtual void Initialize() {
            this.states = this.baseController?.BuildStatesDictionary();

            this.ChangeState(this.initialState);
        }

        public virtual void Execute() {
            if (DEBUG) Debug.Log($"[SM] Current state: {this.currentState?.id}");
            
            this.currentState?.OnExecute();
        }

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Replaces current State to a new one.
        /// State name must be defined in EntityStates enum.
        /// </summary>
        /// <param name="newState">Next transition State set to.</param>
        public virtual void ChangeState(EntityState newState) {
            BaseState nextState = this.states[newState];
            if (this.currentState?.id == nextState.id) return;

            this.currentState?.OnExit();

            if (DEBUG) Debug.Log($"[SM] State {this.currentState?.id} changed to {nextState.id}");
            this.currentState = nextState;

            this.currentState?.OnEnter(this);
        }

        /// <summary>
        /// Sends to Controller animation updates to communicate with Animator. 
        /// </summary>
        public void RequestStateAnimation() {
            this.baseController?.UpdateStateAnimation(this.currentState.id);
        }
        
        /// <summary>
        /// Resets to 0 both Entity rotation and velocity Ridigbody2D values.
        /// </summary>
        public void ResetPhysics() {
            //this.baseController?.ResetRotation();
            this.baseController?.SetVelocity(Vector2.zero);
        }

        /// <summary>
        /// Sends direction of movement force to this Entity Controller.
        /// </summary>
        /// <param name="direction">Movement force value.</param>
        public void MoveDirection(Vector2 direction) => this.baseController?.SetMoveForce(direction);

        /// <summary>
        /// Sends the added Vector2 of current velocity and jump force to this Entity Controller.
        /// </summary>
        /// <param name="force">Jump force value.</param>
        public virtual void JumpDirection(Vector2 force) => this.baseController?.SetVelocity(force + this.GetEntityVelocity);

        /// <summary>
        /// Sends the State movement lock value to this Entity Controller.
        /// </summary>
        /// <param name="locked">Value set movement lock to.</param>
        public void SetMovementLock(bool locked) => this.baseController.movementLocked = locked;
        
        /// <summary>
        /// Sends the State actionss lock value to this Entity Controller.
        /// </summary>
        /// <param name="locked">Value set actions lock to.</param>
        public void SetActionsLock(bool locked) => this.baseController.actionsLocked = locked;
        
        /// <summary>
        /// Sends the scale gravity value to this Entity Controller.
        /// </summary>
        /// <param name="scale">Value to set gravity scale.</param>
        public void SetStateGravity(float scale) => this.baseController.SetGravityScale(scale);
    }

    /// <summary>
    /// Enumeration of possible states for an Entity.
    /// Used for identifying and indexing States.
    /// </summary>
    [Serializable]
    public enum EntityState {
        idle,
        move,
        charge,
        jump,
        fall,
        dig_in,
        dig,
        dig_out,
    }
}