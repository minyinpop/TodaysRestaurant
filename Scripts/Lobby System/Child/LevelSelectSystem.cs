using UI_System.Lobby_UI_System.Main;
using UnityEngine;

namespace Lobby_System.Child
{
    public sealed class LevelSelectSystem : MonoBehaviour
    {
        [field: SerializeField] private Camera levelSelectCamera; // TODO 暫時名字
        
        private void Awake()
        {
            if (levelSelectCamera == null)
            {
                Debug.Log($"{nameof(LevelSelectSystem)} > {nameof(levelSelectCamera)} cannot be null.");
            }
            else
            {
                levelSelectCamera.gameObject.SetActive(false);
            }
        }

        public void ShowLevelSelectUI()
        {
            levelSelectCamera.gameObject.SetActive(true);
            
            LobbyUISystem.ShowLevelSelectUI();
        }
        
        public void HideLevelSelectUI()
        {
            levelSelectCamera.gameObject.SetActive(false);
            
            LobbyUISystem.HideLevelSelectUI();
        }
    }
}