using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HarvestCode.Core {
    [RequireComponent(typeof(Harvester), typeof(Planter), typeof(Waterer))]
    [RequireComponent(typeof(GridMovement))]
    public class PlayerBehaviour : MonoBehaviour {

        [SerializeField] private InputReaderSO _inputReader;

        private Planter planter = default;
        private Harvester harvester = default;
        private Waterer waterer = default;

        private Vector3 startingPositon = Vector3.zero;

        void Awake() {
            planter = GetComponent<Planter>();
            harvester = GetComponent<Harvester>();
            waterer = GetComponent<Waterer>();
        }

        private void OnEnable() {
            _inputReader.GameResetEvent += ResetPlayer;
        }

        private void OnDisable() {
            _inputReader.GameResetEvent -= ResetPlayer;
        }

        public void InteractWithTile(GridObject gridObject) {

            if(gridObject == null) return;

            //Try plant or harvest
            FarmlandRoutine(gridObject);
            ResourceTileRoutine(gridObject);

        }

        private void FarmlandRoutine(GridObject gridObject) {

            var farmland = gridObject.GetLand().GetComponent<Farmland>();

            // Player is not on farmland tile
            if(farmland == null) return;

            // Player successfully planted the crop, no other action needed.
            if(planter.PlantCropOnFarmland(farmland)) return;

            if(waterer.WaterCropOnFarmland(farmland)) return;

            harvester.HarvestFarmland(farmland);

        }

        private void ResourceTileRoutine(GridObject gridObject) {
            var resourceTile = gridObject.GetLand().GetComponent<ResourceTile>();

            // Player is not on resource type tile
            if(resourceTile == null) return;

            switch(resourceTile.GetResource().typeName) {
                case "Water":
                    waterer.GetWater();
                    break;
                default:
                    break;
            }

        }

        public void InitPlayerData(LevelDataSO levevData) {
            startingPositon = levevData.playerStartPoint;
        }

        public void ResetPlayer() {
            transform.position = startingPositon;
            planter.ResetPlanter();
        }
    }
}