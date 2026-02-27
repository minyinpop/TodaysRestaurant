using System;
using Common.Enemy.Enemy_Alert_Display_Object;
using UnityEngine;

namespace Common.Enemy.Enemy_Object
{
    public partial class EnemyObject
    {
        [field: Header("Status Display Settings")]
        [field: SerializeField] private RectTransform statusDisplayParent;
        [field: SerializeField] private EnemyAlertDisplayObject alertDisplayPrefab;
                                private EnemyAlertDisplayObject _alertDisplayObject;
        
        private void DisplayAlert(Action onDisplayEnd)
        {
            if (_alertDisplayObject == null)
            {
                _alertDisplayObject = Instantiate(alertDisplayPrefab, statusDisplayParent);
                _alertDisplayObject.Initialize(
                    countDownTime: 1,
                    onCountDownEnd: () =>
                    {
                        Destroy(_alertDisplayObject.gameObject);
                        _alertDisplayObject = null;
                        
                        onDisplayEnd.Invoke();
                    });
            }
            else
            {
                // TODO
            }
        }
    }
}