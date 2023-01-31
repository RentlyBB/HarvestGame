using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HarvestCode.Systems {
    public class GameManager : MonoBehaviour {

        [Header("Level to be load")]
        [SerializeField] public LevelDataSO levelData = default;

        [Header("Broadcasting Events")]
        [SerializeField] private VoidEventChannelSO GameStartEvent = default;
        [SerializeField] private VoidEventChannelSO GrowthEvent = default;
        [SerializeField] private VoidEventChannelSO SeedQueueUpdateEvent = default;


        [SerializeField] private LevelDataEventChannelSO LoadLevelEvent = default;

        [Header("Listen to")]
        [SerializeField] private VoidEventChannelSO OnMovementEndEvent = default;


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



        private void OnMovementEnd() {
            GrowthEvent.RaiseEvent();
        }

        private void LoadLevel() {
            if(levelData != null) {

                // Reset non persistant data
                levelData.Reset();

                LoadLevelEvent.RaiseEvent(levelData);
                SeedQueueUpdateEvent.RaiseEvent();

            } else {
                Debug.LogError("There is no level to load. Make sure you set levelData variable in GameManager.");
            }
        }


    }
}