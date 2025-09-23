using System.Collections;
using System.Collections.Generic;
using System.General.DOTween;
using System.General.Item_Slot;
using System.Linq;
using Data.Animation.DOTween.Basic;
using Data.Item.Base;
using DG.Tweening;
using General.Object;
using UnityEngine;

namespace System.Message.Child
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class ItemGetSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("UI")]
        [field: SerializeField] private GameObject ItemGetUI;
        [field: SerializeField] private CanvasGroup ItemGetUICanvasGroup;
        
        [field: Header("Button")]
        [field: SerializeField] private Button ConfirmButton;
        
        [field: Header("Item Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        private readonly List<ItemSlot> ItemSlots = new();
        
        private IEnumerator ShowCor;

        private void OnDisable()
        {
            if (ShowCor is not null)
            {
                StopCoroutine(ShowCor);
                ShowCor = null;
            }
        }

        public void Show(List<ItemSO> items)
        {
            ItemGetUI.SetActive(true);
            DoAnimation.DoFade_CanvasGroup(ItemGetUICanvasGroup, new DoFade_CanvasGroup(1, .5f, Ease.Linear),
                onComplete: () =>
                {
                    ShowCor = ShowCoroutine();
                    StartCoroutine(ShowCor);
                });
            return;

            IEnumerator ShowCoroutine()
            {
                var completes = new List<bool>();
                for (var i = 0; i < items.Count; i++)
                {
                    var index = i;
                    var slot = Instantiate(SlotPrefab, SpawnParent);
                    var slotScript = slot.GetComponent<ItemSlot>();
                    var item = items[index];
                    completes.Add(false);
                    ItemSlots.Add(slotScript);
                    slotScript.Add(item,
                        onComplete: () =>
                        {
                            completes[index] = true;
                        });
                    yield return new WaitForSeconds(.1f);
                }

                yield return new WaitUntil(() => completes.All(c => c));
                ConfirmButton.SetInteractable(true);
            }
        }
    }
}