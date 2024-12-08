using UnityEngine;

namespace Game.Physics
{
    public interface ICollisionLogic
    {
        RaycastHit[] CheckForCollision(Ray origin, Vector3 halfExtents, LayerMask mask);
    }
}