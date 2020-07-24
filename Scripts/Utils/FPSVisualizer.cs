using UnityEngine;

public class FPSVisualizer : BaseMonoBehaviour
{
	public int fontSize = 20;
	public Color fontColor = Color.black;
	public int topOffset = 50;

	double deltaTime = 0.0;
	int fps = 0;
	private string text;
	private GUIStyle style = new GUIStyle();
	
	void Update()
	{
		deltaTime += Time.deltaTime;
		deltaTime /= 2.0;
		fps = (int) (1.0 / deltaTime);
	}

	void OnGUI()
	{
		// Display it

		text = string.Format("FPS: {0}", fps);

		style.normal.textColor = fontColor;
		style.fontSize = fontSize;
		GUI.Label(new Rect(Screen.width - 100, topOffset, 150, 20), text, style);
	}
}