using UnityEngine.InputSystem;
using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Player-specific implementation of the EntityCon oller.
    /// Handles player-specific state logic and behaviours.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    [RequireComponent(typeof(PlayerStateMachine), typeof(PlayerInput))]
    //[RequireComponent(typeof(PlayerAnimator), typeof(Animator))]
    //[RequireComponent(typeof(PlayerSounder), typeof(AudioSource))]
    public class PlayerController : BaseController {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES

        // * INTERNAL

            if (ReferenceEquals(this.stateMachine, null)) if (DEBUG) Debug.Log("[PC] Player SM initialized");
            else Debug.LogError("[PC] No PlayerStateMachine assigned!");
    // ? BASE METHODS===============================================================================================================================
        protected override void Awake() {
        }
        protected override void Start() {

            base.Start();
        }
        
        protected override void FixedUpdate() {
            base.FixedUpdate();
        }

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Custom Move implementation for Player entity.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void Move() {
            if (this.currentState != EntityStates.move) this.stateMachine.ChangeState(this.stateMachine.EntityStatesList[(int)EntityStates.move]);
        }

    }
}