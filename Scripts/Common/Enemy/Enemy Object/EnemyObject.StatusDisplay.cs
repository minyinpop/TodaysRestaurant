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
        
        private void DisplayAlert()
        {
            if (_alertDisplayObject == null)
            {
                _alertDisplayObject = Instantiate(alertDisplayPrefab, statusDisplayParent);
                _alertDisplayObject.Initialize(1, () => Debug.Log("開始移動"));
            }
            else
            {
                // TODO
            }
        }
    }
}