using System.Collections;
using UnityEngine;
using HarvestCode.Core;
using HarvestCode.DataClasses;

namespace HarvestCode.Tiles {
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

