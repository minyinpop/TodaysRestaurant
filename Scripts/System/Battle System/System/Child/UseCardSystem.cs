using System.Battle_System.Object.Card_Slot;
using System.Battle_System.Object.Card.Base;
using System.Battle_System.System.Child.Selected_Card_System.Main;
using System.Collections;
using System.Collections.Generic;
using Data.DOTween.Basic;
using DG.Tweening;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class UseCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        private readonly List<CardSlot> CardSlots = new();

        private IEnumerator UseCor;

        private void OnEnable()
        {
            SelectedCardSystem.BeforeCloseUI += Add;
        }

        private void OnDisable()
        {
            SelectedCardSystem.BeforeCloseUI -= Add;
            if (UseCor is not null)
            {
                StopCoroutine(UseCor);
                UseCor = null;
            }
        }

        private void Add(List<ICard> cards, Action onComplete = null)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var index = i;
                var slot = Instantiate(SlotPrefab, SpawnParent);
                var slotScript = slot.GetComponent<CardSlot>();
                var card = cards[index];
                CardSlots.Add(slotScript);
                slotScript.Set(card);
                card.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo), () =>
                {
                    if (index != cards.Count - 1) return;
                    onComplete?.Invoke();
                });
            }
        }
        
        public void Use(Action onComplete = null)
        {
            UseCor = UseCoroutine(onComplete);
            StartCoroutine(UseCor);
        }

        private IEnumerator UseCoroutine(Action onComplete = null)
        {
            for (var i = 0; i < CardSlots.Count; i++)
            {
                var index = i;
                var onUseComplete = false;
                var slot = CardSlots[i];
                slot.Get(out var card);
                card.Use(() =>
                {
                    card.Destroy();
                    onUseComplete = true;
                });
                yield return new WaitUntil(() => onUseComplete);
                yield return new WaitForSeconds(.5f);
                if (index != CardSlots.Count - 1) continue;
                onComplete?.Invoke();
            }

            for (var i = 0; i < CardSlots.Count; i++)
            {
                var slot = CardSlots[i];
                CardSlots.Remove(slot);
                Destroy(slot.gameObject);
            }
        }
    }
}