using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Player-specific implementation of the EntityCon oller.
    /// Handles player-specific state logic and behaviours.
    /// </summary>
    [RequireComponent(typeof(PlayerInput), typeof(PlayerAnimator))]
    //[RequireComponent(typeof(PlayerSounder))]
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
            if (this.movementUnlocked && this.inputDirection.sqrMagnitude >= 0.01f) {
                this.rb.AddForce(100 * this.speed * this.inputDirection.x * Vector2.right, ForceMode2D.Force);
            }

            base.FixedUpdate();
        }

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================
        public override void UpdateStateAnimation(EntityState id) {
            this.baseAnimator?.PlayAnimation("mogura_" + id.ToString());
        }
        
        /// <summary>
        /// Custom Move implementation for Player entity.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnMove(InputAction.CallbackContext context) {
            if (context.performed || context.canceled) {
                if (DEBUG) Debug.Log($"[PI] Move input: {this.inputDirection}");
                this.inputDirection = context.ReadValue<Vector2>();
            }
        }
        
        /// <summary>
        /// Dig mechanic. Toggle the mode when available.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnToggleDig(InputAction.CallbackContext context) {
            if (!this.actionsUnlocked) return;
            else {
                if (context.started) {
                     if (DEBUG) Debug.Log("[PI] : Dig mode started");
                    this.stateMachine.ChangeState(EntityState.dig_in);
                }
            }
        }

        /// <summary>
        /// Returns velocities, forces and rotations to 0.
        /// </summary>
        public void ResetPhysics() {
            this.rb.linearVelocity = Vector2.zero;
        }
    }
}