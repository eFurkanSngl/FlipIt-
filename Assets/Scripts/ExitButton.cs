using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : UIBTN
{
    protected override void OnClick()
    {
        ClosedPanel();
    }

    private void ClosedPanel()
    {
        PausePanelEvents.PausePanelEvent?.Invoke();
    }

    protected override void RegisterEvents()
    {
        base.RegisterEvents();
        ExitButtonEvents.ExitButtonEvent += ClosedPanel;
    }

    protected override void UnRegisterEvents()
    {
        base.UnRegisterEvents();
        ExitButtonEvents.ExitButtonEvent -= ClosedPanel;
    }
}
