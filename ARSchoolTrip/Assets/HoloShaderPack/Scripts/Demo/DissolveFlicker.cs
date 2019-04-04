using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class DissolveFlicker : MonoBehaviour
	{
		// Fields
		public Material Material;
		public float MinStrength = 0;
		public float MaxStrength = 1;
		public float MaxStep = 0.05f;
		public float TickDelay = 0.1f;

		private float timer = 0;

		// Mono
		void Update()
		{
			timer += Time.deltaTime;

			if (timer >= TickDelay)
			{
				timer -= TickDelay;
				UpdateDissolve();
			}
		}

		// DissolveTimer
		private void UpdateDissolve()
		{
			Vector4 currentStength = Material.GetVector("_DistortionSettings");

			currentStength.x += (Random.value - 0.5f)*MaxStep;
			if (currentStength.x < MinStrength)
				currentStength.x = MinStrength;
			if (currentStength.x > MaxStrength)
				currentStength.x = MaxStrength;

			Material.SetVector("_DistortionSettings", currentStength);
		}
	}
}