using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GridObject Event Channel")]
public class GridObjectEventChannelSO : ScriptableObject {

	public UnityAction<GridObject> OnEventRaised;

	public void RaiseEvent(GridObject levelData) {
		OnEventRaised?.Invoke(levelData);
	}
}
