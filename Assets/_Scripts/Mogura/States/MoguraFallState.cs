using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura fall State class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Fall", fileName = "MoguraFallState")]
    public class FallState : PlayerState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        
        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        public override void OnExecute () {
            base.OnExecute();

            this.MoveFromInput();

            if (this.baseGravity < 2.0f) {
                this.baseGravity += Time.fixedDeltaTime;
                this.SM?.SetStateGravity(this.baseGravity);
            } else this.baseGravity = 2.0f;

            // TODO: Add floor detection
            if (this.PlayerVelocity.y >= 0.0f) this.SM?.ChangeState(EntityState.idle); 
        }

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
            
            this.SM?.SetActionsLock(true);
            this.SM?.SetMovementLock(false);
            this.baseGravity = 1.0f;
        }
    }
}