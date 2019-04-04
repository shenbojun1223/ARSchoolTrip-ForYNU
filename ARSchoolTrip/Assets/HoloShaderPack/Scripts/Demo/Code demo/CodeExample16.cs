using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample16 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Time.time%2 < 1)
			{
				HoloShader.EnableDistortion(Material, HoloShader.Distortion.Distirtion);
			}
			else
			{
				HoloShader.EnableDistortion(Material, HoloShader.Distortion.Off);
			}
		}
	}
}