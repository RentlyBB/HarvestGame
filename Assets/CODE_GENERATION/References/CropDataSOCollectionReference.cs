using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class CropDataSOCollectionReference : BaseReference<CropDataSOCollection, CropDataSOCollectionVariable>
	{
	    public CropDataSOCollectionReference() : base() { }
	    public CropDataSOCollectionReference(CropDataSOCollection value) : base(value) { }
	}
}