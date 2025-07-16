using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Player-specific implementation of the EntityCon oller.
    /// Handles player-specific state logic and behaviours.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    [RequireComponent(typeof(PlayerInput))]
    //[RequireComponent(typeof(PlayerAnimator), typeof(Animator))]
    //[RequireComponent(typeof(PlayerSounder), typeof(AudioSource))]
    public class PlayerController : BaseController {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        [Header("Attibutes")]
        [Tooltip("Determines the current Player speed value. (Ranging from 0 to 1)")]
        [SerializeField, Range(0, 1)] private float speed = 1.0f;

        // * INTERNAL
        private Vector2 inputDirection;

    // ? BASE METHODS===============================================================================================================================
        protected override void Awake() {
            this.stateMachine = new PlayerStateMachine();
            this.stateMachine.baseController = this;

            if (!this.rb) Debug.LogError("[PC] Player Rigidbody not attached, cannot create reference!");
            else this.GetComponent<Rigidbody2D>();
        }
        
        protected override void FixedUpdate() {
            // Evita aplicar fuerza si no hay input
            if (this.inputDirection.sqrMagnitude >= 0.01f) {
                this.rb.AddForce(50 * this.speed * this.inputDirection.x * Vector2.right, ForceMode2D.Force);
            }

            base.FixedUpdate();
        }

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Custom Move implementation for Player entity.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnMove(InputAction.CallbackContext context) {
            this.inputDirection = context.ReadValue<Vector2>();
            if (DEBUG && (context.performed || context.canceled)) Debug.Log($"[PI] Move input: {this.inputDirection}");
        }
        
        /// <summary>
        /// Dig mechanic. Toggle the mode when available.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnToggleDig(InputAction.CallbackContext context) {
            if (DEBUG && (context.performed || context.canceled)) Debug.Log("[PI] : Dig mode started");

            this.stateMachine.ChangeState(EntityStates.dig);
        }

        /// <summary>
        /// Returns velocities, forces and rotations to 0.
        /// </summary>
        public void ResetPhyisics() {
            this.rb.linearVelocity = Vector2.zero;
        }
        
        public override void UpdateStateAnimation(EntityStates id) {
            this.baseAnimator?.PlayAnimation("mogura_" + id.ToString());
        }
    }
}