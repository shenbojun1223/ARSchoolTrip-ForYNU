using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class HoloShader
	{
		// Enums
		public enum Rim
		{
			Off = 0,
			Rim,
			Inverted
		}

		public enum Scanline
		{
			Off = 0,
			World,
			Local,
			Face
		}

		public enum Noise
		{
			Off = 0,
			On,
			World,
			Local,
			Face
		}

		public enum Distortion
		{
			Off = 0,
			Distirtion,
			Dissolve
		}

		//Constants
		private static string[] RIM = {"RIM_OFF", "RIM_ON", "RIM_INVERTED"};
		private static string[] SCANLINE = {"SCANLINES_OFF", "SCANLINES_ON_WORLD", "SCANLINES_ON_LOCAL", "SCANLINES_ON_FACE"};
		private static string[] NOISE = {"NOISE_OFF", "NOISE_ON", "NOISE_ON_WORLD", "NOISE_ON_LOCAL", "NOISE_ON_FACE"};
		private static string[] DISTORTION = {"DISTORTION_OFF", "DISTORTION_ON", "DISTORTION_ON_DISSOLVE"};

		// Helper
		public static void EnableRim(Material material, Rim type)
		{
			for (int a = 0; a < RIM.Length; ++a)
			{
				material.DisableKeyword(RIM[a]);
			}
			material.EnableKeyword(RIM[(int) type]);
		}

		public static void EnableScanline(Material material, Scanline type)
		{
			for (int a = 0; a < SCANLINE.Length; ++a)
			{
				material.DisableKeyword(SCANLINE[a]);
			}
			material.EnableKeyword(SCANLINE[(int) type]);
		}

		public static void EnableNoise(Material material, Noise type)
		{
			for (int a = 0; a < NOISE.Length; ++a)
			{
				material.DisableKeyword(NOISE[a]);
			}
			material.EnableKeyword(NOISE[(int) type]);
		}

		public static void EnableDistortion(Material material, Distortion type)
		{
			for (int a = 0; a < DISTORTION.Length; ++a)
			{
				material.DisableKeyword(DISTORTION[a]);
			}
			material.EnableKeyword(DISTORTION[(int) type]);
		}
	}
}