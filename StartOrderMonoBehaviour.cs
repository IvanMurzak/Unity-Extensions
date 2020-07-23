using UnityEngine;
using System.Collections;

public abstract class StartOrderMonoBehaviour : BaseMonoBehaviour
{
    private bool inited = false;

    public void Init()
    {
        if (!inited)
        {
            OnInit();
            inited = true;
        }
    }

    protected abstract void OnInit();

    protected virtual void Start()
    {
        Init();
    }
}
