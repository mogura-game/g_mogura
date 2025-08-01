using UnityEngine;
using System.Collections.Generic;
using System;

namespace App.Game.Entities.Test {
    /// <summary>
    /// Test class for implementing a default Test StateMachines from ScriptableObject States.
    /// </summary>
    public class TestStateMachine : BaseStateMachine {
        // ? DEBUG======================================================================================================================================

        // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        
        // * ATTRIBUTES
        [SerializeField] public List<BaseState> TestStatesList = new List<BaseState>();

        // * INTERNAL

        // ? BASE METHODS===============================================================================================================================
        protected override void Awake() {
            this.controller ??= this.GetComponent<TestEntityController>();

            base.Awake();
        }

        // ? CUSTOM METHODS=============================================================================================================================

        // ? EVENT METHODS==============================================================================================================================

    }
}
