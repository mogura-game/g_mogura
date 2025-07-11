namespace App.Game.Entities.Player {
    /// <summary>
    /// Player entity class for implementing a default StateMachine from custom ScriptableObject States.
    /// </summary>
    public class PlayerStateMachine : BaseStateMachine {
        // ? DEBUG======================================================================================================================================

        // ? PARAMETERS=================================================================================================================================
        // * REFERENCES
        
        // * ATTRIBUTES

        // * INTERNAL

        // ? BASE METHODS===============================================================================================================================
        protected override void Awake() {
            this.controller ??= this.GetComponent<EntityController>();

            base.Awake();
        }

        // ? CUSTOM METHODS=============================================================================================================================

        // ? EVENT METHODS==============================================================================================================================

    }
}