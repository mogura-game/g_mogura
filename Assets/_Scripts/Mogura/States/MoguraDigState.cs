using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura dig State class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Dig", fileName = "MoguraDigState")]
    public class DigState : BaseState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        
        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
            
            this.stateMachine?.SetActionsLock(true);
            this.stateMachine?.SetMovementLock(true);
            this.stateMachine?.SetStateGravity(this.baseGravity);
            this.stateMachine?.ResetPhysics();
        }
    }
}