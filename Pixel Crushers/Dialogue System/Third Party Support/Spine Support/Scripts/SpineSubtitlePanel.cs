using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Pixel_Crushers.Dialogue_System.Third_Party_Support.Spine_Support.Scripts
{

    /// <summary>
    /// This subclass of StandardUISubtitlePanel is aware of characters that
    /// have SpineDialogueActor components.
    /// </summary>
    public class SpineSubtitlePanel : StandardUISubtitlePanel
    {
        private SpineDialogueActor visibleSpineDialogueActor = null;

        [Obsolete("Use OpenOnStartConversation(Sprite,string,DialogueActor) instead.")]
        public override void OpenOnStartConversation(Texture2D portraitSprite, string portraitName, DialogueActor dialogueActor)
        {
            base.OpenOnStartConversation(portraitSprite, portraitName, dialogueActor);
            if (dialogueActor != null) ShowSpineDialogueActor(dialogueActor.transform);
        }

        public override void ShowSubtitle(Subtitle subtitle)
        {
            base.ShowSubtitle(subtitle);
            ShowSpineDialogueActor(subtitle.speakerInfo.transform);
        }

        public virtual void ShowSpineDialogueActor(Transform actorTransform)
        {
            if (actorTransform == null) return;
            var spineDialogueActor = actorTransform.GetComponent<SpineDialogueActor>();
            if (spineDialogueActor != visibleSpineDialogueActor)
            {
                if (visibleSpineDialogueActor != null) visibleSpineDialogueActor.Hide(this);
                if (spineDialogueActor != null) spineDialogueActor.Show(this);
                visibleSpineDialogueActor = spineDialogueActor;
            }
        }

        public override void Close()
        {
            if (visibleSpineDialogueActor != null) visibleSpineDialogueActor.Hide(this);
            visibleSpineDialogueActor = null;
            base.Close();
        }
    }
}
