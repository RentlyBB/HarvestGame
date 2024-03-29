using UnityEngine;
using RnT.Utilities;

namespace HarvestCode.DataClasses {

    [CreateAssetMenu(menuName = "Gameplay/CropDataSO")]
    public class CropDataSO : DescriptionBaseSO {

        [Header("Gameplay")]
        [SerializeField] private GameObject cropPrefab;

        [Header("UI")]
        [SerializeField] public Sprite sprite;

        public GameObject GetPrefab() {
            return cropPrefab;
        }
    }
}