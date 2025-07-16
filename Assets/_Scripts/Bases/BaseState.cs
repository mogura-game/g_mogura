using UnityEngine;

namespace App.Game.Entities {
    public abstract class BaseState : ScriptableObject, IState {
    /// <summary>
    /// Base class for implementing States using ScriptableObject.
    /// Inherit from this class to define specific transition and behaviours per State.
    /// </summary>
    // ? DEBUG======================================================================================================================================
        [SerializeField] protected bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Reference to the active StateMachine.")]
        [SerializeField] protected BaseStateMachine stateMachine;

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Identifier for this state.")]
        [SerializeField] public EntityStates id;
        [SerializeField] protected float stopThreshold;
        [SerializeField] protected float fallThreshold;

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        public virtual void OnExecute() {
            if (DEBUG) Debug.Log($"[S] State " + this + " executed");
        }

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================
        public virtual void OnEnter(BaseStateMachine stateMachine) {
            this.stateMachine = stateMachine;

            this.stateMachine.RequestStateAnimation();

            if (DEBUG) Debug.Log($"[S] State " + this + " entered");
        }

        public virtual void OnExit() {
            if (DEBUG) Debug.Log($"[S] State " + this + " exited");
        }
    }
}