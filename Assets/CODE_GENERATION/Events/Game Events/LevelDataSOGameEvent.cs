using HarvestCode.Core;
using UnityEngine;

namespace ScriptableObjectArchitecture {
    [System.Serializable]
	[CreateAssetMenu(
	    fileName = "LevelDataSOGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "LevelData",
	    order = 120)]
	public sealed class LevelDataSOGameEvent : GameEventBase<LevelDataSO>
	{
	}
}