//https://github.com/GarrettGunnell/Post-Processing/blob/main/Assets/Sharpness/Sharpness.shader
Shader "Hidden/Custom/Sharpeness"
{
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
            #pragma vertex VertDefault
            #pragma fragment frag

            sampler2D _MainTex;
            
            float _Amount;
            float _Thickness;

            float3 Sample(float2 uv, float deltaX, float deltaY, float2 pixelSize) {
                return saturate(tex2D(_MainTex, uv + float2(deltaX, deltaY) * pixelSize).rgb);
            }

            float3 GetMin(float3 x, float3 y, float3 z) {
                return min(x, min(y, z));
            }

            float3 GetMax(float3 x, float3 y, float3 z) {
                return max(x, max(y, z));
            }

            float4 frag (VaryingsDefault i) : SV_Target
            {
                float sharpness = -(1.0f / lerp(10.0f, 4.1f, _Amount));

                float pixelSize = float2(_Thickness / _ScreenParams.x, _Thickness / _ScreenParams.y);

                float3 a = Sample(i.texcoord, -1, -1, pixelSize);
                float3 b = Sample(i.texcoord,  0, -1, pixelSize);
                float3 c = Sample(i.texcoord,  1, -1, pixelSize);
                float3 d = Sample(i.texcoord, -1,  0, pixelSize);
                float3 e = Sample(i.texcoord,  0,  0, pixelSize);
                float3 f = Sample(i.texcoord,  1,  0, pixelSize);
                float3 g = Sample(i.texcoord, -1,  1, pixelSize);
                float3 h = Sample(i.texcoord,  0,  1, pixelSize);
                float3 j = Sample(i.texcoord,  1,  1, pixelSize);

                float3 minRGB = GetMin(GetMin(d, e, f), b, h);
                float3 minRGB2 = GetMin(GetMin(minRGB, a, c), g, j);

                minRGB += minRGB2;

                float3 maxRGB = GetMax(GetMax(d, e, f), b, h);
                float3 maxRGB2 = GetMax(GetMax(maxRGB, a, c), g, j);

                maxRGB += maxRGB2;

                float3 rcpM = 1.0f / maxRGB;
                float3 amp = saturate(min(minRGB, 2.0f - maxRGB) * rcpM);
                amp = sqrt(amp);

                float3 w = amp * sharpness;
                float3 rcpW = 1.0f / (1.0f + 4.0f * w);

                float3 output = saturate((b * w + d * w + f * w + h * w + e) * rcpW);

                return float4(output, 1.0f);
            }
            ENDHLSL
        }
    }
}
