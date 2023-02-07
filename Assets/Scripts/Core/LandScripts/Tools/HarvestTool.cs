using UnityEngine;

namespace HarvestCode.Core {

    [CreateAssetMenu(menuName = "Gameplay/Tools/HarvestTool")]
    public class HarvestTool : ToolBehaviour {

        [SerializeField] private bool canHarvest = true;

        public override bool UseTool(GridObject gridObject) {
            if(!canHarvest) return false;

            var farmland = gridObject.GetLand().GetComponent<Farmland>();

            if(farmland == null) return false;

            if(!farmland.HarvestCrop()) return false;

            canHarvest = false;
            return true;
        }

        public override bool ResetTool() {
            
            canHarvest = true;

            return true;
        }

      
    }
}