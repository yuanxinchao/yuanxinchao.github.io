using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ExampleClass : MonoBehaviour
{
    public Font font;
    void Start()
    {
        TextGenerationSettings settings = new TextGenerationSettings();
        settings.textAnchor = TextAnchor.MiddleCenter;
        settings.color = Color.red;
        settings.generationExtents = new Vector2(500.0F, 200.0F);
        settings.pivot = Vector2.zero;
        settings.richText = true;
        settings.font = font;
        settings.fontSize = 18;
        settings.fontStyle = FontStyle.Normal;
        settings.verticalOverflow = VerticalWrapMode.Overflow;
        TextGenerator generator = new TextGenerator();
        generator.PopulateWithErrors("this is costum!", settings, gameObject);
        Debug.Log("I generated: " + generator.vertexCount + " verts!");

        Mesh mesh = new Mesh();
        mesh.name = "costumRenderer";
        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer  mr = GetComponent<MeshRenderer>();
        mf.mesh = mesh;
        mr.sharedMaterial = font.material;
        GetMesh(generator,mesh);
    }


    public void GetMesh(TextGenerator iGenerator, Mesh o_Mesh)
    {
        if (o_Mesh == null)
            return;

        int vertSize = iGenerator.vertexCount;
        Vector3[] tempVerts = new Vector3[vertSize];
        Color32[] tempColours = new Color32[vertSize];
        Vector2[] tempUvs = new Vector2[vertSize];
        IList<UIVertex> generatorVerts = iGenerator.verts;



        for (int i = 0; i < vertSize; ++i)
        {
            tempVerts[i] = generatorVerts[i].position / 20f;
            tempColours[i] = generatorVerts[i].color;
            tempUvs[i] = generatorVerts[i].uv0;
        }

        for (int i = 0; i < tempVerts.Length; i++)
        {
            tempVerts[i].x += (i / 4) * 0.3f;//字间距
        }
        o_Mesh.vertices = tempVerts;
        o_Mesh.colors32 = tempColours;
        o_Mesh.uv = tempUvs;

        int characterCount = vertSize / 4;
        int[] tempIndices = new int[characterCount * 6];
        for (int i = 0; i < characterCount; ++i)
        {
            int vertIndexStart = i * 4;
            int trianglesIndexStart = i * 6;
            tempIndices[trianglesIndexStart++] = vertIndexStart;
            tempIndices[trianglesIndexStart++] = vertIndexStart + 1;
            tempIndices[trianglesIndexStart++] = vertIndexStart + 2;
            tempIndices[trianglesIndexStart++] = vertIndexStart;
            tempIndices[trianglesIndexStart++] = vertIndexStart + 2;
            tempIndices[trianglesIndexStart] = vertIndexStart + 3;
        }
        o_Mesh.triangles = tempIndices;
        //TODO: setBounds manually
        o_Mesh.RecalculateBounds();
    }

}