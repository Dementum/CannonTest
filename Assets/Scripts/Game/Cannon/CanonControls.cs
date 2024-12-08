using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Cannon
{
    public class CanonControls : MonoBehaviour
    {
        [SerializeField] private InputActionReference _aimAction;
        
        [SerializeField] private Transform _yewTransform;
        [SerializeField] private Transform _pitchTransform;
        [SerializeField] private Vector2 _yewRotationConstraints;
        [SerializeField] private Vector2 _pitchRotationConstraints;
        
        private void AimCanon(InputAction.CallbackContext context)
        {
            Vector2 deltaAim = context.action.ReadValue<Vector2>();
            var yewClamped = Mathf.Clamp(_yewTransform.localEulerAngles.y + deltaAim.x * Time.deltaTime * 10f, _yewRotationConstraints.x,
                _yewRotationConstraints.y);
            _yewTransform.localRotation = Quaternion.Euler(0f, yewClamped, 0f);
            var pitchClamped = Mathf.Clamp(_pitchTransform.localEulerAngles.x - deltaAim.y * Time.deltaTime * 10f, _pitchRotationConstraints.x,
                _pitchRotationConstraints.y);
            _pitchTransform.localRotation = Quaternion.Euler(pitchClamped, 0f, 0f);
        }
        
        private void Start()
        {
            _aimAction.action.performed += AimCanon;
        }
    }
}