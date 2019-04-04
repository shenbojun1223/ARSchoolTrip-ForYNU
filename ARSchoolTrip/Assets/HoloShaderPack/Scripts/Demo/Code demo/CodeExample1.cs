using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample1 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Fields
		public Texture Texture;

		// Mono
		void Update()
		{
			if (Time.time%2f < 1f)
			{
				Material.SetTexture("_MainTex", Texture);
			}
			else
			{
				Material.SetTexture("_MainTex", null);
			}
		}
	}
}