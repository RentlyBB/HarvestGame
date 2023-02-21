using UnityEngine;
using UnityEngine.Events;
using HarvestCode.Utilities;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class GridObjectUnityEvent : UnityEvent<GridObject>
	{
	}
}