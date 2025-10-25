using System.Collections.Generic;
using System.Player.Child.Detect_System.Child;
using System.Restaurant.Child.Cookware.System.Main;
using UnityEngine;

namespace System.Player.Child.Detect_System.Main
{
    internal sealed class DetectSystem : MonoBehaviour
    {
        [field: Header("Detect Area")]
        [field: SerializeField] private DetectArea CookwareDetectArea;
        
        private readonly List<Action> ActiveActions = new();
        
        private void Start()
        {
            StartDetect();
        }

        private void OnDisable()
        {
            StopDetect();
        }

        private void StartDetect()
        {
            CookwareDetectArea.OnDetect += CookwareOnDetect;
            ActiveActions.Add(() => CookwareDetectArea.OnDetect -= CookwareOnDetect);
            
            CookwareDetectArea.OnUnDetect += CookwareOnUnDetect;
            ActiveActions.Add(() => CookwareDetectArea.OnUnDetect -= CookwareOnUnDetect);
            return;

            void CookwareOnDetect(GameObject cookware)
            {
                var system = cookware.GetComponent<CookwareSystem>();
                system.SetInteractable(true);
            }
            
            void CookwareOnUnDetect(GameObject cookware)
            {
                var system = cookware.GetComponent<CookwareSystem>();
                system.SetInteractable(false);
            }
        }

        private void StopDetect()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }
    }
}