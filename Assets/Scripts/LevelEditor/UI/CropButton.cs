using UnityEngine;
using UnityEngine.UI;
using HarvestCode.Core;

namespace HarvestCode.LevelEditor {
    public class CropButton : MonoBehaviour {

        public CropDataSO cropData;

        private Image img;

        private void Start() {

            img = GetComponent<Image>();
            img.sprite = cropData.sprite;
        }

        public void Select() {
            EditorManager.Instance.selectedCrop = cropData;
            EditorManager.Instance.placeMode = PlaceModes.CropMode;
        }
    }
}