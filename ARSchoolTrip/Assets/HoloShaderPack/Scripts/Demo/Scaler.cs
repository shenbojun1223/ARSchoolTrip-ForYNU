using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class Scaler : MonoBehaviour
	{
		// Fields
		public float MinScale = 0f;
		public float MaxScale = 1f;
		public float Speed = 1f;

		// Mono
		void Update()
		{
			float yScale = MinScale + (MaxScale - MinScale)*(Mathf.Sin(Time.time*Speed)*0.5f + 0.5f);
			transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
		}
	}
} 