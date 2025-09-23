using Data.General;
using UnityEngine;

namespace Data.Item.Base
{
    internal interface ITem
    {
        public void GetInformationSettings(out Sprite sprite);
        public void GetStackSettings(out bool stackable, out Range stackRange);
    }
}