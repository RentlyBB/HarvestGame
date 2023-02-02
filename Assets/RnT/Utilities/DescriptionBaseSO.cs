using UnityEngine;

/// <summary>
/// Base class for ScriptableObjects that need a public description field.
/// </summary>
///

namespace RnT.ScriptableObjectArchitecture {
    public class DescriptionBaseSO : SerializableScriptableObject {
        [TextArea] public string description;
    }

}
