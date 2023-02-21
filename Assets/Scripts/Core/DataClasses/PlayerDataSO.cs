using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RnT.Utilities;

namespace HarvestCode.Core {
    [CreateAssetMenu(menuName = "Game/PlayerDataSO")]
    public class PlayerDataSO : DescriptionBaseSO {

        [Space]
        [SerializeField] private ToolBehaviour activeTool;
        [SerializeField] private List<CropDataSO> seeds_list;

        private void OnEnable() {
            Reset();
        }

        private void OnDisable() {
            Reset();
        }

        public void SetActiveTool(ToolBehaviour tool) {
            activeTool = tool;
            //Raise UI tool update event
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

            //Raise UI seed update event
        }

        public void AddCropSeed(CropDataSO cropSeed) {
            //Raise UI seed update event
            seeds_list.Add(cropSeed);
        }

        public void Reset() { 
            activeTool = null;
            seeds_list = new List<CropDataSO>();
        }
    }
}