﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ScoreEvents: MonoBehaviour
{
    public static UnityAction<int> ScoreEvent;
    public static UnityAction<int> CurrentLives;
}
