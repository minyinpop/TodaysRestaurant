using System.Collections.Generic;
using Database.Restaurant.Customer.Skin;
using Spine.Unity.Examples;
using UnityEngine;

namespace Restaurant.Customer
{
    public class CustomerSkin : MonoBehaviour
    {
        [field: Header("顧客外觀的資料庫")]
        [field: SerializeField] private List<CustomerSkinSO> CustomerSkins { get; set; }
        
        private CombinedSkin CombinedSkin { get; set; }
        
        private void Awake()
        {
            CombinedSkin = GetComponent<CombinedSkin>();
            
            foreach (var customerSkin in CustomerSkins)
                CombinedSkin.skinsToCombine.Add(customerSkin.GetRandomSkin());
        }
    }
}