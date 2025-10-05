using System.Collections.Generic;
using System.Cook.Cook_Selection.Object;
using System.General;
using Data.Animation.DOTween.Basic;
using Data.Item.Type.Dish;
using Data.Player;
using DG.Tweening;
using General.Object;
using UnityEngine;

namespace System.Cook.Cook_Selection.Child
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class SelectionSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("UI")]
        [field: SerializeField] private GameObject UIObject;
        [field: SerializeField] private CanvasGroup UICanvasGroup;
        [field: SerializeField] private Button CloseButton;
        
        [field: Header("Sticky Note")]
        [field: SerializeField] private List<GameObject> StickyNotePrefabs;
        [field: SerializeField] private Transform SpawnParent;
        private readonly List<StickyNote> StickyNotes = new();
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;

        private void OnEnable()
        {
            CloseButton.OnClick += OnCloseButtonClicked;
        }
        
        private void OnDisable()
        {
            CloseButton.OnClick -= OnCloseButtonClicked;
        }

        public void Show(Action<DishSO> onSelect)
        {
            PlayerData.GetUnlockedDishes(out var dishes);
            foreach (var dish in dishes)
            {
                var randomIndex = UnityEngine.Random.Range(0, StickyNotePrefabs.Count);
                var stickyNote = Instantiate(StickyNotePrefabs[randomIndex], SpawnParent);
                var stickyNoteScript = stickyNote.GetComponent<StickyNote>();
                StickyNotes.Add(stickyNoteScript);
                stickyNoteScript.Init(dish);
            }

            UIObject.SetActive(true);
            DoAnimation.DoFade_CanvasGroup(UICanvasGroup, new DoFade_CanvasGroup(1, .2f, Ease.Linear),
                onComplete: () =>
                {
                    CloseButton.SetInteractable(true);
                    foreach (var stickyNote in StickyNotes)
                    {
                        stickyNote.OnClick += OnStickyNoteClicked;
                        stickyNote.SetInteractable(true);
                    }
                });
            return;

            void OnStickyNoteClicked(DishSO dishData)
            {
                onSelect?.Invoke(dishData);
            }
        }

        public void Hide(Action onComplete = null)
        {
            CloseButton.SetInteractable(false);
            foreach (var stickyNote in StickyNotes)
                stickyNote.SetInteractable(false);
            DoAnimation.DoFade_CanvasGroup(UICanvasGroup, new DoFade_CanvasGroup(0, .2f, Ease.Linear),
                onComplete: () =>
                {
                    UIObject.SetActive(false);
                    foreach (var stickyNote in StickyNotes)
                        Destroy(stickyNote.gameObject);
                    StickyNotes.Clear();
                    onComplete?.Invoke();
                });
        }

        private void OnCloseButtonClicked()
        {
            Hide();
        }
    }
}