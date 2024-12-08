using System.Collections;
using UnityEngine;

namespace Game
{
    public static class SimpleAnimator
    {
        public static IEnumerator LinearMotionRoutine(Transform target, Vector3 taregtPos, float duration)
        {
            Vector3 startPos = target.localPosition;

            for (float i = 0; i <= 1f; i+= Time.deltaTime / duration)
            {
                target.localPosition = Vector3.Lerp(startPos, taregtPos, i);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}