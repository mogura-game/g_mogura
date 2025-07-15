using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Game.Entities {
    /// <summary>
    /// Base class for implementing StateMachines from ScriptableObject States.
    /// Inherit from this class to define specific behaviors for each State Machine.
    /// </summary>
    [RequireComponent(typeof(BaseController))]
    public abstract class BaseStateMachine : MonoBehaviour {
    // ? DEBUG======================================================================================================================================
        [Tooltip("Enables debugging logs for this object.")]
        [SerializeField] protected bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Default State for State Machine to execute.")]
        [SerializeField] protected EntityStates initialState;
        [Tooltip("Reference to the EntityController inherited class this machine is assigned to.")]
        [SerializeField] public BaseController controller;

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Displays currently executing State.")]
        [SerializeField] public BaseState currentState;
        [Tooltip("List of all available States this BaseStateMachine inherited class can execute.")]
        [SerializeField] public List<BaseState> entityStatesList = new List<BaseState>();

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        protected virtual void Awake() {
            this.controller ??= this.GetComponent<BaseController>();
        }

        protected virtual void Start() {
            this.ChangeState(this.initialState);
        }

        protected virtual void FixedUpdate() {
            if (DEBUG) Debug.Log($"[SM] Current state: {this.currentState?.id}", this);
            
            this.currentState?.OnExecute();
        }

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Replaces current State to a new one.
        /// State name must be defined in EntityStates enum.
        /// </summary>
        /// <param name="newState">State to transition to.</param>
        public virtual void ChangeState(EntityStates newState) {
            BaseState nextState = this.entityStatesList[(int)newState];
            if (this.currentState && this.currentState.id == nextState.id) return;

            this.currentState?.OnExit();

            if (DEBUG) Debug.Log($"[SM] State {this.currentState?.id} changed to {nextState.id}", this);
            this.currentState = nextState;

            this.currentState?.OnEnter(this);
        }
    }

    /// <summary>
    /// Enumeration of possible states for an Entity.
    /// Used for identifying and indexing States.
    /// </summary>
    [Serializable]
    public enum EntityStates {
        idle,
        move
    }
}