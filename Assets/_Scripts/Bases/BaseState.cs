using UnityEngine;

namespace App.Game.Entities {
    /// <summary>
    /// Base class for implementing States using ScriptableObject.
    /// Inherit from this class to define specific transition and behaviours per State.
    /// </summary>
    public abstract class BaseState : ScriptableObject, IState {
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
        [SerializeField] public EntityState id;
        [Tooltip("Defines default gravity scale on entering the State.")]
        [SerializeField, Range(0, 5)] protected float baseGravity = 1.0f;
        [Tooltip("Min velocity to set Player to Idle.")]
        [SerializeField] protected float stopThreshold = 0.25f;
        [Tooltip("Min velocity to set Player to Fall.")]
        [SerializeField] protected float fallThreshold = 0.1f;

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