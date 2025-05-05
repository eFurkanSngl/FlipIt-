using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EventListenerMono : MonoBehaviour
{
    private void Start()
    {
        RegisterEvents();
    }
    private void OnEnable()
    {
        RegisterEvents();
    }

    private void OnDisable()
    {
        UnRegisterEvents();
    }

    protected abstract void RegisterEvents();
    protected abstract void UnRegisterEvents();
    
}