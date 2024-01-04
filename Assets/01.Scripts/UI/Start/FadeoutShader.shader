// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Unlit/FadeoutShader" {
 Properties {
  _Color ("Color", Color) = (0.5,0.5,0.5,0.5)
  [HideInInspector]_MainTex ("Albedo (RGB)", 2D) = "white" {}
  [HideInInspector]_Stencil("Stencil ID", Float) = 0
  _Alpha("전체알파", Range(0, 1)) = 1
  _MaxLeft("왼쪽 Fade", Range(-1, 0)) = 0
  _MaxRight("오른쪽 Fade", Range(-1, 0)) = 0
  _MaxUp("위쪽 Fade", Range(-1, 0)) = 0
  _MaxDown("아래쪽 Fade", Range(-1, 0)) = 0
  _HorizontalLineMul("가로 경계선 강도", int) = 20
  _VerticalLineMul("세로 경계선 강도", int) = 20
 }
 SubShader {
  Tags { "RenderType"="Transparent" "Queue" = "Transparent"}

  LOD 200
   Lighting Off
   ZWrite Off
   ZTest Off
   Blend SrcAlpha OneMinusSrcAlpha

  Stencil{
   Ref 1
   Comp equal
   Pass keep
  }
  
  CGPROGRAM
  // Physically based Standard lighting model, and enable shadows on all light types
  #pragma surface surf NoLighting alpha:fade

  // Use shader model 3.0 target, to get nicer looking lighting
  #pragma target 2.0

  sampler2D _MainTex;

  struct Input {
   float2 uv_MainTex;
   fixed4 screenPos;
  };

  fixed4 _Color;
  fixed _Alpha;
  fixed _MaxLeft; //왼쪽 마스크
  fixed _MaxRight; //오른쪽 마스크
  fixed _MaxUp; //위쪽 마스크
  fixed _MaxDown; //밑쪽 마스크
  fixed _HorizontalLineMul; //가로 강도
  fixed _VerticalLineMul; //세로 강도


  // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
  // See https://docs.unity3d.com/Manual/GPUIn... for more information about instancing.
  // #pragma instancing_options assumeuniformscaling
  UNITY_INSTANCING_BUFFER_START(Props)
   // put more per-instance properties here
  UNITY_INSTANCING_BUFFER_END(Props)

  void surf (Input IN, inout SurfaceOutput o) {

   fixed3 ScreenUV = IN.screenPos.xyz / IN.screenPos.a; //화면 렌더링

   fixed LeftMask = saturate((ScreenUV.x + _MaxLeft) * _HorizontalLineMul); //왼쪽마스크
   fixed RightMask = saturate((1 - ScreenUV.x + _MaxRight) * _HorizontalLineMul); //오른쪽 마스크
   fixed UpMask = saturate((1 - ScreenUV.y + _MaxUp) * _VerticalLineMul); //위쪽 마스크
   fixed DownMask = saturate((ScreenUV.y + _MaxDown) * _VerticalLineMul); //아래쪽 마스크

   
   fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
   o.Albedo = c.rgb;
   o.Alpha = c.a * _Alpha * LeftMask * RightMask * DownMask * UpMask;
  }

  fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
  {
   fixed4 c;
   c.rgb = s.Albedo;
   c.a = s.Alpha;
   return c;
  }


  ENDCG
 }
 FallBack "Diffuse"
}
