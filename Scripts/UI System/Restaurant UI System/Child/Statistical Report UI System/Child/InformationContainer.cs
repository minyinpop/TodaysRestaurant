using TMPro;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Statistical_Report_UI_System.Child
{
    public sealed class InformationContainer  : MonoBehaviour
    {
        [field: Header("數字文字組件")]
        [field: SerializeField] private TextMeshProUGUI numberText;
                                public TextMeshProUGUI NumberText => numberText;
    }
}