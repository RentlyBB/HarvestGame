using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Transform Event Channel")]
public class TransformEventChannelSO : DescriptionBaseSO {

	public UnityAction<Transform> OnEventRaised;

	public void RaiseEvent(Transform transform) {
		OnEventRaised?.Invoke(transform);
	}
}