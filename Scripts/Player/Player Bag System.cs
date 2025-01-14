using Storage;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Player
{
    public class PlayerBagSystem : MonoBehaviour
    {
        // 輸入系統
        private InputManager _input;
        
        // 背包介面暫存
        private GameObject _tempBagUI;
        
        [field: Header("資料"), Tooltip("玩家背包的資料庫組件"), SerializeField]
        public StorageDataSO StorageDataSO { get; private set; }
        
        [field: Header("介面"), Tooltip("背包介面的預製件"), SerializeField]
        private GameObject bagUIPrefab;
        
        [field: Tooltip("畫布的位置組件"), SerializeField]
        private Canvas canvas;

        private void Awake()
        {
            _input = InputSystem.Input;
        }

        private void OnEnable()
        {
            _input.Player.OpenBag.started += OnOpenBagButtonPressed;
        }

        private void OnDisable()
        {
            _input.Player.OpenBag.started -= OnOpenBagButtonPressed;
        }

        private void OnOpenBagButtonPressed(InputAction.CallbackContext context)
        {
            if (_tempBagUI is null)
            {
                _tempBagUI = Instantiate(bagUIPrefab, canvas.transform);
                Refresh();
            }
            else
            {
                Destroy(_tempBagUI);
                _tempBagUI = null;
            }
        }

        private void Refresh()
        {
            // TODO: 刷新背包的格子 ...
        }
    }
}
