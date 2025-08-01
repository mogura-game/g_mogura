using UnityEngine;

namespace App.Game.Entities.Test {
    [CreateAssetMenu(menuName = "States/TestState", fileName = "TestState")]
    public class TestIdleState : BaseState {
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
            Debug.Log("Idling");

            base.OnEnter(stateMachine);
        }
        
    }
}