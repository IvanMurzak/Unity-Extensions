using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class SuperBaseComponent : SerializedMonoBehaviour
{
    private bool inited = false;
    protected void OnInit() { }

    private void Init()
    {
        if (!inited)
        {
            OnInit();
            inited = true;
        }
    }

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void OnEnable() { }


    protected virtual void OnDisable() { }
}
