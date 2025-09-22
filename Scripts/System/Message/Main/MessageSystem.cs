using System.Message.Child;
using UnityEngine;

namespace System.Message.Main
{
    internal sealed class MessageSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private SwitchSystem SwitchSystem;

        public void ShowSwitchUI(string message, Action onShow, Action onConfirm, Action onCancel, Action onClose)
        {
            SwitchSystem.Show(message, onShow, onConfirm, onCancel, onClose);
        }
    }
}