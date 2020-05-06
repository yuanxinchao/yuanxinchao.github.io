using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
public class UIPar : MaskableGraphic
{
    private ParticleSystem _particleSystem;
    private CanvasRenderer _canvasRenderer;
    private ParticleSystemRenderer _parRenderer;
    private ParticleSystem.MainModule _main;
    private Mesh _mesh;
    static readonly List<Vector3> s_Vertices = new List<Vector3>();
    private Texture _currentTexture;
    protected override void Awake()
    {
        base.Awake();
        //获取组件
        _particleSystem = transform.GetComponent<ParticleSystem>();
        _canvasRenderer = transform.GetComponent<CanvasRenderer>();
        Assert.IsNotNull(_particleSystem);
        Assert.IsNotNull(_canvasRenderer);
        _parRenderer = _particleSystem.GetComponent<ParticleSystemRenderer>();
        _main = _particleSystem.main;

        //新建一个材质使用裁剪shader 纹理使用粒子的纹理
        if (material.name != "Copy")
        {
            var render = _particleSystem.GetComponent<ParticleSystemRenderer>(); 
            var modelMat =  Resources.Load<Material>("UIpar-Material");
            var mat = new Material(modelMat) {name = "Copy"};
            material = mat;
            material.mainTexture = render.sharedMaterial.mainTexture;
            render.enabled = false;
        }
        _mesh = new Mesh();
        _mesh.MarkDynamic();
        _currentTexture = material.mainTexture;
    }

    protected override void OnEnable()
    {
        Canvas.willRenderCanvases += UpdateMeshes;
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        Canvas.willRenderCanvases -= UpdateMeshes;
        base.OnDisable();
    }

    //空 不再走ugui生成mesh
    protected override void UpdateGeometry()
    {

    }
    void UpdateMeshes()
    {
        _mesh.Clear();
        if(!canvas || !canvas.worldCamera)
            return;
        _parRenderer.BakeMesh(_mesh,canvas.worldCamera,true);
        _mesh.GetVertices(s_Vertices);
        for (int j = 0; j < s_Vertices.Count; j++)
        {
            if (_main.simulationSpace == ParticleSystemSimulationSpace.World)
            {
                
                s_Vertices[j] = transform.worldToLocalMatrix.MultiplyPoint(s_Vertices[j]);
            }
            //local && custom
            else if(_main.simulationSpace == ParticleSystemSimulationSpace.Local)
            {
                s_Vertices[j] = transform.worldToLocalMatrix.MultiplyPoint(s_Vertices[j] + transform.position);
            }
            else if(_main.simulationSpace == ParticleSystemSimulationSpace.Custom)
            {
                var refer = _main.customSimulationSpace;
                if (refer == null)
                    refer = transform;
                s_Vertices[j] =transform.InverseTransformPoint(s_Vertices[j] + refer.position);
            }
        }
        _mesh.SetVertices(s_Vertices);
        _mesh.RecalculateBounds();
        s_Vertices.Clear();
        _canvasRenderer.SetMesh(_mesh);
        canvasRenderer.SetTexture(mainTexture);
    }

    //ugui
    public override Texture mainTexture
    {
        get
        {
            return _currentTexture;
        }
    }

}