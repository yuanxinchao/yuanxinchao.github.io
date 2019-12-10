// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/13TillingOffset" {
	Properties {
		_MainTex ("Main Tex", 2D) = "white" {} //一个内置全白纹理
	}
	SubShader {
		Pass{
			Tags { "LightMode"="ForwardBase" }
			
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma vertex vert

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma fragment frag

			#include "Lighting.cginc"
			sampler2D _MainTex;//主纹理
			float4 _MainTex_ST;//Tilling and offset



			struct a2v{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 color    : COLOR;
				float4 tangent : TANGENT;//xyz,方向， w，用1或-1 乘以求出的副切线方向
				float4 texcoord : TEXCOORD0;
			};

			struct v2f{
				float4 pos : SV_POSITION;
				float3 uv : TEXCOORD0;
				float4 color    : COLOR;
				float3 lightDir : TEXCOORD1;
				float3 viewDir : TEXCOORD2;
				
			};
			v2f vert(a2v v){
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);//模型空间到裁剪空间
				//v.texcoord 对应 UIVertex->uv0
				o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				o.color = v.color;
				return o;
			}
			fixed4 frag(v2f i) : SV_Target{
				//取纹理色
				fixed3 albedo = tex2D(_MainTex,i.uv).rgb;
				fixed3 ambient = albedo * i.color.rgb;
				return fixed4(ambient,1);
			}
			ENDCG
		}
	}
		FallBack "Specular"

}
