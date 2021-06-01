using UnityEngine;
using UnityEngine.Animations;

public abstract class UISelectorWorldPopUp<T> : UISelectorPopUp<T> where T : Component
{
	PositionConstraint	constraint;
	UITargetFollower	follower;

	protected override void Awake()
    {
        base.Awake();
        transform.MoveToHell();
		constraint	= GetComponent<PositionConstraint>();
		follower	= GetComponent<UITargetFollower>();
		if (follower) follower.enabled = false;
	}

	protected virtual Vector3 WorldOffset	=> Vector3.zero;
	protected virtual Vector3 ScreenOffset	=> Vector3.zero;

	protected override bool Open(T unit)
    {
        var result = base.Open(unit);

        if (result)
        {
			transform.position = mainCamera.WorldToScreenPoint(unit.transform.position + WorldOffset)
				.SetZ(transform.parent != null ? transform.parent.position.z : 0) + ScreenOffset;

			if (constraint)
			{
				while (constraint.sourceCount > 0) constraint.RemoveSource(0);

				constraint.translationOffset = WorldOffset;
				constraint.AddSource(new ConstraintSource
				{
					sourceTransform = unit.transform,
					weight			= 1
				});
				constraint.enabled = true;
			}

			if (follower)
			{
				follower.offset = WorldOffset;
				follower.screenOffset = ScreenOffset;
				follower.target = unit.transform;
				follower.enabled = true;
			}
		}

        return result;
    }
	protected override void Close(T unit)
	{
		base.Close(unit);
		if (follower)	follower.enabled	= true;
		if (constraint) constraint.enabled	= true;
	}
}
