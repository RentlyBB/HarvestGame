using UnityEngine;
using HarvestCode.Core;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "GridObject Listener")]
	public sealed class GridObjectGameEventListener : BaseGameEventListener<GridObject, GridObjectGameEvent, GridObjectUnityEvent>
	{
	}
}