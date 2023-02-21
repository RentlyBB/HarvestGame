using System.Collections;
using UnityEngine;
using UnityEditor;
using HarvestCode.Utilities;

namespace ScriptableObjectArchitecture.Editor {

    [CustomPropertyDrawer(typeof(GridObject))]
    public class GridObjectPropertyDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        }
    }
}