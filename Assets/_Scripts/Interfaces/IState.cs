namespace App.Game.Entities {
    /// <summary>
    /// Interface for defining State transitions and behaviours.
    /// </summary>
    public interface IState {
    // ? DEBUG======================================================================================================================================

    // ? PARAMETERS=================================================================================================================================
        // * REFERENCES

        // * ATTRIBUTES

        // * INTERNAL
        
    // ? BASE METHODS===============================================================================================================================

    // ? CUSTOM METHODS=============================================================================================================================

    // ? EVENT METHODS==============================================================================================================================
        /// <summary>
        /// Executed right on entering the State.
        /// </summary>
        void OnEnter(BaseStateMachine stateMachine);
        /// <summary>
        /// Executed from every StateMachine fixed update call.
        /// </summary>
        void OnExecute();
        /// <summary>
        /// Executed just before exiting the State.
        /// </summary>
        void OnExit();

    }
}