using UnityEngine;
using System.Collections;

using UnityEditor;
using AC.TimeOfDaySystemFree;
using AC.UtilityEditor;


[CustomEditor(typeof(TimeOfDayManager))] 
public class TimeOfDayManagerEditor : Editor
{


	new SerializedObject serializedObject; 
	TimeOfDayManager timeOfDayManager;
	//-----------------------------------------------------


	#region SerializeProperties

	// Resources.
	SerializedProperty autoAssignSky;
	SerializedProperty skyMaterial;

	SerializedProperty directionalLight;
	SerializedProperty sunTransform;
	SerializedProperty moonTransform;

	SerializedProperty moonTexture;
	SerializedProperty starsCubemap;
	SerializedProperty starsNoiseCubemap;
	//-----------------------------------------------------


	// World and time.
	SerializedProperty playTime;
	SerializedProperty worldLongitude;
	SerializedProperty useWorldLongitudeCurve;
	SerializedProperty worldLongitudeCurve;
	SerializedProperty dayInSeconds;
	SerializedProperty timeline;
	//-----------------------------------------------------


	// Sun.

	SerializedProperty sunType;

	SerializedProperty useSunColorGradient;
	SerializedProperty sunColor;
	SerializedProperty sunColorGradient;
	SerializedProperty useSunSizeCurve;
	SerializedProperty sunSize;
	SerializedProperty sunSizeCurve;
	SerializedProperty useSunLightIntensityCurve;
	SerializedProperty sunLightIntensity;
	SerializedProperty sunLightIntensityCurve;
	//-----------------------------------------------------

	// Atmosphere
	SerializedProperty useSkyTintGradient;
	SerializedProperty skyTint;
	SerializedProperty skyTintGradient;

	SerializedProperty useAtmosphereThicknessCurve;
	SerializedProperty atmosphereThickness;
	SerializedProperty atmosphereThicknessCurve;

	SerializedProperty groundColor;

	SerializedProperty useNightColor;
	SerializedProperty useNightColorGradient;
	SerializedProperty nightColor;
	SerializedProperty nightColorGradient;

	SerializedProperty useHorizonFade;
	SerializedProperty horizonFade;
	//-----------------------------------------------------

	// Moon.
	SerializedProperty useMoon;

	SerializedProperty moonRotationMode;

	SerializedProperty moonYaw;
	SerializedProperty useMoonYawCurve;
	SerializedProperty moonYawCurve;

	SerializedProperty moonPitch;
	SerializedProperty useMoonPitchCurve;
	SerializedProperty moonPitchCurve;

	SerializedProperty useMoonLightColorGradient;
	SerializedProperty moonLightColor;
	SerializedProperty moonLightColorGradient;
	SerializedProperty useMoonLightIntensityCurve;
	SerializedProperty moonLightIntensity;
	SerializedProperty moonLightIntensityCurve;

	SerializedProperty useMoonColorGradient;
	SerializedProperty moonColor;
	SerializedProperty moonColorGradient;

	SerializedProperty useMoonIntensityCurve;
	SerializedProperty moonIntensity;
	SerializedProperty moonIntensityCurve;

	SerializedProperty useMoonSizeCurve;
	SerializedProperty moonSize;
	SerializedProperty moonSizeCurve;

	//------------------------------------------------
	SerializedProperty useMoonHalo;
	SerializedProperty useMoonHaloColorGradient;
	SerializedProperty moonHaloColor;
	SerializedProperty moonHaloColorGradient;

	SerializedProperty useMoonHaloSizeCurve;
	SerializedProperty moonHaloSize;
	SerializedProperty moonHaloSizeCurve;

	SerializedProperty useMoonHaloIntensityCurve;
	SerializedProperty moonHaloIntensity;
	SerializedProperty moonHaloIntensityCurve;
	//-----------------------------------------------------

	// Stars.
	SerializedProperty useStars;
	SerializedProperty starsRotationMode;
	SerializedProperty starsOffsets;
	SerializedProperty useStarsColorGradient;
	SerializedProperty starsColor;
	SerializedProperty starsColorGradient;
	SerializedProperty useStarsIntensityCurve;

	SerializedProperty starsIntensity;
	SerializedProperty starsIntensityCurve;

	SerializedProperty useStarsTwinkle;
	SerializedProperty useStarsTwinkleCurve;
	SerializedProperty starsTwinkle;
	SerializedProperty starsTwinkleCurve;
	SerializedProperty useStarsTwinkleSpeedCurve;
	SerializedProperty starsTwinkleSpeed;
	SerializedProperty starsTwinkleSpeedCurve;
	//-----------------------------------------------------


	// Ambient.
	SerializedProperty ambientMode;
	SerializedProperty useAmbientSkyColorGradient;
	SerializedProperty ambientSkyColor;
	SerializedProperty ambientSkyColorGradient;
	SerializedProperty useAmbientEquatorColorGradient;
	SerializedProperty ambientEquatorColor;
	SerializedProperty ambientEquatorColorGradient;

	SerializedProperty useAmbientGroundColorGradient;
	SerializedProperty ambientGroundColor;
	SerializedProperty ambientGroundColorGradient;
	SerializedProperty useAmbientIntensityCurve;

	SerializedProperty ambientIntensity;
	SerializedProperty ambientIntensityCurve;
	//-----------------------------------------------------


	// fog.
	SerializedProperty fogType;
	SerializedProperty fogMode;
	SerializedProperty useRenderSettingsFog;
	SerializedProperty useFogDensityCurve;

	SerializedProperty fogDensity;
	SerializedProperty fogDensityCurve;
	SerializedProperty useFogStartDistanceCurve;
	SerializedProperty fogStartDistance;

	SerializedProperty fogStartDistanceCurve;

	SerializedProperty useFogEndDistanceCurve;

	SerializedProperty fogEndDistance;
	SerializedProperty fogEndDistanceCurve;

