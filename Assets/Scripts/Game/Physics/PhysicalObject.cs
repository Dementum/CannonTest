using System;
using UnityEngine;

namespace Game.Physics
{
    public class PhysicalObject : MonoBehaviour, IPhysicalObject
    {
        [SerializeField] private Transform _transform;

        private int _index;
        protected bool _idle = true;
        private Vector3 _movementVector = Vector3.zero;
        private ICollisionLogic _collisionLogic;
        private float _spawnTime;
        private Vector3 _originPoint;
        private Vector3 _newDirection;
        private LayerMask _layerMask;

        private Action<int> _unregisterAction;
        
        public const float BOUNCE_MULTIPLIER = 0.5f;
        public const float GRAVITY = 9.8f;
        
        public void AddImpulse(Vector3 impulse)
        {
            _movementVector += impulse;
            _idle = false;
        }

        public void HandleCollision(RaycastHit hit)
        {
            _originPoint = _transform.position;
            _movementVector = Vector3.Reflect(_newDirection / Time.deltaTime, hit.normal) * BOUNCE_MULTIPLIER;
            _spawnTime = Time.time;
            OnCollision(hit.point);
        }

        public virtual void OnCollision(Vector3 point)
        {
            
        }

        public void Cleanup()
        {
            _unregisterAction?.Invoke(_index);
        }

        public void Simulate()
        {
            if (_transform == null)
            {
                return;
            }
            
            if (_idle)
            {
                return;
            }

            float lifetime = Time.time - _spawnTime;
            
            if (lifetime == 0)
            {
                return;
            }

            Vector3 newPosition = new Vector3
            (
                _originPoint.x + _movementVector.x * lifetime,
                CalculateYCoord(_originPoint, _movementVector, lifetime),
                _originPoint.z + _movementVector.z * lifetime
            );
            
            _newDirection = newPosition - _transform.position;
            
            RaycastHit[] hits = _collisionLogic?.CheckForCollision(new Ray(_transform.position, _newDirection.normalized), _transform.lossyScale, _layerMask);
            if (hits != null)
            {
                HandleCollision(hits[0]);
            }
            else
            {
                _transform.position = newPosition;
            }
        }

        public void SetLayerMask(LayerMask mask)
        {
            _layerMask = mask;
        }

        public void SetUnregisterAction(Action<int> action)
        {
            _unregisterAction = action;
        }
        
        private float CalculateYCoord(Vector3 origin, Vector3 direction, float timeStamp)
        {
            return origin.y + direction.y * timeStamp - (GRAVITY / 2f * timeStamp * timeStamp);
        }

        public void SetIndex(int index)
        {
            _index = index;
        }
        
        private void Awake()
        {
            _spawnTime = Time.time;
            _transform = transform;
            _originPoint = _transform.position;
            _collisionLogic = new SphereCollisionLogic();
        }

        private void FixedUpdate()
        {
            Simulate();
        }

        private void Reset()
        {
            _transform = transform;
        }
    }
}

