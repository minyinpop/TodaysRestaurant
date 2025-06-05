using System.Collections.Generic;
using Storage_Slot.Base;
using Storage.Base;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using InputSystem = System.InputSystem;

namespace Player.Base
{
    internal class PlayerStorage : MonoBehaviour
    {
        private InputManager Input { get; set; }
        
        [field: Header("介面相關")]
        [field: SerializeField] private RectTransform PanelParent { get; set; }
        [field: SerializeField] private GameObject HotbarPanelPrefab { get; set; }
        [field: SerializeField] private GameObject BagPanelPrefab { get; set; }
        private GameObject HotbarPanel { get; set; }
        private GameObject BagPanel { get; set; }
        
        [field: FormerlySerializedAs("<StorageBase>k__BackingField")]
        [field: Header("資料庫")]
        [field: SerializeField] private StorageData StorageData { get; set; }
        [field: SerializeField] private List<StorageSlotBase> StorageSlotsBase { get; set; }

        private void Awake() => Input = InputSystem.Input;
        private void Start() => HotbarPanel = Instantiate(HotbarPanelPrefab, PanelParent);
        private void OnEnable() => Input.Bag.Open.performed += OnOpenBag;
        private void OnDisable() => Input.Bag.Open.performed -= OnOpenBag;

        private void OnOpenBag(InputAction.CallbackContext context)
        {
            if (BagPanel is null)
            {
                BagPanel = Instantiate(BagPanelPrefab, PanelParent);
                // TODO BagPanel Init
            }
        }
    }
}