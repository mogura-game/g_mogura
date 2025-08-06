using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Player-specific implementation for handling custom animations and Animator managging.
    /// </summary>
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAnimator : BaseAnimator {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================
        public void UpdateAnimationClipSpeed(float objectSpeed) => this.ani.speed = objectSpeed;
    }
}
