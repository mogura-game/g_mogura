using Unity.Cinemachine;
using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura IdleState class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Idle", fileName = "MoguraIdleState")]
    public class MoguraIdleState : IdleState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        
        // * INTERNAL
        private PlayerController PC => this.stateMachine.baseController as PlayerController;
        private PlayerStateMachine SM => this.stateMachine as PlayerStateMachine;

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
        }

        public override void OnExecute () {
            if (this.PC.rb.linearVelocity.Abs().x >= 0.25f) this.SM.ChangeState(EntityStates.move); 

            base.OnExecute();
        }
    }
}