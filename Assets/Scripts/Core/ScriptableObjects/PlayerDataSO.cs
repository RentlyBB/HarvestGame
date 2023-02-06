using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RnT.ScriptableObjectArchitecture;

namespace HarvestCode.Core {
    [CreateAssetMenu(menuName = "Game/PlayerDataSO")]
    public class PlayerDataSO : DescriptionBaseSO {

        [Space]
        [SerializeField] private ToolBehaviour activeTool;
        [SerializeField] private List<CropDataSO> seeds_list;
    
        public void SetActiveTool(ToolBehaviour tool) {
            activeTool = tool;
        }

        public ToolBehaviour GetActiveTool() {
            return activeTool;
        }

        public CropDataSO GetLastCropSeed() {
            
            //There is no seed to plant
            if(seeds_list.Count == 0) return null;

            var retValue = seeds_list[0];
            seeds_list.RemoveAt(0);

            return retValue;
        }

        public void AddCropSeed(CropDataSO cropSeed) {
            seeds_list.Add(cropSeed);
        }
    }
}