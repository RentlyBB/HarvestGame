using System.Collections;
using UnityEngine;
using RnT.ScriptableObjectArchitecture;

namespace HarvestCode.Core {
    [CreateAssetMenu(menuName = "Gameplay/DispenserDataSO")]
    public class DispenserDataSO : DescriptionBaseSO {

        [Space]
        public CropDataSO cropData;
        public GameObject dispenserVisual;
       
    }
}