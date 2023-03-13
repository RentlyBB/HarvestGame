using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HarvestCode.DataClasses;
using RnT.Utilities;


namespace HarvestCode.UI {
    public class SeedQueue : MonoBehaviour {

        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private Transform iconPrefab;

        public void UpdateQueueUI() {
            foreach(Transform child in transform) {
                Destroy(child.gameObject);
            }

            foreach(CropDataSO crop in playerData.GetSeedList()) {
                var tempIcon = Instantiate(iconPrefab);
                tempIcon.SetParent(transform);
                tempIcon.GetComponent<Image>().sprite = crop.sprite;
            }
        }
    }
}