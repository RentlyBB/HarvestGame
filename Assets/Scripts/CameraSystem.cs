using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

    [SerializeField] private Camera mainCamera;

    [SerializeField] private GridInitializer gridInitializator;


    [Header("Listen to")]
    [SerializeField] private VoidEventChannelSO LoadLevelEvent = default;

    public Vector3 bottomTarget;
    public Vector3 topTarget;

    private void Awake() {
        var grid = GameObject.FindGameObjectWithTag("Grid");

        if(grid != null) {
            gridInitializator = grid.GetComponent<GridInitializer>();
        }
    }

    private void OnEnable() {
        LoadLevelEvent.OnEventRaised += UpdateCameraPosition;
    }
    private void OnDisable() {
        LoadLevelEvent.OnEventRaised -= UpdateCameraPosition;
    }

    private void Update() {
       
    }

    private void UpdateCameraPosition() {

        if(mainCamera == null) return;
        if(gridInitializator == null) return;

        RaycastHit hitPoint;

        Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitPoint, float.MaxValue);


        bottomTarget = gridInitializator.grid.GetWorldPositionCellCenter(0, 0);
        topTarget = gridInitializator.grid.GetWorldPositionCellCenter(gridInitializator.grid.GetWidth() - 1, gridInitializator.grid.GetHeight() - 1);

        var bounds = new Bounds(bottomTarget, Vector3.zero);
        bounds.Encapsulate(topTarget);

        Vector3 newCamPos = bounds.center - hitPoint.point;

        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + newCamPos.x, mainCamera.transform.position.y, mainCamera.transform.position.z + newCamPos.z);

        while(!IsVisible(mainCamera, bottomTarget, 0.2f) || !IsVisible(mainCamera, topTarget, 0.2f )) {
            mainCamera.orthographicSize += 0.2f;
        }

    }


    private bool IsVisible(Camera cam, Transform targetPos, float offset = 0) {

        var camPoint = cam.WorldToViewportPoint(targetPos.position);

        if(camPoint.x > 1 - offset || camPoint.x < 0 + offset || camPoint.y > 1 - offset || camPoint.y < 0 + offset) {
            return false;
        }

        return true;
    }

    private bool IsVisible(Camera cam, Vector3 targetPos, float offset = 0) {

        var camPoint = cam.WorldToViewportPoint(targetPos);

        if(camPoint.x > 1 - offset || camPoint.x < 0 + offset || camPoint.y > 1 - offset || camPoint.y < 0 + offset) {
            return false;
        }

        return true;
    }

}
