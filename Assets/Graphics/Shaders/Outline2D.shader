// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Sprites/Diffuse"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_OutlineColor("Outline Color", Color) = (1,1,1,1)
		_Outline("Outline Width", Float) = 0
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
	[PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		CGPROGRAM
#pragma surface surf Lambert vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
#include "UnitySprites.cginc"

		struct Input
	{
		float2 uv_MainTex;
		fixed4 color;
	};

	void vert(inout appdata_full v, out Input o)
	{
		v.vertex.xy *= _Flip.xy;

#if defined(PIXELSNAP_ON)
		v.vertex = UnityPixelSnap(v.vertex);
#endif

		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color = v.color * _Color * _RendererColor;
	}

	fixed4 _OutlineColor;
	fixed _Outline;
	float4 _MainTex_TexelSize;

	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 c = SampleSpriteTexture(IN.uv_MainTex) * IN.color;

		// If outline is enabled and there is a pixel, try to draw an outline.
		if (_Outline > 0 && c.a != 0) {
			// Get the neighbouring four pixels.
			fixed4 pixelUp = tex2D(_MainTex, IN.uv_MainTex + fixed2(0, _MainTex_TexelSize.y));
			fixed4 pixelDown = tex2D(_MainTex, IN.uv_MainTex - fixed2(0, _MainTex_TexelSize.y));
			fixed4 pixelRight = tex2D(_MainTex, IN.uv_MainTex + fixed2(_MainTex_TexelSize.x, 0));
			fixed4 pixelLeft = tex2D(_MainTex, IN.uv_MainTex - fixed2(_MainTex_TexelSize.x, 0));

			// If one of the neighbouring pixels is invisible, we render an outline.
			if (pixelUp.a * pixelDown.a * pixelRight.a * pixelLeft.a == 0) {
				c.rgba = fixed4(1, 1, 1, 0.3) * _OutlineColor;
			}
		}


		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}
