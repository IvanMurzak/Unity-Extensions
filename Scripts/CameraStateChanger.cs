using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraStateChanger : BaseMonoBehaviour
{
    public bool IsValidTrigger(string triggerName)
    {
        return GetComponent<Animator>().parameters.Any(x =>
        {
            return x.type == AnimatorControllerParameterType.Trigger
                && triggerName.Equals(x.name);
        });
    }

    [Button(ButtonSizes.Large)] public void TutorialNext(string cameraStateTrigger)
    {
        animator.SetTrigger(cameraStateTrigger);
    }
}
