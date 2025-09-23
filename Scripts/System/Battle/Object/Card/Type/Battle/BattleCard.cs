using System.Battle.Object.Card.Base;
using System.Collections;
using System.General.DOTween;
using Data.Animation.DOTween.Basic;
using Data.Animation.DOTween.Combine;
using Data.Animation.Spine;
using Data.Card.Battle;
using Data.General;
using Data.General.Enum;
using DG.Tweening;
using General;
using UnityEngine;

namespace System.Battle.Object.Card.Type.Battle
{
    [RequireComponent(typeof(DoAnimation))]
    internal abstract class BattleCard : PointerEvent, ICard
    {
        [field: Header("Component")]
        [field: SerializeField] private RectTransform CardRect;
        [field: SerializeField] private RectTransform CardSurfaceRect;
        [field: SerializeField] private GameObject CardFront;
        [field: SerializeField] private GameObject CardBack;
        
        [field: Header("Child System")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Data")]
        [field: SerializeField] protected BattleCardSO BattleCardData;
        
        [field: Header("Card Order")]
        [field: SerializeField] private Transform CardOrderParent;
        private GameObject CardOrder;
        
        private bool Interactable;

        public event Action<ICard> OnClick;
        public static event Action<ICard, SpineAnimation, Action, Action> OnUse;
        
        private IEnumerator MoveAndFlipCor;

        private void OnDisable()
        {
            if (MoveAndFlipCor is not null)
            {
                StopCoroutine(MoveAndFlipCor);
                MoveAndFlipCor = null;
            }
        }

        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (!Interactable) return;
                DoAnimation.DoScale(CardSurfaceRect, new DoScale(Vector2.one * 1.25f, .25f, Ease.OutCubic));
            }
            
            protected override void OnPointerExit()
            {
                if (!Interactable) return;
                DoAnimation.DoScale(CardSurfaceRect, new DoScale(Vector2.one, .25f, Ease.OutCubic));
            }
            
            protected override void OnPointerClick()
            {
                if (!Interactable) return;
                OnClick?.Invoke(this);
            }
        #endregion

        #region Information
            public void GetCardType(out CardType type)
            {
                BattleCardData.GetCardType(out type);
            }
            
            public void GetDrawChance(out float chance)
            {
                BattleCardData.GetDrawChance(out chance);
            }
            
            public void GetDamage(out Damage damage)
            {
                BattleCardData.GetDamage(out damage);
            }
        #endregion
        
        #region Status
            public void SetInteractable(bool interactable)
            {
                Interactable = interactable;
            }
        #endregion

        #region Main Function
            public void Use(Action haveEnemyAlive, Action enemyAllDeath)
            {
                BattleCardData.GetRandomAnimation(out var anima);
                OnUse?.Invoke(this, anima,
                    () =>
                    {
                        // haveEnemyAlive
                        haveEnemyAlive?.Invoke();
                    },
                    () =>
                    {
                        // enemyAllDeath
                        enemyAllDeath?.Invoke();
                    });
            }

            public void DestroyCard(Action onComplete)
            {
                DoAnimation.DoScale(
                    rect: CardRect,
                    settings: new DoScale(Vector2.zero, .5f, Ease.InOutBack),
                    onComplete: () =>
                    {
                        onComplete?.Invoke();
                        Destroy(gameObject);
                    });
            }
        #endregion

        #region Order
            public void SetCardOrder(GameObject cardOrderPrefab)
            {
                if (CardOrder is not null)
                    Destroy(CardOrder);
                CardOrder = Instantiate(cardOrderPrefab, CardOrderParent);
            }
            
            public void RemoveCardOrder()
            {
                if (CardOrder is null) return;
                Destroy(CardOrder);
                CardOrder = null;
            }
        #endregion
            
        #region Animation
            public void Move(Transform parent, DoAnchorPos settings, Action onComplete = null)
            {
                CardRect.SetParent(parent);
                DoAnimation.DoAnchorPos(
                    rect: CardRect,
                    settings: settings,
                    onComplete: () =>
                    {
                        onComplete?.Invoke();
                    });
            }

            public void MoveAndFlip(Transform parent, DoAnchorPos anchorPosSettings, DoFlip flipSettings, Action onComplete = null)
            {
                MoveAndFlipCor = MoveAndFlipCoroutine();
                StartCoroutine(MoveAndFlipCor);
                return;
                
                IEnumerator MoveAndFlipCoroutine()
                {
                    var rotateComplete = false;
                    var scaleComplete = false;
                    CardRect.SetParent(parent);
                    flipSettings.GetValues(out var rotateSettings, out var scaleSettings01, out var scaleSettings02);
                    DoAnimation.DoAnchorPos(
                        rect: CardRect,
                        settings: anchorPosSettings,
                        onComplete: () =>
                        {
                            DoAnimation.DoRotate(
                                rect: CardSurfaceRect,
                                settings: rotateSettings,
                                onUpdate: () =>
                                {
                                    var y = CardSurfaceRect.eulerAngles.y;
                                    if (CardBack.activeSelf && y is < 270 and > 90)
                                    {
                                        CardFront.SetActive(true);
                                        CardBack.SetActive(false);
                                    }
                                    else if (CardFront.activeSelf && y is < 360 and > 270 or < 90 and > 0)
                                    {
                                        CardFront.SetActive(false);
                                        CardBack.SetActive(true);
                                    }
                                },
                                onComplete: () =>
                                {
                                    rotateComplete = true;
                                });
                            DoAnimation.DoScale(
                                rect: CardSurfaceRect,
                                settings: scaleSettings01,
                                onComplete: () =>
                                {
                                    DoAnimation.DoScale(
                                        rect: CardSurfaceRect,
                                        settings: scaleSettings02,
                                        onComplete: () =>
                                        {
                                            scaleComplete = true;
                                        });
                                });
                        });
                    yield return new WaitUntil(() => rotateComplete && scaleComplete);
                    onComplete?.Invoke();
                    MoveAndFlipCor = null;
                }
            }
        #endregion
    }
}