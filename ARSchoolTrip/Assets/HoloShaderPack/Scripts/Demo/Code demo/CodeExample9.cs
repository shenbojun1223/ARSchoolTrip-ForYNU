using UnityEngine;

namespace nightowl.HoloShaderPack
{

	public class CodeExample9 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			Vector4 scanlineSettings = Material.GetVector("_ScanLineSettings");
			scanlineSettings.y = 0.01f + CodeExampleHelper.NormalizedTime*0.5f;
			Material.SetVector("_ScanLineSettings", scanlineSettings);

			Material.SetFloat("_ScanLineDistance", Mathf.Sin(Time.time*0.5f)*0.5f + 0.5f);
		}
	}
}