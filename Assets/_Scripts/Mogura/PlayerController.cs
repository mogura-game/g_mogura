using UnityEngine;

namespace App.Game.Entities.Player {
    /// <summary>
    /// Player-specific implementation of the EntityController.
    /// Handles player-specific state transitions and behavior.
    /// </summary>
    public class PlayerController : EntityController {
        // ? DEBUG======================================================================================================================================

        // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES

        // * INTERNAL

        // ? BASE METHODS===============================================================================================================================
        private void Start() {
            if (ReferenceEquals(this.stateMachine, null)) if (DEBUG) Debug.Log("[PC] Player SM initialized");
            else Debug.LogError("[PC] No PlayerStateMachine assigned!");
        }

        // ? CUSTOM METHODS=============================================================================================================================

        // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Custom Move implementation for Player entity.
        /// Called by PlayerInput Move event.
        /// </summary>
        public void Move() {
            if (this.currentState != EntityStates.move) this.stateMachine.ChangeState(this.stateMachine.EntityStatesList[(int)EntityStates.move]);
        }

    }
}