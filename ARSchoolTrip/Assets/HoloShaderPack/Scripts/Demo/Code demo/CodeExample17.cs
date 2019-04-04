using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample17 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			Vector4 noiseSettings = Material.GetVector("_DistortionSettings");
			noiseSettings.x = Mathf.Sin(Time.time)*0.15f + 0.15f;
			Material.SetVector("_DistortionSettings", noiseSettings);
		}
	}
}