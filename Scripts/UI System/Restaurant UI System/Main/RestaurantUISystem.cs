using System.Collections.Generic;
using Common.Item.Data;
using Common.Item.Data.Serving_Note;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.System;
using UI_System.Restaurant_UI_System.Child.Open_Closed_UI_System.System;
using UI_System.Restaurant_UI_System.Child.Serving_Note_UI_System.System;
using UI_System.Restaurant_UI_System.Child.Statistical_Report_UI_System.Main;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Main
{
    public sealed class RestaurantUISystem : MonoBehaviour
    {
        [field: Header("料理選擇")]
        [field: SerializeField] private FoodMenuUISystem foodMenuUISystem;
                                public FoodMenuUISystem FoodMenuUISystem => foodMenuUISystem;
        
        [field: Header("供餐紙條")]
        [field: SerializeField] private ServingNoteUISystem servingNoteUISystem;
                                private static ServingNoteUISystem _servingNoteUISystem;
        
        [field: Header("營業牌子")]
        [field: SerializeField] private OpenClosedUISystem openClosedUISystem;
                                public OpenClosedUISystem OpenClosedUISystem  => openClosedUISystem;
                                
        [field: Header("營業報告")]
        [field: SerializeField] private StatisticalReportUISystem  statisticalReportUISystem;
                                public  StatisticalReportUISystem StatisticalReportUISystem => statisticalReportUISystem;

        private void Awake()
        {
            _servingNoteUISystem = servingNoteUISystem;
        }

        #region 供餐紙條
            public static bool TryInitializeServingNoteUI(ServingNoteSO servingNoteData, GameObject prefab)
            {
                return _servingNoteUISystem.TryInitialize(servingNoteData, prefab);
            }
            
            public static void ToggleServingNoteUI(ServingNoteSO servingNoteData)
            {
                _servingNoteUISystem.ToggleUI(servingNoteData);
            }
            
            public static void GetServingNoteItems(ServingNoteSO servingNoteData, out List<IItem> servingNoteItems)
            {
                _servingNoteUISystem.GetServingNoteItems(servingNoteData, out servingNoteItems);
            }
            
            public static void RemoveServingNoteUI(ServingNoteSO servingNoteData)
            {
                _servingNoteUISystem.RemoveServingNoteUI(servingNoteData); 
            }
        #endregion
    }
}