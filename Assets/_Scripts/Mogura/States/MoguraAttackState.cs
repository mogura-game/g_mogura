using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura attack State class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Attack", fileName = "MoguraAttackState")]
    public class MoguraAttackState : PlayerState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Time to change Player attack State.")]
        [SerializeField] private float attackDuration = 0.15f;
        
        // * INTERNAL
        [SerializeField] private float timeOnState = 0.0f;
        private PlayerController PC => this.SM.baseController as PlayerController;

    // ? BASE METHODS===============================================================================================================================
        public override void OnExecute () {
            base.OnExecute();

            if (this.timeOnState > 0.0f) this.PC.attackCollider.enabled = false;
            if (this.timeOnState > this.attackDuration) {
                if (Mathf.Abs(this.PlayerVelocity.x) > this.stopThreshold) this.SM?.ChangeState(EntityState.move);
                else this.SM?.ChangeState(EntityState.idle);
            }
            this.timeOnState += Time.fixedDeltaTime;
        }

    // ? CUSTOM METHODS=============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
            
            this.timeOnState = 0.0f;
            this.SM?.SetActionsLock(true);
            this.SM?.SetMovementLock(false);
            this.SM?.SetStateGravity(this.baseGravity);
            this.PC.attackCollider.enabled = true;
        }
        
    // ? EVENT METHODS==============================================================================================================================
    }
}