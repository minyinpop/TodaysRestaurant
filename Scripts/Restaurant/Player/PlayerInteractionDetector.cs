using UnityEngine;

namespace Restaurant.Player
{
    // ==================================================
    // 用來檢測玩家是否可以與物品互動的程式碼。
    // ==================================================
    
    public class PlayerInteractionDetector : MonoBehaviour
    {
        // TODO 開放參數 ......
        
        private void Update()
        {}

        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
    }
}