using System.IO;
using Spine.Unity;
using UnityEngine;
using Utage;
using UtageExtensions;

namespace System.Dialogue.Utage
{
    [AddComponentMenu("Utage/ADV/Internal/GraphicObject/Spine/Graphic")]
    public class AdvGraphicObjectSpineGraphic : MonoBehaviour, IAdvGraphicObjectCustom, IAdvGraphicObjectCustomCommand, IAdvGraphicObjectCustomSave
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
        AdvGraphicObjectCustom2D advObj;

        //描画時のリソース変更
        public void ChangeResourceOnDrawSub(AdvGraphicInfo graphic)
        {
        }

        //エフェクト用の色が変化したとき
        public void OnEffectColorsChange(AdvEffectColor color)
        {
            SkeletonGraphic.color = color.MulColor;
        }

        //********描画時の引数適用********//
        public void SetCommandArg(AdvCommand command)
        {
            var trackIndex = command.ParseCellOptional(AdvColumnName.Arg7, 0);
            var animationName = command.ParseCellOptional(AdvColumnName.Arg8, "");
            var loop = command.ParseCellOptional(AdvColumnName.Arg9, false);
            
            SkeletonGraphic.AnimationState.SetAnimation(trackIndex, animationName, loop);
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