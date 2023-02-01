using UnityEditor;
using UnityEngine;

namespace RnT.ScriptableObjectArchitecture.Editor {

    [CustomEditor(typeof(GameEventBase), true)]
    public sealed class GameEventEditor : UnityEditor.Editor {
        private GameEvent Target { get { return (GameEvent)target; } }
        public override void OnInspectorGUI() {

            EditorGUILayout.LabelField("Description");
            Target.description = GUILayout.TextArea(Target.description, GUILayout.Height(100));
            EditorGUILayout.Space();

            if(GUILayout.Button("Raise")) {
                Target.Raise();
            }
        }
    }
}