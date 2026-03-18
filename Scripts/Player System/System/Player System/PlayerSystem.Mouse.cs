using Input_System;
using Restaurant_System.Object.Cookware.Object.Cook_Game.Object;
using UI_System.Player_UI_System.Main;
using UnityEngine;

namespace Player_System.System.Player_System

{
    public partial class PlayerSystem
    {
        [field: Header("Mouse System")]
        [field: SerializeField] private Camera mainCamera;
        
        [field: Header("Tag")]
        [field: SerializeField] private string storageSlot;
        [field: SerializeField] private string utensilsTag;

        private void OnClickedLeftButton()
        {
            var ray = mainCamera.ScreenPointToRay(InputSystem.MousePosition);
            var hit2D = Physics2D.GetRayIntersection(ray);
            
            if (hit2D.collider is null)
            {
                return;
            }
            
            var obj = hit2D.collider.gameObject;
            
            if (obj.CompareTag(utensilsTag))
            {
                obj.GetComponent<Utensils>().OnClick(mainCamera);
            }
        }

        private void OnClickedRightButton()
        {
            PlayerUISystem.ClickRightButton();
        }
    }
}