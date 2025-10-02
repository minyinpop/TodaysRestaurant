using System.Collections.Generic;
using System.Cook.Selection_UI.Object;
using System.General;
using Data.Animation.DOTween.Basic;
using Data.Item.Type.Dish;
using Data.Player;
using DG.Tweening;
using General.Object;
using UnityEngine;

namespace System.Cook.Selection_UI.Main
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class CookSelectionSystem : MonoBehaviour
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
            Cookware.Cookware.OnClickEmptyBubble += Open;
        }

        private void OnDisable()
        {
            Cookware.Cookware.OnClickEmptyBubble -= Open;
        }

        private void Open(Action<DishSO> onConfirm)
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
                    CloseButton.OnClick += OnClickCloseButton;
                    CloseButton.SetInteractable(true);
                    foreach (var stickyNote in StickyNotes)
                    {
                        stickyNote.OnClick += OnClickStickyNote;
                        stickyNote.SetInteractable(true);
                    }
                });
            return;

            void OnClickStickyNote(DishSO dishData)
            {
                TurnOffAllButtons();
                DoAnimation.DoFade_CanvasGroup(UICanvasGroup, new DoFade_CanvasGroup(0, .2f, Ease.Linear),
                    onComplete: () =>
                    {
                        onConfirm?.Invoke(dishData);
                        UIObject.SetActive(false);
                    });
            }

            void OnClickCloseButton()
            {
                TurnOffAllButtons();
                DoAnimation.DoFade_CanvasGroup(UICanvasGroup, new DoFade_CanvasGroup(0, .2f, Ease.Linear),
                    onComplete: () =>
                    {
                        onConfirm?.Invoke(null);
                        UIObject.SetActive(false);
                    });
            }

            void TurnOffAllButtons()
            {
                foreach (var stickyNote in StickyNotes)
                {
                    stickyNote.SetInteractable(false);
                    stickyNote.OnClick -= OnClickStickyNote;
                }

                CloseButton.SetInteractable(false);
                CloseButton.OnClick -= OnClickCloseButton;
            }
        }
    }
}