using UnityEngine;

namespace App.Game.Entities {
    /// <summary>
    /// Generic implementation for handling custom animations and Animator managging.
    /// Inherit from this class to define specific logic and behaviors per Entity.
    /// </summary>
    [RequireComponent(typeof(BaseController), typeof(Animator))]
    public abstract class BaseAnimator : MonoBehaviour {
    // ? DEBUG======================================================================================================================================
        [SerializeField] protected bool DEBUG = false;

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES
        [Tooltip("Reference to the Animator attached to this Entity.")]
        [SerializeField] public Animator ani;

        // * INTERNAL

    // ? BASE METHODS===============================================================================================================================
        protected virtual void Awake() { }
        
        protected virtual void Start() { }
        
        protected virtual void Update() { }

    // ? CUSTOM METHODS=============================================================================================================================
        public virtual void PlayAnimation(string stateId) {
            this.ani.Play(stateId, 0);
        }

    // ? EVENT METHODS==============================================================================================================================

    }
}