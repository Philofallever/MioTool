using Sirenix.OdinInspector;
using UnityEngine;

namespace MioTool
{
    public partial class MioTool
    {
        #region Animation

        [PropertySpace(SpaceBefore = 10)]
        [BoxGroup("Animation"), Button, ShowIf(nameof(ValidicateAnimation))]
        void PlayAnim(string clip)
        {
            var anim = GetComponentInChildren<Animation>();
            if (string.IsNullOrEmpty(clip))
                anim.Play();
            else
                anim.Play(clip);
        }

        [PropertySpace(SpaceAfter = 10)]
        [BoxGroup("Animation"), Button, ShowIf(nameof(ValidicateAnimation))]
        void DumpPlayingAnim()
        {
            var anim = GetComponentInChildren<Animation>();
            foreach (AnimationState animState in anim)
            {
                if (anim.IsPlaying(animState.name))
                {
                    print($"anim is playing: {animState.name}");
                    break;
                }
            }
        }

        bool ValidicateAnimation()
        {
            return GetComponentInChildren<Animation>();
        }

        #endregion
    }
}