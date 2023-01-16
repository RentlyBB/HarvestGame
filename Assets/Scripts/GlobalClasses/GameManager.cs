using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GridXZ<GridObject> grid = default;

    [SerializeField] public LevelDataSO levelData = default;

    [Header("Broadcasting Events")]
    [SerializeField] private VoidEventChannelSO GameStartEvent = default;
    [SerializeField] private GridObjectEventChannelSO PlantEvent = default;
    [SerializeField] private GridObjectEventChannelSO HarvestEvent = default;
    [SerializeField] private VoidEventChannelSO GrowthEvent = default;
    [SerializeField] private LevelDataEventChannelSO LoadLevelEvent = default;
 
    [Header("Event Listeners")]
    [SerializeField] private GridObjectEventChannelSO OnMovementEndEvent = default;

    private void OnEnable() {
        OnMovementEndEvent.OnEventRaised += OnMovementEnd;
    }

    private void OnDisable() {
        OnMovementEndEvent.OnEventRaised -= OnMovementEnd;
    }

    private void Start() {
        GameStartEvent.RaiseEvent();

        LoadLevel();
    }

    private void OnMovementEnd(GridObject gridObject) {
        // Plant Event has to be front of the Harvest Event because otherwise
        // player should Harvest and Plant at the same time in one movement tick
        PlantEvent.RaiseEvent(gridObject);
        HarvestEvent.RaiseEvent(gridObject);
        GrowthEvent.RaiseEvent();
    }

    private void LoadLevel() {
        if(levelData != null) {
            LoadLevelEvent.RaiseEvent(levelData);
        } else {
            Debug.LogError("There is no level to load. Make sure you set levelData variable in GameManager.");
        }
    }


}
