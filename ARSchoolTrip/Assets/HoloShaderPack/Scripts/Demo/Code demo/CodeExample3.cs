using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample3 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Fields
		public float TargetStrength;

		// Mono
		void Update()
		{
			Material.SetFloat("_Strength", 1 + CodeExampleHelper.NormalizedTime*TargetStrength);
		}
	}
}