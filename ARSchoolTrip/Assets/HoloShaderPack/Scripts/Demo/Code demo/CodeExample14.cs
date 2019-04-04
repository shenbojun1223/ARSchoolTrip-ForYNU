using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample14 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			Vector4 noiseSettings = Material.GetVector("_NoiseFrequency");
			noiseSettings.x = Mathf.Sin(Time.time)*0.01f;
			noiseSettings.y = Mathf.Sin(Time.time*2)*0.03f;
			Material.SetVector("_NoiseFrequency", noiseSettings);
		}
	}
}