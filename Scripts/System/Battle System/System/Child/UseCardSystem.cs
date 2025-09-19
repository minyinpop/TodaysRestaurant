using System.Battle_System.Object.Card_Slot;
using System.Battle_System.Object.Card.Base;
using System.Battle_System.System.Child.Selected_Card_System.Main;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Animation.DOTween.Basic;
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

        private IEnumerator CurrentCor;

        private void OnEnable()
        {
            SelectedCardSystem.BeforeCloseUI += Add;
        }

        private void OnDisable()
        {
            SelectedCardSystem.BeforeCloseUI -= Add;
            if (CurrentCor is not null)
            {
                StopCoroutine(CurrentCor);
                CurrentCor = null;
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
        
        public void Use(Action<bool> haveEnemyAlive, Action enemyAllDeath)
        {
            CurrentCor = UseCoroutine(haveEnemyAlive, enemyAllDeath);
            StartCoroutine(CurrentCor);
        }

        private IEnumerator UseCoroutine(Action<bool> haveEnemyAlive, Action enemyAllDeath)
        {
            var onUseComplete = false;
            var haveAnyEnemyAlive = false;
            var slot = CardSlots[0];
            slot.Get(out var card);
            card.Use(
                haveEnemyAlive: () =>
                {
                    card.Destroy();
                    onUseComplete = true;
                    haveAnyEnemyAlive = true;
                },
                enemyAllDeath: () =>
                {
                    card.Destroy();
                    onUseComplete = true;
                });
            yield return new WaitUntil(() => onUseComplete);
            yield return new WaitForSeconds(1);
            CardSlots.Remove(slot);
            Destroy(slot.gameObject);
            if (haveAnyEnemyAlive)
                haveEnemyAlive?.Invoke(CardSlots.Any());
            else
                enemyAllDeath?.Invoke();
            CurrentCor = null;
        }
    }
}