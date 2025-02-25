using System.Collections.Generic;

namespace Player
{
    public static class PlayerStaticValue
    {
        // 玩家當前的背包等級。
        public const int BagLevelIndex = 0;
        // 每個背包等級所解鎖的儲物格數量。
        public static readonly List<int> BagUnlockedSlotPerLevel = new() { 8, 24, 40 };
    }
}