using UnityEngine;

namespace Game.MeshGeneration
{
    public class MeshGenerationTest : MonoBehaviour
    {
        [SerializeField] private Material _material;
        
        public void GenerateCube()
        {
            PrepareTargetObject(out MeshFilter filter);
            MeshFaceGenerator.GenerateCube(filter.sharedMesh, 10);
        }

        public void GenerateSphere()
        {
            PrepareTargetObject(out MeshFilter filter);
            MeshFaceGenerator.GenerateSphere(filter.sharedMesh, 10, 10);
        }

        private void PrepareTargetObject(out MeshFilter filter)
        {
            GameObject targetObject = new GameObject();
            var renderer = targetObject.AddComponent<MeshRenderer>();
            filter = targetObject.AddComponent<MeshFilter>();
            renderer.sharedMaterial = _material;
            filter.sharedMesh = new Mesh();
        }
    }
}