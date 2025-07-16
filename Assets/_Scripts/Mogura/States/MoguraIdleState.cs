using Unity.Cinemachine;
using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura IdleState class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Idle", fileName = "MoguraIdleState")]
    public class MoguraIdleState : PlayerState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        
        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        public override void OnExecute () {
            base.OnExecute();

            this.MoveFromInput();

            if (this.PlayerVelocity.Abs().x >= this.stopThreshold) this.SM?.ChangeState(EntityState.move); 
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