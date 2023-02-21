using UnityEngine;

namespace HarvestCode.Core {
    [CreateAssetMenu(menuName = "Gameplay/LandDataSO")]
    public class LandDataSO : ScriptableObject {
        [SerializeField] public Transform landPrefab;
    }
}