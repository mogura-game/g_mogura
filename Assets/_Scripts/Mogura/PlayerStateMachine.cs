using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Player entity class for implementing a default StateMachine from custom ScriptableObject States.
    /// </summary>
    public class PlayerStateMachine : BaseStateMachine {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        
        // * ATTRIBUTES
        public Vector2 InputDirection => this.PC?.InputDirection ?? Vector2.zero;
        public bool PlayerGrounded => this.PC?.IsGrounded ?? false;

        // * INTERNAL
        PlayerController PC => this.baseController as PlayerController;

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================

    }
}