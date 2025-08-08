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
        [Tooltip("Maximum fall State gravity value.")]
        [SerializeField, Range(0.0f, 5.0f)] private float maxGravity = 3.0f;
        
        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        public override void OnExecute () {
            base.OnExecute();

            if (this.baseGravity < this.maxGravity) {
                this.baseGravity += Time.fixedDeltaTime * 2;
            } else this.baseGravity = this.maxGravity;

            this.SM?.SetStateGravity(this.baseGravity);

            if (this.SM.PlayerGrounded) {
                if (Mathf.Abs(this.PlayerVelocity.x) > this.stopThreshold) this.SM?.ChangeState(EntityState.move);
                else this.SM?.ChangeState(EntityState.idle);
            }
        }

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
            
            this.SM?.SetActionsLock(true);
            this.SM?.SetMovementLock(false);
        }
    }
}