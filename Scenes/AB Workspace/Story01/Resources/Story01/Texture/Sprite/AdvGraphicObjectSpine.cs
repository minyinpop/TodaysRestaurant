using System.IO;
using Spine.Unity;
using UnityEngine;
using Utage;

namespace Scenes.AB_Workspace.Story01.Resources.Story01.Texture.Sprite
{
    [AddComponentMenu("Utage/ADV/Internal/GraphicObject/Spine/Default")]
    internal class AdvGraphicObjectSpine : MonoBehaviour, IAdvGraphicObjectCustom, IAdvGraphicObjectCustomCommand, IAdvGraphicObjectCustomSave
    {
        private Renderer Renderer { get; set; }
        private SkeletonAnimation SkeletonAnimation { get; set; }

        private AdvGraphicObjectCustom _advObj;
        private AdvGraphicObjectCustom AdvObj
        {
            get
            {
                if (_advObj is null)
                    _advObj = GetComponentInParent<AdvGraphicObjectCustom>();
                
                return _advObj;
            }
        }

        private void Awake()
        {
            Renderer = GetComponent<Renderer>();
            SkeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        public void ChangeResourceOnDrawSub(AdvGraphicInfo graphic)
        {
            Renderer.sortingOrder = AdvObj.Layer.Canvas.sortingOrder;
            Renderer.sortingLayerName = AdvObj.Layer.Canvas.sortingLayerName;
        }

        public void OnEffectColorsChange(AdvEffectColor color)
        {
            SkeletonAnimation.Skeleton.SetColor(color.MulColor);
        }

        public void SetCommandArg(AdvCommand command)
        {
            var newAnimationName = command.ParseCellOptional(AdvColumnName.Arg2, "");

            if (string.IsNullOrEmpty(newAnimationName))
                return;

            var track = command.ParseCellOptional(AdvColumnName.Track, 0);
            var isLoop = command.ParseCellOptional(AdvColumnName.Loop, false);

            SkeletonAnimation.AnimationState.SetAnimation(track, newAnimationName, isLoop);
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
            
            if (track == null)
                return "";

            var anim = track.Animation;
            return anim is null ? "" : anim.Name;
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