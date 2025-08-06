using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura dig-out State class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Dig-Out", fileName = "MoguraDigOutState")]
    public class MoguraDigOutState : BaseState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Time to change Player Dig-In State.")]
        [SerializeField] private float digDuration = 0.25f;
        
        // * INTERNAL
        private PlayerStateMachine SM => this.stateMachine as PlayerStateMachine;
        private PlayerController PC => this.SM.baseController as PlayerController;
        [SerializeField] private float timeOnState = 0.0f;

    // ? BASE METHODS===============================================================================================================================
        public override void OnExecute () {
            base.OnExecute();

            if (this.timeOnState > this.digDuration) this.SM?.ChangeState(EntityState.idle);
            this.timeOnState += Time.fixedDeltaTime;
        }

    // ? CUSTOM METHODS=============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
            
            this.timeOnState = 0.0f;
            this.SM?.SetActionsLock(true);
            this.SM?.SetMovementLock(true);
            this.SM?.SetStateGravity(this.baseGravity);
            this.SM?.ResetPhysics();

            // ! TEMP: dig state position correction
            this.SM.baseController.transform.position += Vector3.down * 0.5f; 
        }
        
    // ? EVENT METHODS==============================================================================================================================
    }
}