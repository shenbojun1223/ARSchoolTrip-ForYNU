using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample12 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Time.time%2 < 1)
			{
				HoloShader.EnableNoise(Material, HoloShader.Noise.On);
			}
			else
			{
				HoloShader.EnableNoise(Material, HoloShader.Noise.Off);
			}
		}
	}
}