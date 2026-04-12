using Audio_System.Data;
using Audio_System.Main;
using UnityEngine;

namespace Player_System.Object
{
    public partial class PlayerObject
    {
        [field: Header("聲音系統 - 拿取")]
        [field: SerializeField] private PlaySFXData takeItemSFX;
        
        [field: Header("聲音系統 - 腳步")]
        [field: SerializeField] private PlaySFXData[] walkOnDirtSFX;
        
        private void PlayTakeItemSFX()
        {
            if (takeItemSFX is null)
            {
                Debug.Log($"{nameof(PlayerObject)} 的 {nameof(takeItemSFX)} 為空的，無法播放相關音效。");
                return;
            }
            
            AudioSystem.Instance.InteractSFX.PlayOneShot(takeItemSFX);
        }
        
        private void PlayWalkOnDirtSFX()
        {
            if (walkOnDirtSFX.Length <= 0)
            {
                Debug.Log($"{nameof(PlayerObject)} 的 {nameof(walkOnDirtSFX)} 為空的，無法播放相關音效。");
                return;
            }
            
            AudioSystem.Instance.FootstepSFX.PlayOneShot(walkOnDirtSFX[Random.Range(0, walkOnDirtSFX.Length)]);
        }
    }
}