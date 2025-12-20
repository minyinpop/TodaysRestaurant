using System.Collections.Generic;
using Input_System.Main;
using Mouse_System.Child.Item;
using Restaurant_System.Object.Cookware.Object.Cook_Game.Object;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mouse_System.Main
{
    internal sealed class MouseSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private ItemDragSystem ItemDragSystem;
        
        [field: Header("Camera")]
        [field: SerializeField] private Camera MainCamera;
        
        [field: Header("Event System")]
        [field: SerializeField] private EventSystem EventSystem;
        
        [field: Header("Tag")]
        [field: SerializeField] private string StorageSlot;
        [field: SerializeField] private string UtensilsTag;
        
        private void OnEnable()
        {
            InputSystem.OnClickedLeftButton += OnPointerClicked;
        }
        
        private void OnDisable()
        {
            InputSystem.OnClickedLeftButton -= OnPointerClicked;
        }

        private void OnPointerClicked()
        {
            #region Main
                InputSystem.GetMousePosition(out var position);
                if (!UI()) WorldSpace();
                return;
            #endregion

            #region UI
                bool UI()
                {
                    var pointer = new PointerEventData(EventSystem)
                    {
                        position = position
                    };
                    
                    var results = new List<RaycastResult>();
                    EventSystem.RaycastAll(pointer, results);
                    
                    if (results.Count == 0) return false;
                    if (results[0].gameObject.CompareTag(StorageSlot))
                    {
                        ItemDragSystem.OnClick(results[0].gameObject);
                        return true;
                    }

                    return false;
                }
            #endregion

            #region WorldSpace
                void WorldSpace()
                {
                    var ray = MainCamera.ScreenPointToRay(position);
                    var hit2D = Physics2D.GetRayIntersection(ray);
                    if (hit2D.collider is null) return;
                    
                    var obj = hit2D.collider.gameObject;
                    if (obj.CompareTag(UtensilsTag)) obj.GetComponent<Utensils>().OnClick(MainCamera);
                }
            #endregion
        }
    }
}