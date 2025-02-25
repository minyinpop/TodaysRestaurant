using UnityEngine;

namespace Input
{
    public class InputSystem : MonoBehaviour
    {
        public static Input_Manager input;
        
        public void Awake() => input = new Input_Manager();
        
        public void OnEnable() => input.Enable();
        public void OnDisable() => input.Disable();
    }
}