using System;
using UnityEngine;

namespace App.Game.Entities.Test {
    public class TestEntityController : MonoBehaviour {
        // ? DEBUG======================================================================================================================================
        [SerializeField] protected bool DEBUG = false;

        // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        [Header("References")]
        [Tooltip("Attached reference to the SM for testing.")]
        [SerializeField] private TestStateMachine testMachine;

        // * ATTRIBUTES
        [SerializeField] public TestStates currentState;

        // * INTERNAL

        // ? BASE METHODS===============================================================================================================================
        private void Start() {
            if (ReferenceEquals(this.testMachine, null)) if (DEBUG) Debug.Log("[PC] SM initialized");
            else Debug.LogError("[PC] No TestStateMachine assigned!");
        }

        // ? CUSTOM METHODS=============================================================================================================================

        // ? EVENT METHODS==============================================================================================================================
    
    [Serializable]
    public enum TestStates {
        idle,
        move
    }
}
