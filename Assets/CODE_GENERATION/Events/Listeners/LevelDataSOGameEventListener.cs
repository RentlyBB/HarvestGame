using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "LevelDataSO Event Listener")]
	public sealed class LevelDataSOGameEventListener : BaseGameEventListener<LevelDataSO, LevelDataSOGameEvent, LevelDataSOUnityEvent>
	{
	}
}