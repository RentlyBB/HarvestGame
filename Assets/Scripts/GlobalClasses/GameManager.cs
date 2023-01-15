using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public GridXZ<GridObject> grid;

    public LevelDataSO levelData;

    [Header("Broadcasting Events")]
    [SerializeField] private VoidEventChannelSO GameStartEvent;
    [SerializeField] private VoidEventChannelSO PlantEvent;
    [SerializeField] private VoidEventChannelSO HarvestEvent;
    [SerializeField] private VoidEventChannelSO GrowthEvent;

    [Header("Event Listeners")]
    [SerializeField] private VoidEventChannelSO OnMovementEndEvent;

    private void OnEnable() {
        OnMovementEndEvent.OnEventRaised += OnMovementEnd;
    }

    private void OnDisable() {
        OnMovementEndEvent.OnEventRaised -= OnMovementEnd;
    }

    private void Start() {
        GameStartEvent.RaiseEvent();
    }

    private void OnMovementEnd() {
        PlantEvent.RaiseEvent();
        HarvestEvent.RaiseEvent();
        GrowthEvent.RaiseEvent();
    }


}
