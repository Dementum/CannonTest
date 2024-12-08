using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MeshGeneration
{
    public static class MeshFaceGenerator
    {
        public static void GenerateCube(Mesh mesh, int resolution)
        {
            int resolutionMinusOne = resolution - 1;
            Vector3[] directions = {Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back};

            var vertices = new Vector3[resolution * resolution * directions.Length];
            var triangles = new int[resolutionMinusOne * resolutionMinusOne * 6 * directions.Length];
            
            int triangleIndex = 0;

            //Generate faces
            for (int faceIndex = 0; faceIndex < directions.Length; faceIndex++)
            {
                Vector3 upVector = directions[faceIndex];
                var rightVector = new Vector3(upVector.y, upVector.z, upVector.x);
                var forwardVector = Vector3.Cross(upVector, rightVector);
                
                for (int i = 0; i < resolution; i++)
                {
                    for (int j = 0; j < resolution; j++)
                    {
                        int index = (faceIndex * resolution * resolution) + i * resolution + j;
                        Vector2 percent = new Vector2(j, i) / resolutionMinusOne;
                        Vector3 pointOnUnitCube = upVector + (percent.x - 0.5f) * 2 * rightVector + (percent.y - 0.5f) * 2 * forwardVector;
                        vertices[index] = pointOnUnitCube;

                        //Generate Quad
                        if (j != resolutionMinusOne && i != resolutionMinusOne)
                        {
                            triangles[triangleIndex++] = index;
                            triangles[triangleIndex++] = index + resolution + 1;
                            triangles[triangleIndex++] = index + resolution;
                            triangles[triangleIndex++] = index;
                            triangles[triangleIndex++] = index + 1;
                            triangles[triangleIndex++] = index + resolution + 1;
                        }
                    }
                }
            }
            
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
        
        public static void GenerateSphere(Mesh mesh, int resolution, int radius)
        {
            int resolutionMinusOne = resolution - 1;
            Vector3[] directions = {Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back};

            var vertices = new Vector3[resolution * resolution * directions.Length];
            var triangles = new int[resolutionMinusOne * resolutionMinusOne * 6 * directions.Length];
            
            int triangleIndex = 0;

            //Generate faces
            for (int faceIndex = 0; faceIndex < directions.Length; faceIndex++)
            {
            
                Vector3 upVector = directions[faceIndex];
                var rightVector = new Vector3(upVector.y, upVector.z, upVector.x);
                var forwardVector = Vector3.Cross(upVector, rightVector);
                
                for (int i = 0; i < resolution; i++)
                {
                    for (int j = 0; j < resolution; j++)
                    {
                        int index = (faceIndex * resolution * resolution) + i * resolution + j;
                        Vector2 percent = new Vector2(j, i) / resolutionMinusOne;
                        Vector3 pointOnUnitCube = upVector + (percent.x - 0.5f) * 2 * rightVector + (percent.y - 0.5f) * 2 * forwardVector;
                        Vector3 pointOnUnitSphere;
                        //Don't randomize vertex positions on face borders
                        if (i is 0 or 1 || i == resolution - 1 || i == resolution - 2 || j is 0 or 1 || j == resolution - 1 || j == resolution - 2)
                        {
                            pointOnUnitSphere = pointOnUnitCube.normalized * radius;
                        }
                        else
                        {
                            pointOnUnitSphere = pointOnUnitCube.normalized * (radius * Random.Range(0.9f, 1f));
                        }
                        vertices[index] = pointOnUnitSphere;

                        //Generate Quad
                        if (j != resolutionMinusOne && i != resolutionMinusOne)
                        {
                            triangles[triangleIndex++] = index;
                            triangles[triangleIndex++] = index + resolution + 1;
                            triangles[triangleIndex++] = index + resolution;
                            triangles[triangleIndex++] = index;
                            triangles[triangleIndex++] = index + 1;
                            triangles[triangleIndex++] = index + resolution + 1;
                        }
                    }
                }
            }
            
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
    }
}

