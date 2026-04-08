using System;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Database;
using Common.Scene_Name;
using Common.Scene_Starter;
using DG.Tweening;
using Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Child;
using Explore_System.System.Child.Battle_System.System.Main;
using TMPro;
using UnityEngine;

namespace Tutorial_System
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class BattleTutorialSystem : SceneStarter
    {
        [field: Header("自身組件")]
        [field: SerializeField] private new DoAnimation animation;
        [field: SerializeField] private BattleSystem battleSystem;
        
        [field: Header("提示 1")]
        [field: SerializeField] private CanvasGroup tip1CanvasGroup;
        [field: SerializeField] private TextMeshProUGUI tip1Text;
        
        [field: Header("下個劇情的資料")]
        [field: SerializeField] private SceneStarterData dialogueStarterData;

        private DoFade_CanvasGroup _fadeIn;
        private DoFade_CanvasGroup _fadeOut;
        
        public static event Action<SceneNameSO, SceneStarterData> OnTutorialComplete;
        
        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }
            
            if (battleSystem is null)
            {
                throw new InvalidOperationException($"{nameof(battleSystem)} 沒有被掛載。");
            }
            
            if (tip1CanvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(tip1CanvasGroup)} 沒有被掛載。");
            }
            
            if (tip1Text is null)
            {
                throw new InvalidOperationException($"{nameof(tip1Text)} 沒有被掛載。");
            }
            
            _fadeIn = new DoFade_CanvasGroup(
                endValue: 1,
                duration: .05f,
                ease: Ease.Linear);
            
            _fadeOut = new DoFade_CanvasGroup(
                endValue: 0,
                duration: .05f,
                ease: Ease.Linear);

            TutorialMuuEnemyObject.OnHalfHealth += OnMuuHalfHealth;
        }

        public override void StartSystemWhenFinish(SceneStarterData starterData)
        {
            battleSystem.StartSystem(
                starterData: starterData,
                onComplete: () =>
                {
                    RefreshTip1("<b><color=yellow>持續攻擊</color></b>粉頭少女直到<b><color=yellow>喚醒她</color></b>吧！");
                });
        }

        private void OnDestroy()
        {
            TutorialMuuEnemyObject.OnHalfHealth -= OnMuuHalfHealth;
        }

        private void OnMuuHalfHealth()
        {
            if (OnTutorialComplete is null)
            {
                Debug.Log($"沒有 class 訂閱 {nameof(OnTutorialComplete)}。");
                return;
            }
            
            SceneNameDatabase.GetSceneName(SceneNameType.Dialogue_Scene, out var sceneNameData);
            OnTutorialComplete.Invoke(sceneNameData, dialogueStarterData);
        }

        private void RefreshTip1(string content)
        {
            animation.DoFade_CanvasGroup(
                canvasGroup: tip1CanvasGroup,
                settings: _fadeOut,
                onComplete: () =>
                {
                    tip1Text.text = content;

                    animation.DoFade_CanvasGroup(
                        canvasGroup: tip1CanvasGroup,
                        settings: _fadeIn);
                });
        }
    }
}