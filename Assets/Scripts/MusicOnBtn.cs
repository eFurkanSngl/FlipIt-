using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnBtn : UIBTN
{
    [SerializeField] private AudioSource _musicSource;
    protected override void OnClick()
    {
        MusicOn();
    }

    private void MusicOn()
    {
        _musicSource.Play();
    }
}
