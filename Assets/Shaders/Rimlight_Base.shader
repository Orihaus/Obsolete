Shader "Rimlight_Base" 
{
    
Properties 
{
	_Color ( "Main Color", Color ) = ( 1, 1, 1, 1 )
	_RimColor ("Rim Color", Color) = ( 0.89, 0.945, 1.0, 0.0 )
	_RimPower ("Rim Power", Range(0.5,32.0)) = 3.0
	_RimOffset ("Rim Offset", Range(0.0,0.1)) = 0.0
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.00, 1)) = 0.078125
}
    
SubShader 
{

	Tags { "RenderType"="Opaque" }

	CGPROGRAM
       
	#pragma surface surf SimpleSpecular
   
	float _Shininess;

	float4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
	{
          
		float3 h = normalize (lightDir + viewDir);
		float diff = max (0, dot (s.Normal, lightDir));
		float nh = max (0, dot (s.Normal, h));
		float spec = pow (nh, 48.0);

		float4 c;
		c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec * _Shininess * _SpecColor) * atten;
        c.a = 1.0;

		return c;

	}
	struct Input 
	{
		float3 viewDir;

	};
      

	float4 _RimColor;
	float _RimPower;
	float _RimOffset;
	
	float4 _Color;
	void surf (Input IN, inout SurfaceOutput o) 
	{
		o.Albedo = _Color.rgb;
		float rim = ( 1.0 - _RimOffset ) - saturate( dot( normalize( IN.viewDir ), o.Normal ) );
		o.Emission = _RimColor.rgb * pow (rim, _RimPower);
	}

	ENDCG
} 
    
Fallback "Diffuse"
}