using UnityEngine;

namespace Game.Physics
{
    public interface IPhysicalObject
    {
        void SetIndex(int index);
        void Simulate();
        void AddImpulse(Vector3 impulse);

        void HandleCollision(RaycastHit hit);
        void Cleanup();
    }
}