using Audio_System.Data;
using Audio_System.Main;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player_System.Object
{
    public partial class PlayerObject
    {
        [field: Header("聲音系統 - 腳步")]
        [field: SerializeField, FormerlySerializedAs("walkOnDirtSFX")] private PlaySFXData[] walkSFX;
        
        private void PlayWalkOnDirtSFX()
        {
            if (walkSFX.Length <= 0)
            {
                Debug.Log($"{nameof(PlayerObject)} 的 {nameof(walkSFX)} 為空的，無法播放相關音效。");
                return;
            }
            
            AudioSystem.Instance.FootstepSFX.PlayOneShot(walkSFX[Random.Range(0, walkSFX.Length)]);
        }
    }
}