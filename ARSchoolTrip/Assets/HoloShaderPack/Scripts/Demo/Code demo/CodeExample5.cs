using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample5 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Fields
		public float TargetStrength;

		// Mono
		void Update()
		{
			if (CodeExampleHelper.NormalizedTime*TargetStrength > 1)
			{
				Material.EnableKeyword("RIM_ON");
				Material.DisableKeyword("RIM_ON_INVERT");
				Material.DisableKeyword("RIM_OFF");
			}
			else
			{
				Material.EnableKeyword("RIM_OFF");
				Material.DisableKeyword("RIM_ON");
				Material.DisableKeyword("RIM_ON_INVERT");
			}
			Material.SetFloat("_RimStrength", CodeExampleHelper.NormalizedTime*TargetStrength);
		}
	}
}