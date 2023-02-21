using HarvestCode.Core;
using UnityEngine;

namespace ScriptableObjectArchitecture {
    [System.Serializable]
	public sealed class LevelDataSOReference : BaseReference<LevelDataSO, LevelDataSOVariable>
	{
	    public LevelDataSOReference() : base() { }
	    public LevelDataSOReference(LevelDataSO value) : base(value) { }
	}
}