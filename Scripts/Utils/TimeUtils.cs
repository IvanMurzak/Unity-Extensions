using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TimeUtils
{
    static TimeUtils instance;
    public static TimeUtils Instance { get { return instance ?? (instance = new TimeUtils()); } }

    [NonSerialized, ShowInInspector] public FloatReactiveProperty timeScale = new FloatReactiveProperty(1);

    private TimeUtils()
    {
        timeScale.Subscribe(x => Time.timeScale = x);
    }
}
