using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

public class TimeUtilsComponent : BaseMonoBehaviour
{
    [Range(0, 10)] public float timeScale = 1;

    private void Awake()
    {
        UpdateParameters();
        TimeUtils.Instance.timeScale.Subscribe(x => timeScale = x).AddTo(this);
    }

    [Button]
    void UpdateParameters()
    {
        TimeUtils.Instance.timeScale.Value = timeScale;
    }
}
