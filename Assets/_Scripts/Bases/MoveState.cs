using UnityEngine;

namespace App.Game.Entities {
    [CreateAssetMenu(menuName = "States/MoveState", fileName = "MoveState")]
    public class MoveState : BaseState {
    /// <summary>
    /// Base class for implementing a default Move State using ScriptableObject.
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

            base.OnEnter(stateMachine);
        }

    }
}