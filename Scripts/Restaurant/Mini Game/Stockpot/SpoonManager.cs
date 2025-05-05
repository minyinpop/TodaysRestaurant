using Input;
using UnityEngine;

namespace Restaurant.Mini_Game.Stockpot
{
    public class SpoonManager : MonoBehaviour
    {
        [field: Header("自身組件")]
        [field: SerializeField] private BoxCollider2D StirringArea { get; set; }

        private InputManager Input { get; set; }
        private Vector2 MousePos => Input.Mouse.MousePos.ReadValue<Vector2>();

        private void Awake() => Input = InputSystem.Input;
    }
}