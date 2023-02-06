using UnityEngine;

namespace HarvestCode.Core {

    [CreateAssetMenu(menuName = "Gameplay/Tools/WaterBucketTool")]
    public class WaterBucketTool : ToolBehaviour {

        [Header("Settings")]
        [SerializeField] private int numberOfPours;

        private int poursToUse;

        public override bool UseTool() {
            if(poursToUse <= 0) return false;

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