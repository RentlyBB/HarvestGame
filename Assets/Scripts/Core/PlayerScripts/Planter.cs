using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

namespace HarvestCode.Core {
    public class Planter : MonoBehaviour {

        [Header("Broadcasting Events")]
        [SerializeField] private GameEvent SeedQueueUpdateEvent;

        private List<Transform> persistentSeeds_list;

        public List<Transform> seeds_list;

        public bool PlantCropOnFarmland(Farmland farmland) {
            if(seeds_list.Count > 0) {
                if(farmland.PlantCrop(seeds_list[0])) {
                    seeds_list.RemoveAt(0);
                    SeedQueueUpdateEvent.Raise();
                    return true;
                }
            }
            return false;
        }

        // Listening to LoadLevelData
        public void InitCropSeeds(LevelDataSO levelData) {
            persistentSeeds_list = levelData.seeds_list;
            ResetPlanter();
        }

        public void ResetPlanter() {
            seeds_list = new List<Transform>(persistentSeeds_list);
        }
    }
}