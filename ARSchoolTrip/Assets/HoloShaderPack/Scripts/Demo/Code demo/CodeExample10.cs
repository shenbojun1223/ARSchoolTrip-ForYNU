using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample10 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Fields
		public float switchDelay = 2f;

		private float currentTime = 0;

		// Mono
		void Update()
		{
			currentTime += Time.deltaTime;
			if (currentTime > switchDelay)
			{
				currentTime -= switchDelay;

				Vector4 scanlineSettings = Material.GetVector("_ScanLineSettings");
				scanlineSettings.z = Random.value*30f;
				Material.SetVector("_ScanLineSettings", scanlineSettings);
			}
		}
	}
}