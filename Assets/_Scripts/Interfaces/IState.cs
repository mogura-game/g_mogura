namespace App.Game.Entities {
    /// <summary>
    /// Interface for defining State behavior.
    /// </summary>
    public interface IState {
    // ? BASE METHODS===============================================================================================================================
        void OnEnter(BaseStateMachine stateMachine);
        void OnExecute();
        void OnExit();
    }
}