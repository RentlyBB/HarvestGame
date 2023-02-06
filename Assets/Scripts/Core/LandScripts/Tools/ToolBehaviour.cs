using RnT.ScriptableObjectArchitecture;

namespace HarvestCode.Core {

    public abstract class ToolBehaviour :  DescriptionBaseSO {

        public abstract bool UseTool();
        public abstract bool ResetTool();
    }
}