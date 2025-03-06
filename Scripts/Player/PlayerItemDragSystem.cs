using UnityEngine;

namespace Player
{
    public class PlayerItemDragSystem : MonoBehaviour
    {
        [Header("預製件"), Tooltip("用來顯示物品拖曳時的預覽，用於 UI。"), SerializeField]
        private GameObject itemDragPreview;
    }
}
