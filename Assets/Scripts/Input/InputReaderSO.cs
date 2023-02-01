using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using RnT.ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReaderSO : DescriptionBaseSO, GameInput.IGameplayActions {

    // Gameplay
    public event UnityAction GameClickButton = delegate { };
    public event UnityAction GameResetEvent = delegate { };

    public GameInput _gameInput;

    private void OnEnable() {

        if(_gameInput == null) {
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);
        }
        _gameInput.Gameplay.Enable();
    }

    private void OnDisable() {
        _gameInput.Gameplay.Disable();
    }

    public void OnGameClickButton(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
            GameClickButton?.Invoke();
        }
    }

    public void OnGameReset(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
            GameResetEvent?.Invoke();
        }
    }
}
