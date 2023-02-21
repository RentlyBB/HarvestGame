using UnityEngine;
using RnT.Utilities;
using HarvestCode.Core;

namespace HarvestCode.Tiles {

    [CreateAssetMenu(menuName = "Gameplay/DispenserDataSO")]
    public class DispenserDataSO : DescriptionBaseSO {

        [Space]
        public CropDataSO cropData;
        public GameObject dispenserVisual;
       
    }
}