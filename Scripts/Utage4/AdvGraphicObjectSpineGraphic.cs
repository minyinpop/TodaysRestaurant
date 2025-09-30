using System.IO;
using Spine.Unity;
using UnityEngine;
using Utage;
using UtageExtensions;

namespace Utage4
{
    [AddComponentMenu("Utage/ADV/Internal/GraphicObject/Spine/Graphic")]
    internal class AdvGraphicObjectSpineGraphic : MonoBehaviour, IAdvGraphicObjectCustom, IAdvGraphicObjectCustomCommand, IAdvGraphicObjectCustomSave
    {
        private SkeletonGraphic SkeletonGraphic => this.GetComponentCache(ref skeletonGraphic);
        private SkeletonGraphic skeletonGraphic;

        private AdvGraphicObjectCustom2D AdvObj
        {
            get
            {
                if (advObj == null)
                {
                    advObj = GetComponentInParent<AdvGraphicObjectCustom2D>();
                }

                return advObj;
            }
        }

        private AdvGraphicObjectCustom2D advObj;
        public void ChangeResourceOnDrawSub(AdvGraphicInfo graphic)
        {
        }

        public void OnEffectColorsChange(AdvEffectColor color)
        {
            SkeletonGraphic.color = color.MulColor;
        }

        public void SetCommandArg(AdvCommand command)
        {
            var trackIndex = command.ParseCellOptional(AdvColumnName.Arg7, 0);
            var animationName = command.ParseCellOptional(AdvColumnName.Arg8, "");
            var isLoop = command.ParseCellOptional(AdvColumnName.Arg9, false);
            
            if (string.IsNullOrEmpty(animationName)) return;
            // var fadeTime = command.ParseCellOptional<float>(AdvColumnName.Arg6, 0.2f);
            SkeletonGraphic.AnimationState.SetAnimation(trackIndex, animationName, isLoop);
        }

        private const int Version = 0;
        public void WriteSaveDataCustom(BinaryWriter writer)
        {
            writer.Write(Version);
            writer.Write(CurrentAnimationName());
        }

        private string CurrentAnimationName()
        {
            var track = SkeletonGraphic.AnimationState.GetCurrent(0);
            if (track == null) return "";
            var anim = track.Animation;
            if (anim == null) return "";
            return  anim.Name;
        }

        public void ReadSaveDataCustom(BinaryReader reader)
        {
            var version = reader.ReadInt32();
            var animationName = reader.ReadString();
            if(!string.IsNullOrEmpty(animationName))
            {
                SkeletonGraphic.AnimationState.SetAnimation(0, animationName, true);
            }
        }
    }
}