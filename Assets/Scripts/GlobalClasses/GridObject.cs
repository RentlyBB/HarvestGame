using System.Collections;
using UnityEngine;

[System.Serializable]
public class GridObject {

    private GridXZ<GridObject> g;
    private int x;
    private int z;
    private Transform tile;

    public GridObject(GridXZ<GridObject> g, int x, int z) {
        this.g = g;
        this.x = x;
        this.z = z;
    }

    public void SetTile(Transform tile) {
        if(CanCreateTile()) this.tile = tile;
    }

    public Transform GetTile() {
        return this.tile;
    }

    public void ClearTile() {
        tile = null;
    }

    public bool CanCreateTile() {
        return tile == null;
    }

    public int GetX() {
        return this.x;
    }

    public int GetZ() {
        return this.z;
    }

    public override string ToString() {
        return x + ", " + z;
    }
}
