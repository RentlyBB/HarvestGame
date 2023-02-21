using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class CropDataSOCollectionEvent : UnityEvent<CropDataSOCollection> { }

	[CreateAssetMenu(
	    fileName = "CropDataSOCollectionVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "CropDataSOCollection",
	    order = 120)]
	public class CropDataSOCollectionVariable : BaseVariable<CropDataSOCollection, CropDataSOCollectionEvent>
	{
	}
}