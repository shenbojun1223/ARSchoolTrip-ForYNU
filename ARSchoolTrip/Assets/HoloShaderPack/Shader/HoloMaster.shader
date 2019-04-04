// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "HoloShaderPack/Master"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color tint", Color) = (1,1,1,1)
		_Strength ("Color strength", Range(0.01, 10)) = 1

		_ScanLineColor ("Scan line color", Color) = (1,1,1,1)
		_ScanLineSettings("ScanlineSettings", Vector) = (0.1, 0.2, 0, 0)
		_ScanLineDistance ("Scan line distance", Float) = 0.2
		
		_RimStrength ("Rim strength", Range(0, 5)) = 1.0

		_NoiseColor ("Noise color", Color) = (1,1,1,1)
		_NoiseSettings("NoiseSettings", Vector) = (0.1, 0.1, 0.1, 0)
		_NoiseFrequency ("Noise frequency", Vector) = (0,0,0)
		
		_DistortionTex ("Texture", 2D) = "white" {}
		_DistortionSettings ("DistortionSettings", Vector) = (0,0,0,0)

	}
	SubShader
	{
		// Subshader Tags
		Tags { "Queue"="Transparent+1" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite On
		
		// Render Pass
		Pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"
			#include "HoloCG.cginc"
			#pragma target 3.0
			
			#pragma fragmentoption ARB_precision_hint_fastest
			
			#pragma multi_compile SCANLINES_ON_WORLD SCANLINES_ON_LOCAL SCANLINES_ON_FACE SCANLINES_OFF
			#pragma multi_compile NOISE_ON NOISE_ON_WORLD NOISE_ON_LOCAL NOISE_ON_FACE NOISE_OFF
			#pragma multi_compile DISTORTION_ON DISTORTION_ON_DISSOLVE DISTORTION_OFF
			#pragma multi_compile RIM_ON RIM_ON_INVERT RIM_OFF
			
			#pragma vertex vert
			#pragma fragment frag
	
	
		////// Uniform user variable definition
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform fixed4 _Color;
			uniform fixed _Strength;

			uniform fixed4 _ScanLineColor;
			uniform float4 _ScanLineSettings;
			uniform float _ScanLineDistance;

			uniform float _RimStrength;

			uniform fixed4 _NoiseColor;
			uniform float4 _NoiseSettings;
			uniform float3 _NoiseFrequency;
			
			uniform sampler2D _DistortionTex;
			uniform float4 _DistortionTex_ST;
			uniform float4 _DistortionSettings;
	
		////// Input structs
	
		////// Shader functions
			// Vertex shader
			Vert2Frag vert(VertexInput vertIn)
			{
				Vert2Frag output;
				
				output.pos = UnityObjectToClipPos(vertIn.vertex);
				output.posWorld = mul(unity_ObjectToWorld, vertIn.vertex);
				output.posLocal = vertIn.vertex;
				output.normal = vertIn.normal;
                output.uv = vertIn.texcoord0 * _MainTex_ST.xy + _MainTex_ST.zw;
				
				return(output);
			}
			
			// Fragent shader
			float4 frag(Vert2Frag fragIn) : SV_Target
			{
				fixed2 uv = fragIn.uv;
				
				// Distortion
				#ifdef DISTORTION_ON
					fixed2 distortion = getDistortion(fragIn.uv, _DistortionTex, _DistortionSettings);
					uv = uv + distortion;
				#endif
				
				// Texture
				fixed4 mainColor = tex2D(_MainTex, uv);

			////// Final
				fixed4 final;

				// Scanlines
				#ifndef SCANLINES_OFF
					#ifdef DISTORTION_ON
						fixed scanLine = getScanLineMultiplierDistortion(fragIn, _ScanLineSettings, _ScanLineDistance, distortion.y);
					#else
						fixed scanLine = getScanLineMultiplier(fragIn, _ScanLineSettings, _ScanLineDistance);
					#endif
					final = (1-scanLine) * mainColor * _Color + mainColor * (scanLine) * _ScanLineColor;
				#else
					final = fixed4(mainColor.xyz, 1) * _Color;
				#endif
				
				// Noise
				#ifndef NOISE_OFF
					float noise = getNoise(fragIn, _NoiseSettings, _NoiseFrequency);
					final = final*(1-noise) + final * _NoiseColor * noise;
				#endif
				
				// Dissolve
				#ifdef DISTORTION_ON_DISSOLVE
					final = getDissolve(fragIn.uv, _DistortionTex, _DistortionSettings) * final;
				#endif

				// Rim
				#ifndef RIM_OFF
					final.w = getRim(fragIn, _RimStrength) * final.w;
				#endif

				return final * _Strength;
			}
			ENDCG
		}
	}

	FallBack "HoloSimple"
    CustomEditor "HoloMasterInspector"
}
