using System.Collections;
using UnityEngine;

namespace Game.Cannon
{
    public class CannonAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _barrelTransform;
        [SerializeField] private Vector3 _barrelRetractedPos;
        
        public void AnimateShot()
        {
            StartCoroutine(RecoilRoutine());
        }

        private IEnumerator RecoilRoutine()
        {
            Vector3 targetPos = _barrelTransform.localPosition;
            yield return StartCoroutine(SimpleAnimator.LinearMotionRoutine(_barrelTransform, _barrelRetractedPos, 0.1f));
            yield return StartCoroutine(SimpleAnimator.LinearMotionRoutine(_barrelTransform, targetPos, 0.4f));

            yield return null;
        }
    }
}