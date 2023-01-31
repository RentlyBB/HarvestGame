using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/LevelData Event Channel")]
public class LevelDataEventChannelSO : DescriptionBaseSO {

	public UnityAction<LevelDataSO> OnEventRaised;

	public void RaiseEvent(LevelDataSO levelData) {
		OnEventRaised?.Invoke(levelData);
	}
}
