using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParticleSystem : MaskableGraphic
{
    private ParticleSystem _particleSystem;
    private UIVertex[] _quad = new UIVertex[4];
    private ParticleSystem.Particle[] _particles;
    private ParticleSystem.MainModule _mainModule;
    //使用序列帧的粒子
    private ParticleSystem.TextureSheetAnimationModule textureSheetAnimation;
    private int textureSheetAnimationFrames;//帧数
    private Vector2 textureSheetAnimationFrameSize;//每帧uv

    private Transform _referTransform;//参考的transform原点  world 及 custom可能为空
    private Texture _currentTexture;
    private Vector4 _imageUV = Vector4.zero;//贴图uv


    protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif
        Initialize();

        //不需要响应事件
        raycastTarget = false;
    }
    public override Texture mainTexture
    {
        get
        {
            return _currentTexture;
        }
    }
    private bool Initialize()
    {
        if (_particleSystem == null)
        {
            _particleSystem = transform.GetComponent<ParticleSystem>();
            _particleSystem = GetComponent<ParticleSystem>();
            if (_particleSystem == null)
            {
                return false;
            }

            if (material.name != "Copy")
            {
                var render = _particleSystem.GetComponent<ParticleSystemRenderer>(); 
                var modelMat = JDResources.Load<Material>("UIpar-Material");
                var mat = new Material(modelMat) {name = "Copy"};
                material = mat;
                material.mainTexture = render.sharedMaterial.mainTexture;
                render.enabled = false;
                render.material = null;
                render.trailMaterial = null;
            }
            _currentTexture = material.mainTexture;
        }
            
        if (_particles == null)
            _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
        _mainModule = _particleSystem.main;
        _imageUV = new Vector4(0, 0, 1, 1);


        if (_mainModule.simulationSpace == ParticleSystemSimulationSpace.Local
            || _mainModule.simulationSpace == ParticleSystemSimulationSpace.World)
        {
            _referTransform = _particleSystem.transform;
        }
        else if (_mainModule.simulationSpace == ParticleSystemSimulationSpace.Custom)
        {
            _referTransform = _particleSystem.main.customSimulationSpace;

            if (_referTransform == null)
                throw new Exception("customSimulationSpace transform should not null");
        }
        // prepare texture sheet animation
        textureSheetAnimation = _particleSystem.textureSheetAnimation;
        textureSheetAnimationFrames = 0;
        textureSheetAnimationFrameSize = Vector2.zero;
        if (textureSheetAnimation.enabled && textureSheetAnimation.mode == ParticleSystemAnimationMode.Grid)
        {
            textureSheetAnimationFrames = textureSheetAnimation.numTilesX * textureSheetAnimation.numTilesY;
            textureSheetAnimationFrameSize = new Vector2(1f / textureSheetAnimation.numTilesX, 1f / textureSheetAnimation.numTilesY);
        }

        return true;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif
        // prepare vertices
        vh.Clear();

        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        Vector2 temp = Vector2.zero;
        Vector2 parPos = Vector2.zero;//储存粒子世界空间下位置
        Vector2 corner1 = Vector2.zero;//左下角
        Vector2 corner2 = Vector2.zero;//右上角
        int count = _particleSystem.GetParticles(_particles);
        for (int i = 0; i < count; i++)
        {
            var particle = _particles[i];

            //获取粒子颜色
            Color32 particleColor = particle.GetCurrentColor(_particleSystem);


            //获取粒子位置
            var particlePos = particle.position;

            if (_mainModule.simulationSpace == ParticleSystemSimulationSpace.World)
            {
                parPos = particlePos;
            }
            else
            {
                Vector3 scale = GetParticlePosChangeScale();
                var localPos = new Vector3(particlePos.x * scale.x, particlePos.y * scale.y, particlePos.z * scale.z);
                parPos = _referTransform.position + localPos;
            }
            //粒子旋转
            float rotation = -particle.rotation * Mathf.Deg2Rad;
            float rotation90 = rotation + Mathf.PI / 2;

            Vector3 size = Vector3.zero;
            //粒子大小
            if (_particleSystem.main.startSize3D)
            {
                size = particle.GetCurrentSize3D(_particleSystem) * 0.5f;
            }
            else
            {
                float nowSize = particle.GetCurrentSize(_particleSystem) * 0.5f;
                size.x = nowSize;
                size.y = nowSize;
                size.z = nowSize;
            }
            

            Vector3 sizeVector = Vector3.zero;
            //自己以及父级的缩放会影响粒子大小尺度
            var trans = _particleSystem.transform;
            if (_mainModule.scalingMode == ParticleSystemScalingMode.Local)
            {
                sizeVector.x = (size.x * trans.localScale.x)/ trans.lossyScale.x;
                sizeVector.y = (size.y * trans.localScale.y) / trans.lossyScale.y;
                sizeVector.z = (size.z * trans.localScale.z)/ trans.lossyScale.z;
            }
            else if (_mainModule.scalingMode == ParticleSystemScalingMode.Hierarchy)
                sizeVector = size * 2;
            else if (_mainModule.scalingMode == ParticleSystemScalingMode.Shape)
            {
                sizeVector.x = size.x / trans.lossyScale.x;
                sizeVector.y = size.y / trans.lossyScale.y;
                sizeVector.z = size.z / trans.lossyScale.z;
            }

            // apply texture sheet animation
            //根据粒子生存时长占总时长的比例取uv
            Vector4 particleUv = _imageUV;
            if (textureSheetAnimation.enabled && textureSheetAnimation.mode == ParticleSystemAnimationMode.Grid)
            {
                float frameProgress = 1 - (particle.remainingLifetime / particle.startLifetime);

                if (textureSheetAnimation.frameOverTime.curveMin != null)
                {
                    frameProgress = textureSheetAnimation.frameOverTime.curveMin.Evaluate(1 - (particle.remainingLifetime / particle.startLifetime));
                }
                else if (textureSheetAnimation.frameOverTime.curve != null)
                {
                    frameProgress = textureSheetAnimation.frameOverTime.curve.Evaluate(1 - (particle.remainingLifetime / particle.startLifetime));
                }
                else if (textureSheetAnimation.frameOverTime.constant > 0)
                {
                    frameProgress = textureSheetAnimation.frameOverTime.constant - (particle.remainingLifetime / particle.startLifetime);
                }

                frameProgress = Mathf.Repeat(frameProgress * textureSheetAnimation.cycleCount, 1);
                int frame = 0;

                switch (textureSheetAnimation.animation)
                {
                    case ParticleSystemAnimationType.WholeSheet:
                        frame = Mathf.FloorToInt(frameProgress * textureSheetAnimationFrames);
                        break;

                    case ParticleSystemAnimationType.SingleRow:
                        frame = Mathf.FloorToInt(frameProgress * textureSheetAnimation.numTilesX);

                        int row = textureSheetAnimation.rowIndex;
                        frame += row * textureSheetAnimation.numTilesX;
                        break;

                }

                frame %= textureSheetAnimationFrames;

                particleUv.x = (frame % textureSheetAnimation.numTilesX) * textureSheetAnimationFrameSize.x;
                particleUv.y = Mathf.FloorToInt(frame / textureSheetAnimation.numTilesX) * textureSheetAnimationFrameSize.y;
                particleUv.z = particleUv.x + textureSheetAnimationFrameSize.x;
                particleUv.w = particleUv.y + textureSheetAnimationFrameSize.y;
            }


            temp.x = particleUv.x;
            temp.y = particleUv.y;
            _quad[0] = UIVertex.simpleVert;
            _quad[0].color = particleColor;
            _quad[0].uv0 = temp;


            temp.x = particleUv.x;
            temp.y = particleUv.w;
            _quad[1] = UIVertex.simpleVert;
            _quad[1].color = particleColor;
            _quad[1].uv0 = temp;


            temp.x = particleUv.z;
            temp.y = particleUv.w;
            _quad[2] = UIVertex.simpleVert;
            _quad[2].color = particleColor;
            _quad[2].uv0 = temp;

            temp.x = particleUv.z;
            temp.y = particleUv.y;
            _quad[3] = UIVertex.simpleVert;
            _quad[3].color = particleColor;
            _quad[3].uv0 = temp;


            var pos = transform.InverseTransformPoint(parPos);
            if (Math.Abs(rotation) < 0.1)
            {
                // no rotation
                corner1.x = pos.x - sizeVector.x;
                corner1.y = pos.y - sizeVector.y;
                corner2.x = pos.x + sizeVector.x;
                corner2.y = pos.y + sizeVector.y;

                temp.x = corner1.x;
                temp.y = corner1.y;
                _quad[0].position = temp;
                temp.x = corner1.x;
                temp.y = corner2.y;
                _quad[1].position = temp;
                temp.x = corner2.x;
                temp.y = corner2.y;
                _quad[2].position = temp;
                temp.x = corner2.x;
                temp.y = corner1.y;
                _quad[3].position = temp;
            }
            else
            {
                // apply rotation
                Vector2 right = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation)) * sizeVector.x;
                Vector2 up = new Vector2(Mathf.Cos(rotation90), Mathf.Sin(rotation90)) * sizeVector.y;

                _quad[0].position = (Vector2)pos - right - up;
                _quad[1].position = (Vector2)pos - right + up;
                _quad[2].position = (Vector2)pos + right + up;
                _quad[3].position = (Vector2)pos + right - up;
            }
            vh.AddUIVertexQuad(_quad);
        }


    }
    void Update()
    {
        if (Application.isPlaying)
        {
            _particleSystem.Simulate(Time.unscaledDeltaTime, false, false, true);
            SetAllDirty();
        }
    }
    public Vector3 GetParticlePosChangeScale()
    {
        Vector3 scale = Vector3.zero;//缩放影响位置
        if (_mainModule.scalingMode == ParticleSystemScalingMode.Local)
            scale = _referTransform.localScale;
        else if (_mainModule.scalingMode == ParticleSystemScalingMode.Hierarchy)
            scale = _referTransform.lossyScale;
        else if (_mainModule.scalingMode == ParticleSystemScalingMode.Shape)
        {
            scale = Vector3.one;
        }

        return scale;
    }
}
