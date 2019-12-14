Shader "Unlit/CameraDark"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_XColor("XColor",Range(0,1)) = 1
		_YColor("YColor",Range(0,1)) = 1
		_ZColor("ZColor",Range(0,1)) = 1
		_AColor("AColor",Range(0,1)) = 1
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _XColor;
			float _YColor;
			float _ZColor;
			float _AColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				col = col * fixed4(_XColor,_YColor,_ZColor,_AColor);
                return col;
            }
            ENDCG
        }
    }
}
