using System;
using Audio_System.Main;
using Common.Interactable_Object;
using Common.Item.Data;
using Input_System;
using Player_System.Object;
using UnityEngine;

namespace Common.Item.Object
{
    public sealed class ItemObject : MonoBehaviour, InteractableObject
    {
        [field: Header("物品資料")]
        [field: SerializeField] private ItemSO itemData;
        
        [field: Header("特效位置")]
        [field: SerializeField] private Transform VFXParent;

        public static event Action OnTake;

        private void Awake()
        {
            if (itemData is null)
            {
                throw new InvalidOperationException($"{nameof(itemData)} 沒有被掛載。");
            }
            
            if (VFXParent is null)
            {
                throw new InvalidOperationException($"{nameof(VFXParent)} 沒有被掛載。");
            }
        }

        public void OnEnterDetect(PlayerObject playerObject)
        {
        }

        public void OnExitDetect(PlayerObject playerObject)
        {
        }

        public void OnInteractStart()
        {
            InputSystem.DisablePlayerWalk();
        }

        public bool OnInteract(PlayerObject playerObject)
        {
            if (playerObject.TryAddItem(itemData))
            {
                #region 播放拿取物品的音效
                    AudioSystem.Instance.InteractSFX.PlayOneShot(itemData.TakeSFX);
                #endregion

                #region 播放拿取物品的特效
                    var takeVFX = Instantiate(itemData.TakeVFX.gameObject, VFXParent.position, Quaternion.identity).GetComponent<ParticleSystem>();
                    takeVFX.Play();
                    
                    Destroy(takeVFX.gameObject, takeVFX.main.duration);
                #endregion
                
                OnTake?.Invoke();
                
                Destroy(gameObject);
                return true;
            }

            return false;
        }

        public void OnInteractEnd()
        {
            InputSystem.EnablePlayerWalk();
        }
    }
}