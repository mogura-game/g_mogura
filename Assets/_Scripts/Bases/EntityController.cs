using UnityEngine;

namespace App.Game.Entities {
    public abstract class EntityController : MonoBehaviour {
        // ? DEBUG======================================================================================================================================
        [SerializeField] protected bool DEBUG = false;

        // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Reference to the Entity State Machine inherited class controlling this entity.")]
        [SerializeField] protected BaseStateMachine stateMachine;

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Current state of the entity.")]
        [SerializeField] public EntityStates currentState;

        // * INTERNAL

        // ? BASE METHODS===============================================================================================================================
        private void Start() {
            if (ReferenceEquals(this.stateMachine, null)) if (DEBUG) Debug.Log("[PC] Entity SM initialized");
            else Debug.LogError("[PC] No StateMachine assigned!");
        }

        // ? CUSTOM METHODS=============================================================================================================================

        // ? EVENT METHODS==============================================================================================================================

    }
}