using UnityEngine;
using UnityEngine.Events;

namespace RnT.ScriptableObjectArchitecture {

    public abstract class GameEventListenerBase<TType, TEvent, TResponse> : MonoBehaviour, IGameEventListener<TType>
    where TEvent : GameEventBase<TType>
    where TResponse : UnityEvent<TType> {

        [SerializeField, HideInInspector]
        private TEvent _previouslyRegisteredEvent = default(TEvent);
        [SerializeField]
        private TEvent _event = default(TEvent);
        [SerializeField]
        private TResponse _response = default(TResponse);

        public void OnEventRaised(TType value) {
            RaiseResponse(value);

        }
        protected void RaiseResponse(TType value) {
            _response.Invoke(value);
        }
        private void OnEnable() {
            if(_event != null)
                Register();
        }
        private void OnDisable() {
            if(_event != null)
                _event.RemoveListener(this);
        }
        private void Register() {
            if(_previouslyRegisteredEvent != null) {
                _previouslyRegisteredEvent.RemoveListener(this);
            }

            _event.AddListener(this);
            _previouslyRegisteredEvent = _event;
        }
    }


    public abstract class GameEventListenerBase<TEvent, TResponse> : MonoBehaviour, IGameEventListener 
    where TEvent : GameEventBase 
    where TResponse : UnityEvent {

        [SerializeField, HideInInspector]
        private TEvent _previouslyRegisteredEvent = default(TEvent);
        [SerializeField]
        public TEvent _event = default(TEvent);
        [SerializeField]
        private TResponse _response = default(TResponse);

        public void OnEventRaised() {
            RaiseResponse();

        }
        protected void RaiseResponse() {
            _response.Invoke();
        }
        private void OnEnable() {
            if(_event != null)
                Register();
        }
        private void OnDisable() {
            if(_event != null)
                _event.RemoveListener(this);
        }
        private void Register() {
            if(_previouslyRegisteredEvent != null) {
                _previouslyRegisteredEvent.RemoveListener(this);
            }

            _event.AddListener(this);
            _previouslyRegisteredEvent = _event;
        }
    }
}