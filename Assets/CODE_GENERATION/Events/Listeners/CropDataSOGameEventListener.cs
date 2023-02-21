using UnityEngine;
using HarvestCode.Core;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "CropDataSO Listener")]
	public sealed class CropDataSOGameEventListener : BaseGameEventListener<CropDataSO, CropDataSOGameEvent, CropDataSOUnityEvent>
	{
	}
}