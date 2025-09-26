using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.SequencerCommands;
using UnityEngine;

namespace Pixel_Crushers.Dialogue_System.Third_Party_Support.Spine_Support.Scripts
{

    /// <summary>
    /// Sequencer command: SpineAnimation(animationName, [subject], [trackIndex], [loop])
    /// 
    /// Parameters:
    /// - animationName: Name of an animation asset in the subject's SpineSequencerReferences component.
    /// - subject: (Optional) Spine GameObject with a SpineSequencerReferences component. Default: speaker.
    /// - trackIndex: (Optional) Track index to use. Default: 0.
    /// - loop: (Optional) Loop or not. Default: true.
    /// </summary>
    public class SequencerCommandSpineAnimation : SequencerCommand
    {

        public void Start()
        {
            var animationName = GetParameter(0);
            var subject = GetSubject(1, speaker);
            var trackIndex = GetParameterAsInt(2);
            var loop = GetParameterAsBool(3, true);
            var references = (subject != null) ? subject.GetComponentInChildren<SpineSequencerReferences>() : null;

            // First check in reference list:
            var animationRef = (references != null) ? references.animationReferenceAssets.Find(x => x.name == animationName) : null; 
            var animation = (animationRef != null) ? animationRef.Animation : null;

            Spine.AnimationState state = null;

            if (references != null)
            {
                if (references.skeletonAnimation != null)
                {
                    if (animation == null) animation = references.skeletonAnimation.Skeleton?.Data?.FindAnimation(animationName); // Fallback to dynamic lookup from skeleton data
                    state = references.skeletonAnimation.AnimationState;
                }
                else if (references.skeletonGraphic != null)
                {
                    if (animation == null) animation = references.skeletonGraphic.Skeleton?.Data?.FindAnimation(animationName); // Fallback to dynamic lookup from skeleton data
                    state = references.skeletonGraphic.AnimationState;
                }
            }

            if (subject == null)
            {
                if (DialogueDebug.logWarnings) Debug.LogWarning("Dialogue System: Sequencer: SpineAnimation(" + GetParameters() + ") can't find the subject.");
            }
            else if (references == null)
            {
                if (DialogueDebug.logWarnings) Debug.LogWarning("Dialogue System: Sequencer: SpineAnimation(" + GetParameters() + ") subject " + subject + " needs a SpineSequencerReferences component.", subject);
            }
            else if (animation == null)
            {
                if (DialogueDebug.logWarnings) Debug.LogWarning("Dialogue System: Sequencer: SpineAnimation(" + GetParameters() + ") SpineSequencerReferences on " + subject + " doesn't have an AnimationReferenceAsset named '" + animationName + "'.", subject);
            }
            else if (state == null)
            {
                if (DialogueDebug.logWarnings) Debug.LogWarning("Dialogue System: Sequencer: SpineAnimation(" + GetParameters() + ") SkeletonAnimation referenced by SpineSequencerReferences on " + subject + " doesn't have an AnimationState.", subject);
            }
            else
            {
                if (DialogueDebug.logInfo) Debug.Log("Dialogue System: Sequencer: SpineAnimation(" + GetParameters() + ")", subject);
                state.SetAnimation(trackIndex, animation, loop);
            }
            Stop();
        }

    }
}
