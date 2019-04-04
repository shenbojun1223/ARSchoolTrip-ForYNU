using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExample6 : MonoBehaviour
	{
		// Refs
		public Material Material;

		// Mono
		void Update()
		{
			if (Time.time%2 < 1)
			{
				HoloShader.EnableScanline(Material, HoloShader.Scanline.World);
			}
			else
			{
				HoloShader.EnableScanline(Material, HoloShader.Scanline.Off);
			}
		}
	}
}