using UnityEngine;
using UtageExtensions;
using Spine.Unity;
using System.IO;

namespace Utage
{
    [AddComponentMenu("Utage/ADV/Internal/GraphicObject/Spine/Graphic")]
    internal class AdvGraphicObjectSpineGraphic : MonoBehaviour
        , IAdvGraphicObjectCustom
        , IAdvGraphicObjectCustomCommand
        , IAdvGraphicObjectCustomSave
    {
        SkeletonGraphic SkeletonGraphic { get { return this.GetComponentCache<SkeletonGraphic>(ref skeletonGraphic); } }
        SkeletonGraphic skeletonGraphic;

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
            string animationName = command.ParseCellOptional<string>(AdvColumnName.Arg2, "");
            if (string.IsNullOrEmpty(animationName)) return;

            //          float fadeTime = command.ParseCellOptional<float>(AdvColumnName.Arg6, 0.2f);
            SkeletonGraphic.AnimationState.SetAnimation(0, animationName, true);
        }

        const int Version = 0;
        public void WriteSaveDataCustom(BinaryWriter writer)
        {
            writer.Write(Version);
            writer.Write(CurrentAnimationName());
        }

        string CurrentAnimationName()
        {
            var track = SkeletonGraphic.AnimationState.GetCurrent(0);
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
                SkeletonGraphic.AnimationState.SetAnimation(0, animationName, true);
            }
        }
    }
}