Shader "Rimlight_Transparent_Texture" 
{
	Properties 
	{ 
		_Color ( "Main Color", Color ) = ( 1, 1, 1, 1 )
		_MainMap ("Main Texture", 2D) = "main" {}
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
		_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125 
		_Alpha ("Alpha", Range (1.0, 0.01)) = 1.0
		_AlphaOffset ("Alpha Offset", Range (1.0, 0.01)) = 0.1
	}
    
SubShader 
{

	Tags { "Queue" = "Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

	CGPROGRAM
      
       
	#pragma surface surf SimpleSpecular alpha
   
	float _Shininess;
	float _Alpha;
	float _AlphaOffset;
   
      
	half4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
	{
          
		half3 h = normalize (lightDir + viewDir);
		half diff = max (0, dot (s.Normal, lightDir));
		float nh = max (0, dot (s.Normal, h));
		float spec = pow (nh, 48.0);

		half4 c;
		c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec * _Shininess * _SpecColor) * atten;
        c.a = s.Alpha;

		return c;

	}
	struct Input 
	{

		float2 uv_MainMap;

		float2 uv_BumpMap;

		float3 viewDir;

	};
      

 	sampler2D _BumpMap;
 	sampler2D _MainMap;

	float4 _RimColor;

	float _RimPower;

	float4 _Color;
	void surf (Input IN, inout SurfaceOutput o) 
	{  
		float texAlpha = tex2D (_MainMap, IN.uv_MainMap).a;
		o.Albedo = _Color.rgb;
		o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
		half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
		o.Emission = _RimColor.rgb * pow (rim, _RimPower) * texAlpha;
		o.Alpha = _Alpha * pow (rim, _RimPower) + _AlphaOffset * texAlpha;
	}
	ENDCG
} 
    
Fallback "Diffuse"
}