using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HarvestCode.Core;

namespace HarvestCode.LevelEditor {
    public class LandButton : MonoBehaviour {

        public LandDataSO landToSelect;

        private TextMeshProUGUI text;

        private void Start() {
            if(landToSelect != null) {
                UpdateBtnText();
            }
        }

        public void SetLandToSelect(LandDataSO land) {
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