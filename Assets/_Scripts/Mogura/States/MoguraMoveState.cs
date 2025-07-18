using Unity.Cinemachine;
using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura move State class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Move", fileName = "MoguraMoveState")]
    public class MoguraMoveState : PlayerState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        
        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        public override void OnExecute () {
            base.OnExecute();

            this.MoveFromInput();

            if (Mathf.Abs(this.PlayerVelocity.x) < this.stopThreshold) this.SM?.ChangeState(EntityState.idle);
            else if (!this.SM.PlayerGrounded && this.PlayerVelocity.y < 0.0f) this.SM?.ChangeState(EntityState.fall);
        }

    // ? CUSTOM METHODS=============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
            
            this.SM?.SetActionsLock(false);
            this.SM?.SetMovementLock(false);
            this.SM?.SetStateGravity(this.baseGravity);
        }
        
    // ? EVENT METHODS==============================================================================================================================

    }
}