	SerializedProperty useFogColorGradient;
	SerializedProperty fogColor;
	SerializedProperty fogColorGradient;
	//-----------------------------------------------------


	// Other Settings.
	SerializedProperty exposure;
	SerializedProperty useExposureCurve;
	SerializedProperty exposureCurve;
	//-----------------------------------------------------

	#endregion

	#region foldouts
	//-----------------------------------------
	bool m_ResourcesFoldout;
	bool m_WorldAndTimeFoldout;
	bool m_SunFoldout;
	bool m_AtmosphereFoldout;
	bool m_MoonFoldout;
	bool m_StarsFoldout;
	bool m_AmbientFoldout;
	bool m_FogFoldout;
	bool m_OtherSettingsFoldout;
	//_________________________________________
	#endregion




	GUIStyle textTitleStyle
	{

		get 
		{

			GUIStyle style = new GUIStyle (EditorStyles.label); 
			style.fontStyle = FontStyle.Bold;
			style.fontSize = 12;

			return style;
		}
	}

	GUIStyle miniTextStyle
	{

		get 
		{
			GUIStyle style = new GUIStyle(EditorStyles.label); 
			style.fontStyle = FontStyle.Bold;
			style.fontSize = 8;

			return style;
		}
	}


	Color WhiteColor { get { return Color.white; } }

