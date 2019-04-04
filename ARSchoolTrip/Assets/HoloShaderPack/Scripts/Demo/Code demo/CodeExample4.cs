using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample4 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Fields
		public float TargetStrength;

		// Mono
		void Update()
		{
			Material.SetFloat("_RimStrength", CodeExampleHelper.NormalizedTime*TargetStrength);
		}
	}
}