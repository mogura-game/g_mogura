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
    public class PlayerController : BaseController, IGroundboundEntity {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        [Header("Attibutes")]
        [Tooltip("Required time from holding Input to enter jump State.")]
        [SerializeField] private float jumpHoldTime = 0.5f;
        [Tooltip(".")]
        [SerializeField] private LayerMask groundMask;
        [Tooltip(".")]
        [SerializeField] private float raySeparation = 0.5f;
        [Tooltip(".")]
        [SerializeField] private float rayDistance = 0.25f;
        [Tooltip("Property access to get Player Inputs.")]
        public Vector2 InputDirection => this.inputDirection;
        [Tooltip(".")]
        public bool IsGrounded => this.isGrounded;

        // * INTERNAL
        private Vector2 inputDirection;
        private bool isGrounded = false;

    // ? BASE METHODS===============================================================================================================================
        protected override void Awake() {
            this.stateMachine = new PlayerStateMachine();
            this.stateMachine.baseController = this;

            if (this.rb) this.GetComponent<Rigidbody2D>();
            else Debug.LogError("[PC] Player Rigidbody not attached, cannot create reference!");
        }
        
        protected override void FixedUpdate() {
            base.FixedUpdate();

            this.DetectGrounded();
        }

        private void OnDrawGizmos() {
            if (!DEBUG) return;

            Gizmos.color = Color.red;
            Vector2 position = (Vector2)this.transform.position + (Vector2.up * (this.rayDistance / 2));

            Vector2 leftOrigin = position + (this.raySeparation / 2 * Vector2.left);
            Vector2 rightOrigin = position + (this.raySeparation / 2 * Vector2.right);

            Gizmos.DrawLine(leftOrigin, leftOrigin + Vector2.down * this.rayDistance);
            Gizmos.DrawLine(rightOrigin, rightOrigin + Vector2.down * this.rayDistance);
        }
        
    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================
        public override void UpdateStateAnimation(EntityState id) {
            this.baseAnimator?.PlayAnimation("mogura_" + id.ToString());
        }
        
        /// <summary>
        /// Custom move implementation for Player entity.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnMove(InputAction.CallbackContext context) {
            if (context.performed || context.canceled) {
                if (DEBUG) Debug.Log($"[PI] Move input: {this.inputDirection}");
                this.inputDirection = context.ReadValue<Vector2>();
            }
        }
        
        /// <summary>
        /// Custom jump implementation for Player entity.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnJump(InputAction.CallbackContext context) {
            if (this.actionsLocked) return;
            else if (context.performed && context.duration >= this.jumpHoldTime) {
                if (DEBUG) Debug.Log("[PI] : Jump started");
                this.stateMachine.ChangeState(EntityState.jump);
            }
        }
        
        /// <summary>
        /// Dig mechanic. Toggle the mode when available.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnToggleDig(InputAction.CallbackContext context) {
            if (this.actionsLocked) return;
            else if (context.started) {
                if (DEBUG) Debug.Log("[PI] : Dig mode started");
                this.stateMachine.ChangeState(EntityState.dig_in);
            }
        }

        /// <summary>
        /// Returns Rigidbody2D velocity, forces and rotations to 0.
        /// </summary>
        public void ResetPhysics() {
            //this.rb.SetRotation(0);
            this.rb.linearVelocity = Vector2.zero;
        }

        
        public bool DetectGrounded() {
            Vector2 startPosition = (Vector2)this.transform.position + (Vector2.up * (this.rayDistance / 2));

            Vector2 leftRay = startPosition + (this.raySeparation / 2 * Vector2.left);
            Vector2 rightRay = startPosition + (this.raySeparation / 2 * Vector2.right);

            return this.GroundCheck(leftRay) || this.GroundCheck(rightRay);
        }

        
        public bool GroundCheck(Vector2 origin) {
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, this.rayDistance, this.groundMask);
            
            return hit.collider != null;
        }
    }
}