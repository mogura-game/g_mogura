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
        public override void OnExecute () {
            if (this.PC.Velocity.Abs().x >= this.stopThreshold) this.SM.ChangeState(EntityStates.move); 

            base.OnExecute();
        }

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
        }
    }
}