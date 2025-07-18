using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Base class for implementing a default Player States from BaseState.
    /// This enables use IMoveFromInput and send Inputs-based movements.
    /// </summary>
    public abstract class PlayerState : BaseState, IPlayerMovableState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Property to get Player Entity current linear velocity.")]
        public Vector2 PlayerVelocity => this.SM?.GetEntityVelocity ?? Vector2.zero;
        [Tooltip("Property to get Player Input direction")]
        public Vector2 PlayerDirection => this.SM?.InputDirection ?? Vector2.zero;
        
        // * INTERNAL
        protected PlayerStateMachine SM => this.stateMachine as PlayerStateMachine;

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================
        public virtual void MoveFromInput() {
            if (!this.SM.MovementLocked && this.PlayerDirection.sqrMagnitude >= 0.01f) this.SM?.MoveDirection(16 * this.PlayerDirection.x * Vector2.right);
        }
        
    // ? EVENT METHODS==============================================================================================================================

    }
}