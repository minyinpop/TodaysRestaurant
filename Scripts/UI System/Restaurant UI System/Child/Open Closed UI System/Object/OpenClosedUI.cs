using Animation_System.DOTween;
using Common.Button;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Open_Closed_UI_System.Object
{
    [RequireComponent(typeof(Button))]
    public sealed class OpenClosedUI : MonoBehaviour
    {
        [field: Header("")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("")]
        [field: SerializeField] private Button button;
    }
}