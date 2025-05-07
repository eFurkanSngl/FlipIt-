using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeBtn : UIBTN
{
    protected override void OnClick()
    {
        OnLoadMainMenu();
    }

    private void OnLoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
