﻿using System.Collections;
using UnityEngine;

[System.Serializable]
public class GridObject {

    private GridXZ<GridObject> g;
    private int x;
    private int z;
    private Transform land;

    public GridObject(GridXZ<GridObject> g, int x, int z) {
        this.g = g;
        this.x = x;
        this.z = z;
    }

    public void SetLand(Transform land) {
        if(CanCreateLand()) this.land = land;
    }

    public Transform GetLand() {
        return this.land;
    }

    public void ClearLand() {
        land = null;
    }

    public bool CanCreateLand() {
        return land == null;
    }

    public int GetX() {
        return this.x;
    }

    public int GetZ() {
        return this.z;
    }

    public GridXZ<GridObject> GetGrid() {
        return g;
    }

    public override string ToString() {
        return x + ", " + z;
    }

    public Vector3 GetWorldPositionCellCenter() {
        return g.GetWorldPositionCellCenter(x, z);
    }
}
