using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Systems
{
    public class DecalManager : MonoBehaviour
    {
        [SerializeField] private GameObject _decalPrefab;

        private List<GameObject> _spawnedDecals;
        
        public async Task SpawnDecal(Vector3 position)
        {
            var decal = Instantiate(_decalPrefab, position, quaternion.identity);
            await Task.Delay(5000);
            if (decal != null)
            {
                DestroyImmediate(decal);
            }
        }
    }
}