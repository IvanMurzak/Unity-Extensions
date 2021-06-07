using UnityEngine;

[ExecuteAlways]
public abstract class UIBehaviourShapeNested<T> : UIBehaviourShape<T> where T : Component
{
    private T _targetComponent;

    protected override T GetTargetComponent()
    {
        if (_targetComponent != null)
            return _targetComponent;

        _targetComponent = GetComponentInChildren<T>();
        if (_targetComponent?.transform == transform)
            _targetComponent = null;

        if (_targetComponent == null)
        {
            var targetNeedToMove = GetComponent<T>();
            if (targetNeedToMove == null)
			{
                Debug.LogError($"Missed {typeof(T).Name} Shape. Please add the shape");
                return null;
			}

            var targetGameObject = new GameObject("Shape");
            targetGameObject.transform.SetParent(transform);
            targetGameObject.transform.localPosition = Vector3.zero;
            targetGameObject.transform.localScale = Vector3.one;
            targetGameObject.transform.localRotation = Quaternion.identity;

            var newComponent = targetGameObject.AddComponent<T>();
            CopyValues(targetNeedToMove, newComponent);
            GameObject.DestroyImmediate(targetNeedToMove);

            _targetComponent = newComponent;
        }

        return _targetComponent;
    }

    protected virtual void CopyValues(T from, T to)
    {
        var json = JsonUtility.ToJson(from);
        JsonUtility.FromJsonOverwrite(json, to);
    }
}