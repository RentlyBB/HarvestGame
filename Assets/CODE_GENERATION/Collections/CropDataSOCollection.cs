using UnityEngine;
using HarvestCode.Core;

namespace ScriptableObjectArchitecture
{
	[CreateAssetMenu(
	    fileName = "CropDataSOCollection.asset",
	    menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "CropDataSO",
	    order = 120)]
	public class CropDataSOCollection : Collection<CropDataSO>
	{
	}
}