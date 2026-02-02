using UnityEngine;

namespace UI_System.Example
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