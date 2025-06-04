using Item.Settings;
using UnityEngine;

namespace Item.Base
{
    internal abstract class ItemBase : ScriptableObject
    {
        internal abstract StackSettings GetStackSettings();
    }
}