	void OnEnable()
	{

		serializedObject = new SerializedObject (target);
		timeOfDayManager = (TimeOfDayManager)target;
		//---------------------------------------------------------------------------------------------------

		// Resources and components.

		autoAssignSky     = serializedObject.FindProperty ("m_AutoAssignSky");
		skyMaterial       = serializedObject.FindProperty ("skyMaterial");
		directionalLight  = serializedObject.FindProperty ("m_DirectionalLight");
		sunTransform      = serializedObject.FindProperty ("m_SunTransform");
		moonTransform     = serializedObject.FindProperty ("m_MoonTransform");

		moonTexture       = serializedObject.FindProperty ("moonTexture");
		starsCubemap      = serializedObject.FindProperty ("starsCubemap");
		starsNoiseCubemap = serializedObject.FindProperty ("starsNoiseCubemap");
		//---------------------------------------------------------------------------------------------------

		// World and time.
		playTime               = serializedObject.FindProperty ("playTime");
		worldLongitude         = serializedObject.FindProperty ("m_WorldLongitude");
		useWorldLongitudeCurve = serializedObject.FindProperty ("useWorldLongitudeCurve");
		worldLongitudeCurve    = serializedObject.FindProperty ("worldLongitudeCurve");
		dayInSeconds           = serializedObject.FindProperty ("dayInSeconds");
		timeline               = serializedObject.FindProperty ("timeline");
		//---------------------------------------------------------------------------------------------------

		// Sun.
		sunType                       = serializedObject.FindProperty ("sunType");
		useSunColorGradient           = serializedObject.FindProperty ("useSunColorGradient");
		sunColor                      = serializedObject.FindProperty ("m_SunColor");
		sunColorGradient              = serializedObject.FindProperty ("sunColorGradient");
		useSunSizeCurve               = serializedObject.FindProperty ("useSunSizeCurve");
		sunSize                       = serializedObject.FindProperty ("m_SunSize");
		sunSizeCurve                  = serializedObject.FindProperty ("sunSizeCurve");
		useSunLightIntensityCurve     = serializedObject.FindProperty ("useSunLightIntensityCurve");
		sunLightIntensity             = serializedObject.FindProperty ("m_SunLightIntensity");
		sunLightIntensityCurve        = serializedObject.FindProperty ("sunLightIntensityCurve");
		//---------------------------------------------------------------------------------------------------

		// Atmosphere.
		useSkyTintGradient                 = serializedObject.FindProperty ("useSkyTintGradient");
		skyTint                            = serializedObject.FindProperty ("m_SkyTint");
		skyTintGradient                    = serializedObject.FindProperty ("skyTintGradient");
		useAtmosphereThicknessCurve        = serializedObject.FindProperty ("useAtmosphereThicknessCurve");
		atmosphereThickness                = serializedObject.FindProperty ("m_AtmosphereThickness");
		atmosphereThicknessCurve           = serializedObject.FindProperty ("atmosphereThicknessCurve");
		groundColor                        = serializedObject.FindProperty ("groundColor");
		useNightColor                      = serializedObject.FindProperty ("useNightColor");
		useNightColorGradient              = serializedObject.FindProperty ("useNightColorGradient");
		nightColor                         = serializedObject.FindProperty ("m_NightColor");
		nightColorGradient                 = serializedObject.FindProperty ("nightColorGradient");
		useHorizonFade                     = serializedObject.FindProperty ("useHorizonFade");
		horizonFade                        = serializedObject.FindProperty ("m_HorizonFade");
		//---------------------------------------------------------------------------------------------------

		// Use moon.
		useMoon                       = serializedObject.FindProperty ("useMoon");
		moonRotationMode              = serializedObject.FindProperty ("moonRotationMode");

		moonYaw                    = serializedObject.FindProperty ("m_MoonYaw");
		useMoonYawCurve            = serializedObject.FindProperty ("useMoonYawCurve");
		moonYawCurve               = serializedObject.FindProperty ("moonYawCurve");

		moonPitch                  = serializedObject.FindProperty ("m_MoonPitch");
		useMoonPitchCurve          = serializedObject.FindProperty ("useMoonPitchCurve");
		moonPitchCurve             = serializedObject.FindProperty ("moonPitchCurve");

		useMoonLightColorGradient     = serializedObject.FindProperty ("useMoonLightColorGradient");
		moonLightColor                = serializedObject.FindProperty ("m_MoonLightColor");
		moonLightColorGradient        = serializedObject.FindProperty ("moonLightColorGradient");
		useMoonLightIntensityCurve    = serializedObject.FindProperty ("useMoonLightIntensityCurve");
		moonLightIntensity            = serializedObject.FindProperty ("m_MoonLightIntensity");
		moonLightIntensityCurve       = serializedObject.FindProperty ("moonLightIntensityCurve");
		useMoonColorGradient          = serializedObject.FindProperty ("useMoonColorGradient");
		moonColor                     = serializedObject.FindProperty ("m_MoonColor");
		moonColorGradient             = serializedObject.FindProperty ("moonColorGradient");
		useMoonIntensityCurve         = serializedObject.FindProperty ("useMoonIntensityCurve");
		moonIntensity                 = serializedObject.FindProperty ("m_MoonIntensity");
		moonIntensityCurve            = serializedObject.FindProperty ("moonIntensityCurve");
		useMoonSizeCurve              = serializedObject.FindProperty ("useMoonSizeCurve");
		moonSize                      = serializedObject.FindProperty ("m_MoonSize");
		moonSizeCurve                 = serializedObject.FindProperty ("moonSizeCurve");

		useMoonHalo                   = serializedObject.FindProperty ("useMoonHalo");
		useMoonHaloColorGradient      = serializedObject.FindProperty ("useMoonHaloColorGradient");
		moonHaloColor                 = serializedObject.FindProperty ("m_MoonHaloColor");
		moonHaloColorGradient         = serializedObject.FindProperty ("moonHaloColorGradient");
		useMoonHaloSizeCurve          = serializedObject.FindProperty ("useMoonHaloSizeCurve");
		moonHaloSize                  = serializedObject.FindProperty ("m_MoonHaloSize");
		moonHaloSizeCurve             = serializedObject.FindProperty ("moonHaloSizeCurve");
		useMoonHaloIntensityCurve     = serializedObject.FindProperty ("useMoonHaloIntensityCurve");
		moonHaloIntensity             = serializedObject.FindProperty ("m_MoonHaloIntensity");
		moonHaloIntensityCurve        = serializedObject.FindProperty ("moonHaloIntensityCurve");
		//---------------------------------------------------------------------------------------------------

		// Stars.
		useStars                  = serializedObject.FindProperty ("useStars");
		starsRotationMode         = serializedObject.FindProperty ("starsRotationMode");
		starsOffsets              = serializedObject.FindProperty ("starsOffsets");
		useStarsColorGradient     = serializedObject.FindProperty ("useStarsColorGradient");
		starsColor                = serializedObject.FindProperty ("m_StarsColor");
		starsColorGradient        = serializedObject.FindProperty ("starsColorGradient");
		useStarsIntensityCurve    = serializedObject.FindProperty ("useStarsIntensityCurve");
		useStarsTwinkle           = serializedObject.FindProperty ("useStarsTwinkle");
		starsIntensity            = serializedObject.FindProperty ("m_StarsIntensity");
		starsIntensityCurve       = serializedObject.FindProperty ("starsIntensityCurve");
		useStarsTwinkleCurve      = serializedObject.FindProperty ("useStarsTwinkleCurve");
		starsTwinkle              = serializedObject.FindProperty ("m_StarsTwinkle");
		starsTwinkleCurve         = serializedObject.FindProperty ("starsTwinkleCurve");
		useStarsTwinkleSpeedCurve = serializedObject.FindProperty ("useStarsTwinkleSpeedCurve");
		starsTwinkleSpeed         = serializedObject.FindProperty ("m_StarsTwinkleSpeed");
		starsTwinkleSpeedCurve    = serializedObject.FindProperty ("starsTwinkleSpeedCurve");
		//---------------------------------------------------------------------------------------------------

		// Ambient.
		ambientMode                      = serializedObject.FindProperty ("m_AmbientMode");
		useAmbientSkyColorGradient       = serializedObject.FindProperty ("useAmbientSkyColorGradient");
		ambientSkyColor                  = serializedObject.FindProperty ("m_AmbientSkyColor");
		ambientSkyColorGradient          = serializedObject.FindProperty ("ambientSkyColorGradient");
		useAmbientEquatorColorGradient   = serializedObject.FindProperty ("useAmbientEquatorColorGradient");
		ambientEquatorColor              = serializedObject.FindProperty ("m_AmbientEquatorColor");
		ambientEquatorColorGradient      = serializedObject.FindProperty ("ambientEquatorColorGradient");
		useAmbientGroundColorGradient    = serializedObject.FindProperty ("useAmbientGroundColorGradient");
		ambientGroundColor               = serializedObject.FindProperty ("m_AmbientGroundColor");
		ambientGroundColorGradient       = serializedObject.FindProperty ("ambientGroundColorGradient");
		useAmbientIntensityCurve         = serializedObject.FindProperty ("useAmbientIntensityCurve");
		ambientIntensity                 = serializedObject.FindProperty ("m_AmbientIntensity");
		ambientIntensityCurve            = serializedObject.FindProperty ("ambientIntensityCurve");
		//---------------------------------------------------------------------------------------------------

		// fog.
		fogType                  = serializedObject.FindProperty ("fogType");
		fogMode                  = serializedObject.FindProperty ("fogMode");
		useRenderSettingsFog     = serializedObject.FindProperty ("useRenderSettingsFog");
		useFogDensityCurve       = serializedObject.FindProperty ("useFogDensityCurve");
		fogDensity               = serializedObject.FindProperty ("m_FogDensity");
		fogDensityCurve          = serializedObject.FindProperty ("fogDensityCurve");
		useFogStartDistanceCurve = serializedObject.FindProperty ("useFogStartDistanceCurve");
		fogStartDistance         = serializedObject.FindProperty ("m_FogStartDistance");
		fogStartDistanceCurve    = serializedObject.FindProperty ("fogStartDistanceCurve");
		useFogEndDistanceCurve   = serializedObject.FindProperty ("useFogEndDistanceCurve");
		fogEndDistance           = serializedObject.FindProperty ("m_FogEndDistance");
		fogEndDistanceCurve      = serializedObject.FindProperty ("fogEndDistanceCurve");
		useFogColorGradient      = serializedObject.FindProperty ("useFogColorGradient");
		fogColor                 = serializedObject.FindProperty ("m_FogColor");
		fogColorGradient         = serializedObject.FindProperty ("fogColorGradient");
		//---------------------------------------------------------------------------------------------------

		// Other settings.
		exposure             = serializedObject.FindProperty ("m_Exposure");
		useExposureCurve     = serializedObject.FindProperty ("useExposureCurve");
		exposureCurve        = serializedObject.FindProperty ("exposureCurve");
		//---------------------------------------------------------------------------------------------------
	}

