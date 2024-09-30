Shader "Hidden/Custom/ColorBanding"
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
            int _Bins;
            
            float4 frag (VaryingsDefault i) : SV_Target
            {
                return float4(floor(tex2D(_MainTex, i.texcoord).xyz * _Bins) / _Bins, 1);
            }
            ENDHLSL
        }
    }
}
