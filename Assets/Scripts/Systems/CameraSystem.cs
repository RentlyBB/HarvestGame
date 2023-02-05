using System.Collections;
using UnityEngine;

namespace HarvestCode.Systems {
    public class CameraSystem : MonoBehaviour {

        [SerializeField] private Camera mainCamera;

        [SerializeField] private GridInitializer gridInitializator;

        [Space]
        public Vector3 bottomTarget;
        public Vector3 topTarget;


        private void Awake() {
            var grid = GameObject.FindGameObjectWithTag("Grid");

            if(grid != null) {
                gridInitializator = grid.GetComponent<GridInitializer>();
            }
        }

        public void UpdateCameraPosition() {

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