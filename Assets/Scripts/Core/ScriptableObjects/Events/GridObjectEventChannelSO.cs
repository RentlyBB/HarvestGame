using UnityEngine;
using UnityEngine.Events;
using HarvestCode.Core;

[CreateAssetMenu(menuName = "Events/GridObject Event Channel")]
public class GridObjectEventChannelSO : DescriptionBaseSO {

	public UnityAction<GridObject> OnEventRaised;

	public void RaiseEvent(GridObject levelData) {
		OnEventRaised?.Invoke(levelData);
	}
}
