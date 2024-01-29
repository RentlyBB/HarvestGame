using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "Sprite Listener")]
	public sealed class SpriteGameEventListener : BaseGameEventListener<Sprite, SpriteGameEvent, SpriteUnityEvent>
	{
	}
}