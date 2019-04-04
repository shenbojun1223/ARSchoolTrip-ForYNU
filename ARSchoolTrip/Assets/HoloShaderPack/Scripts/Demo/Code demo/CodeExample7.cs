using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample7 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Fields
		public Color TargetColor;

		// Mono
		void Update()
		{
			Material.SetColor("_ScanLineColor",
				CodeExampleHelper.NormalizedTime*TargetColor + (1 - CodeExampleHelper.NormalizedTime)*Color.white);
		}
	}
}