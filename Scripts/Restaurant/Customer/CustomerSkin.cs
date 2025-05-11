using System.Collections.Generic;
using Database.Restaurant.Customer.Skin;
using Spine.Unity.Examples;
using UnityEngine;

namespace Restaurant.Customer
{
    public class CustomerSkin : MonoBehaviour
    {
        private CombinedSkin CombinedSkin { get; set; }
        
        private List<CustomerSkinSO> CustomerSkins { get; set; } = new();

        private void Awake()
        {
            CombinedSkin = GetComponent<CombinedSkin>();
        }

        private void Start()
        {
            foreach (var skin in CustomerSkins)
                CombinedSkin.skinsToCombine.Add(skin.GetRandomSkin());
        }

        public void Init(List<CustomerSkinSO> skins)
        {
            CustomerSkins = skins;
        }
    }
}