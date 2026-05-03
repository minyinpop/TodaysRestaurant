using System;
using Audio_System.Data;
using Audio_System.Main;
using Common.Interactable_Object;
using Common.Item.Data;
using Player_System.Object;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Common.Item.Object
{
    public sealed class ItemObject : MonoBehaviour, InteractableObject
    {
        [field: Header("狀態")]
        [field: SerializeField] private bool interactable;
                                public bool Interactable => interactable;
        
        [field: Header("資料")]
        [field: SerializeField] private ItemSO itemData;
        
        [field: Header("模型")]
        [field: SerializeField] private Transform modelTransform;
        
        [field: Header("音效")]
        [field: SerializeField] private PlaySFXData takeSFX;
        
        [field: Header("特效")]
        [field: SerializeField] private Transform VFXParent;
        [field: SerializeField] private ParticleSystem takeVFX;
        
        [field: Header("提示")]
        [field: SerializeField] private Image tipImage;
        
        public static event Action OnTake;

        private bool _initialized;

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

        public void Initialize()
        {
            if (_initialized)
            {
                Debug.Log($"{name} 已經初始化過了！");
                return;
            }

            modelTransform.eulerAngles = new Vector3(transform.localRotation.x, Random.Range(0, 360), transform.localRotation.z);
        }

        public void OnEnterDetect(PlayerObject playerObject)
        {
            tipImage.gameObject.SetActive(true);
        }

        public void OnExitDetect()
        {
            tipImage.gameObject.SetActive(false);
        }

        public void Interact(PlayerObject playerObject)
        {
            if (playerObject.TryAddItem(itemData))
            {
                #region 播放拿取物品的音效
                    AudioSystem.Instance.InteractSFX.PlayOneShot(takeSFX);
                #endregion

                #region 播放拿取物品的特效
                    var takeVFX = Instantiate(this.takeVFX.gameObject, VFXParent.position, Quaternion.identity).GetComponent<ParticleSystem>();
                    takeVFX.Play();
                    
                    Destroy(takeVFX.gameObject, takeVFX.main.duration);
                #endregion
                
                OnTake?.Invoke();
                
                Destroy(gameObject);
            }
        }
    }
}