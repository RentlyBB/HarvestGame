using RnT.Utilities;
using HarvestCode.Utilities;

namespace HarvestCode.Core {

    public abstract class ToolBehaviour :  DescriptionBaseSO {

        public abstract bool UseTool(GridObject gridObject);
        public abstract bool ResetTool();
    }
}