using UnityEngine;

namespace Item.Base
{
    internal abstract class ItemBase : ScriptableObject
    {
        internal virtual InfoSettings GetInfoSettings() => null;
        internal virtual StackSettings GetStackSettings() => null;
    }
}