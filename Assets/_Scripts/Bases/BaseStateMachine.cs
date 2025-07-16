using System.Collections.Generic;
using UnityEngine;
using System;

namespace App.Game.Entities {
    /// <summary>
    /// Base class for implementing StateMachines from ScriptableObject States.
    /// Inherit from this class to define specific behaviors for each State Machine.
    /// </summary>
    public abstract class BaseStateMachine {
    // ? DEBUG======================================================================================================================================
        [Tooltip("Enables debugging logs for this object.")]
        [SerializeField] protected bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Default State for this StateMachine to execute.")]
        [SerializeField] protected EntityState initialState;
        [Tooltip("Reference to the Controller script this machine is assigned to.")]
        [SerializeField] public BaseController baseController;

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Displays currently executing State.")]
        [SerializeField] public BaseState currentState;

        // * INTERNAL
        private Dictionary<EntityState, BaseState> states;

    // ? BASE METHODS===============================================================================================================================
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
        /// <param name="newState">State to transition to.</param>
        public virtual void ChangeState(EntityState newState) {
            BaseState nextState = this.states[newState];
            if (this.currentState?.id == nextState.id) return;

            this.currentState?.OnExit();

            if (DEBUG) Debug.Log($"[SM] State {this.currentState?.id} changed to {nextState.id}");
            this.currentState = nextState;

            this.currentState?.OnEnter(this);
        }

        public void RequestStateAnimation() {
            this.baseController?.UpdateStateAnimation(this.currentState.id);
        }

        public void EnableMovementLock() => this.baseController.movementUnlocked = true;
        
        
        public void EnableActionsLock() => this.baseController.actionsUnlocked = true;
        

        public void DisableMovementLock() => this.baseController.movementUnlocked = false;
        
        
        public void DisableActionsLock() => this.baseController.actionsUnlocked = false;
        
    }

    /// <summary>
    /// Enumeration of possible states for an Entity.
    /// Used for identifying and indexing States.
    /// </summary>
    [Serializable]
    public enum EntityState {
        idle,
        move,
        dig_in,
    }
}