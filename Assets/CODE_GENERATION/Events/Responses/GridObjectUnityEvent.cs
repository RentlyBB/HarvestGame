using UnityEngine;
using UnityEngine.Events;
using HarvestCode.Core;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class GridObjectUnityEvent : UnityEvent<GridObject>
	{
	}
}