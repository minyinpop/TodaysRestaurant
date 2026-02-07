using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common;
using Common.Data.Enemy;
using Common.Data.Item;
using UnityEngine;
using UnityEngine.UI;

namespace UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Child.Level_Information_UI.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class LevelInformationSlot : PointerEvent
    {
        [field: Header("Components")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("Item Slot")]
        [field: SerializeField] private RectTransform slotRect;
        [field: SerializeField] private DoScale pointerEnterScale;
        [field: SerializeField] private DoScale pointerExitScale;
        
        [field: Header("Item Image")]
        [field: SerializeField] private Image itemImage;
        [field: SerializeField] private Color itemLockColor;
        [field: SerializeField] private Color itemUnlockColor;

        private bool _initialized;

        private void Awake()
        {
            if (animation == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(animation)} cannot be null.");
            }
            else if (slotRect == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(slotRect)} cannot be null.");
            }
            else if (itemImage == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(itemImage)} cannot be null.");
            }
            else
            {
                itemImage.gameObject.SetActive(false);
            }
        }

        #region PointerEvent
            protected override void OnPointerEnter()
            {
                animation.DoScale_UI(slotRect, pointerEnterScale);
            }

            protected override void OnPointerExit()
            {
                animation.DoScale_UI(slotRect, pointerExitScale);
            }
        #endregion

        public void Initialize(ItemSO item)
        {
            if (_initialized) return;
            
            _initialized = true;

            itemImage.sprite = item.ItemSprite;
            itemImage.color = itemLockColor; // TODO 判斷玩家的資料
            itemImage.gameObject.SetActive(true);
        }

        public void Initialize(EnemySO enemy)
        {
            if (_initialized) return;

            _initialized = true;

            itemImage.sprite = enemy.EnemyImage;
            itemImage.color = itemLockColor; // TODO 判斷玩家的資料
            itemImage.gameObject.SetActive(true);
        }
    }
}