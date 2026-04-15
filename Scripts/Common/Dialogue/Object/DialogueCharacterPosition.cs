using System;
using Common.Dialogue.SO.Child.Character;
using UnityEngine;

namespace Common.Dialogue.Object
{
    public sealed class DialogueCharacterPosition : MonoBehaviour
    {
        [field: Header("生成點")]
        [field: SerializeField] private RectTransform spawnParent;
        
        private GameObject _characterObject;
        public GameObject CharacterObject => _characterObject;

        private void Awake()
        {
            if (spawnParent is null)
            {
                throw new InvalidOperationException($"{nameof(spawnParent)} 是空的。");
            }
        }

        public bool AddCharacter(ShowCharacter dialogueData)
        {
            if (_characterObject is not null)
            {
                return false;
            }

            _characterObject = Instantiate(dialogueData.CharacterGraphic.gameObject, spawnParent);
            return true;
        }

        public bool RemoveCharacter()
        {
            if (_characterObject is null)
            {
                return false;
            }
            
            Destroy(_characterObject);
            _characterObject = null;
            return true;
        }
    }
}