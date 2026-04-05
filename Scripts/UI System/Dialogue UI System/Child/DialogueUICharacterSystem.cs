using System;
using System.Collections.Generic;
using Common.Dialogue.Child.Character;
using Common.Dialogue.Data;
using Spine.Unity;
using UnityEngine;

namespace UI_System.Dialogue_UI_System.Child
{
    public sealed class DialogueUICharacterSystem : MonoBehaviour
    {
        [field: Header("Parent")]
        [field: SerializeField] private RectTransform spawnParent;

        private readonly Dictionary<DialogueCharacterType, SkeletonGraphic> _characters = new();

        private void Awake()
        {
            if (spawnParent is null)
            {
                throw new InvalidOperationException(nameof(spawnParent));
            }
        }

        public void ShowCharacter(ShowCharacter dialogueData, Action onComplete)
        {
            if (dialogueData.CharacterType == DialogueCharacterType.Narrator)
            {
                Debug.Log("無法顯示旁白，將跳過此行指令。");
            }
            else
            {
                if (_characters.ContainsKey(dialogueData.CharacterType))
                {
                    Debug.Log($"場上已顯示類型為 {dialogueData.CharacterType} 的角色，將跳過此行指令。");
                }
                else
                {
                    var character = Instantiate(dialogueData.CharacterGraphic.gameObject, spawnParent).GetComponent<SkeletonGraphic>();
                    _characters.Add(dialogueData.CharacterType, character);
                }
            }
            
            onComplete.Invoke();
        }

        public void HideCharacter(HideCharacter dialogueData, Action onComplete)
        {
            if (_characters.Remove(dialogueData.CharacterType, out var character))
            {
                Destroy(character.gameObject);
            }
            else
            {
                Debug.Log($"場上未顯示類型為 {dialogueData.CharacterType} 的角色，將跳過此行指令。");
            }

            onComplete.Invoke();
        }

        public void PlayCharacterAnimation(PlayCharacterAnimation dialogueData, Action onComplete)
        {
            if (_characters.TryGetValue(dialogueData.CharacterType, out var character))
            {
                foreach (var spineAnimation in dialogueData.SpineAnimations)
                {
                    spineAnimation.GetValues(out var layer, out var animationName, out var loop);
                    var entry = character.AnimationState.SetAnimation(
                        trackIndex: layer,
                        animationName: animationName,
                        loop: loop);
                    entry.TrackTime = dialogueData.StartSeconds;
                }
            }
            else
            {
                Debug.Log($"場上未顯示類型為 {dialogueData.CharacterType} 的角色，將跳過此行指令。");
            }
            
            onComplete.Invoke();
        }
    }
}