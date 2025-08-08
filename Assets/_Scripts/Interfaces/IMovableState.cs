using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Base class for implementing a default Player States from BaseState.
    /// This enables use IMoveFromInput and send Inputs-based movements.
    /// </summary>
    public interface IPlayerMovableState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        /// <summary>
        /// Property to get Player Entity current Rigidbody2D velocity.
        /// </summary>
        Vector2 PlayerVelocity { get; }
        /// <summary>
        /// Property to get Player Input direction.
        /// </summary>
        Vector2 PlayerDirection { get; }
        
        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// Method to add Entity StateMachine movement force.
        /// </summary>
        void MoveFromInput();
        
    // ? EVENT METHODS==============================================================================================================================
    
    }
}