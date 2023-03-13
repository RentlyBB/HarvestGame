using UnityEngine;

namespace HarvestCode.Utilities {
    [CreateAssetMenu(menuName = "Gameplay/LandDataSO")]
    public class LandDataSO : ScriptableObject {
        [SerializeField] public Transform landPrefab;
    }
}