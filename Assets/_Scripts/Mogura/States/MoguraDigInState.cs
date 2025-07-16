using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura DigEnter class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Dig-In", fileName = "MoguraDigInState")]
    public class MoguraDigInState : BaseState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Time to change Player Dig-In State.")]
        [SerializeField] private float digTime = 0.1f;
        
        // * INTERNAL
        private PlayerController PC => this.stateMachine.baseController as PlayerController;
        private PlayerStateMachine SM => this.stateMachine as PlayerStateMachine;
        [SerializeField] private float timeOnState = 0.0f;

    // ? BASE METHODS===============================================================================================================================
        public override void OnExecute () {
            base.OnExecute();

            if (this.timeOnState > this.digTime) this.SM.ChangeState(EntityState.idle);
            this.timeOnState += Time.fixedDeltaTime;
        }

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
            
            this.SM.DisableActionsLock();
            this.SM.DisableMovementLock();
            this.timeOnState = 0.0f;
            this.PC?.ResetPhyisics();
        }

        public override void OnExit() {
            base.OnExit();
        }
    }
}