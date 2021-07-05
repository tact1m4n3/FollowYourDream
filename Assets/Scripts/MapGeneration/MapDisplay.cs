using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Material mapMaterial;

    public void UpdateMapMat(Texture2D tex)
    {
        mapMaterial.mainTexture = tex;
    }

    public void DrawMesh(MeshData meshData, PropData[] propData, Texture2D tex)
    {
        GameObject currentMesh = GameObject.FindGameObjectWithTag("Map");
        if (!currentMesh)
        {
            currentMesh = GameObject.CreatePrimitive(PrimitiveType.Plane);
            currentMesh.name = "Map";
            currentMesh.tag = "Map";
        }

        Mesh mesh = meshData.CreateMesh();
        MeshFilter meshFilter = currentMesh.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = currentMesh.GetComponent<MeshRenderer>();
        MeshCollider meshCollider = currentMesh.GetComponent<MeshCollider>();

        meshFilter.mesh = mesh;
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;

        // UpdateMapMat(tex);

        meshRenderer.material.mainTexture = tex;
        meshRenderer.sharedMaterial.mainTexture = tex;

        var children = new List<GameObject>();
        foreach (Transform child in currentMesh.transform) children.Add(child.gameObject);
        children.ForEach(child => DestroyImmediate(child));

        foreach (PropData prop in propData)
        {
            if (prop.pos != Vector3.zero)
                Instantiate(prop.prefab, prop.pos, Quaternion.identity, currentMesh.transform);
        }
    }

    public void DeleteMesh()
    {
        GameObject currentMesh = GameObject.FindGameObjectWithTag("Map");
        if (currentMesh)
            Destroy(currentMesh);
    }
}
