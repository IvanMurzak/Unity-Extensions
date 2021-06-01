using Sirenix.OdinInspector;

public class UITransitionManager : BaseMonoBehaviour
{
	ITransitionAnimation[] transitions;

	private void Awake()
	{
		transitions = GetComponents<ITransitionAnimation>();
	}

	[HorizontalGroup("Buttons"), Button(ButtonSizes.Medium)]
	public void Animate()
	{
		if (transitions == null) return;
		foreach (var transition in transitions)
		{
			if (transition != null)
			{
				transition.Animate();
			}
		}
	}
	[HorizontalGroup("Buttons"), Button(ButtonSizes.Medium)]
	public void AnimateBack()
	{
		if (transitions == null) return;
		foreach (var transition in transitions)
		{
			if (transition != null)
			{
				transition.AnimateBack();
			}
		}
	}
}