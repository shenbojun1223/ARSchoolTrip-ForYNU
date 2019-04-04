using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample8 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Fields
		public float TargetStrength;

		// Mono
		void Update()
		{
			Vector4 scanlineSettings = Material.GetVector("_ScanLineSettings");
			float type = Time.time%6;
			if (type < 2f)
			{
				scanlineSettings.x = -1f;
			}
			else if (type < 4f)
			{
				scanlineSettings.x = -1.5f;
			}
			else
			{
				scanlineSettings.x = -1000f;
			}
			Material.SetVector("_ScanLineSettings", scanlineSettings);
		}
	}
}