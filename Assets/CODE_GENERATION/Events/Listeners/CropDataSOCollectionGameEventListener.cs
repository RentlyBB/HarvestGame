using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "CropDataSOCollection Listener")]
	public sealed class CropDataSOCollectionGameEventListener : BaseGameEventListener<CropDataSOCollection, CropDataSOCollectionGameEvent, CropDataSOCollectionUnityEvent>
	{
	}
}