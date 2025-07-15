using UnityEngine;

namespace App.Game.Entities {
    /// <summary>
    /// Base class for implementing a default Move State.
    /// </summary>
    [CreateAssetMenu(menuName = "States/BaseMove", fileName = "MoveState")]
    public abstract class MoveState : BaseState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        private void Awake() {
            this.id = EntityStates.move;
        }

    // ? CUSTOM METHODS=============================================================================================================================
        
    // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {

            base.OnEnter(stateMachine);
        }

    }
}