using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGrid : MonoBehaviour {

    private GridXZ<CubeGridObject> cubeGrid;

    public GameObject cube;

    public GameObject cursorObject;

    private int width = 10;
    private int height = 10;
    private float cellSize = 2f;

    public LayerMask layerMask;

    private void Start() {
        
        cubeGrid = new GridXZ<CubeGridObject>(width, height, cellSize, transform.position, (GridXZ<CubeGridObject> g, int x, int z) => new CubeGridObject(g, x, z));
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 position = getMousePosition3D();

            //cursorObject.transform.position = position;

            CubeGridObject temp = cubeGrid.GetGridObject(position);
            if(temp != null && temp.canBuild()) {
                var gameObjectToBuild = Instantiate(cube, temp.getGridObjectWorldPosition(), Quaternion.identity);
                temp.setGameObject(gameObjectToBuild);
            }
        }
    }

    private Vector3 getMousePosition3D() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask)) {
            return raycastHit.point;
        } else {
            Debug.Log("JHAHA");
            return Vector3.zero;
        }
    }

    public class CubeGridObject {

        private GridXZ<CubeGridObject> grid;
        private int x;
        private int z;
        private GameObject gameObject;
    
        public CubeGridObject(GridXZ<CubeGridObject> grid, int x, int z) {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void setGameObject(GameObject gameObject) {
            this.gameObject = gameObject;
        }

        public void clearGameObject() {
            this.gameObject = null;
        }

        public bool canBuild() {
            return gameObject == null;
        }

        public Vector3 getGridObjectWorldPosition() {
            return grid.GetWorldPositionCellCenter(x, z);
        }

        public override string ToString() {
            return x + "," + z;            
        }

        public string GetLocString() {
            return "x: " + x + ", y: " + z;
        }
    }
}
