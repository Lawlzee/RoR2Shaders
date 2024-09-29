Shader "Hidden/Custom/Outline"
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
            float4 _Color;
            float _Thinness;
            float _DensityInverse;
            
            inline void GetOffsets3x3(float resolutionX, float resolutionY, out float2 result[9])
            {
                float offsetX = 1.0 / resolutionX;
                float offsetY = 1.0 / resolutionY;
                float2 offsets[9] = {
                    float2(-offsetX,  offsetY), // top-left
                    float2(0.0,    offsetY), // top-center
                    float2( offsetX,  offsetY), // top-right
                    float2(-offsetX,  0.0),   // center-left
                    float2(0.0,    0.0),   // center-center
                    float2( offsetX,  0.0),   // center-right
                    float2(-offsetX, -offsetY), // bottom-left
                    float2(0.0,   -offsetY), // bottom-center
                    float2(offsetX, -offsetY)  // bottom-right    
                };
                result = offsets;
            }
            
            inline float Grayscale(float3 frag)
            {
                float average = 0.2126 * frag.r + 0.7152 * frag.g + 0.0722 * frag.b;
                return average;
            }
            
            inline float3 Sobel(float3 textures[9])
            {
                float3x3 KERNEL_SOBELX = float3x3(
                    1.0, 0.0, -1.0,
                    2.0, 0.0, -2.0,
                    1.0, 0.0, -1.0
                );
                float3x3 KERNEL_SOBELY = float3x3(
                    1.0, 2.0, 1.0,
                    0.0, 0.0, 0.0,
                    -1.0, -2.0, -1.0
                );
            
                float mag = 0.0;
            
                float mGx = 0.0;
                float mGy = 0.0;
            
                for (int i = 0, k = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++, k++)
                    {
                        float Gx = KERNEL_SOBELX[k][j] * Grayscale(textures[j * 3 + k]);
                        float Gy = KERNEL_SOBELY[k][j] * Grayscale(textures[j * 3 + k]);
            
                        mGx += Gx;
                        mGy += Gy;
                    }
                }
            
                mag = pow(mGx * mGx + mGy * mGy, _DensityInverse);
                return float3(mag, mag, mag);
            
            }

            float4 frag (VaryingsDefault i) : SV_Target
            {
                float2 offsets[9];
                GetOffsets3x3(_Thinness * _ScreenParams.x, _Thinness * _ScreenParams.y, offsets);
                
                float3 textures[9];
                for (int j = 0; j < 9; j++)
                {
                    textures[j] = tex2D(_MainTex, i.texcoord + offsets[j]).rgb;
                }

                float4 FragColor = float4(Sobel(textures), 1);

                _Color = float4(float3(1, 1, 1) - _Color.rgb, 1.0);
                FragColor = float4(FragColor.rgb * _Color, 1.0);
                FragColor = float4(textures[4] - FragColor.xyz, 1);

                return FragColor;
            }
            ENDHLSL
        }
    }
}
