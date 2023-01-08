using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CropSelection : MonoBehaviour {

    public CropSO cropToSelect;

    private TextMeshProUGUI text;
    private void Start() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = cropToSelect.name;
    }

    public void Select() {
        EditorManager.Instance.selectedCrop = cropToSelect;
        EditorManager.Instance.placeMode = PlaceModes.CropMode;
    }
}
