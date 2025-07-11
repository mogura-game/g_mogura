using UnityEngine;

namespace App.Game.Entities {
    [CreateAssetMenu(menuName = "States/IdleState", fileName = "IdleState")]
    public class IdleState : BaseState {
    /// <summary>
    /// Base class for implementing a default Idle State using ScriptableObject.
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