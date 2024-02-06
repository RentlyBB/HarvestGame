using System.Collections;
using UnityEngine;
using HarvestCode.LevelEditor;
using HarvestCode.Utilities;
using RnT.Utilities;

namespace HarvestCode.Systems {
    public class CameraSystem : MonoBehaviour {

        [SerializeField] private Camera mainCamera;

        [SerializeField] private GridXZ<GridObject> gridInitializator;

        [Space]
        public Vector3 bottomTarget;
        public Vector3 topTarget;


        private void Awake() {
           FindGridInitializator();
        }

        public void FindGridInitializator(){
            var tempGrid = GameObject.FindGameObjectWithTag("Grid");
            if(tempGrid != null) {
                    
                if(tempGrid.GetComponent<GridInitializer>() != null){
                    gridInitializator = tempGrid.GetComponent<GridInitializer>().grid;
                }else {
                    gridInitializator = tempGrid.GetComponent<FieldEditor>().grid;
                }
            }

            if(gridInitializator == null){
                Debug.LogWarning("Grid Initializator not found. " + gridInitializator);
            }
        }

        public void UpdateCameraPosition() {

            if(mainCamera == null) {
                Debug.Log("Main camera not found.");
                return;
            }
            
            FindGridInitializator();

            RaycastHit hitPoint;

            Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitPoint, float.MaxValue);

            bottomTarget = gridInitializator.GetWorldPositionCellCenter(0, 0);
            topTarget = gridInitializator.GetWorldPositionCellCenter(gridInitializator.GetWidth() - 1, gridInitializator.GetHeight() - 1);

            var bounds = new Bounds(bottomTarget, Vector3.zero);
            bounds.Encapsulate(topTarget);

            Vector3 newCamPos = bounds.center - hitPoint.point;

            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + newCamPos.x, mainCamera.transform.position.y, mainCamera.transform.position.z + newCamPos.z);

            if(!IsVisible(mainCamera, bottomTarget, 0.2f) || !IsVisible(mainCamera, topTarget, 0.2f)) {

                float originalValue = mainCamera.orthographicSize;
                float targetValue = mainCamera.orthographicSize + 0.2f;


                mainCamera.orthographicSize = Mathf.Lerp(originalValue, targetValue, 0.5f);
            }
        }

        private bool IsVisible(Camera cam, Vector3 targetPos, float offset = 0) {

            var camPoint = cam.WorldToViewportPoint(targetPos);

            if(camPoint.x > 1 - offset || camPoint.x < 0 + offset || camPoint.y > 1 - offset || camPoint.y < 0 + offset) {
                return false;
            }

            return true;
        }

    }
}