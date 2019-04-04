using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample18 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			Vector4 noiseSettings = Material.GetVector("_DistortionSettings");
			noiseSettings.z = CodeExampleHelper.NormalizedTime*0.1f;
			Material.SetVector("_DistortionSettings", noiseSettings);
		}
	}
}