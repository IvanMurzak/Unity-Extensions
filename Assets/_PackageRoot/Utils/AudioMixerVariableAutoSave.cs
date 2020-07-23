using UnityEngine.Audio;

public class AudioMixerVariableAutoSave : AudioMixerVariable
{
	PlayerPrefsExFloat masterVolumePrefs;

	public AudioMixerVariableAutoSave(AudioMixer audioMixer, string variableName, float defaultValue = 0.75f, float min = 0, float max = 1) : base(audioMixer, variableName, min, max)
	{
		masterVolumePrefs = new PlayerPrefsExFloat("save_" + variableName, defaultValue);
		base.Value = masterVolumePrefs.Value;
	}

	public override float Value
	{
		get => base.Value;
		set
		{
			base.Value = value;
			masterVolumePrefs.Value = value;
		}
	}
	public override void SetMin() => Value = min;
	public override void SetMax() => Value = max;

	public float SavedValue => masterVolumePrefs.Value;
}