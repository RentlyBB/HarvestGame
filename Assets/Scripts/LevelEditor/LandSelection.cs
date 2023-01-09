using UnityEngine;


public class LandSelection : MonoBehaviour {

    private LandSO[] lands;
    public GameObject buttonPrefab;

    private void Start() {
        lands = Resources.LoadAll<LandSO>("Lands");

        foreach(LandSO land in lands) {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(transform, false);
            newButton.GetComponent<LandButton>().landToSelect = land;
        }
    }
}
