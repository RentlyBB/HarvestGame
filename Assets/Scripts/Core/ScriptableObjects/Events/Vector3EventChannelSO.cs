using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using RnT.ScriptableObjectArchitecture;

[CreateAssetMenu(menuName = "Events/Vector3 Event Channel")]
public class Vector3EventChannelSO : DescriptionBaseSO {

	public UnityAction<Vector3> OnEventRaised;

	public void RaiseEvent(Vector3 vector) {
		OnEventRaised?.Invoke(vector);
	}
}
