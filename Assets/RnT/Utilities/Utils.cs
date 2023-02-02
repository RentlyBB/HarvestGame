using UnityEngine;
using UnityEngine.InputSystem;

namespace RnT.Utilities {
    public class Utils : MonoBehaviour {

        public static Vector3 GetMousePosition3D() {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue)) {
                return raycastHit.point;
            } else {
                return new Vector3(1000, 1000, 1000);
            }
        }
    }
}

