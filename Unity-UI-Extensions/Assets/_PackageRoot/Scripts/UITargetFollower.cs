using UnityEngine;
using Sirenix.OdinInspector;

public class UITargetFollower : BaseMonoBehaviour
{
	public bool PersistantPosition = false;

    [ShowIf("PersistantPosition")]				public Vector3		persistantTarget;
    [HideIf("PersistantPosition")]				public Transform	target;

    private bool		IsTarget		{ get { return PersistantPosition || target != null; } }
    private Vector3		TargetPosition	{ get { return PersistantPosition ? persistantTarget : target.position; } }
    
    public	bool		isTargetWorldSpaced;
    public	Vector3		offset;
    public	Vector3		screenOffset;
    [BoxGroup("Free Position")]
    public	bool		X, Y, Z = true;
    
	        Vector3		pos;

	private void LateUpdate()
    {
        if (IsTarget)
        {
            if (isTargetWorldSpaced)
            {
                pos = mainCamera.WorldToScreenPoint(TargetPosition + offset) + screenOffset;
            }
            else
            {
                pos = TargetPosition + offset + screenOffset.Multiply(parentCanvas.transform.localScale);
            }

            if (X) pos.x = transform.position.x;
            if (Y) pos.y = transform.position.y;
            if (Z) pos.z = transform.position.z;
            
            transform.position = pos;
        }
    }
}