using System.Collections;
using UnityEngine;

namespace HarvestCode.Core {
    [System.Serializable]
    public class TileData {

        [Header("Grid Coords")]
        [SerializeField] private int x;
        [SerializeField] private int z;

        [Header("Crop Settings")]
        [SerializeField] private Transform crop;
        [SerializeField] private int startPhase;

        [Header("Land Settings")]
        [SerializeField] private LandDataSO land;

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

        public void SetLand(LandDataSO land) {
            this.land = land;
        }

        public LandDataSO GetLand() {
            return land;
        }

        public void SetStartPhase(int startPhase) {
            this.startPhase = startPhase;
        }

        public int GetStartPhase() {
            return startPhase;
        }
    }
}