using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public static class GeometricCenterPointCalculator
    {

        public static Vector3 CalculateGeometricCenter(GameObject obj)
        {
            Vector3 pivot = Vector3.zero;
            int numberOfPoints = 0;
            var meshFilters = obj.GetComponentsInChildren<MeshFilter>();

            foreach (MeshFilter meshes in meshFilters)
            {
                var vertices = meshes.mesh.vertices;

                foreach (Vector3 vert in vertices)
                {
                    pivot += obj.transform.TransformPoint(vert);
                    numberOfPoints++;
                }
            }

            return pivot / numberOfPoints;
        }
    }
}
