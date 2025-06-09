using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : UIBTN
{
    protected override void OnClick()
    {
        LoadNextlevel();
        Debug.Log("New Level");
    }

    private void LoadNextlevel()
    {
        SceneManager.LoadScene("Level-Select");

    }
}
