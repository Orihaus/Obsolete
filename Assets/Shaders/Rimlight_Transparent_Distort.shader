Shader "Rimlight_Transparent_Distort" 
{
    
Properties 
{
	_Color ( "Main Color", Color ) = ( 1, 1, 1, 1 )
	_RimColor ("Rim Color", Color) = ( 0.89, 0.945, 1.0, 0.0 )
	_RimPower ("Rim Power", Range(0.5,32.0)) = 3.0
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.00, 1)) = 0.078125
	_Alpha ("Alpha", Range (1.0, 0.00)) = 1.0
	_AlphaOffset ("Alpha Offset", Range (0.0313725490196078, 0.00)) = 0.01
	_Ambient ("Ambient", Range (1.0, 0.00)) = 0.01
	_BumpAmt  ("Distortion", range (0,128)) = 10
	_BumpMap ("Normalmap", 2D) = "bump" {}
}

Category {
	Tags { "Queue" = "Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }


	SubShader {

		// This pass grabs the screen behind the object into a texture.
		// We can access the result in the next pass as _GrabTexture
		GrabPass {							
			Name "BASE"
			Tags { "LightMode" = "Always" }
 		}
 		
 		// Main pass: Take the texture grabbed above and use the bumpmap to perturb it
 		// on to the screen
		Pass {
			Name "BASE"
			Tags { "LightMode" = "Always" }

	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#pragma fragmentoption ARB_precision_hint_fastest
	#include "UnityCG.cginc"
	
	struct appdata_t {
		float4 vertex : POSITION;
		float2 texcoord: TEXCOORD0;
	};
	
	struct v2f {
		float4 vertex : POSITION;
		float4 uvgrab : TEXCOORD0;
		float2 uvbump : TEXCOORD1;
	};
	
	float _BumpAmt;
	float4 _BumpMap_ST;
	
	v2f vert (appdata_t v)
	{
		v2f o;
		o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		#if UNITY_UV_STARTS_AT_TOP
		float scale = -1.0;
		#else
		float scale = 1.0;
		#endif
		o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
		o.uvgrab.zw = o.vertex.zw;
		o.uvbump = TRANSFORM_TEX( v.texcoord, _BumpMap );
		return o;
	}
	
	sampler2D _GrabTexture;
	float4 _GrabTexture_TexelSize;
	sampler2D _BumpMap;
	
	float4 _RimColor;
	float _RimPower;
	float _Ambient;
	float4 _Color;
	float _Shininess;
	float _Alpha;
	float _AlphaOffset;
	
	half4 frag( v2f i ) : COLOR
	{
		float rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
	
		// calculate perturbed coordinates
		half2 bump = UnpackNormal(tex2D( _BumpMap, i.uvbump )).rg; // we could optimize this by just reading the x & y without reconstructing the Z
		float2 offset = bump * _BumpAmt * _GrabTexture_TexelSize.xy;
		i.uvgrab.xy = offset * i.uvgrab.z + i.uvgrab.xy;
		
		half4 col = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
		return col;
}
ENDCG
		}
	}

	// ------------------------------------------------------------------
	// Fallback for older cards and Unity non-Pro
	
	SubShader {
		Blend DstColor Zero
		Pass {
			Name "BASE"
			SetTexture [_MainTex] {	combine texture }
		}
	}
}

}
