using UnityEngine;

namespace nightowl.HoloShaderPack
{
	public class CodeExampleHelper : MonoBehaviour
	{

		public static float NormalizedTime
		{
			get { return Mathf.Sin(Time.time)*0.5f + 0.5f; }
		}
	}
}