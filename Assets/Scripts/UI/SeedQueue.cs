using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HarvestCode.Core;


namespace HarvestCode.UI {
    public class SeedQueue : MonoBehaviour {

        [SerializeField] private Transform iconPrefab;

        public void UpdateQueueUI(CropDataSOCollection cropDataCollection) {
            foreach(Transform child in transform) {
                Destroy(child.gameObject);
            }

            foreach(CropDataSO crop in cropDataCollection) {
                var tempIcon = Instantiate(iconPrefab);
                tempIcon.SetParent(transform);
                tempIcon.GetComponent<Image>().sprite = crop.sprite;
            }
        }
    }
}