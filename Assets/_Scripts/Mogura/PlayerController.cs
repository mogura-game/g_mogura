using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Player-specific implementation of the EntityCon oller.
    /// Handles player-specific state logic and behaviours.
    /// </summary>
    public class PlayerController : BaseController {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES

        // * INTERNAL

        private void Start() {
            if (ReferenceEquals(this.stateMachine, null)) if (DEBUG) Debug.Log("[PC] Player SM initialized");
            else Debug.LogError("[PC] No PlayerStateMachine assigned!");
    // ? BASE METHODS===============================================================================================================================
        }


    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Custom Move implementation for Player entity.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void Move() {
            if (this.currentState != EntityStates.move) this.stateMachine.ChangeState(this.stateMachine.EntityStatesList[(int)EntityStates.move]);
        }

    }
}