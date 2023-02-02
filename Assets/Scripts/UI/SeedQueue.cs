using HarvestCode.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HarvestCode.UI {
    public class SeedQueue : MonoBehaviour {

        private List<Transform> cropSeeds_list = new List<Transform>();

        [SerializeField] private Transform iconPrefab;

        public void UpdateUI() {
            foreach(Transform child in transform) {
                Destroy(child.gameObject);
            }

            foreach(Transform crop in cropSeeds_list) {
                var tempIcon = Instantiate(iconPrefab);
                tempIcon.SetParent(transform);
                tempIcon.GetComponent<Image>().sprite = crop.GetComponent<Plantable>().cropData.sprite;
            }

        }

        public void GetCropSeeds(LevelDataSO levelData) {
            cropSeeds_list = levelData.cropSeeds_list;
        }
    }
}