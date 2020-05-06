// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "多写写效果/testNum"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Back ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct appdata members color)
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geo
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = v.vertex;
                o.uv = v.uv;
                return o;
            }

            float GetNum(float2 normalizedV,int x)
            {
                float param = x==0?480599.0:x==1?139810.0:x==2?476951.0:x==3?476999.0:x==4?350020.0:x==5?464711.0:x==6?464727.0:x==7?476228.0:x==8?481111.0:x==9?481095.0:0.0;

                float result = (param / pow(2.0, floor(frac (normalizedV.x) * 4.0) + (floor(normalizedV.y* 5.0) * 4.0)));
                float result2 = floor(result - 2.0 * floor(result/2.0));
                return result2;
            }
            //显示数字
            float GetPosColor(float2 vertex,int fValue)
            {
                //vertex x[0,750] y[0,1334*area]
                float2 font = float2(_ScreenParams.x, _ScreenParams.y);
                float area = 1/6.0;
                //经过视口变换区域要/2
                float area2 = area / 2;
                vertex = vertex/font;       //vertex 理应是 x[0,1] y[0,1/12]  但实际是x[0,1] y[1,11/12] 
                vertex.y = 1 - (vertex.y * (1/area2) - (1-area2)*(1/area2)); //vertex x[0,1] y[0,1]
                vertex.x = vertex.x * 9;  //vertex x[0,8] y[0,1]  一行显示9个

                
                //x位返回x-1，如4->0   43 -> 1   349->2  4562->3  
                float fBiggestIndex = max(floor(log2(abs(fValue)) / log2(10.0)), 0.0);
                // int grid = ceil(sqrt((fBiggestIndex + 1)/9));//位数太多 拓展
                // vertex.x = vertex.x * grid;
                // vertex.y = vertex.y * grid;
                //根据vertex决定显示第几位
                int index =floor(vertex.x);
                index = fBiggestIndex - index;//索引个位是0 十位是1 向上递推
                if(index < 0)
                    return 0;
                //算出index位的数是几
                uint fDigitValue = fValue / uint(round(pow(10, index)));
                uint indexV = fDigitValue - 10 * (fDigitValue/10);
                float2 normalizedV = float2(frac(vertex.x), frac(vertex.y));

                return GetNum(normalizedV,indexV);
            }
            [maxvertexcount(9)]
            void geo( triangle v2f patch[3], inout TriangleStream<v2f> stream, uint id:SV_PRIMITIVEID )
            {
                float area = 1/6.0; //显示区域占 NDC的1/12
                v2f o;
                for (uint i = 0; i < 3; i++)
                {
                    //坐标转换到裁剪空间
                    o.vertex = UnityObjectToClipPos(patch[i].vertex);
                    o.uv = patch[i].uv;
                    stream.Append(o);
                }
                //重新开始一批顶点 即不与之前append的顶点组成图元
                stream.RestartStrip();
             
                if (id == 0)  // determine quad
                {
                    //新建新建四个点 组成两个三角形 顺时针为正面
                    //uv偏移 用来判断数字显示
                    float2 diff =  float2(10000.0,10000.0);
                    //添加4个顶点 坐标是裁剪空间
                    // 左下
                    o.uv = float2(0,0) + diff;  // UV offset
                    //裁剪空间下的坐标
                    o.vertex = float4(-1,-1,1,1);
                    stream.Append(o);
                    //左上
                    o.uv = float2(0,1) + diff;  
                    o.vertex = float4(-1,-1 + area,1,1);
                    stream.Append(o);
                    //右下
                    o.uv = float2(1,0) + diff;
                    o.vertex = float4(1,-1,1,1);
                    stream.Append(o);
                    //右上
                    o.uv = float2(1,1) + diff;
                    o.vertex = float4(1,-1 + area,1,1);
                    stream.Append(o);
                }
                stream.RestartStrip();
            }
            //接上：vert 的输出在进入 frag之前 会
            //剪裁坐标: 将 范围 -w,w 外的裁减掉
            //透视除法: 将坐标(x,y,z,w) 同时处以w,这时候摄像头看到的区域坐标点都会在归一化在 -1,1 的立方体内
            //视口变换: 会将 -1,1 的坐标转换成屏幕坐标(也就是分辨率) 即 原来的x->(x+1)/2*pixelW  y->(y+1)/2*pixelH
            //视口变换后： 图元对应关系为上述公式，
            //感觉上是移动加拉伸了，但是最后有个处理不明白，就是原来NDC里向上为正方向，经过视口变换后，屏幕向下为正方向了
            //难道视口变换后 为了表面显示上不翻转  先将y = _ScreenParams.y - y 然后又将y坐标轴翻转了？

            //光栅化: 通过插值一到多？顶点面片插值变成到一个个小片元
            //综上：frag输入的顶点坐标并不是我们在顶点着色器赋予的，而是经过视口变换过的坐标
            sampler2D _MainTex;
            fixed4 frag (v2f i) : SV_Target
            {
                if (i.uv.x > 9000.0)  // determine quad
                {
                   return float4(GetPosColor(i.vertex.xy,343).xxx,1);
                }
                return float4(1,1,1,1.0);//返回颜色
            }
            ENDCG
        }
    }
}
