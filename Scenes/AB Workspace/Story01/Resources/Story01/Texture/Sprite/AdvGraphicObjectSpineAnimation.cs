using System.IO;
using Spine.Unity;
using UnityEngine;
using Utage;
using UtageExtensions;

namespace Scenes.AB_Workspace.Story01.Resources.Story01.Texture.Sprite
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
            // SkeletonAnimation���䴩Graphic.color
            // ���A�i�H�ۤv�[ shader ����A�θ��L
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
