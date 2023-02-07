using RnT.ScriptableObjectArchitecture;

namespace HarvestCode.Core {

    public abstract class ToolBehaviour :  DescriptionBaseSO {

        public abstract bool UseTool(GridObject gridObject);
        public abstract bool ResetTool();
    }
}