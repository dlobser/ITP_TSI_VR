Shader "Custom/URPCircularGradient"
{
    Properties
    {
        _GradientSize("Gradient Size", Range(0.0, 1.0)) = 0.5
        _Brightness("Brightness", Range(0.0, 2.0)) = 1.0
        _Power("Power", Float) = 2.0
        _InnerColor("Inner Color", Color) = (1,1,1,1)
        _OuterColor("Outer Color", Color) = (0,0,0,1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float4 positionCS : SV_POSITION;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            float _GradientSize;
            float _Brightness;
            float _Power;
            float4 _InnerColor;
            float4 _OuterColor;

            float4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uv - float2(0.5, 0.5); // Center the gradient
                float dist = length(uv) * 2; // Normalize distance
                float gradient = smoothstep(_GradientSize, _GradientSize + _Brightness, dist);
                gradient = pow(gradient, _Power);

                float4 color = lerp(_InnerColor, _OuterColor, gradient);
                return color;
            }
            ENDHLSL
        }
    }
}
