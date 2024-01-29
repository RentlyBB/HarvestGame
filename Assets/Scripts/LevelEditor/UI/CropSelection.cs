using HarvestCode.DataClasses;
using UnityEngine;

namespace HarvestCode.LevelEditor {
    public class CropSelection : MonoBehaviour {

        private CropDataSO[] crops;
        public GameObject buttonPrefab;

        private void Start() {
            crops = Resources.LoadAll<CropDataSO>("Crops");

            foreach(CropDataSO crop in crops) {
                GameObject newButton = Instantiate(buttonPrefab);
                newButton.transform.SetParent(transform, false);
                newButton.GetComponent<CropButton>().cropData = crop;
            }
        }
    }
}