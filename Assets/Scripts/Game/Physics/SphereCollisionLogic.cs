using UnityEngine;

namespace Game.Physics
{
    public class SphereCollisionLogic : ICollisionLogic
    {
        private RaycastHit[] _triggeredColliders = new RaycastHit[1];
        public RaycastHit[] CheckForCollision(Ray origin, Vector3 halfExtents, LayerMask mask)
        {
            if (UnityEngine.Physics.SphereCastNonAlloc(origin.origin, 0.5f, origin.direction, _triggeredColliders, 0.5f) == 0 || _triggeredColliders[0].collider == null)
            {
                return null;
            }
            return _triggeredColliders;
        }
    }
}