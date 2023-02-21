using UnityEngine;
using UnityEngine.Events;
using HarvestCode.Utilities;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class GridObjectEvent : UnityEvent<GridObject> { }

	[CreateAssetMenu(
	    fileName = "GridObjectVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "GridObject",
	    order = 120)]
	public class GridObjectVariable : BaseVariable<GridObject, GridObjectEvent>
	{
	}
}