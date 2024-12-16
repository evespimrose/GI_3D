Shader "Custom/OutLine2"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline Width", Range(0.001, 0.1)) = 0.02
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }
        LOD 100

        Pass
        {
            Name "OutlinePass"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;

            v2f vert(appdata v)
            {
                v2f o;
                // Transform normal to world space
                float3 normalWS = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));

                // Offset vertex along normal to create outline
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz + normalWS * _OutlineWidth;

                o.pos = UnityObjectToClipPos(float4(worldPos, 1.0));
                o.normalWS = normalWS;
                o.uv = v.uv;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Outline color with transparency
                return _OutlineColor;
            }
            ENDCG
        }

        Pass
        {
            Name "BasePass"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment fragBase
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 fragBase(v2f i) : SV_Target
            {
                // Sample main texture
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
