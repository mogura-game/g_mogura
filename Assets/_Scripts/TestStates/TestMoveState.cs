using UnityEngine;

namespace App.Game.Entities.Test {
    [CreateAssetMenu(menuName = "States/TestState", fileName = "TestState")]
    public class TestMoveState : BaseState {
    /// <summary>
    /// Test class for implementing a default Test State using ScriptableObject.
    /// </summary>
        // ? DEBUG======================================================================================================================================

        // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES

        // * INTERNAL

        // ? BASE METHODS===============================================================================================================================

        // ? CUSTOM METHODS=============================================================================================================================
        
        // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            Debug.Log("Moving");
            stateMachine.controller.currentState = this.id;

            base.OnEnter(stateMachine);
        }

    }
}