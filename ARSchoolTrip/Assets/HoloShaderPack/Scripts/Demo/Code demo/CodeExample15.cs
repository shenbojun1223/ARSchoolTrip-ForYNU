using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample15 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			Vector4 noiseSettings = Material.GetVector("_NoiseSettings");
			noiseSettings.x = 0.01f + Mathf.Sin(Time.time*0.1f + 10f)*0.15f + 0.15f;
			Material.SetVector("_NoiseSettings", noiseSettings);
		}
	}
}