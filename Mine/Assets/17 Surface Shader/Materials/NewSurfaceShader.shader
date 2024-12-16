Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _MainTex("Texture",2D) = "white"{}
        OverlapTex("OverLap Texture",2D) = "gray"{}

        _ColorMultiple ("Color Multiplier", Range(0,1)) = 1
        _ClippingMultiple("Clipping Multiplier",integer) = 1
        _ClippingInclin("Clipping Inclin", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert
        sampler2D _MainTex;
        sampler2D OverlapTex;
        float _ColorMultiple;
        int _ClippingMultiple;
        float _ClippingInclin;

        struct Input
        {
            float2 uv_MainTex;
            float4 screenPos;

            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput output)
        {
            clip(frac(((IN.worldPos.y * _ClippingMultiple) + (IN.worldPos.x * _ClippingInclin)) - 0.5));
            output.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _ColorMultiple;
            float2 screenUV = (IN.screenPos.xy / IN.screenPos.w) * float2(10,5);

            float2 timeScale = float2(_SinTime.w, 0);

            output.Albedo *= tex2D(OverlapTex, screenUV + timeScale).rgb * 2;
        }


        ENDCG
    }
    FallBack "Diffuse"
}
