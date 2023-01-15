using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Grid Vector3 Event Channel")]
public class GridVector3EventChannelSO : ScriptableObject {

	public UnityAction<GridXZ<GridObject>, Vector3> OnEventRaised;

	public void RaiseEvent(GridXZ<GridObject> grid, Vector3 vector) {
		OnEventRaised?.Invoke(grid, vector);
	}
}
