using System.Collections;
using UnityEngine;
using HarvestCode.DataClasses;

namespace HarvestCode.Tiles {
    public class Dispenser : MonoBehaviour, IInteractableTile {

        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] DispenserDataSO dispenserData;
        [SerializeField] private int numberOfCrops;


        private void Start() {
            Instantiate(dispenserData.dispenserVisual, transform.GetChild(0));
        }

        public void Interact() {
            if(numberOfCrops > 0) { 
                playerData.AddCropSeed(dispenserData.cropData);
                numberOfCrops--;
            }
        }
    }
}