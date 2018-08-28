Shader "Custom/Waves" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		//_Amplitude ("Amplitude", Float) = 1
		//_Steepness ("Steepness", Range(0, 1)) = 0.7
		//_Wavelength ("Wavelength", Float) = 10
		//_Direction ("Direction (2D)", Vector) = (1,0,0,0)
		_WaveA ("Wave A (dir, steepness, wavelength)", Vector) = (1,0,0.5,10)
		//_WaveB ("Wave B", Vector) = (0,1,0.25,20)
		//_WaveC ("Wave C", Vector) = (1,1,0.15,10)
		//_Speed ("Speed", Float) = 1
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert addshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		//float _Steepness, _Wavelength; //Removed _Speed;
		//float2 _Direction;
		float4 _WaveA; //_WaveC Removed

		float3 GerstnerWave (float4 wave, float3 p, inout float3 tangent, inout float3 binormal)
		{
			float steepness = wave.z;
		    float wavelength = wave.w;
			float k = 2 * UNITY_PI / wavelength;
			float c = sqrt(9.8 / k);
			float2 d = normalize(wave.xy);
			float f = k * (dot(d, p.xz) - c * _Time.y);
			float a = steepness / k;

		//p.x += d.x * (a * cos(f));
		//p.x += _Amplitude * cos(f);
		//p.y = a * sin(f);
		//p.z += d.y * (a * cos(f));

		//Tangent Vector
		tangent += float3
		(
			- d.x * d.x * (steepness * sin(f)),
			d.x * (steepness * cos(f)),
			-d.x * d.y * (steepness * sin(f))
		);

		//Binormal Vector
		binormal += float3
		(
			-d.x * d.y * (steepness * sin(f)),
			d.y * (steepness * cos(f)),
			1 - d.y * d.y * (steepness * sin(f))
		);

		//Returns all the float 3 values.
		return float3
		(
				d.x * (a * cos(f)),
				a * sin(f),
				d.y * (a * cos(f))
		);
		}

	void vert(inout appdata_full vertexData)
	{
		//Getting the vertecis of the plane and assigning it to float3.
		float3 gridPoint = vertexData.vertex.xyz;
		float3 tangent = float3(1, 0, 0);
		float3 binormal = float3(0, 0, 1);
		float3 p = gridPoint;

		//First Wave
		p += GerstnerWave(_WaveA, gridPoint, tangent, binormal);

		//Second Wave
		//p += GerstnerWave(_WaveB, gridPoint, tangent, binormal);

		//Third Wave
		//p += GerstnerWave(_WaveC, gridPoint, tangent, binormal);

		//Normalizing the Wave light
		float3 normal = normalize(cross(binormal, tangent));
		
		vertexData.vertex.xyz = p;
		vertexData.normal = normal;
	}

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
