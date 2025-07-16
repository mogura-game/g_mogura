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
        [SerializeField] private float timeOnState = 0.0f;
        
        // * INTERNAL
        private PlayerController PC => this.stateMachine.baseController as PlayerController;
        private PlayerStateMachine SM => this.stateMachine as PlayerStateMachine;

    // ? BASE METHODS===============================================================================================================================
        public override void OnExecute () {
            if (this.timeOnState > 5.0f) this.SM.ChangeState(EntityStates.idle);
            this.timeOnState += Time.fixedDeltaTime;

            base.OnExecute();
        }

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            this.PC.ResetPhyisics();
            
            base.OnEnter(stateMachine);
        }

        public override void OnExit() {
            this.timeOnState = 0.0f;

            base.OnExit();
        }
    }
}