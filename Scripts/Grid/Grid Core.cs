using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Grid
{
    public abstract class GridCore : MonoBehaviour
    {
        // 自身的圖片
        private Image image;
        
        // 格子資訊
        public GridInfo GridInfo;
    }
}