using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RnT.ScriptableObjectArchitecture.Editor {

    [CustomEditor(typeof(GameEventListenerBase<,>), true)]
    public sealed class GameEventListenerEditor : UnityEditor.Editor {

        private MethodInfo _raiseMethod;

        private GameEventListener Target { get { return (GameEventListener)target; } }

        private void OnEnable() {
            _raiseMethod = target.GetType().BaseType.GetMethod("OnEventRaised");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if(GUILayout.Button("Raise")) {
                _raiseMethod.Invoke(target, null);
            }
        }
    }
}