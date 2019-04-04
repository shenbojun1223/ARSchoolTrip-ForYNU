using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample2 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Fields
		public Color TargetColor;

		// Mono
		void Update()
		{
			Material.SetColor("_Color",
				CodeExampleHelper.NormalizedTime*TargetColor + (1 - CodeExampleHelper.NormalizedTime)*Color.white);
		}
	}
}