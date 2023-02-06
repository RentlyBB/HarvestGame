using System.Collections;
using UnityEngine;

namespace HarvestCode.Core {
    public class ToolBox : MonoBehaviour, IInteractableTile {

        [SerializeField] private PlayerDataSO playerData;

        // WaterBucket, HarvestTool, SoilTool
        [SerializeField] private ToolBehaviour toolBehaviour;

        public void Interact() {
            if(toolBehaviour == null) return;
            toolBehaviour.ResetTool();
            playerData.SetActiveTool(toolBehaviour);
        }
    }
}

