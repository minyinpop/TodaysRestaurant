using System;
using UnityEngine;

namespace Data.General
{
    [Serializable]
    internal sealed class PopUpUIContent
    {
        [field: SerializeField] private string Message;
        [field: SerializeField] private string ConfirmButtonTitle;
        [field: SerializeField] private string CancelButtonTitle;
        [field: SerializeField] private string CloseButtonTitle;
        
        public PopUpUIContent(string message, string confirmButtonTitle, string cancelButtonTitle, string closeButtonTitle)
        {
            Message = message;
            ConfirmButtonTitle = confirmButtonTitle;
            CancelButtonTitle = cancelButtonTitle;
            CloseButtonTitle = closeButtonTitle;
        }
        
        public void GetValues(out string message, out string confirmButtonTitle, out string cancelButtonTitle, out string closeButtonTitle)
        {
            message = Message;
            confirmButtonTitle = ConfirmButtonTitle;
            cancelButtonTitle = CancelButtonTitle;
            closeButtonTitle = CloseButtonTitle;
        }
    }
}