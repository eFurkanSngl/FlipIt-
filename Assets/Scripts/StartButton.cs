using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : UIBTN
{
    protected override void OnClick()
    {
        OnLoadScene();
    }

    private void OnLoadScene()
    {
        SceneManager.LoadScene("Level-Select");

    }
}
