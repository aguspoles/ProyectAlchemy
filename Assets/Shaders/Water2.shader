Shader "Unlit/Water2"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		waterTex ("WaterTexture", 2D) = "white"{}
		speed("Water Speed", float) = 1
		textureSize ("Texture Size", float) = 1
		textureDept ("Texture Dept", float) = 1
		textureTile("Texture tile", float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			#pragma target 5.0
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D waterTex;
			float4 _MainTex_ST;
			float speed;
			float textureSize;
			float textureDept;
			float textureTile;
			
			v2f vert (appdata v)
			{
				v.uv *= textureSize;
				v.uv.x += _Time.x * speed;
				float4 color = tex2Dlod(waterTex, float4(v.uv,0,0));
				v.vertex.y += color.r*textureDept;


				v2f o;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.worldPos.xz * textureTile);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
