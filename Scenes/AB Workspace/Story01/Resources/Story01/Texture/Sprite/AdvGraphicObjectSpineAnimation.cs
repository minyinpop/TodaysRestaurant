using UnityEngine;
using UtageExtensions;
using Spine.Unity;
using System.IO;

namespace Utage
{
    [AddComponentMenu("Utage/ADV/Internal/GraphicObject/Spine/Animation")]
    internal class AdvGraphicObjectSpineAnimation : MonoBehaviour
        , IAdvGraphicObjectCustom
        , IAdvGraphicObjectCustomCommand
        , IAdvGraphicObjectCustomSave
    {
        SkeletonAnimation SkeletonAnim { get { return this.GetComponentCache<SkeletonAnimation>(ref skeletonAnim); } }
        SkeletonAnimation skeletonAnim;

        AdvGraphicObjectCustom2D AdvObj
        {
            get
            {
                if (advObj == null)
                {
                    advObj = this.GetComponentInParent<AdvGraphicObjectCustom2D>();
                }
                return advObj;
            }
        }
        AdvGraphicObjectCustom2D advObj;

        public void ChangeResourceOnDrawSub(AdvGraphicInfo graphic) { }

        public void OnEffectColorsChange(AdvEffectColor color)
        {
            // SkeletonAnimation不支援Graphic.color
            // 但你可以自己加 shader 控制，或跳過
        }

        public void SetCommandArg(AdvCommand command)
        {
            string animationName = command.ParseCellOptional<string>(AdvColumnName.Arg2, "");
            if (string.IsNullOrEmpty(animationName)) return;

            SkeletonAnim.AnimationState.SetAnimation(0, animationName, true);
        }

        const int Version = 0;
        public void WriteSaveDataCustom(BinaryWriter writer)
        {
            writer.Write(Version);
            writer.Write(CurrentAnimationName());
        }

        string CurrentAnimationName()
        {
            var track = SkeletonAnim.AnimationState.GetCurrent(0);
            if (track == null) return "";
            var anim = track.Animation;
            if (anim == null) return "";
            return anim.Name;
        }

        public void ReadSaveDataCustom(BinaryReader reader)
        {
            int version = reader.ReadInt32();
            string animationName = reader.ReadString();
            if (!string.IsNullOrEmpty(animationName))
            {
                SkeletonAnim.AnimationState.SetAnimation(0, animationName, true);
            }
        }
    }
}
