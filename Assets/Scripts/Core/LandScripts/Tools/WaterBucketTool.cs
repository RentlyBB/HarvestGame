using UnityEngine;

namespace HarvestCode.Core {

    [CreateAssetMenu(menuName = "Gameplay/Tools/WaterBucketTool")]
    public class WaterBucketTool : ToolBehaviour {


        [Header("Settings")]
        [SerializeField] private int numberOfPours;

        private int poursToUse;

        public override bool UseTool(GridObject gridObject) {
            if(poursToUse <= 0) return false;

            var farmland = gridObject.GetLand().GetComponent<Farmland>();

            if(farmland == null) return false;

            if(!farmland.WaterFarmland()) return false;

            poursToUse--;
            return true;
        }

        public override bool ResetTool() {
            FillWaterBucket();
            return true;
        }

        public void FillWaterBucket() {
            poursToUse = numberOfPours;
        }
    }
}