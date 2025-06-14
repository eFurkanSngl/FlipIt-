using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOff : UIBTN
{
    [SerializeField] private AudioSource musicSource;
    protected override void OnClick()
    {
        MusifOff();
    }

    private void MusifOff()
    {
        musicSource.Stop();
    }
}

