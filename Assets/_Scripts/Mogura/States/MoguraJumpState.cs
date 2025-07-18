using UnityEngine;

namespace App.Game.Entities.Mogura {
    /// <summary>
    /// Mogura jump State class for managing custom State transitions.
    /// </summary>
    [CreateAssetMenu(menuName = "States/Mogura/Jump", fileName = "MoguraJumpState")]
    public class MoguraJumpState : PlayerState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        [Header("Attributes")]
        [Tooltip("Defines Player size jump force scale. (1 means 1 Mogura height)")]
        [SerializeField, Min(0)] private float jumpForce = 1.0f;
        
        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        public override void OnExecute () {
            base.OnExecute();

            this.MoveFromInput();

            if (!this.SM.PlayerGrounded && this.PlayerVelocity.y < 0.0f) this.SM?.ChangeState(EntityState.fall); 
        }

    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// Sends the added Vector2 of current velocity and jump force to this Entity Controller.
        /// </summary>
        /// <param name="force">Jump force value.</param>
        public void JumpFromInput(float force) => this.SM?.JumpDirection(5.666f * force * Vector2.up); 

    // ? EVENT METHODS==============================================================================================================================
        public override void OnEnter(BaseStateMachine stateMachine) {
            base.OnEnter(stateMachine);
            
            this.SM?.SetActionsLock(true);
            this.SM?.SetMovementLock(false);
            this.SM?.SetStateGravity(this.baseGravity);
            
            this.JumpFromInput(this.jumpForce);
        }
    }
}