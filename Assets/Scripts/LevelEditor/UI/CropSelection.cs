using UnityEngine;

namespace HarvestCode.LevelEditor {
    public class CropSelection : MonoBehaviour {

        private Transform[] crops;
        public GameObject buttonPrefab;

        private void Start() {
            crops = Resources.LoadAll<Transform>("Crops");

            foreach(Transform crop in crops) {
                GameObject newButton = Instantiate(buttonPrefab);
                newButton.transform.SetParent(transform, false);
                newButton.GetComponent<CropButton>().cropToSelect = crop;
            }
        }
    }
}