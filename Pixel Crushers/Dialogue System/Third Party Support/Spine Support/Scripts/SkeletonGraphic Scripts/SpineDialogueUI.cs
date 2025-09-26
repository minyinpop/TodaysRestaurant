using PixelCrushers.DialogueSystem;
using CharacterInfo = PixelCrushers.DialogueSystem.CharacterInfo;

namespace Pixel_Crushers.Dialogue_System.Third_Party_Support.Spine_Support.Scripts.SkeletonGraphic_Scripts
{

    /// <summary>
    /// Overrides the StandardDialogueUI Open() method to read the conversation's custom fields.
    /// - "Panel # Actor" specifies the actor in panel #.
    /// - "Panel # Start Visible" specifies whether panel # should start visible.
    /// </summary>
    public class SpineDialogueUI : StandardDialogueUI
    {
        public override void Open()
        {
            var conversation = DialogueManager.masterDatabase.GetConversation(DialogueManager.lastConversationID);
            for (int i = 0; i < conversationUIElements.subtitlePanels.Length; i++)
            {
                // Set panel visibility according to "Panel # Start Visible":
                var startVisible = conversation.LookupBool("Panel " + i + " Start Visible");
                var panel = conversationUIElements.subtitlePanels[i];
                panel.visibility = startVisible ? UIVisibility.AlwaysFromStart : UIVisibility.AlwaysOnceShown;

                // Setup the Spine dialogue actor that should appear in this panel:
                if (!(panel is SpineSubtitlePanel)) continue;
                var actorID = conversation.LookupInt("Panel " + i + " Actor");
                var actor = DialogueManager.masterDatabase.GetActor(actorID);
                if (actor == null) continue;
                var actorTransform = CharacterInfo.GetRegisteredActorTransform(actor.Name);
                if (actorTransform == null) continue;
                var dialogueActor = actorTransform.GetComponent<DialogueActor>();
                if (dialogueActor == null) continue;
                dialogueActor.SetSubtitlePanelNumber(PanelNumberUtility.IntToSubtitlePanelNumber(i));
                (panel as SpineSubtitlePanel).ShowSpineDialogueActor(actorTransform);
            }

            base.Open();
        }
    }
}
