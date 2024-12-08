using System.Collections;
using Game.Physics;
using Game.Systems;
using UnityEngine;

namespace Game.Cannon
{
    public class CanonAmmo : PhysicalObject
    {
        private DecalManager _decalManager;
        [SerializeField] private MeshFilter _filter;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private ParticleSystem _explosionParticles;

        private bool _isRicocheting;

        public void SetDecalManager(DecalManager decalManager)
        {
            _decalManager = decalManager;
        }
        
        public override void OnCollision(Vector3 point)
        {
            base.OnCollision(point);
            _decalManager.SpawnDecal(point);
            if (!_isRicocheting)
            {
                _isRicocheting = true;
                return;
            }

            StartCoroutine(ExplodeRoutine());
        }

        private IEnumerator ExplodeRoutine()
        {
            Cleanup();
            _renderer.enabled = false;
            _explosionParticles.Play();
            _idle = true;
            yield return new WaitForSeconds(2f);
            DestroyImmediate(gameObject);
        }
        
        public MeshFilter GetFilter()
        {
            return _filter;
        }

        public MeshRenderer GetRenderer()
        {
            return _renderer;
        }
        
    }
}