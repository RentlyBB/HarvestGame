using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "CropDataSOCollectionGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "CropDataSOCollection",
	    order = 120)]
	public sealed class CropDataSOCollectionGameEvent : GameEventBase<CropDataSOCollection>
	{
	}
}