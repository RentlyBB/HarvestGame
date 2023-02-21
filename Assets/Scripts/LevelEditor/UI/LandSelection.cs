using HarvestCode.Utilities;
using UnityEngine;
using HarvestCode.Core;

namespace HarvestCode.LevelEditor {
    public class LandSelection : MonoBehaviour {

        private LandDataSO[] lands;
        public GameObject buttonPrefab;

        private void Start() {
            lands = Resources.LoadAll<LandDataSO>("Lands");

            foreach(LandDataSO land in lands) {
                GameObject newButton = Instantiate(buttonPrefab);
                newButton.transform.SetParent(transform, false);
                newButton.GetComponent<LandButton>().landToSelect = land;
            }
        }
    }
}