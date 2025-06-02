using Input;
using UnityEngine;

namespace Player
{
    public class PlayerItemDragHandler : MonoBehaviour
    {
        private InputManager Input { get; set; }
        
        private void Awake()
        {
            Input = InputSystem.Input;
        }
    }
}