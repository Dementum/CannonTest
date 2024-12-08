using Game.MeshGeneration;
using Game.Systems;
using UnityEngine;

namespace Game.Cannon
{
    public class ProjectileData
    {
        public CanonAmmo Prefab;
        public Transform TargetTransform;
        public LayerMask LayerMask;
        public DecalManager DecalManager;
        public Material ProjectileMaterial;
    }
    
    public class ProjectileSpawner
    {
        private ProjectileData _projectileData;
        
        public ProjectileSpawner(ProjectileData data)
        {
            _projectileData = data;
        }
        
        public void SpawnProjectile(float shotPower)
        {
            CanonAmmo ammo = PrepareProjectile();
            ammo.SetDecalManager(_projectileData.DecalManager);
            ammo.SetLayerMask(_projectileData.LayerMask);
            ammo.AddImpulse(_projectileData.TargetTransform.forward * shotPower);
        }
        
        private CanonAmmo PrepareProjectile()
        {
            CanonAmmo ammo = Object.Instantiate(_projectileData.Prefab, _projectileData.TargetTransform.position, Quaternion.identity);
            var renderer = ammo.GetRenderer();
            var filter = ammo.GetFilter();
            renderer.sharedMaterial = _projectileData.ProjectileMaterial;
            filter.sharedMesh = new Mesh();
            MeshFaceGenerator.GenerateSphere(filter.sharedMesh, 10, 1);

            return ammo;
        }
    }
}