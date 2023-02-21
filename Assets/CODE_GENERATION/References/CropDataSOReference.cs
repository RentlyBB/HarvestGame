using UnityEngine;
using HarvestCode.Core;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class CropDataSOReference : BaseReference<CropDataSO, CropDataSOVariable>
	{
	    public CropDataSOReference() : base() { }
	    public CropDataSOReference(CropDataSO value) : base(value) { }
	}
}