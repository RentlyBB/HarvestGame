using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedQueue : MonoBehaviour {

    private List<Transform> cropSeeds_list = new List<Transform>();

    [SerializeField] private Transform iconPrefab;

    [Header("Listen to")]
    [SerializeField] private VoidEventChannelSO SeedQueueUpdateEvent;
    [SerializeField] private LevelDataEventChannelSO OnLevelLoadEvent;


    private void OnEnable() {
        OnLevelLoadEvent.OnEventRaised += GetCropSeeds;
        SeedQueueUpdateEvent.OnEventRaised += UpdateUI;
    }

    private void OnDisable() {
        OnLevelLoadEvent.OnEventRaised -= GetCropSeeds;
        SeedQueueUpdateEvent.OnEventRaised -= UpdateUI;
    }

    private void Start() {
        if(cropSeeds_list.Count > 0) {
            foreach(Transform crop in cropSeeds_list) {
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
