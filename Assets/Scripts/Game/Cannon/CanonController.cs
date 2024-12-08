using System.Collections;
using Game.MeshGeneration;
using Game.Physics;
using Game.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Cannon
{
    public class CanonController : MonoBehaviour
    {
        [SerializeField] private InputActionReference _shotPowerAction;
        [SerializeField] private InputActionReference _fireAction;
        [SerializeField] private TrajectoryDisplay _trajectoryDisplay;
        [SerializeField] private Vector2 _shotPowerConstraints = new Vector2(0, 100);

        [SerializeField] private Transform _muzzleTransform;
        [SerializeField] private Material _projectileMaterial;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private CannonAnimator _cannonAnimator;
        [SerializeField] private DecalManager _decalManager;
        [SerializeField] private CameraShaker _cameraShaker;
        [SerializeField] private CanonAmmo _ammoPrefab;
        
        private ProjectileSpawner _projectileSpawner;

        private float _shotPower = 20f;
        private bool _canShoot = true;

        private void Start()
        {
            _trajectoryDisplay.ToggleDisplay(true);
            _fireAction.action.performed += Shoot;
            _shotPowerAction.action.performed += SetCannonPower;
            _trajectoryDisplay.SetPower(_shotPower);
            _projectileSpawner = new ProjectileSpawner(new ProjectileData()
            {
                Prefab = _ammoPrefab,
                TargetTransform = _muzzleTransform,
                LayerMask = _layerMask,
                DecalManager = _decalManager,
                ProjectileMaterial = _projectileMaterial
            });
        }

        private void SetCannonPower(InputAction.CallbackContext context)
        {
            float value = context.action.ReadValue<float>();
            _shotPower = Mathf.Clamp(_shotPower += value > 0 ? 2 : -2, _shotPowerConstraints.x, _shotPowerConstraints.y);
            _trajectoryDisplay.SetPower(_shotPower);
        }
        
        private void Shoot(InputAction.CallbackContext context)
        {
            if (!_canShoot)
            {
                return;
            }
            StartCoroutine(ShootRoutine());
        }

        private IEnumerator ShootRoutine()
        {
            _canShoot = false;
            _trajectoryDisplay.ToggleDisplay(false);
            _cannonAnimator.AnimateShot();
            _cameraShaker.ShakeCam();
            _projectileSpawner.SpawnProjectile(_shotPower);
            yield return new WaitForSeconds(1f);
            _trajectoryDisplay.ToggleDisplay(true);
            _canShoot = true;
        }
    }
}