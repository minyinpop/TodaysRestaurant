using System.Collections.Generic;
using System.Cook.Child.Cookware.System.Child.Cook_Selection.Object;
using System.General;
using Data.Animation.DOTween.Basic;
using Data.General.Enum;
using Data.Item.Type.Food;
using Data.Player.Base;
using UnityEngine;

namespace System.Cook.Child.Cookware.System.Child.Cook_Selection.System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class SelectionSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("UI")]
        [field: SerializeField] private GameObject SelectionUI;
        [field: SerializeField] private CanvasGroup SelectionUI_CanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup FadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup FadeOutSettings;
        
        [field: Header("Button")]
        [field: SerializeField] private Button CloseButton;
        
        [field: Header("Sticky Note")]
        [field: SerializeField] private List<GameObject> StickyNotePrefabs;
        [field: SerializeField] private Transform StickyNoteParent;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;
        
        private readonly List<StickyNote> StickyNotes = new();
        
        private readonly List<Action> CloseAction = new();

        private void OnDisable()
        {
            foreach (var action in CloseAction) action?.Invoke();
            CloseAction.Clear();
        }

        public void Show(CookType cookwareType, Action<FoodSO> onSelect, Action onClose)
        {
            if (SelectionUI.activeSelf) return;
            
            // CloseButton
            CloseButton.OnClick += onClose;
            CloseAction.Add(() => CloseButton.OnClick -= onClose);
            
            // StickyNote
            PlayerData.GetUnlockFoods(out var unlockedDishesData);
            foreach (var category in unlockedDishesData)
            {
                category.GetValues(out _, out var dishesData);
                foreach (var dishData in dishesData)
                {
                    dishData.GetItemType(out _, out var cookType, out _);
                    if (cookType != cookwareType) continue;
                    var randomIndex = UnityEngine.Random.Range(0, StickyNotePrefabs.Count);
                    var stickyNote = Instantiate(StickyNotePrefabs[randomIndex], StickyNoteParent);
                    var stickyNote_StickyNote = stickyNote.GetComponent<StickyNote>();
                    StickyNotes.Add(stickyNote_StickyNote);
                    stickyNote_StickyNote.Init(dishData);
                    stickyNote_StickyNote.OnClick += onSelect;
                    CloseAction.Add(() => stickyNote_StickyNote.OnClick -= onSelect);
                }
            }
            
            // UI
            SelectionUI.SetActive(true);
            DoAnimation.DoFade_CanvasGroup(SelectionUI_CanvasGroup, FadeInSettings,
                onComplete: () => SetInteractable(true));
        }

        public void Hide(Action onComplete)
        {
            SetInteractable(false);
            foreach (var action in CloseAction) action?.Invoke();
            CloseAction.Clear();
            
            DoAnimation.DoFade_CanvasGroup(SelectionUI_CanvasGroup, FadeOutSettings,
                onComplete: () =>
                {
                    SelectionUI.SetActive(false);
                    foreach (var stickyNote in StickyNotes) Destroy(stickyNote.gameObject);
                    StickyNotes.Clear();
                    onComplete?.Invoke();
                });
        }

        public void SetInteractable(bool interactable)
        {
            CloseButton.SetInteractable(interactable);
            foreach (var stickyNote in StickyNotes) stickyNote.SetInteractable(interactable);
        }
    }
}