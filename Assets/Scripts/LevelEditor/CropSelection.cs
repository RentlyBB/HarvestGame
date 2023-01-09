using UnityEngine;

public class CropSelection : MonoBehaviour {

    private CropSO[] crops;
    public GameObject buttonPrefab;

    private void Start() {
        crops = Resources.LoadAll<CropSO>("Crops");

        foreach(CropSO crop in crops) {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(transform, false);
            newButton.GetComponent<CropButton>().cropToSelect = crop;
        }
    }
}
