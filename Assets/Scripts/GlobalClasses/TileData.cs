using System.Collections;
using UnityEngine;

[System.Serializable]
public class TileData {

    [SerializeField] private int x;
    [SerializeField] private int z;
    [SerializeField] private Transform crop;
    [SerializeField] private LandSO land;
    [SerializeField] private int startPhase;

    public TileData(int x, int z) {
        this.x = x;
        this.z = z;
    }

    public int GetX() {
        return x;
    }

    public int GetZ() {
        return z;
    }

    public void SetCrop(Transform crop) {
        this.crop = crop;
    }

    public Transform GetCrop() {
        return crop;
    }

    public void SetLand(LandSO land) {
        this.land = land;
    }

    public LandSO GetLand() {
        return land;
    }

    public void SetStartPhase(int startPhase) {
        this.startPhase = startPhase;
    }

    public int GetStartPhase() {
        return startPhase;
    }
}
