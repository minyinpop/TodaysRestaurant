using UI_System.Lobby_UI_System.Child.Level_Select_UI_System.Object.Level_Select_UI.Main;
using UI_System.Player_UI_System.Main;
using UnityEngine;

namespace UI_System.Lobby_UI_System.Child.Level_Select_UI_System.System
{
    public sealed class LevelSelectUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private LevelSelectUI levelSelectUI;

        private void Awake()
        {
            if (levelSelectUI is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(levelSelectUI)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            levelSelectUI.gameObject.SetActive(false);
        }

        public void ShowLevelSelectUI()
        {
            #region 禁用無關的 UI
                PlayerUISystem.SetHotbarUI(false);
                PlayerUISystem.SetBackpackUI(false);
            #endregion
            
            #region 開啟選地圖的 UI
                levelSelectUI.gameObject.SetActive(true);
            #endregion
        }
        
        public void HideLevelSelectUI()
        {
            #region 恢復之前的 UI
                PlayerUISystem.SetHotbarUI(true);
                PlayerUISystem.SetBackpackUI(true);
            #endregion
            
            #region 關閉選地圖的 UI
                levelSelectUI.gameObject.SetActive(false);
            #endregion
        }
    }
}