	public override void OnInspectorGUI()
	{

		serializedObject.Update ();

		AC_UtilityEditor.Separator (WhiteColor, 2);
		AC_UtilityEditor.Text("Time of Day Manager [Free]", textTitleStyle, true);
		AC_UtilityEditor.Separator (WhiteColor, 2);

		//---------------------------------------------
		ResourcesAndComponents(); 
		WorldAndTime();

		Atmosphere();
		Sun(); 
		Moon();
		Stars();   
		Ambient();  
		Fog();
		OtherSettings();
		//---------------------------------------------

		serializedObject.ApplyModifiedProperties();

	}
		
	void ResourcesAndComponents()
	{

		m_ResourcesFoldout = EditorGUILayout.Foldout (m_ResourcesFoldout, "Resources");
		if (m_ResourcesFoldout) 
		{

			AC_UtilityEditor.Separator (WhiteColor, 2);
			AC_UtilityEditor.Text ("Resources And Components", textTitleStyle, true);
			AC_UtilityEditor.Separator (WhiteColor, 2);
			//---------------------------------------------------------------------------------------------------

			EditorGUILayout.PropertyField(autoAssignSky, new GUIContent("Auto Assign Sky?"));

			EditorGUILayout.PropertyField(skyMaterial, new GUIContent("Sky Material"));
			if (skyMaterial.objectReferenceValue == null)
			{
				EditorGUILayout.HelpBox ("Please Assign Sky Material", MessageType.Warning);
			}
			//---------------------------------------------------------------------------------------------------

			EditorGUILayout.PropertyField(sunTransform, new GUIContent("Sun Transform"));
			if (sunTransform.objectReferenceValue == null) 
			{
				EditorGUILayout.HelpBox ("Please Assign Sun Transform", MessageType.Warning);
			} 
				
			EditorGUILayout.PropertyField(moonTransform, new GUIContent("Moon Transform"));
			if (moonTransform.objectReferenceValue == null) 
			{
				EditorGUILayout.HelpBox ("Please Assign Moon Light", MessageType.Warning);
			} 

			EditorGUILayout.PropertyField(directionalLight, new GUIContent("Directional Light"));
			//---------------------------------------------------------------------------------------------------

			AC_UtilityEditor.Separator (WhiteColor, 2);

			EditorGUILayout.PropertyField(moonTexture, new GUIContent("Moon Texture"));
			if (moonTexture.objectReferenceValue == null) 
			{
				EditorGUILayout.HelpBox ("Please Assign Moon Texture", MessageType.Warning);
			} 
			//---------------------------------------------------------------------------------------------------

			AC_UtilityEditor.Separator (WhiteColor, 2);

			EditorGUILayout.PropertyField(starsCubemap, new GUIContent("Stars Cubemap")); 
			if (starsCubemap.objectReferenceValue == null) 
			{
				EditorGUILayout.HelpBox ("Please Assign Stars Cubemap", MessageType.Warning);
			}


			EditorGUILayout.PropertyField(starsNoiseCubemap, new GUIContent("Stars Noise Cubemap")); 
			if (starsNoiseCubemap.objectReferenceValue == null) 
			{
				EditorGUILayout.HelpBox ("Please Assign Stars Noise Cubemap", MessageType.Warning);
			}
			//---------------------------------------------------------------------------------------------------

			AC_UtilityEditor.Separator (WhiteColor, 2);
		}
	}

	void WorldAndTime()
	{

		m_WorldAndTimeFoldout = EditorGUILayout.Foldout (m_WorldAndTimeFoldout , "World And Time");
		if(m_WorldAndTimeFoldout)
		{

			AC_UtilityEditor.Separator (WhiteColor, 2);
			AC_UtilityEditor.Text ("World And Time", textTitleStyle, true);
			AC_UtilityEditor.Separator (WhiteColor, 2);
			//---------------------------------------------------------------------------------------------------

			// World Longitude.
			EditorGUILayout.BeginHorizontal ();

			if(useWorldLongitudeCurve.boolValue)
				AC_UtilityEditor.CurveField ("Longitude", worldLongitudeCurve, Color.white, new Rect (0, 0, 1, 360f), 75);
			else
				EditorGUILayout.PropertyField(worldLongitude, new GUIContent("Longitude"));

			AC_UtilityEditor.ToggleButton(useWorldLongitudeCurve, "C");

			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.Separator ();

			//---------------------------------------------------------------------------------------------------
			EditorGUILayout.BeginHorizontal ();

			playTime.boolValue = EditorGUILayout.Toggle ("Play Time", playTime.boolValue, EditorStyles.radioButton);

			GUI.enabled = playTime.boolValue;

			AC_UtilityEditor.Text ("Day in seconds", miniTextStyle, false, 75);
			EditorGUILayout.PropertyField (dayInSeconds, new GUIContent (""), GUILayout.Width (50));

			EditorGUILayout.EndHorizontal ();

			GUI.enabled = true;

			AC_UtilityEditor.Separator (WhiteColor, 2);


			EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.PropertyField(timeline, new GUIContent("Timeline"));
			//---------------------------------------------------------------------------------------------------

			// Time debug.
		//	EditorGUILayout.BeginHorizontal ();

			EditorGUILayout.LabelField("Time" + " " + timeOfDayManager.TimeString, EditorStyles.miniLabel, GUILayout.Width(75));

			//
			//---------------------------------------------------------------------------------------------------

			EditorGUILayout.EndHorizontal ();
			//EditorGUILayout.EndHorizontal ();

			AC_UtilityEditor.Separator (WhiteColor, 2);

			//---------------------------------------------------------------------------------------------------
		}

	}

