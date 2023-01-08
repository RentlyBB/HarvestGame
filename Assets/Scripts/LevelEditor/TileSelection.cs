using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TileSelection : MonoBehaviour {

    public TileSO tileToSelect;

    private TextMeshProUGUI text;

    private void Start() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = tileToSelect.name;
    }

    public void Select() {
        EditorManager.Instance.selectedTile = tileToSelect;
        EditorManager.Instance.placeMode = PlaceModes.TileMode;
    }
}
