Shader "Hidden/LerpGrayScaleImageEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_GrayscaleRatio("Grayscale ratio", Range(0,1)) = 0.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
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
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			float _GrayscaleRatio;
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				//col = 1 - col;
				col.rgb = lerp(col.rgb, dot(col.rgb, float3(0.3, 0.59, 0.11)), _GrayscaleRatio);
				return col;
			}
			ENDCG
		}
	}
}
