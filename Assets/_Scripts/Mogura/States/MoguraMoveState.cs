using Unity.Cinemachine;
using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura MoveState class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Move", fileName = "MoguraMoveState")]
    public class MoguraMoveState : MoveState {
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
            base.OnExecute();
        }
        
    }
}