using System;
using Common.Value;
using UI_System.Message_UI_System.Child.Item_Get_UI_System.Object;
using UnityEngine;

namespace UI_System.Child.Message_UI_System.System
{
    internal sealed class DefeatUISystem : MonoBehaviour
    {
        // [field: Header("UI")]
        // [field: SerializeField] private RectTransform popUpUIParent;
        // [field: SerializeField] private GameObject popUpUIPrefab;
        //
        // private ItemGetUI _itemGetUI;
        //
        // public void SpawnUI(PopUpUIContent content, Action onConfirm)
        // {
        //     if (onConfirm == null)
        //     {
        //         Debug.LogError("DefeatUISystem > SpawnUI > onConfirm cannot be null.");
        //         return;
        //     }
        //
        //     _itemGetUI = Instantiate(popUpUIPrefab, popUpUIParent).GetComponent<ItemGetUI>();
        //     _itemGetUI.ShowUI(content);
        //     
        //     _itemGetUI.OnClickConfirmButton += OnConfirmButtonClicked;
        //     _itemGetUI.SetButtonInteractable(true);
        //     return;
        //
        //     void OnConfirmButtonClicked()
        //     {
        //         _itemGetUI.OnClickConfirmButton -= OnConfirmButtonClicked;
        //         _itemGetUI.SetButtonInteractable(false);
        //         
        //         Destroy(_itemGetUI.gameObject);
        //         _itemGetUI = null;
        //         
        //         onConfirm.Invoke();
        //     }
        // }
    }
}