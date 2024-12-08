using System.Collections;
using Game.Cannon;
using Game.Physics;
using UnityEngine;

namespace Game
{
    public class CannonGame : MonoBehaviour
    {
        [SerializeField] private CanonController _cannonController;

        private void Awake()
        {
            StartCoroutine(GameStartRoutine());
        }

        private IEnumerator GameStartRoutine()
        {
            yield return new WaitForSeconds(1);
            _cannonController.enabled = true;
        }
    }
}