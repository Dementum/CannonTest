using System;
using UnityEngine;

namespace Game.Physics
{
    public class TrajectoryDisplay : MonoBehaviour
    {
        [SerializeField] private Transform _muzzle;
        [SerializeField] private float _timeStep = 0.5f;
        [SerializeField] private int _stepsCount = 8;
        [SerializeField] private LineRenderer _lineRenderer;

        private bool _isDisplayed;
        private float _power = 10f;

        public void ToggleDisplay(bool enabled)
        {
            _isDisplayed = enabled;
        }
        
        public void TestTrajectory()
        {
            Vector3 origin = _muzzle.position;
            Simulate(origin, _muzzle.forward * _power);
        }

        public void SetPower(float value)
        {
            _power = value;
        }
        
        public void Simulate(Vector3 origin, Vector3 startImpulse)
        {
            Vector3 startPosition = origin;
            Vector3 newPosition = origin;
            _lineRenderer.positionCount = _stepsCount;
            _lineRenderer.SetPosition(0, origin);
            for (int i = 0; i < _stepsCount; i++)
            {
                newPosition = new Vector3
                (
                    origin.x + startImpulse.x * _timeStep * i,
                        CalculateYCoord(origin, startImpulse, _timeStep * i),
                    origin.z + startImpulse.z * _timeStep * i
                );
                
                _lineRenderer.SetPosition(i, startPosition);

                Vector3 direction = newPosition - startPosition;
                if (i < _stepsCount -1 && UnityEngine.Physics.Raycast(startPosition, direction, direction.magnitude))
                {
                    _lineRenderer.SetPosition(i + 1, newPosition);
                    _lineRenderer.positionCount = i + 2;
                    break;
                }

                startPosition = newPosition;
            }
        }

        private float CalculateYCoord(Vector3 origin, Vector3 direction, float timeStamp)
        {
            return origin.y + direction.y * timeStamp - (PhysicalObject.GRAVITY / 2f * timeStamp * timeStamp);
        }

        private void FixedUpdate()
        {
            if (_isDisplayed)
            {
                TestTrajectory();
            }
            else
            {
                _lineRenderer.positionCount = 0;
            }
        }
    }
}