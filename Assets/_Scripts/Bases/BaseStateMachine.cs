using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Game.Entities {
    /// <summary>
    /// Base class for implementing StateMachines from ScriptableObject States.
    /// Inherit from this class to define specific behaviors for each State Machine.
    /// </summary>
    public abstract class BaseStateMachine : MonoBehaviour {
    // ? DEBUG======================================================================================================================================
        [Tooltip("Enables debugging logs for this object.")]
        [SerializeField] protected bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Default State for State Machine to execute.")]
        [SerializeField] protected BaseState initialState;
        [Tooltip("Reference to the EntityController inherited class this machine is assigned to.")]
        [SerializeField] public EntityController controller;

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Displays currently executing State.")]
        [SerializeField] public BaseState currentState;
        [SerializeField] public List<BaseState> EntityStatesList = new List<BaseState>();
        [Tooltip("List of all available States this BaseStateMachine inherited class can execute.")]

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        protected virtual void Awake() {
            this.controller ??= this.GetComponent<EntityController>();
        }

        protected virtual void Start() {
            if (this.initialState) this.ChangeState(this.initialState);
        }

        protected virtual void Update() {
            this.currentState?.OnExecute();

            if (DEBUG) Debug.Log($"[SM] Current state: {this.currentState?.GetType().Name}", this);
        }

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Replaces current State to a new one.
        /// </summary>
        /// <param name="newState">State to transition to.</param>
        public virtual void ChangeState(BaseState newState) {
            if (!newState) return; //Prevents null input

            if (DEBUG) Debug.Log("[SM] State" + this.currentState?.GetType().Name + " changed to " + newState.GetType().Name + ", this");

            this.currentState?.OnExit();
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