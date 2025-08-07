using TMPro;
using UnityEngine;

namespace BATTLE.OBJECT.INITIATIVE
{
    internal class InitiativeText : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI TextTMP;

        public void SetText(string content)
        {
            TextTMP.text = content;
        }
    }
}