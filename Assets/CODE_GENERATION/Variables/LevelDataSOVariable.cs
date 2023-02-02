using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class LevelDataSOEvent : UnityEvent<LevelDataSO> { }

	[CreateAssetMenu(
	    fileName = "LevelDataSOVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "LevelData",
	    order = 120)]
	public class LevelDataSOVariable : BaseVariable<LevelDataSO, LevelDataSOEvent>
	{
	}
}