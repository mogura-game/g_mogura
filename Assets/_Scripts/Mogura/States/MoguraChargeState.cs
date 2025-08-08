using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura charge State class for managing custom State transitions.
    /// </summary>
    // TODO: Update Interfaces to allow PlayerStates without needing Inputs or thresholds unnecesarely
    [CreateAssetMenu(menuName = "States/Mogura/Charge", fileName = "MoguraChargeState")]
    public class ChargeState : BaseState {
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
            
            this.stateMachine?.SetActionsLock(false);
            this.stateMachine?.SetMovementLock(true);
            this.stateMachine?.SetStateGravity(this.baseGravity);
            this.stateMachine?.ResetPhysics();
        }
    }
}