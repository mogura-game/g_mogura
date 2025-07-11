using UnityEngine;

namespace App.Game.Entities {
    public abstract class BaseState : ScriptableObject, IState {
    /// <summary>
    /// Base class for implementing States using ScriptableObject.
    /// Inherit from this class to define specific transition and behaviours for each State.
    /// </summary>
        // ? DEBUG======================================================================================================================================
        [SerializeField] protected bool DEBUG = false;

        // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Reference to the active state machine.")]
        [SerializeField] protected BaseStateMachine stateMachine;

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Identifier for this state.")]
        [SerializeField] protected EntityStates id;

        // * INTERNAL

        // ? BASE METHODS===============================================================================================================================

        // ? CUSTOM METHODS=============================================================================================================================
        
        // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Executed right at the start of the State.
        /// </summary>
        public virtual void OnEnter(BaseStateMachine stateMachine) {
            this.stateMachine = stateMachine;

            if (DEBUG) Debug.Log($"[S] State " + this + " started");
        }
 
        /// <summary>
        /// Executed from every StateMachine fixed update call.
        /// </summary>
        public virtual void OnExecute() {
            if (DEBUG) Debug.Log($"[S] State " + this + " executed");
        }

        /// <summary>
        /// Executed just before ending the State.
        /// </summary>
        public virtual void OnExit() {
            if (DEBUG) Debug.Log($"[S] State " + this + " ended");
        }
    }
}