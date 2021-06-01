using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIToggleColor : MonoBehaviour
{
	public	Color		colorIsOff	= Color.white;
	public	Color		colorIsOn	= Color.green;
	public	Graphic[]	targets;

			Toggle		toggle;

	private void Awake()
    {
		toggle = GetComponent<Toggle>();
	}
	private void OnEnable()
	{
		toggle.onValueChanged.AddListener(OnValueChanged);
		OnValueChanged(toggle.isOn);
	}
	private void OnDisable()
	{
		toggle.onValueChanged.RemoveListener(OnValueChanged);
	}

	void OnValueChanged(bool isOn)
	{
		foreach(var target in targets)
		{
			target.color = isOn ? colorIsOn : colorIsOff;
		}
	}
}
