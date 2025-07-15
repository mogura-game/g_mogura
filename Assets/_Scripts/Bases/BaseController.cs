using UnityEngine;

namespace App.Game.Entities {
    /// <summary>
    /// Player-specific implementation of the EntityCon oller.
    /// Handles player-specific state logic and behaviours.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
    [RequireComponent(typeof(BaseStateMachine))]
    //[RequireComponent(typeof(EntityAnimator), typeof(Animator))]
    //[RequireComponent(typeof(EntitySounder), typeof(AudioSource))]
    public abstract class BaseController : MonoBehaviour {
    // ? DEBUG======================================================================================================================================
        [SerializeField] protected bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Reference to the BaseStateMachine inherited class controlling this Entity.")]
        [SerializeField] protected BaseStateMachine stateMachine;
        [Tooltip("Reference to the Animator attached to this Entity.")]
        [SerializeField] public Animator ani;

        // * ATTRIBUTES

        // * INTERNAL
        protected EntityStates currentState => this.stateMachine.currentState.id;

    // ? BASE METHODS===============================================================================================================================
        protected virtual void Awake() {}

        protected virtual void Start() {
            if (ReferenceEquals(this.stateMachine, null)) if (DEBUG) Debug.Log("[EC] Entity SM initialized");
            else Debug.LogError("[EC] No StateMachine assigned!");
        }

        protected virtual void FixedUpdate() {}

        protected virtual void Update() {}

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================

    }
}