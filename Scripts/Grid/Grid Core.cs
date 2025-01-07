using UnityEngine;
using UnityEngine.UI;

namespace Grid
{
    public abstract class GridCore : MonoBehaviour
    {
        // 物品圖片
        protected Image Image;
        
        // 格子資訊
        public GridInfo GridInfo { get; private set; }

        private void Awake()
        {
            GridInfo = new GridInfo();
        }
    }
}
