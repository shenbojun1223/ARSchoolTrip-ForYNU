using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class HoloMasterInspector : MaterialEditor
{
	// Fields
	protected static int scanLines = 0;
	protected static int noise = 0;
	protected static int rim = 0;
	protected static int distortion = 0;

	private static Material material
	{
		get { return _material; }
		set
		{
			_material = value;
			UpdateKeywordState();
		}
	}
	private static Material _material;

	// Mono
	void Start()
	{
		material = target as Material;
	}

	protected static void UpdateKeywordState()
	{
		if (material == null)
			return;

		string[] keywords = material.shaderKeywords;

		if (IsKeywordEnabled(keywords, "SCANLINES_OFF"))
			scanLines = 0;
		else if (IsKeywordEnabled(keywords, "SCANLINES_ON_WORLD"))
			scanLines = 1;
		else if (IsKeywordEnabled(keywords, "SCANLINES_ON_LOCAL"))
			scanLines = 2;
		else if (IsKeywordEnabled(keywords, "SCANLINES_ON_FACE"))
			scanLines = 3;

		if (IsKeywordEnabled(keywords, "NOISE_OFF"))
			noise = 0;
		else if (IsKeywordEnabled(keywords, "NOISE_ON"))
			noise = 1;
		else if (IsKeywordEnabled(keywords, "NOISE_ON_WORLD"))
			noise = 2;
		else if (IsKeywordEnabled(keywords, "NOISE_ON_LOCAL"))
			noise = 3;
		else if (IsKeywordEnabled(keywords, "NOISE_ON_FACE"))
			noise = 4;

		if (IsKeywordEnabled(keywords, "RIM_OFF"))
			rim = 0;
		else if (IsKeywordEnabled(keywords, "RIM_ON"))
			rim = 1;
		else if (IsKeywordEnabled(keywords, "NOISE_ON_INVERT"))
			rim = 2;

		if (IsKeywordEnabled(keywords, "DISTORTION_OFF"))
			distortion = 0;
		else if (IsKeywordEnabled(keywords, "DISTORTION_ON"))
			distortion = 1;
		else if (IsKeywordEnabled(keywords, "DISTORTION_ON_DISSOLVE"))
			distortion = 2;
	}

	public override void OnInspectorGUI()
	{
		EditorGUIUtility.wideMode = false;
		material = target as Material;

		if (!isVisible)
			return;

		EditorGUILayout.Separator();
		DrawGeneralSettings();
		DrawRimSettings();
		DrawScanLineSettings();
		DrawNoiseSettings();
		DrawDistortionSettings();
	}

	private static bool IsKeywordEnabled(string[] keywords, string keyword)
	{
		for (int a=0; a<keywords.Length; ++a) 
		{
			if(keywords[a] == keyword)
			{
				return true;
			}
		}
		return false;
	}

	protected void DrawGeneralSettings()
	{
		material.SetColor("_Color", EditorGUILayout.ColorField("Color tint", material.GetColor("_Color")));
		material.SetFloat("_Strength", EditorGUILayout.Slider("Color strength", material.GetFloat("_Strength"), 0.01f, 10f));
		DrawTextureSettings("_MainTex", "MainTexture");
	}

	protected void DrawTextureSettings(string textureName, string label)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField(label);
		material.SetTextureOffset(textureName, EditorGUILayout.Vector2Field("Offset", material.GetTextureOffset(textureName), GUILayout.Height(30)));
		material.SetTextureScale(textureName, EditorGUILayout.Vector2Field("Scale", material.GetTextureScale(textureName), GUILayout.Height(30)));
		EditorGUILayout.EndVertical();
		material.SetTexture(textureName, (Texture2D)EditorGUILayout.ObjectField(material.GetTexture(textureName), typeof(Texture2D), false, GUILayout.Width(80), GUILayout.Height(80)));
		EditorGUILayout.EndHorizontal();
	}

	protected void DrawScanLineSettings()
	{
		EditorGUILayout.Separator();
		EditorGUILayout.PrefixLabel("ScanLines");

		DrawScanLineKeywords();
		if (scanLines == 0)
			return;

		material.SetColor("_ScanLineColor", EditorGUILayout.ColorField("Line color", material.GetColor("_ScanLineColor")));
		Vector4 scanlineSetting = material.GetVector("_ScanLineSettings");
		scanlineSetting.x = GetScanLineEdgeSettings(scanlineSetting);
		scanlineSetting.y = EditorGUILayout.FloatField("Line Size", scanlineSetting.y);
		material.SetFloat("_ScanLineDistance", EditorGUILayout.FloatField("Line distance", material.GetFloat("_ScanLineDistance")));
		scanlineSetting.z = EditorGUILayout.FloatField("Line Speed", scanlineSetting.z);
		scanlineSetting.w = EditorGUILayout.FloatField("Line OffsetY", scanlineSetting.w);

		scanlineSetting = CheckScanLineSetting(scanlineSetting);
		
		material.SetVector("_ScanLineSettings", scanlineSetting);
	}

	private float GetScanLineEdgeSettings(Vector4 scanlineSetting)
	{
		int edge = 0;
		float[] values = new[] { 1f, -1f, 2f, -2f, 10000f };
		for (int a = 0; a < values.Length; ++a)
		{
			if (scanlineSetting.x == values[a])
				edge = a;
		}

		edge = EditorGUILayout.Popup("Type", edge, new[] { "DownSmooth", "UpSmooth", "DownBlock", "UpBlock", "Hard" });
		return values[edge];
	}

	private Vector4 CheckScanLineSetting(Vector4 scanlineSetting)
	{
		if (scanlineSetting.y <= 0)
		{
			scanlineSetting.y = 0.0001f;
		}

		if (material.GetFloat("_ScanLineDistance") < 0)
		{
			EditorGUILayout.HelpBox("Negative distance create overlapping scan line!", MessageType.Warning);
		}

		if (scanlineSetting.z != 0)
		{
			EditorGUILayout.HelpBox(
				"At y=0 is the center point. this moves along if movement(speed) is enabled. If this cernter point should not show up you can set the offsetY",
				MessageType.Info);
		}
		return scanlineSetting;
	}

	protected void DrawNoiseSettings()
	{
		EditorGUILayout.Separator();
		EditorGUILayout.PrefixLabel("Noise");

		DrawNoiseKeywords();

		if (noise <= 0)
			return;

		material.SetColor("_NoiseColor", EditorGUILayout.ColorField("Noise color (multiply)", material.GetColor("_NoiseColor")));

		if (noise <= 1)
			return;

		Vector4 noiseSetting = material.GetVector("_NoiseSettings");
		if (noise == 3)
		{
			Vector2 xy = EditorGUILayout.Vector2Field("Noise dimensions",
				new Vector3(noiseSetting.x, noiseSetting.y, noiseSetting.z));
			noiseSetting.x = xy.x;
			noiseSetting.y = xy.y;
		}
		else
		{
			noiseSetting = EditorGUILayout.Vector3Field("Noise dimensions",
				new Vector3(noiseSetting.x, noiseSetting.y, noiseSetting.z));
		}
		material.SetVector("_NoiseSettings", noiseSetting);

		Vector4 noiseFrequency = material.GetVector("_NoiseFrequency");
		noiseFrequency = EditorGUILayout.Vector3Field("Noise frequency",
			new Vector3(noiseFrequency.x, noiseFrequency.y, noiseFrequency.z));
		material.SetVector("_NoiseFrequency", noiseFrequency);
	}

	protected void DrawRimSettings()
	{
		EditorGUILayout.Separator();
		EditorGUILayout.PrefixLabel("Rim shading");
		DrawRimKeyword();

		if (rim < 1)
			return;

		material.SetFloat("_RimStrength", EditorGUILayout.Slider("Rim strength", material.GetFloat("_RimStrength"), 0, 5));
	}

	protected void DrawDistortionSettings()
	{
		EditorGUILayout.Separator();
		EditorGUILayout.PrefixLabel("Distortion");
		DrawDistortionKeyword();
		if (distortion == 0)
			return;

		Vector4 distortionSettings = material.GetVector("_DistortionSettings");

		if (distortion == 1)
		{
			DrawTextureSettings("_DistortionTex", "Distortion Normal(RGB)");
			distortionSettings = DrawDistortionSettings(distortionSettings);
		}
		else
		{
			DrawTextureSettings("_DistortionTex", "Dissolve Mask(R)");
			distortionSettings = DrawDissolveSettings(distortionSettings);
		}

		material.SetVector("_DistortionSettings", distortionSettings);
	}

	private Vector4 DrawDistortionSettings(Vector4 settings)
	{
		Vector2 strength = new Vector2(settings.x, settings.y);
		Vector2 speed = new Vector2(settings.z, settings.w);

		speed = EditorGUILayout.Vector2Field("Distortion speed", speed);
		strength = EditorGUILayout.Vector2Field("Distortion strength", strength);

		settings = new Vector4(strength.x, strength.y, speed.x, speed.y);
		return settings;
	}

	private Vector4 DrawDissolveSettings(Vector4 settings)
	{
		float strength = settings.x;
		Vector2 speed = new Vector2(settings.z, settings.w);

		speed = EditorGUILayout.Vector2Field("Dissolve speed", speed);
		strength = EditorGUILayout.FloatField("Dissolve strength", strength);

		settings = new Vector4(strength, settings.y, speed.x, speed.y);
		return settings;
	}

	protected void DrawScanLineKeywords()
	{
		EditorGUI.BeginChangeCheck();
		scanLines = EditorGUILayout.Popup(scanLines, new[] { "Off", "World", "Local", "Face" });
		if (EditorGUI.EndChangeCheck())
		{
			Debug.Log("change scanline: " + scanLines);
			if (scanLines == 0)
				material.EnableKeyword("SCANLINES_OFF");
			else
				material.DisableKeyword("SCANLINES_OFF");

			if (scanLines == 1)
				material.EnableKeyword("SCANLINES_ON_WORLD");
			else
				material.DisableKeyword("SCANLINES_ON_WORLD");

			if (scanLines == 2)
				material.EnableKeyword("SCANLINES_ON_LOCAL");
			else
				material.DisableKeyword("SCANLINES_ON_LOCAL");

			if (scanLines == 3)
				material.EnableKeyword("SCANLINES_ON_FACE");
			else
				material.DisableKeyword("SCANLINES_ON_FACE");
		}
	}

	protected void DrawNoiseKeywords()
	{
		EditorGUI.BeginChangeCheck();
		noise = EditorGUILayout.Popup(noise, new[] { "Off", "On", "World", "Local", "Face" });
		if (EditorGUI.EndChangeCheck())
		{
			Debug.Log("change noise: " + noise);
			if (noise == 0)
				material.EnableKeyword("NOISE_OFF");
			else
				material.DisableKeyword("NOISE_OFF");

			if (noise == 1)
				material.EnableKeyword("NOISE_ON");
			else
				material.DisableKeyword("NOISE_ON");

			if (noise == 2)
				material.EnableKeyword("NOISE_ON_WORLD");
			else
				material.DisableKeyword("NOISE_ON_WORLD");

			if (noise == 3)
				material.EnableKeyword("NOISE_ON_LOCAL");
			else
				material.DisableKeyword("NOISE_ON_LOCAL");

			if (noise == 4)
				material.EnableKeyword("NOISE_ON_FACE");
			else
				material.DisableKeyword("NOISE_ON_FACE");
		}
	}

	protected void DrawRimKeyword()
	{
		EditorGUI.BeginChangeCheck();
		rim = EditorGUILayout.Popup(rim, new[] { "Off", "On", "Invert" });
		if (EditorGUI.EndChangeCheck())
		{
			Debug.Log("change rim: " + rim);
			if (rim == 0)
				material.EnableKeyword("RIM_OFF");
			else
				material.DisableKeyword("RIM_OFF");

			if (rim == 1)
				material.EnableKeyword("RIM_ON");
			else
				material.DisableKeyword("RIM_ON");

			if (rim == 2)
				material.EnableKeyword("RIM_ON_INVERT");
			else
				material.DisableKeyword("RIM_ON_INVERT");
		}
	}



	protected void DrawDistortionKeyword()
	{
		EditorGUI.BeginChangeCheck();
		distortion = EditorGUILayout.Popup(distortion, new[] { "Off", "On", "Dissolve" });
		if (EditorGUI.EndChangeCheck())
		{
			Debug.Log("change distortion: " + rim);
			if (distortion == 0)
				material.EnableKeyword("DISTORTION_OFF");
			else
				material.DisableKeyword("DISTORTION_OFF");

			if (distortion == 1)
				material.EnableKeyword("DISTORTION_ON");
			else
				material.DisableKeyword("DISTORTION_ON");

			if (distortion == 2)
				material.EnableKeyword("DISTORTION_ON_DISSOLVE");
			else
				material.DisableKeyword("DISTORTION_ON_DISSOLVE");
		}
	}
}