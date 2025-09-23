using System;
using UnityEngine;

namespace Data.General
{
    [Serializable]
    internal sealed class PopUpUIContent
    {
        [field: SerializeField] private string Message;
        [field: SerializeField] private string ConfirmBtnTitle;
        [field: SerializeField] private string CancelBtnTitle;
        
        public PopUpUIContent(string message, string confirmBtnTitle, string cancelBtnTitle)
        {
            Message = message;
            ConfirmBtnTitle = confirmBtnTitle;
            CancelBtnTitle = cancelBtnTitle;
        }
        
        public void GetValues(out string message, out string confirmBtnTitle, out string cancelBtnTitle)
        {
            message = Message;
            confirmBtnTitle = ConfirmBtnTitle;
            cancelBtnTitle = CancelBtnTitle;
        }
    }
}