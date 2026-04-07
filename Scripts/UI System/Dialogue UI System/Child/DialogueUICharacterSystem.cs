using System;
using System.Collections.Generic;
using Common.Dialogue.Child.Character;
using Common.Dialogue.Data;
using Common.Dialogue.Object;
using Spine.Unity;
using UnityEngine;

namespace UI_System.Dialogue_UI_System.Child
{
    public sealed class DialogueUICharacterSystem : MonoBehaviour
    {
        [field: Header("Parent")]
        [field: SerializeField] private DialogueCharacterPosition leftPosition;
        [field: SerializeField] private DialogueCharacterPosition middlePosition;
        [field: SerializeField] private DialogueCharacterPosition rightPosition;
        
        private readonly Dictionary<DialogueCharacterType, DialogueCharacterPosition> _characters = new();

        private void Awake()
        {
            if (leftPosition is null)
            {
                throw new InvalidOperationException(nameof(leftPosition));
            }
            
            if (middlePosition is null)
            {
                throw new InvalidOperationException(nameof(middlePosition));
            }
            
            if (rightPosition is null)
            {
                throw new InvalidOperationException(nameof(rightPosition));
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
                    DialogueCharacterPosition position;
                    
                    switch (dialogueData.PositionType)
                    {
                        case DialogueCharacterPositionType.NoShow:
                        {
                            return;
                        }
                        case DialogueCharacterPositionType.Left:
                        {
                            position = leftPosition;
                            break;
                        }
                        case DialogueCharacterPositionType.Middle:
                        {
                            position = middlePosition;
                            break;
                        }
                        case DialogueCharacterPositionType.Right:
                        {
                            position = rightPosition;
                            break;
                        }
                        default:
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                    }

                    if (!position.AddCharacter(dialogueData))
                    {
                        Debug.Log($"無法添加 {dialogueData.CharacterType.ToString()} 到 {dialogueData.PositionType.ToString()}，因為已經有角色了。");
                        return;
                    }

                    _characters.Add(dialogueData.CharacterType, position);
                }
            }
            
            onComplete.Invoke();
        }

        public void HideCharacter(HideCharacter dialogueData, Action onComplete)
        {
            if (!_characters.Remove(dialogueData.CharacterType, out var position))
            {
                Debug.Log($"場上未顯示類型為 {dialogueData.CharacterType} 的角色，將跳過此行指令。");
            }
            
            if (!position.RemoveCharacter())
            {
                Debug.Log($"場上未顯示類型為 {dialogueData.CharacterType} 的角色，將跳過此行指令。");
            }
            
            onComplete.Invoke();
        }

        public void PlayCharacterAnimation(PlayCharacterAnimation dialogueData, Action onComplete)
        {
            if (_characters.TryGetValue(dialogueData.CharacterType, out var position))
            {
                var character = position.CharacterObject.GetComponent<SkeletonGraphic>();
                
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