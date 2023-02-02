using UnityEngine;
using HarvestCode.Core;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class GridObjectReference : BaseReference<GridObject, GridObjectVariable>
	{
	    public GridObjectReference() : base() { }
	    public GridObjectReference(GridObject value) : base(value) { }
	}
}