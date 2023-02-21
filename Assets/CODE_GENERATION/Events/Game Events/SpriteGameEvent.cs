using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "SpriteGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "Sprite",
	    order = 120)]
	public sealed class SpriteGameEvent : GameEventBase<Sprite>
	{
	}
}