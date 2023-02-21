using UnityEngine;
using UnityEngine.Events;
using HarvestCode.Core;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class CropDataSOEvent : UnityEvent<CropDataSO> { }

	[CreateAssetMenu(
	    fileName = "CropDataSOVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "CropDataSO",
	    order = 120)]
	public class CropDataSOVariable : BaseVariable<CropDataSO, CropDataSOEvent>
	{
	}
}