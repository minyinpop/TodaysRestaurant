using System.IO;
using Spine.Unity;
using UnityEngine;
using Utage;
using UtageExtensions;

namespace Utage4
{
    [AddComponentMenu("Utage/ADV/Internal/GraphicObject/Spine/Default")]
    internal class AdvGraphicObjectSpine : MonoBehaviour, IAdvGraphicObjectCustom, IAdvGraphicObjectCustomCommand, IAdvGraphicObjectCustomSave
    {
        private SkeletonAnimation SkeletonAnimation => this.GetComponentCache(ref skeletonAnimation);
        private SkeletonAnimation skeletonAnimation;

        private AdvGraphicObjectCustom AdvObj
        {
            get
            {
                if (advObj is null)
                {
                    advObj = GetComponentInParent<AdvGraphicObjectCustom>();
                }

                return advObj;
            }
        }

        private AdvGraphicObjectCustom advObj;
        public void ChangeResourceOnDrawSub(AdvGraphicInfo graphic)
        {
            SetSortingOrder(AdvObj.Layer.Canvas.sortingOrder, AdvObj.Layer.Canvas.sortingLayerName);
        }
        
        private void SetSortingOrder(int sortingOrder, string sortingLayerName)
        {
            var render = GetComponent<Renderer>();
            render.sortingOrder = sortingOrder;
            render.sortingLayerName = sortingLayerName;
        }

        public void OnEffectColorsChange(AdvEffectColor color)
        {
            // SkeletonAnimation.color = color.MulColor;
        }

        public void SetCommandArg(AdvCommand command)
        {
            var trackIndex = command.ParseCellOptional(AdvColumnName.Arg7, 0);
            var animationName = command.ParseCellOptional(AdvColumnName.Arg8, "");
            var isLoop = command.ParseCellOptional(AdvColumnName.Arg9, false);
            
            if (string.IsNullOrEmpty(animationName)) return;
            // var fadeTime = command.ParseCellOptional<float>(AdvColumnName.Arg6, 0.2f);
            SkeletonAnimation.state.SetAnimation(trackIndex, animationName, isLoop);
        }

        private const int Version = 0;
        public void WriteSaveDataCustom(BinaryWriter writer)
        {
            writer.Write(Version);
            writer.Write(CurrentAnimationName());
        }

        private string CurrentAnimationName()
        {
            var track = SkeletonAnimation.state.GetCurrent(0);
            if (track == null) return "";
            var anim = track.Animation;
            if (anim == null) return "";
            return anim.Name;
        }

        public void ReadSaveDataCustom(BinaryReader reader)
        {
            var version = reader.ReadInt32();
            var animationName = reader.ReadString();
            if(!string.IsNullOrEmpty(animationName))
            {
                SkeletonAnimation.state.SetAnimation(0, animationName, true);
            }
        }
    }
}