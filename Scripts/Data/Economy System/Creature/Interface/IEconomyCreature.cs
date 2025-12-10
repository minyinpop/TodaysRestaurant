using Spine;

namespace Data.Economy_System.Creature.Interface
{
    public interface IEconomyCreature
    {
        #region Skin
            public void GetRandomSkin(SkeletonData skeletonData, out Skin skin);
        #endregion
    }
}