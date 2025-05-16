using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : UIBTN
{
    protected override void OnClick()
    {
        Restart();
    }


    private void Restart()
    {
        RestartButtonEvents.RestartEvents?.Invoke();
    }
}
