using UnityEngine;
using UtageExtensions;
using Spine.Unity;
using System.IO;

namespace Utage
{
    [AddComponentMenu("Utage/ADV/Internal/GraphicObject/Spine/Default")]
    internal class AdvGraphicObjectSpine : MonoBehaviour
        , IAdvGraphicObjectCustom
        , IAdvGraphicObjectCustomCommand
        , IAdvGraphicObjectCustomSave
    {
        SkeletonAnimation SkeletonAnimation { get { return this.GetComponentCache<SkeletonAnimation>(ref skeletonAnimation); } }
        SkeletonAnimation skeletonAnimation;

        AdvGraphicObjectCustom AdvObj
        {
            get
            {
                if (advObj == null)
                {
                    advObj = this.GetComponentInParent<AdvGraphicObjectCustom>();
                }
                return advObj;
            }
        }
        AdvGraphicObjectCustom advObj;

        //描画時のリソース変更
        public void ChangeResourceOnDrawSub(AdvGraphicInfo graphic)
        {
            SetSortingOrder(this.AdvObj.Layer.Canvas.sortingOrder, this.AdvObj.Layer.Canvas.sortingLayerName);
        }

        //描画順の設定
        void SetSortingOrder(int sortingOrder, string sortingLayerName)
        {
            Renderer render = GetComponent<Renderer>();
            render.sortingOrder = sortingOrder;
            render.sortingLayerName = sortingLayerName;
        }

        //エフェクト用の色が変化したとき
        public void OnEffectColorsChange(AdvEffectColor color)
        {
            //          SkeletonAnimation.color = color.MulColor;
        }

        //********描画時の引数適用********//
        public void SetCommandArg(AdvCommand command)
        {
            string animationName = command.ParseCellOptional<string>(AdvColumnName.Arg2, "");
            if (string.IsNullOrEmpty(animationName)) return;

            //          float fadeTime = command.ParseCellOptional<float>(AdvColumnName.Arg6, 0.2f);
            SkeletonAnimation.state.SetAnimation(0, animationName, true);
        }
        const int Version = 0;
        public void WriteSaveDataCustom(BinaryWriter writer)
        {
            writer.Write(Version);
            writer.Write(CurrentAnimationName());
        }

        string CurrentAnimationName()
        {
            var track = SkeletonAnimation.state.GetCurrent(0);
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
                SkeletonAnimation.state.SetAnimation(0, animationName, true);
            }
        }
    }
}