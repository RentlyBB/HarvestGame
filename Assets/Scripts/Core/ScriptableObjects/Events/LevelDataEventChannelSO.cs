using UnityEngine;
using UnityEngine.Events;
using RnT.ScriptableObjectArchitecture;

[CreateAssetMenu(menuName = "Events/LevelData Event Channel")]
public class LevelDataEventChannelSO : DescriptionBaseSO {

	public UnityAction<LevelDataSO> OnEventRaised;

	public void RaiseEvent(LevelDataSO levelData) {
		OnEventRaised?.Invoke(levelData);
	}
}
