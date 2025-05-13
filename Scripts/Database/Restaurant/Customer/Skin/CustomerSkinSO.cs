using System.Collections.Generic;
using UnityEngine;

namespace Database.Restaurant.Customer.Skin
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Customer/Skin", fileName = "New Data", order = 3)]
    public class CustomerSkinSO : ScriptableObject
    {
        [field: Header("外觀檔案的位置")]
        [field: SerializeField] private List<string> Skins { get; set; }
        
        public string GetRandomSkin() => Skins[Random.Range(0, Skins.Count)];
    }
}