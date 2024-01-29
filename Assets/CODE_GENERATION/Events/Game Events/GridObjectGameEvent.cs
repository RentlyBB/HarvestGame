using UnityEngine;
using HarvestCode.Utilities;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "GridObjectGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "GridObject",
	    order = 120)]
	public sealed class GridObjectGameEvent : GameEventBase<GridObject>
	{
	}
}