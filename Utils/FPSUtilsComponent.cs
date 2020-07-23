using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class FPSUtilsComponent : BaseMonoBehaviour
{
    [Range(-1, 144)] public int fpsLimit = -1;

    private void Awake()
    {
        UpdateParameters();
        FPSUtils.Instance.fpsLimit.Subscribe(x => fpsLimit = x).AddTo(this);
    }

    [Button]
    void UpdateParameters()
    {
        FPSUtils.Instance.fpsLimit.Value = fpsLimit;
    }
}
