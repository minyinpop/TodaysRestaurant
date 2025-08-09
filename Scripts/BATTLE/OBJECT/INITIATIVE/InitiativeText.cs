using TMPro;
using UnityEngine;

namespace BATTLE.OBJECT.INITIATIVE
{
    internal class InitiativeText : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI TextTMP;

        public void SetText(Color textColor, string content)
        {
            TextTMP.color = textColor;
            TextTMP.text = content;
        }
    }
}