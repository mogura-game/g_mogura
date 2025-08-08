using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura block State class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Block", fileName = "MoguraBlockState")]
    public class MoguraBlockState : PlayerState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        
        // * INTERNAL
        private PlayerController PC => this.SM.baseController as PlayerController;
        [SerializeField] private float timeOnState = 0.0f;

    // ? BASE METHODS===============================================================================================================================
        public override void OnExecute () {
            base.OnExecute();

            if (this.timeOnState > this.PC.blockTime) this.SM?.ChangeState(EntityState.idle);
            this.timeOnState += Time.fixedDeltaTime;
            this.PC.blockTime -= Time.fixedDeltaTime;
        }

    // ? CUSTOM METHODS=============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
            
            this.timeOnState = 0.0f;
            this.SM?.SetActionsLock(true);
            this.SM?.SetMovementLock(true);
            this.SM?.SetStateGravity(this.baseGravity);
            this.SM?.ResetPhysics();
        }
        
    // ? EVENT METHODS==============================================================================================================================
    }
}