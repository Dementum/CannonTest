using UnityEngine;

namespace Game.Physics
{
    public class BoxCollisionLogic : ICollisionLogic
    {
        private RaycastHit[] _triggeredColliders = new RaycastHit[1];

        public RaycastHit[] CheckForCollision(Ray originRay, Vector3 halfExtents, LayerMask mask)
        {
            UnityEngine.Physics.BoxCastNonAlloc(originRay.origin, originRay.direction,  halfExtents, _triggeredColliders);
            return _triggeredColliders;
        }
    }
}