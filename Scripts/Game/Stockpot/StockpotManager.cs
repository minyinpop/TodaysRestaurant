using Input;
using UnityEngine;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace Game.Stockpot
{
    public class StockpotManager : MonoBehaviour
    {
        [Header("組件"), Tooltip("深煮鍋的湯匙的類，用於管理與串聯數據用。"), SerializeField]
        private StockpotSpoon spoon;
        
        [Tooltip("深煮鍋的攪拌進度條的類，用於管理與串聯數據用。"), SerializeField]
        private StockpotProgressBar progressBar;
        
        [Header("標籤"), Tooltip("湯勺的標籤，用於偵測玩家是否點到。"), SerializeField]
        private string spoonTag;

        private void OnEnable()
        {
            InputSystem.input.Mouse.LeftClick.performed += SpoonDetect;
        }

        private void OnDisable()
        {
            InputSystem.input.Mouse.LeftClick.performed -= SpoonDetect;
        }

        private void SpoonDetect(InputAction.CallbackContext context)
        {
            if (Camera.main is null)
            {
                Debug.LogWarning("在玩深煮鍋小遊戲時，攝影機不見了，無法偵測玩家是否握到湯勺。");
                return;
            }

            // 用於生成射線的位置。
            var ray = Camera.main.ScreenPointToRay(InputSystem.MousePos());
            
            // 用於計算射線與平面之間的相交點。
            var xyPlane = new Plane(Vector3.forward, Vector3.zero);
            
            // 發射射線，並與 xyPlane 確認相交點位置，過後把位置儲存進 distance 裡面。
            if (xyPlane.Raycast(ray, out var distance))
            {
                // 用於獲取在 xyPlane 上打到的位置點。
                var hit2DPoint = ray.GetPoint(distance);
                
                // 使用 Physics2D 來獲取該位置上的物品信息。
                var hitInfo = Physics2D.Raycast(hit2DPoint, Vector2.zero);

                if (hitInfo.collider is null)
                    return;

                if (!hitInfo.collider.gameObject.CompareTag(spoonTag))
                    return;
                
                Debug.Log("YES!");
                
                // TODO: 製作玩家把湯勺握起來的邏輯。
            }
        }
    }
}
