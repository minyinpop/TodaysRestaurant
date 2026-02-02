using UI_System.Lobby_UI_System.Main;
using UnityEngine;

namespace Lobby_System.Child
{
    public sealed class LevelSelectSystem : MonoBehaviour
    {
        [field: SerializeField] private Camera developCamera; // TODO 暫時名字
        
        private void Awake()
        {
            if (developCamera == null)
            {
                Debug.Log($"{nameof(LevelSelectSystem)} > {nameof(developCamera)} cannot be null.");
            }
            else
            {
                developCamera.gameObject.SetActive(false);
            }
        }

        public void TriggerLevelSelectUI()
        {
            LobbyUISystem.TriggerLevelSelectUI();
        }
    }
}