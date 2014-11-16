Shader "Custom/Glow" 
{
	//what you can change in unity
	Properties 
	{
		_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
		_rimColor ("Rim Color", Color) = (1, 1, 1, 1)
		_rimPower ("Rim Power", Range(0.0, 6.0)) = 1
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM
		#pragma surface surf Lambert

		//how we get values from unity
		struct Input 
		{
			float4 color : Color;
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float3 viewDir;
		};

		float4 _colorTint;
		sampler2D _mainTex;
		sampler2D _bumpMap;
		float4 _rimColor;
		float _rimPower;

		//surface properties
		void surf (Input IN, inout SurfaceOutput o) 
		{
			IN.color = _colorTint; 
			o.Albedo = tex2D (_mainTex, IN.uv_MainTex).rgb * IN.color;
			o.Normal = UnpackNormal(tex2D (_bumpMap, IN.uv_BumpMap));
			
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _rimColor.rgb * pow(rim, _rimPower);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