	void Sun()
	{

		m_SunFoldout = EditorGUILayout.Foldout (m_SunFoldout, "Sun");
		if(m_SunFoldout)
		{

			AC_UtilityEditor.Separator (WhiteColor, 2);
			AC_UtilityEditor.Text ("Sun", textTitleStyle, true);
			AC_UtilityEditor.Separator (WhiteColor, 2);
			//---------------------------------------------------------------------------------------------------

			EditorGUILayout.PropertyField(sunType, new GUIContent("Sun Type"));

			// Sun Color.
			EditorGUILayout.BeginHorizontal ();

			if(useSunColorGradient.boolValue)
				AC_UtilityEditor.ColorField (sunColorGradient, "Sun Color",75);
			else
				AC_UtilityEditor.ColorField (sunColor, "Sun Color",75);

			AC_UtilityEditor.ToggleButton(useSunColorGradient, "G");

			EditorGUILayout.EndHorizontal ();
			//---------------------------------------------------------------------------------------------------

			// Sun Size.
			EditorGUILayout.BeginHorizontal ();

			if(useSunSizeCurve.boolValue)
				AC_UtilityEditor.CurveField ("Sun Size", sunSizeCurve, Color.white, new Rect (0, 0, 1, 0.3f), 75);
			else
				EditorGUILayout.PropertyField(sunSize, new GUIContent("Sun Size"));

			AC_UtilityEditor.ToggleButton(useSunSizeCurve, "C");

			EditorGUILayout.EndHorizontal ();
			//---------------------------------------------------------------------------------------------------

			// Sun Light Intensity.
			EditorGUILayout.BeginHorizontal ();

			if(useSunLightIntensityCurve.boolValue)
				AC_UtilityEditor.CurveField ("Sun Light Intensity", sunLightIntensityCurve, Color.white, new Rect (0, 0, 1, 8f), 75);
			else
				EditorGUILayout.PropertyField(sunLightIntensity, new GUIContent("Sun Light Intensity"));

			AC_UtilityEditor.ToggleButton(useSunLightIntensityCurve, "C");

			EditorGUILayout.EndHorizontal ();

			//---------------------------------------------------------------------------------------------------

		}

	}

	void Atmosphere()
	{

		m_AtmosphereFoldout = EditorGUILayout.Foldout (m_AtmosphereFoldout, "Atmosphere");
		if (m_AtmosphereFoldout) 
		{

			AC_UtilityEditor.Separator (WhiteColor, 2);
			AC_UtilityEditor.Text ("Atmosphere", textTitleStyle, true);
			AC_UtilityEditor.Separator (WhiteColor, 2);
			//---------------------------------------------------------------------------------------------------

			// Sky Tint.
			EditorGUILayout.BeginHorizontal ();

			if(useSkyTintGradient.boolValue)
				AC_UtilityEditor.ColorField (skyTintGradient, "Sky Tint",75);
			else
				AC_UtilityEditor.ColorField (skyTint, "Sky Tint",75);

			AC_UtilityEditor.ToggleButton(useSkyTintGradient, "G");

			EditorGUILayout.EndHorizontal ();
			//---------------------------------------------------------------------------------------------------


			// Atmosphere Thickness.
			EditorGUILayout.BeginHorizontal ();

			if(useAtmosphereThicknessCurve.boolValue)
				AC_UtilityEditor.CurveField ("Atmosphere Thickness", atmosphereThicknessCurve, Color.white, new Rect (0, 0, 1, 7f), 75);
			else
				EditorGUILayout.PropertyField(atmosphereThickness, new GUIContent("Atmosphere Thickness"));

			AC_UtilityEditor.ToggleButton(useAtmosphereThicknessCurve, "C");

			EditorGUILayout.EndHorizontal ();
			//---------------------------------------------------------------------------------------------------

			AC_UtilityEditor.ColorField(groundColor, "Ground Color",99);
			EditorGUILayout.Separator();
			//---------------------------------------------------------------------------------------------------

			AC_UtilityEditor.Separator (WhiteColor, 2);

			// Night color.
			EditorGUILayout.BeginHorizontal ();
			useNightColor.boolValue = EditorGUILayout.Toggle ("Night Color", useNightColor.boolValue, EditorStyles.radioButton);

			//EditorGUILayout.PropertyField(useNightColor, new GUIContent("Night Color"));


			GUI.enabled = useNightColor.boolValue;

			EditorGUILayout.BeginHorizontal ();

			if(useNightColorGradient.boolValue)
				AC_UtilityEditor.ColorField (nightColorGradient, "",150);
			else
				AC_UtilityEditor.ColorField (nightColor, "",150);

			AC_UtilityEditor.ToggleButton(useNightColorGradient, "G");

			EditorGUILayout.EndHorizontal ();

			GUI.enabled = true;

			EditorGUILayout.EndHorizontal ();
			//---------------------------------------------------------------------------------------------------
			AC_UtilityEditor.Separator (WhiteColor, 2);


			EditorGUILayout.Separator();

			// Horizon fade.
			EditorGUILayout.BeginHorizontal ();
			useHorizonFade.boolValue = EditorGUILayout.Toggle ("Horizon Fade", useHorizonFade.boolValue, EditorStyles.radioButton);

			GUI.enabled = useHorizonFade.boolValue;

			EditorGUILayout.PropertyField (horizonFade, new GUIContent (""));
			EditorGUILayout.EndHorizontal ();
			
			//---------------------------------------------------------------------------------------------------

			GUI.enabled = true;

			AC_UtilityEditor.Separator (WhiteColor, 2);
		}
	}

