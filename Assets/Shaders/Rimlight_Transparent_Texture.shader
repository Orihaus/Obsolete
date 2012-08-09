Shader "Rimlight_Transparent_Texture" 
{
	Properties 
	{ 
		_MainMap ("Main Texture", 2D) = "main" {}
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower ("Rim Power", Range(0.5,32.0)) = 3.0
		_RimPowerSecondary ("Rim Power - Secondary", Range(0.5,32.0)) = 3.0
		_RimLevel ("Rim Level", Range(0.0,1.0)) = 0.0
		_RimLevelSecondary ("Rim Level - Secondary", Range(0.0,1.0)) = 0.0
		_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125 
		_SpecPower ("Spec Power", Range (0.00, 64.0)) = 48.0
		_Alpha ("Alpha", Range (1.0, 0.01)) = 1.0
		_AlphaOffset ("Alpha Offset", Range (4.0, 0.01)) = 0.1
		_EmissionOffset ("Emission Offset", Range (4.0, 0.01)) = 0.1
	}
		
	Category 
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha One
		AlphaTest Greater .01
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
		BindChannels 
		{
			Bind "Color", color
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
		}
		
	SubShader 
	{
		Pass 
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile_particles

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			float _Shininess;
			float _Alpha;
			float _AlphaOffset;
			float _EmissionOffset;
			float _SpecPower;
			float4 _SpecColor;
			
		 	sampler2D _MainMap;
		
			float4 _RimColor;
			float _RimPower;
			float _RimLevel;
			float _RimPowerSecondary;
			float _RimLevelSecondary;
			
			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};
 
			struct v2f 
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				float3 normal : TEXCOORD2;
				float3 lightDir : TEXCOORD3;
			};
			
			float4 _MainMap_ST;
			
			v2f vert( appdata v )
			{
				v2f o;
				o.vertex = mul( UNITY_MATRIX_MVP, v.vertex );
				o.texcoord = TRANSFORM_TEX( v.texcoord, _MainMap );
				o.viewDir = ObjSpaceViewDir( v.vertex );
				o.normal = v.normal;
				o.lightDir = ObjSpaceLightDir ( v.vertex );
				return o;
			}
			
			half4 _LightColor0;

			fixed4 frag( v2f i ) : COLOR
			{
				float4 color = float4( 1.0 );
				float inAlpha = tex2D( _MainMap, i.texcoord ).a * _Alpha;
				
				half3 h = normalize( i.lightDir + i.viewDir );
				float spec = max( 0, dot( i.normal, h ) );
				
				float rim = 1.0 - saturate( dot( normalize( i.viewDir ), i.normal ) );
				float rimpower = pow( rim, _RimPower ) * _RimLevel;
				rimpower += pow( rim, _RimPowerSecondary ) * _RimLevelSecondary;
				
				color.xyz = i.lightDir;// * _SpecColor * pow( spec, _SpecPower ) * _Shininess;
				//color.xyz += ( _RimColor.rgb * _EmissionOffset ) * rimpower * inAlpha;
				color.a = 1.0; //( rimpower * _AlphaOffset ) * inAlpha;
			
				return color;
			}
		      
			ENDCG
		}
		}
} 
    
Fallback "Diffuse"
}