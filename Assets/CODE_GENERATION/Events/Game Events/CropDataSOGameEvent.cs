using UnityEngine;
using HarvestCode.Core;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "CropDataSOGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "CropDataSO",
	    order = 120)]
	public sealed class CropDataSOGameEvent : GameEventBase<CropDataSO>
	{
	}
}