	void Moon()
	{

		m_MoonFoldout = EditorGUILayout.Foldout (m_MoonFoldout, "Moon");
		if (m_MoonFoldout)
		{
			AC_UtilityEditor.Separator (WhiteColor, 2);
			AC_UtilityEditor.Text ("Moon", textTitleStyle, true);
			AC_UtilityEditor.Separator (WhiteColor, 2);
			//---------------------------------------------------------------------------------------------------

			EditorGUILayout.PropertyField (useMoon, new GUIContent ("Use Moon"));

			if (useMoon.boolValue) 
			{

				EditorGUILayout.PropertyField (moonRotationMode, new GUIContent ("Moon Rotation Mode"));


				if (moonRotationMode.intValue != 0) 
				{

					// Moon Longitude.
					EditorGUILayout.BeginHorizontal ();

					if (useMoonYawCurve.boolValue)
						AC_UtilityEditor.CurveField ("Moon Yaw", moonYawCurve, Color.white, new Rect (0, 0, 1, 360f), 75);
					else
						EditorGUILayout.PropertyField (moonYaw, new GUIContent ("Moon Yaw"));

					AC_UtilityEditor.ToggleButton (useMoonYawCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//---------------------------------------------------------------------------------------------------


					// Moon Latitude.
					EditorGUILayout.BeginHorizontal ();

					if (useMoonPitchCurve.boolValue)
						AC_UtilityEditor.CurveField ("Moon Pitch", moonPitchCurve, Color.white, new Rect (0, 0, 1, 360f), 75);
					else
						EditorGUILayout.PropertyField (moonPitch, new GUIContent ("Moon Pitch"));

					AC_UtilityEditor.ToggleButton (useMoonPitchCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//---------------------------------------------------------------------------------------------------
				}
				EditorGUILayout.Separator ();
				//---------------------------------------------------------------------------------------------------


				// Moon Light Color.
				EditorGUILayout.BeginHorizontal ();

				if(useMoonLightColorGradient.boolValue)
					AC_UtilityEditor.ColorField (moonLightColorGradient, "Moon Light Color",75);
				else
					AC_UtilityEditor.ColorField (moonLightColor, "Moon Light Color",75);

				AC_UtilityEditor.ToggleButton(useMoonLightColorGradient, "G");

				EditorGUILayout.EndHorizontal ();

				//---------------------------------------------------------------------------------------------------


				// Moon Light Intensity.
				EditorGUILayout.BeginHorizontal ();

				if(useMoonLightIntensityCurve.boolValue)
					AC_UtilityEditor.CurveField ("Moon Light Intensity", moonLightIntensityCurve, Color.white, new Rect (0, 0, 1, 1f), 75);
				else
					EditorGUILayout.PropertyField(moonLightIntensity, new GUIContent("Moon Light Intensity"));

				AC_UtilityEditor.ToggleButton(useMoonLightIntensityCurve, "C");

				EditorGUILayout.EndHorizontal ();
				//---------------------------------------------------------------------------------------------------

				EditorGUILayout.Separator ();

				// Moon Color.
				EditorGUILayout.BeginHorizontal ();

				if(useMoonColorGradient.boolValue)
					AC_UtilityEditor.ColorField (moonColorGradient, "Moon Color (RGBA)",75);
				else
					AC_UtilityEditor.ColorField (moonColor, "Moon Color(RGBA)",75);

				AC_UtilityEditor.ToggleButton(useMoonColorGradient, "G");

				EditorGUILayout.EndHorizontal ();
				//---------------------------------------------------------------------------------------------------


				// Moon Intensity.
				EditorGUILayout.BeginHorizontal ();

				if(useMoonIntensityCurve.boolValue)
					AC_UtilityEditor.CurveField ("Moon Intensity", moonIntensityCurve, Color.white, new Rect (0, 0, 1, 3f), 75);
				else
					EditorGUILayout.PropertyField(moonIntensity, new GUIContent("Moon Intensity"));

				AC_UtilityEditor.ToggleButton(useMoonIntensityCurve, "C");

				EditorGUILayout.EndHorizontal ();
				//---------------------------------------------------------------------------------------------------


				// Moon Size.
				EditorGUILayout.BeginHorizontal ();

				if(useMoonSizeCurve.boolValue)
					AC_UtilityEditor.CurveField ("Moon Size", moonSizeCurve, Color.white, new Rect (0, 0, 1, 1f), 75);
				else
					EditorGUILayout.PropertyField(moonSize, new GUIContent("Moon Size"));

				AC_UtilityEditor.ToggleButton(useMoonSizeCurve, "C");

				EditorGUILayout.EndHorizontal ();
				//---------------------------------------------------------------------------------------------------

				EditorGUILayout.Separator ();

				//---------------------------------------------------------------------------------------------------


				EditorGUILayout.PropertyField(useMoonHalo, new GUIContent("Use Moon Halo"));
				if (useMoonHalo.boolValue) 
				{
					// Moon Halo Color.
					EditorGUILayout.BeginHorizontal ();

					if(useMoonHaloColorGradient.boolValue)
						AC_UtilityEditor.ColorField (moonHaloColorGradient, "Moon Halo Color",75);
					else
						AC_UtilityEditor.ColorField (moonHaloColor, "Moon Halo Color",75);

					AC_UtilityEditor.ToggleButton(useMoonHaloColorGradient, "G");

					EditorGUILayout.EndHorizontal ();

					//---------------------------------------------------------------------------------------------------

					// Moon Halo Intensity.
					EditorGUILayout.BeginHorizontal ();

					if(useMoonHaloIntensityCurve.boolValue)
						AC_UtilityEditor.CurveField ("Moon Halo Intensity", moonHaloIntensityCurve, Color.white, new Rect (0, 0, 1, 5f), 75);
					else
						EditorGUILayout.PropertyField(moonHaloIntensity, new GUIContent("Moon Halo Intensity"));

					AC_UtilityEditor.ToggleButton(useMoonHaloIntensityCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//---------------------------------------------------------------------------------------------------


					// Moon Halo Size.
					EditorGUILayout.BeginHorizontal ();

					if(useMoonHaloSizeCurve.boolValue)
						AC_UtilityEditor.CurveField ("Moon Halo Size", moonHaloSizeCurve, Color.white, new Rect (0, 0, 1, 10f), 75);
					else
						EditorGUILayout.PropertyField(moonHaloSize, new GUIContent("Moon Halo Size"));

					AC_UtilityEditor.ToggleButton(useMoonHaloSizeCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//---------------------------------------------------------------------------------------------------
				}
			}

		}

	}

	void Stars()
	{


		m_StarsFoldout = EditorGUILayout.Foldout (m_StarsFoldout, "Stars");
		if (m_StarsFoldout) 
		{

			AC_UtilityEditor.Separator (WhiteColor, 2);
			AC_UtilityEditor.Text ("Stars", textTitleStyle, true);
			AC_UtilityEditor.Separator (WhiteColor, 2);
			//---------------------------------------------------------------------------------------------------

			EditorGUILayout.PropertyField (useStars, new GUIContent ("Use Stars"));
			if (useStars.boolValue) 
			{

				EditorGUILayout.PropertyField (starsRotationMode, new GUIContent ("Stars Rotation Mode"));
				EditorGUILayout.PropertyField (starsOffsets, new GUIContent ("Stars Offsets"));
				//---------------------------------------------------------------------------------------------------

				EditorGUILayout.Separator ();

				// Stars Color.
				EditorGUILayout.BeginHorizontal ();

				if(useStarsColorGradient.boolValue)
					AC_UtilityEditor.ColorField (starsColorGradient, "Stars Color",75);
				else
					AC_UtilityEditor.ColorField (starsColor, "Stars Color",75);

				AC_UtilityEditor.ToggleButton(useStarsColorGradient, "G");

				EditorGUILayout.EndHorizontal ();

				//---------------------------------------------------------------------------------------------------



				// Stars Intensity.
				EditorGUILayout.BeginHorizontal ();

				if(useStarsIntensityCurve.boolValue)
					AC_UtilityEditor.CurveField ("Stars Intensity", starsIntensityCurve, Color.white, new Rect (0, 0, 1, 5f), 75);
				else
					EditorGUILayout.PropertyField(starsIntensity, new GUIContent("Stars Intensity"));

				AC_UtilityEditor.ToggleButton(useStarsIntensityCurve, "C");

				EditorGUILayout.EndHorizontal ();
				//---------------------------------------------------------------------------------------------------

				EditorGUILayout.Separator ();

				// Stars Twinkle.
				EditorGUILayout.PropertyField (useStarsTwinkle, new GUIContent ("Use Stars Twinkle"));

				if (useStarsTwinkle.boolValue) 
				{
					EditorGUILayout.BeginHorizontal ();

					if (useStarsTwinkleCurve.boolValue)
						AC_UtilityEditor.CurveField ("Stars Twinkle", starsTwinkleCurve, Color.white, new Rect (0, 0, 1, 1f), 75);
					else
						EditorGUILayout.PropertyField (starsTwinkle, new GUIContent ("Stars Twinkle"));

					AC_UtilityEditor.ToggleButton (useStarsTwinkleCurve, "C");

					EditorGUILayout.EndHorizontal ();

					//---------------------------------------------------------------------------------------------------

					// Stars Twinkle Speed.
					EditorGUILayout.BeginHorizontal ();

					if (useStarsTwinkleSpeedCurve.boolValue)
						AC_UtilityEditor.CurveField ("Stars Twinkle Speed", starsTwinkleSpeedCurve, Color.white, new Rect (0, 0, 1, 10f), 75);
					else
						EditorGUILayout.PropertyField (starsTwinkleSpeed, new GUIContent ("Stars Twinkle Speed"));

					AC_UtilityEditor.ToggleButton (useStarsTwinkleSpeedCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//---------------------------------------------------------------------------------------------------
				}

			}

		}
	}

	void Ambient()
	{

		m_AmbientFoldout = EditorGUILayout.Foldout (m_AmbientFoldout, "Ambient");
		if (m_AmbientFoldout) 
		{

			AC_UtilityEditor.Separator (WhiteColor, 2);
			AC_UtilityEditor.Text("Ambient", textTitleStyle, true);
			AC_UtilityEditor.Separator (WhiteColor, 2);
			//---------------------------------------------------------------------------------------------------

			EditorGUILayout.PropertyField (ambientMode, new GUIContent ("Ambient Mode"));
			//---------------------------------------------------------------------------------------------------

			string ambientColorName = (ambientMode.enumValueIndex == 0) ? "Ambient Color" : "Sky Color";

			if (ambientMode.enumValueIndex != 2) 
			{

				// Ambient Color.
				EditorGUILayout.BeginHorizontal ();

				if (useAmbientSkyColorGradient.boolValue)
					AC_UtilityEditor.ColorField (ambientSkyColorGradient, ambientColorName, 75);
				else
					AC_UtilityEditor.ColorField (ambientSkyColor, ambientColorName, 75);

				AC_UtilityEditor.ToggleButton (useAmbientSkyColorGradient, "G");

				EditorGUILayout.EndHorizontal ();

				//---------------------------------------------------------------------------------------------------

			} 
			else 
			{


				// Ambient Intensity.
				EditorGUILayout.BeginHorizontal ();

				if(useAmbientIntensityCurve.boolValue)
					AC_UtilityEditor.CurveField ("Ambient Intensity", ambientIntensityCurve, Color.white, new Rect (0, 0, 1, 8f), 75);
				else
					EditorGUILayout.PropertyField(ambientIntensity, new GUIContent("Ambient Intensity"));

				AC_UtilityEditor.ToggleButton(useAmbientIntensityCurve, "C");

				EditorGUILayout.EndHorizontal ();
				//---------------------------------------------------------------------------------------------------
			}


			if (ambientMode.enumValueIndex == 1) 
			{

				// Ambient Equator Color.
				EditorGUILayout.BeginHorizontal ();

				if (useAmbientEquatorColorGradient.boolValue)
					AC_UtilityEditor.ColorField (ambientEquatorColorGradient, "Ambient Equator Color", 75);
				else
					AC_UtilityEditor.ColorField (ambientEquatorColor, "Ambient Equator Color", 75);

				AC_UtilityEditor.ToggleButton (useAmbientEquatorColorGradient, "G");

				EditorGUILayout.EndHorizontal ();

				//---------------------------------------------------------------------------------------------------


				// Ambient Ground Color.
				EditorGUILayout.BeginHorizontal ();

				if (useAmbientGroundColorGradient.boolValue)
					AC_UtilityEditor.ColorField (ambientGroundColorGradient, "Ambient Ground Color", 75);
				else
					AC_UtilityEditor.ColorField (ambientGroundColor, "Ambient Ground Color", 75);

				AC_UtilityEditor.ToggleButton (useAmbientGroundColorGradient, "G");

				EditorGUILayout.EndHorizontal ();

				//---------------------------------------------------------------------------------------------------


			}

		}

	}

	void Fog()
	{

		m_FogFoldout = EditorGUILayout.Foldout (m_FogFoldout , "Fog");
		if (m_FogFoldout) 
		{
			AC_UtilityEditor.Separator (WhiteColor, 2);
			AC_UtilityEditor.Text ("Fog", textTitleStyle, true);
			AC_UtilityEditor.Separator (WhiteColor, 2);
			//---------------------------------------------------------------------------------------------------

			EditorGUILayout.PropertyField (fogType, new GUIContent ("Fog Type"));

			if (fogType.intValue == 0)
				EditorGUILayout.PropertyField (useRenderSettingsFog, new GUIContent ("Use Fog"));
			//---------------------------------------------------------------------------------------------------

			if (fogType.intValue != 2) 
			{

				EditorGUILayout.PropertyField (fogMode, new GUIContent ("Fog Mode"));

				if (fogMode.enumValueIndex == 0) {

					// Fog Start Distance.
					EditorGUILayout.BeginHorizontal ();

					if (useFogStartDistanceCurve.boolValue)
						AC_UtilityEditor.CurveField ("Start Distance", fogStartDistanceCurve, Color.white, new Rect (0, 0, 1, 1000f), 75);
					else
						EditorGUILayout.PropertyField (fogStartDistance, new GUIContent ("Fog Start Distance"));

					AC_UtilityEditor.ToggleButton (useFogStartDistanceCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//---------------------------------------------------------------------------------------------------

					// Fog End Distance.
					EditorGUILayout.BeginHorizontal ();

					if (useFogEndDistanceCurve.boolValue)
						AC_UtilityEditor.CurveField ("End Distance", fogEndDistanceCurve, Color.white, new Rect (0, 0, 1, 1000f), 75);
					else
						EditorGUILayout.PropertyField (fogEndDistance, new GUIContent ("Fog End Distance"));

					AC_UtilityEditor.ToggleButton (useFogEndDistanceCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//---------------------------------------------------------------------------------------------------

				} 
				else 
				{

					// Fog Density.
					EditorGUILayout.BeginHorizontal ();

					if (useFogDensityCurve.boolValue)
						AC_UtilityEditor.CurveField ("Fog Density", fogDensityCurve, Color.white, new Rect (0, 0, 1, 1f), 75);
					else
						EditorGUILayout.PropertyField (fogDensity, new GUIContent ("Fog Density"));

					AC_UtilityEditor.ToggleButton (useFogDensityCurve, "C");

					EditorGUILayout.EndHorizontal ();
					//---------------------------------------------------------------------------------------------------
				}

				// Fog Color.
				EditorGUILayout.BeginHorizontal ();

				if (useFogColorGradient.boolValue)
					AC_UtilityEditor.ColorField (fogColorGradient, "Fog Color", 75);
				else
					AC_UtilityEditor.ColorField (fogColor, "Fog Color", 75);

				AC_UtilityEditor.ToggleButton (useFogColorGradient, "G");

				EditorGUILayout.EndHorizontal ();

				//---------------------------------------------------------------------------------------------------
			}
		}
	}

	void OtherSettings()
	{

		m_OtherSettingsFoldout = EditorGUILayout.Foldout (m_OtherSettingsFoldout , "Other Settings");
		if (m_OtherSettingsFoldout) 
		{

			AC_UtilityEditor.Separator (WhiteColor, 2);
			AC_UtilityEditor.Text ("Other Settings",textTitleStyle, true);
			AC_UtilityEditor.Separator (WhiteColor, 2);
			//---------------------------------------------------------------------------------------------------

			// Exposure.
			EditorGUILayout.BeginHorizontal ();

			if(useExposureCurve.boolValue)
				AC_UtilityEditor.CurveField ("Exposure", exposureCurve, Color.white, new Rect (0, 0, 1, 5f), 75);
			else
				EditorGUILayout.PropertyField(exposure, new GUIContent("Exposure"));

			AC_UtilityEditor.ToggleButton(useExposureCurve, "C");

			EditorGUILayout.EndHorizontal ();
			//---------------------------------------------------------------------------------------------------
		}
	}


}

