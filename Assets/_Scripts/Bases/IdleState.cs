using UnityEngine;

namespace App.Game.Entities {
    /// <summary>
    /// Base class for implementing a default Idle State from BaseState.
    /// </summary>
    public abstract class IdleState : BaseState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        
        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        private void Awake() {
            this.id = EntityStates.idle;
        }

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {

            base.OnEnter(stateMachine);
        }
        
    }
}