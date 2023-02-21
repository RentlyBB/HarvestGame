using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class SpriteEvent : UnityEvent<Sprite> { }

	[CreateAssetMenu(
	    fileName = "SpriteVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Sprite",
	    order = 120)]
	public class SpriteVariable : BaseVariable<Sprite, SpriteEvent>
	{
	}
}