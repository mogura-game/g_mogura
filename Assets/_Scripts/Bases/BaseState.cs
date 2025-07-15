using UnityEngine;

namespace App.Game.Entities {
    public abstract class BaseState : ScriptableObject, IState {
    /// <summary>
    /// Base class for implementing States using ScriptableObject.
    /// Inherit from this class to define specific transition and behaviours for each State.
    /// </summary>
    // TODO: Update naming convention for expresion-bodied properties, matching global entity attributes and entity-specific attributes
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
        [SerializeField] public EntityStates id;
        [Tooltip("AnimationClip to play on entering this State.")]
        [SerializeField] public AnimationClip clip;

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        public virtual void OnEnter(BaseStateMachine stateMachine) {
            this.stateMachine = stateMachine;

            if (DEBUG) Debug.Log($"[S] State " + this + " started");
        }

        public virtual void OnExecute() {
            if (DEBUG) Debug.Log($"[S] State " + this + " executed");
        }

        public virtual void OnExit() {
            if (DEBUG) Debug.Log($"[S] State " + this + " ended");
        }
    }
}