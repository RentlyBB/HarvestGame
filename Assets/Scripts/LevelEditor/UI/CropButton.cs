using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HarvestCode.Core;

namespace HarvestCode.LevelEditor {
    public class CropButton : MonoBehaviour {

        public Transform cropToSelect;

        //private TextMeshProUGUI text;

        private Image img;

        private void Start() {

            img = GetComponent<Image>();
            img.sprite = cropToSelect.GetComponent<Plantable>().cropData.sprite;
        }

        public void Select() {
            EditorManager.Instance.selectedCrop = cropToSelect;
            EditorManager.Instance.placeMode = PlaceModes.CropMode;
        }
    }
}