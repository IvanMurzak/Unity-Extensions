using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class FPSUtils
{
    static FPSUtils instance;
    public static FPSUtils Instance { get { return instance ?? (instance = new FPSUtils()); } }

    [NonSerialized, ShowInInspector] public IntReactiveProperty fpsLimit = new IntReactiveProperty(-1);

    private FPSUtils()
    {
        fpsLimit.Subscribe(x => Application.targetFrameRate = x);
    }
}
