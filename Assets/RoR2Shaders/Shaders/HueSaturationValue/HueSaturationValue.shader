Shader "Hidden/Custom/HueSaturationValue"
{
  SubShader
  {
      Cull Off ZWrite Off ZTest Always
      Pass
      {
          HLSLPROGRAM
              #pragma vertex VertDefault
              #pragma fragment Frag

              // StdLib.hlsl holds pre-configured vertex shaders (VertDefault), varying structs (VaryingsDefault), and most of the data you need to write common effects.
              #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
              TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

              float _HueShift;
              float _MinSaturation;
              float _MaxSaturation;
              float _MinValue;
              float _MaxValue;

              float3 RGBToHSV(float3 c)
              {
                  float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                  float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
                  float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
                  float d = q.x - min( q.w, q.y );
                  float e = 1.0e-10;
                  return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
              }
              
              float3 HSVToRGB(float3 c)
              {
                  float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
                  float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
                  return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
              }
              
              float4 Frag(VaryingsDefault i) : SV_Target
              {
                  float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);

                  float3 hsv = RGBToHSV(color.rgb);
                  hsv.x += _HueShift;
                  hsv.y = lerp(_MinSaturation, _MaxSaturation, hsv.y);
                  hsv.z = lerp(_MinValue, _MaxValue, hsv.z);

                  float3 output = HSVToRGB(hsv);
              
                  return float4(output, 1);
              }
          ENDHLSL
      }
  }
}