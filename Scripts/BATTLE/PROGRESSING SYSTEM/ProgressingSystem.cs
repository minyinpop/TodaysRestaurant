using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;
using BATTLE.PROGRESSING_SYSTEM.STATE;
using BATTLE.SELECTION_INITIATIVE_SYSTEM;
using DG.Tweening;
using UnityEngine;

namespace BATTLE.PROGRESSING_SYSTEM
{
    [RequireComponent(typeof(SelectionInitiativeSystem))]
    internal class ProgressingSystem : MonoBehaviour
    {
        /// <summary>
        /// 用來執行決定哪方先手的系統
        /// </summary>
        private SelectionInitiativeSystem SelectionInitiativeSystem;
        
        /// <summary>
        /// 當前的戰鬥進程
        /// </summary>
        private StateMachine StateMachine = new();

        private void Awake()
        {
            SelectionInitiativeSystem = GetComponent<SelectionInitiativeSystem>();
        }
        
        private void Start()
        {
            DOTween.Sequence()
                .AppendInterval(0) // TODO 未來可修改延遲多少秒，才開始執行相機縮放敵人的程式碼
                .AppendCallback(() =>
                {
                    StateMachine.ChangeState(this, new OnBeginning());
                });
        }

        /// <summary>
        /// 用來生成決定哪方先手的硬幣
        /// </summary>
        public void SpawnInitiativeCoin()
        {
            SelectionInitiativeSystem.SpawnCoin();
        }
    }
}