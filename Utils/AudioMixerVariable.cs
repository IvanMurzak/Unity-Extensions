using UnityEngine;
using UnityEngine.Audio;
using System;
using Sirenix.OdinInspector;

[Serializable]
public class AudioMixerVariable
{
	[SerializeField, Required]	protected	AudioMixer		audioMixer;
	[SerializeField]			protected	string			variableName;
	[SerializeField]			protected	float			min;
	[SerializeField]			protected	float			max;
								protected	float			tempValue;

	public AudioMixerVariable(AudioMixer audioMixer, string variableName, float min = 0, float max = 1)
	{
		this.audioMixer		= audioMixer;
		this.variableName	= variableName;
		this.min			= min;
		this.max			= max;
	}

	public virtual	float	Value
	{
		set
		{
			var clamped = Mathf.Clamp(value, 0.001f, 1f);
			var result	= Mathf.Log10(clamped) * 20;
			audioMixer.SetFloat(variableName, result);
			// DebugFormat.Log<string>($"audioMixer.SetFloat({variableName}, {result})");
		}
		get
		{
			if (audioMixer.GetFloat(variableName, out tempValue))
			{
				// DebugFormat.Log<string>($"audioMixer.GetFloat({variableName}, {tempValue})");
				return tempValue;
			}
			throw new FieldAccessException($"Can't read {variableName} from audioMixer");
		}
	}
	public virtual	void	SetMin()			=> Value = min;
	public virtual	void	SetMax()			=> Value = max;
	public			bool	IsHearing			=> Value > -60f;
}