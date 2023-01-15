using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedQueue : MonoBehaviour {


    [SerializeField] private LevelDataSO levelData = default;

    [SerializeField] private Transform iconPrefab;

    [SerializeField] private VoidEventChannelSO seedQueueUpdateListener;


    private void OnEnable() {
        seedQueueUpdateListener.OnEventRaised += UpdateUI;
    }

    private void OnDisable() {
        
    }

    private void Start() {
        if(levelData.cropToPlant_list.Count > 0) {
            foreach(Transform crop in levelData.cropToPlant_list) {
                var tempIcon = Instantiate(iconPrefab);
                tempIcon.SetParent(transform);
                tempIcon.GetComponent<Image>().sprite = crop.GetComponent<Plantable>().cropData.sprite;
            }
        }
    }

    private void UpdateUI() {

        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }

        foreach(Transform crop in levelData.cropToPlant_list) {
            var tempIcon = Instantiate(iconPrefab);
            tempIcon.SetParent(transform);
            tempIcon.GetComponent<Image>().sprite = crop.GetComponent<Plantable>().cropData.sprite;
        }

    }


}
