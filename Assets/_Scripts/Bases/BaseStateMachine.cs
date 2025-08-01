using App.Game.Entities.Test;
using UnityEngine;

namespace App.Game.Entities {
    /// <summary>
    /// Base class for implementing StateMachines from ScriptableObject States.
    /// Inherit from this class to define specific behaviors for each State Machine.
    /// </summary>
    public abstract class BaseStateMachine : MonoBehaviour {
        // ? DEBUG======================================================================================================================================
        [SerializeField] protected bool DEBUG = false;

        // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("Base State Machine")]
        [Tooltip("Default State for State Machine to execute.")]
        [SerializeField] protected BaseState initialState;
        [SerializeField] public TestEntityController controller;

        // * ATTRIBUTES
        [Header("State Machine Attributes")]
        [Tooltip("Displays currently executing State.")]
        [SerializeField] protected BaseState currentState;

        // * INTERNAL

        // ? BASE METHODS===============================================================================================================================
        protected virtual void Awake() {
            if (this.initialState) this.ChangeState(this.initialState);
        }

        protected virtual void Update() {
            this.currentState?.OnExecute();

            if (DEBUG) Debug.Log($"[SM] Current state: {this.currentState?.GetType().Name}", this);
        }

        // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// Replaces current State to a new one.
        /// </summary>
        /// <param name="newState">State to transition to.</param>
        public virtual void ChangeState(BaseState newState) {
            if (newState == null) return;

            if (DEBUG) Debug.Log("[SM] State" + this.currentState?.GetType().Name + " changed to " + newState.GetType().Name + ", this");

            this.currentState?.OnExit();
            this.currentState = newState;
            this.currentState?.OnEnter(this);
        }

        // ? EVENT METHODS==============================================================================================================================

    }
}