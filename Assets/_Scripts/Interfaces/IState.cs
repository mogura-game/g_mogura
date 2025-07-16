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
        /// <summary>
        /// Method executed from every StateMachine fixed update call.
        /// </summary>
        void OnExecute();

    // ? CUSTOM METHODS=============================================================================================================================
        /// <summary>
        /// Method executed right on entering the State.
        /// </summary>
        void OnEnter(BaseStateMachine stateMachine);
        /// <summary>
        /// Method executed just before exiting the State.
        /// </summary>
        void OnExit();

    // ? EVENT METHODS==============================================================================================================================

    }
}