Shader "Rimlight_Transparent_Base" 
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
}
    
SubShader 
{
	Tags { "Queue" = "Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

	CGPROGRAM
       
	#pragma surface surf SimpleSpecular alpha noambient nolightmap
   
	float _Shininess;
	float _Alpha;
	float _AlphaOffset;

	float4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
	{
		float3 h = normalize (lightDir + viewDir);
		float diff = max (0, dot (s.Normal, lightDir));
		float nh = max (0, dot (s.Normal, h));
		float spec = pow (nh, 48.0);

		float4 c;
		c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec * _Shininess * _SpecColor) * atten;
        c.a = s.Alpha;

		return c;

	}
	
	struct Input 
	{
		float3 viewDir;

	};
      
	float4 _RimColor;
	float _RimPower;
	float _Ambient;
	float4 _Color;
	
	void surf (Input IN, inout SurfaceOutput o) 
	{
		o.Albedo = _Color.rgb;
		float rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
		o.Emission = _Ambient;// + _RimColor.rgb * pow (rim, _RimPower);
		o.Alpha = _Alpha * pow (rim, _RimPower) + _AlphaOffset;
	}

	ENDCG

} 
    

Fallback "Diffuse"
}