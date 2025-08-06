using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEditor;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Player-specific implementation of the EntityController.
    /// Handles player-specific state logic and behaviours.
    /// </summary>
    [RequireComponent(typeof(PlayerInput), typeof(PlayerAnimator))]
    //[RequireComponent(typeof(PlayerSounder))]
    public class PlayerController : BaseController, IGroundboundEntity {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Box 2D.")]
        [SerializeField] private Collider2D fullCollider;
        [Tooltip("Circle.")]
        [SerializeField] private Collider2D digCollider;
        [Tooltip("Capsule 2D.")]
        [SerializeField] public Collider2D attackCollider;

        // * ATTRIBUTES
        [Header("Attibutes")]
        [Tooltip("Required time from holding Input to enter jump State.")]
        [SerializeField] private float jumpHoldTime = 0.5f;
        [Tooltip(".")]
        [SerializeField] private LayerMask groundMask;
        [Tooltip(".")]
        [SerializeField, Range(-1, 1)] private float groundDetectionOffset = 0.5f;
        [Tooltip(".")]
        [SerializeField, Range(0, 1)] private float groundDetectionRadius = 0.35f;
        [Tooltip("Property access to get Player Inputs.")]
        public Vector2 InputDirection => this.inputDirection;
        [Tooltip(".")]
        public bool IsGrounded => this.isGrounded;
        [Tooltip("Nax time Player Mogura on block State.")]
        [SerializeField] public float blockTime = 2.0f;

        // * INTERNAL
        private Vector2 inputDirection = Vector2.zero;
        public Vector2 lookDirection = Vector2.zero;
        private bool isGrounded = false;
        private bool isCharging = false;
        private bool isDigging = false;
        private bool isBlocking = false;
        public PlayerAnimator PA => baseAnimator as PlayerAnimator;

    // ? BASE METHODS===============================================================================================================================
        protected override void Awake() {
            this.stateMachine = new PlayerStateMachine();
            this.stateMachine.baseController = this;

            if (this.rb) this.GetComponent<Rigidbody2D>();
            else Debug.LogError("[PC] Player Rigidbody not attached, cannot create reference!");
        }
        
        protected override void FixedUpdate() {
            base.FixedUpdate();

            this.isGrounded = this.DetectGround();

            if (this.isDigging) this.PA?.UpdateAnimationClipSpeed(this.GetCurrentLinearVelocity.magnitude);
        }

        private void OnDrawGizmos() {
            if (!DEBUG) return;

            Gizmos.color = Color.red;
            Vector2 position = (Vector2)this.transform.position + (Vector2.up * this.groundDetectionOffset);

            Gizmos.DrawSphere(position, this.groundDetectionRadius);
            
            Gizmos.color = Color.blue;
            Vector3 end = this.transform.position + (Vector3)this.lookDirection.normalized;

            Gizmos.DrawLine(this.transform.position, end);
            Gizmos.DrawSphere(end, 0.05f);
        }
        
    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================
        public override void SetMoveForce(Vector2 movement) => this.rb.AddForce(movement * this.speed * (this.isGrounded ? 1.0f : 0.25f), ForceMode2D.Force);

        public override void UpdateStateAnimation(EntityState id) => this.baseAnimator?.PlayAnimation("mogura_" + id.ToString());

        public void OnLook(InputAction.CallbackContext context) {
            Vector2 input = context.ReadValue<Vector2>();
            
            // ? Should it be calculated from Player character position or Screen center position?
            if (context.control.device is Mouse || context.control.device is Keyboard) {
                Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
                this.lookDirection = (input - screenCenter).normalized;
                isAiming = true;
                this.aimDirection = this.lookDirection;
            } else if (context.control.device is Gamepad) this.lookDirection = input.normalized;
            
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
            if (this.actionsLocked || !this.IsGrounded || this.isDigging) return;
            else if (context.started) {
                if (DEBUG) Debug.Log($"[PI] Charge started");
                this.isCharging = true;
                this.stateMachine.ChangeState(EntityState.charge);
            } else if (context.performed && this.isCharging == true) {
                if (context.duration >= this.jumpHoldTime) {
                    if (DEBUG) Debug.Log("[PI] Jump proceeded");
                    this.stateMachine.ChangeState(EntityState.jump);
                } else {
                    if (DEBUG) Debug.Log($"[PI] Idle returned");
                    this.stateMachine.ChangeState(EntityState.idle);
                }
                this.isCharging = false;
            }
        }
        
        /// <summary>
        /// Dig mechanic. Toggle the mode when available.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnToggleDig(InputAction.CallbackContext context) {
            if (context.started) {
                if (this.isDigging) {
                    this.ExitDigMode();
                } else if (this.IsGrounded && !this.actionsLocked) {
                    this.EnterDigMode();
                }
            }
        }

        private void EnterDigMode() {
            if (DEBUG) Debug.Log("[PI] Dig mode started");
            this.transform.localScale = Vector3.one;
            this.stateMachine.ChangeState(EntityState.dig_in);
            this.rb.linearDamping = 5.0f;
            this.isDigging = true;
            this.fullCollider.enabled = !this.isDigging;
            this.digCollider.enabled = this.isDigging;
        }

        private void ExitDigMode() {
            if (DEBUG) Debug.Log("[PI] Dig mode ended");
            this.transform.localScale = Vector3.one;
            this.PA?.UpdateAnimationClipSpeed(1.0f);
            this.stateMachine.ChangeState(EntityState.dig_out);
            this.rb.linearDamping = 0.0f;
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
            this.isDigging = false;
            this.fullCollider.enabled = !this.isDigging;
            this.digCollider.enabled = this.isDigging;
        }
        
        /// <summary>
        /// Custom dig implementation for Player entity.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnDig(InputAction.CallbackContext context) {
            if (!this.isDigging) return;
            else if (context.started) {
                if (DEBUG) Debug.Log($"[PI] DigTriggered");
                this.rb.AddForce(100 * this.speed * this.transform.right, ForceMode2D.Force);
            }
        }
        
        /// <summary>
        /// Custom attack implementation for Player entity.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnAttack(InputAction.CallbackContext context) {
            if (this.actionsLocked) return;
            else if (context.started) {
                if (DEBUG) Debug.Log($"[PI] Attack perfomed");
                this.stateMachine.ChangeState(EntityState.attack);
                if (this.lookDirection.x > 0.0f) this.facingRight = true;
                else if (this.lookDirection.x < 0.0f) this.facingRight = false;
            }
        }

        /// <summary>
        /// Custom block implementation for Player entity.
        /// Must match On<MethodName> to be called by PlayerInput events.
        /// </summary>
        public void OnBlock(InputAction.CallbackContext context) {
            if (this.actionsLocked || isAiming || isCharging) return;
            else if (context.started) {
                if (DEBUG) Debug.Log($"[PI] Blocking");
                this.stateMachine.ChangeState(EntityState.block);
                this.isBlocking = true;
            } else if (context.canceled) {
                this.stateMachine.ChangeState(EntityState.idle);
                this.isBlocking = false;
            }
        }
        
        /// <summary>
        /// Returns Rigidbody2D velocity, forces and rotations to 0.
        /// </summary>
        public void ResetPhysics() {
            //this.rb.SetRotation(0);
            this.rb.linearVelocity = Vector2.zero;
        }



        }

        public bool DetectGround() {
            return Physics2D.OverlapCircle((Vector2)this.transform.position + (Vector2.up * this.groundDetectionOffset), this.groundDetectionRadius, this.groundMask) != null; 
        }
    }
}