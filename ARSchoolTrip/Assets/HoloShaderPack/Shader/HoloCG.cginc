
////// Input structs
			struct VertexInput 
			{
				float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float3 normal : NORMAL;
			};
			struct Vert2Frag
			{
				fixed4 pos : SV_POSITION;
				float3 posWorld : TEXCOORD0;
				float4 posLocal : TEXCOORD1;
                fixed2 uv : TEXCOORD2;
                float3 normal : TEXCOORD3;
			};

////// Holo helper functions
		////// Scan lines
			fixed getScanLineMultiplier3(fixed inputY, float edge, float size, float lineMovement, float distance)
			{
				fixed output;

				float absEdge = abs(edge);
				float frac = abs( fmod(inputY + lineMovement * _Time.x,(distance+size)) );
				float positive = max(0, size - (frac)) * absEdge / min(1, size);
				float negative = max(0, (frac - size)) * absEdge / min(1, size);
				output = positive * max(0, edge) + negative * min(0, edge);
				
				return saturate(abs(output));
			}

			fixed getScanLineMultiplierDistortion(Vert2Frag fragIn, float4 scanLineSettings, float scanLineDistance, fixed distortionY)
			{
				fixed output;
				
				#ifdef SCANLINES_ON_WORLD
					output = getScanLineMultiplier3(fragIn.posWorld.y+distortionY+scanLineSettings.w, scanLineSettings.x, scanLineSettings.y, scanLineSettings.z, scanLineDistance);
				#endif
				#ifdef SCANLINES_ON_LOCAL
					output = getScanLineMultiplier3(fragIn.posLocal.y+distortionY+scanLineSettings.w, scanLineSettings.x, scanLineSettings.y, scanLineSettings.z, scanLineDistance);
				#endif
				#ifdef SCANLINES_ON_FACE
					output = getScanLineMultiplier3(fragIn.uv.x+distortionY+scanLineSettings.w, scanLineSettings.x, scanLineSettings.y, scanLineSettings.z, scanLineDistance);
				#endif
				
				return(output);
			}

			fixed getScanLineMultiplier(Vert2Frag fragIn, float4 scanLineSettings, float scanLineDistance)
			{
				fixed output;

				#ifdef SCANLINES_ON_WORLD
					output = getScanLineMultiplierDistortion(fragIn, scanLineSettings, scanLineDistance, 0);
				#endif
				#ifdef SCANLINES_ON_LOCAL
					output = getScanLineMultiplierDistortion(fragIn, scanLineSettings, scanLineDistance, 0);
				#endif
				#ifdef SCANLINES_ON_FACE
					output = getScanLineMultiplierDistortion(fragIn, scanLineSettings, scanLineDistance, 0);
				#endif
				
				return(output);
			}

		////// Noise
			float random(float3 input) 
			{
				return frac(sin( dot(input ,float3(12.9898,78.233,45.5432) )) * 43758.5453);
			}

			fixed3 getInputVector(Vert2Frag fragIn)
			{
				#ifdef NOISE_ON_WORLD
					return fragIn.posWorld.xyz;
				#endif
				#ifdef NOISE_ON_LOCAL
					return fragIn.posLocal.xyz;
				#endif
				
				return fixed3(fragIn.uv.xy,0);
			}

			fixed getNoise(Vert2Frag fragIn, float4 noiseSettings, float3 noiseFrequency) 
			{
				#ifdef NOISE_OFF
					return 0;
				#endif
				
				#ifdef NOISE_ON
					return random(fixed3(fragIn.uv.x, _Time.x, 0));
				#endif
				
				fixed3 input = getInputVector(fragIn);
				input = input + _Time*noiseFrequency;
				fixed noiseX = input.x - input.x % noiseSettings.x + 10;
				fixed noiseY = input.y - input.y % noiseSettings.y + 10;
				fixed noiseZ = input.z - input.z % noiseSettings.z + 10;
				fixed rnd = random(fixed3(noiseX, noiseY, noiseZ));
				return rnd;
			}

		////// Rim
			float getRim(Vert2Frag fragIn, float rimStrength) 
			{
				float3 viewDir = normalize(ObjSpaceViewDir(fragIn.posLocal).xyz);
				float3 normalDirection =  fragIn.normal;

				float rim;
				#ifdef RIM_ON
					rim = 1-dot(viewDir, normalDirection);
				#endif
				#ifdef RIM_ON_INVERT
					rim = dot(viewDir, normalDirection);
				#endif
				rim = pow(rim, rimStrength);
				return rim;
			}

		////// Distortion & Dissolve
			fixed4 getDistortionTex(sampler2D _DistortionTex, fixed2 uv)
			{
				return tex2D(_DistortionTex, uv);
			}

			float2 getDistortion(fixed2 uv, sampler2D _DistortionTex, float4 distortionSettings) 
			{		
				#ifndef DISTORTION_ON
					return float2(0,0);
				#endif

				uv = uv + distortionSettings.zw * _Time.y;
				fixed4 distortion = getDistortionTex(_DistortionTex, uv);
				return distortion.xy * distortionSettings.xy;
			}

			float4 getDissolve(fixed2 uv, sampler2D _DistortionTex, float4 distortionSettings) 
			{		
				#ifndef DISTORTION_ON_DISSOLVE
					return float4(1,1,1,1);
				#endif
				
				uv = uv + distortionSettings.zw * _Time.y;
				fixed4 distortion = getDistortionTex(_DistortionTex, uv);
				return fixed4(1,1,1,saturate(saturate(distortion.x-distortionSettings.x)*(1/distortionSettings.x)));

			}
			