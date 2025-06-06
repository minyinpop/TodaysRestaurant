using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class CardGo : MonoBehaviour
{
    private InputManager Input { get; set; }
    private void Awake() => Input = new InputManager();
    private void Start() => Input.Enable();
    private void OnEnable() => Input.Player.Space.performed += Flip;
    private void OnDisable() => Input.Player.Space.performed -= Flip;
    private void OnDestroy() => Input.Disable();
    private void Flip(InputAction.CallbackContext context)
    {
        transform.DORotate(new Vector3(0f, 180f, 0f), 0.5f);
    }
}