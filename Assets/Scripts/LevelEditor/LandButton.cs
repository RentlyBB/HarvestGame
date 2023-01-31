using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HarvestCode.LevelEditor {
    public class LandButton : MonoBehaviour {

        public LandSO landToSelect;

        private TextMeshProUGUI text;

        private void Start() {
            if(landToSelect != null) {
                UpdateBtnText();
            }
        }

        public void SetLandToSelect(LandSO land) {
            landToSelect = land;
            UpdateBtnText();
        }

        private void UpdateBtnText() {
            text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = landToSelect.name;
        }

        public void Select() {
            EditorManager.Instance.selectedLand = landToSelect;
            EditorManager.Instance.placeMode = PlaceModes.LandMode;
        }
    }
}