using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

    [SerializeField] private Camera mainCamera;

    [SerializeField] private GridInitializator farmlandGrid;


    [Header("Listen to")]
    [SerializeField] private VoidEventChannelSO LoadLevelEvent = default;

    private void Awake() {
        farmlandGrid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridInitializator>();
    }

    private void OnEnable() {
        LoadLevelEvent.OnEventRaised += UpdateCameraPosition;
    }
    private void OnDisable() {
        LoadLevelEvent.OnEventRaised -= UpdateCameraPosition;
    }

    private void Update() {

        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 100, Color.red);
    }

    private void UpdateCameraPosition() {

        RaycastHit hitPoint;

        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitPoint, float.MaxValue);

        Vector3 target0;
        Vector3 target1;

        target0 = farmlandGrid.grid.GetWorldPositionCellCenter(0, 0);
        target1 = farmlandGrid.grid.GetWorldPositionCellCenter(farmlandGrid.grid.GetWidth() -1, farmlandGrid.grid.GetHeight() - 1) ;

        var bounds = new Bounds(target0, Vector3.zero);
        bounds.Encapsulate(target1);

        Vector3 newCamPos = bounds.center - hitPoint.point;
        
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + newCamPos.x, mainCamera.transform.position.y, mainCamera.transform.position.z + newCamPos.z);
    }

    private Vector3 GetCenterPoint() {
        return Vector3.zero;
    }

}
