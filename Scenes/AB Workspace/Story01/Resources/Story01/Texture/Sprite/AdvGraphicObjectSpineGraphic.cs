using UnityEngine;
using UtageExtensions;
using Spine.Unity;
using System.IO;
using System;


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
            // 換 skin
            string skinName = command.ParseCellOptional<string>("Arg7", "");
            Debug.Log("Arg7 (skinName) = " + skinName);

            if (!string.IsNullOrEmpty(skinName))
            {
                Debug.Log("Try set skin: " + skinName);
                SkeletonGraphic.Skeleton.SetSkin(skinName);
                SkeletonGraphic.Skeleton.SetSlotsToSetupPose();
            }

            // 播動畫
            string animationName = command.ParseCellOptional<string>(AdvColumnName.Arg2, "");
            if (!string.IsNullOrEmpty(animationName))
            {
                Debug.Log("Play animation: " + animationName);
                SkeletonGraphic.AnimationState.SetAnimation(0, animationName, true);
            }

            SkeletonGraphic.AnimationState.Apply(SkeletonGraphic.Skeleton);
            SkeletonGraphic.UpdateMesh();
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