using System.Collections;
using UnityEngine;

[System.Serializable]
public class TileData {

    [SerializeField] private int x;
    [SerializeField] private int z;
    [SerializeField] private CropSO crop;
    [SerializeField] private LandSO land;
    [SerializeField] private int currentPhase;

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

    public void SetCrop(CropSO crop) {
        this.crop = crop;
    }

    public CropSO GetCrop() {
        return crop;
    }

    public void SetLand(LandSO land) {
        this.land = land;
    }

    public LandSO GetLand() {
        return land;
    }

    public int GetCurrentPhase() {
        return currentPhase;
    }
}
