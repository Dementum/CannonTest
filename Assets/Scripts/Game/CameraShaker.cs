using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public struct KeyPointData
    {
        public Vector3 TargetPos;
        public float DurationSec;
    }
    
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private List<KeyPointData> _keypoints;

        public void ShakeCam()
        {
            StartCoroutine(ShakeCamRoutine());
        }

        private IEnumerator ShakeCamRoutine()
        {
            Vector3 startPos = _camera.localPosition;
            for (int i = 0; i < _keypoints.Count; i++)
            {
                KeyPointData keypointData = _keypoints[i];
                yield return StartCoroutine(SimpleAnimator.LinearMotionRoutine(_camera, keypointData.TargetPos, keypointData.DurationSec));
            }
            
            yield return StartCoroutine(SimpleAnimator.LinearMotionRoutine(_camera, startPos, .1f));
        }
    }
}