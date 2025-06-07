using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level Data", menuName ="FlipIt/LevelData")]
public class LevelData : ScriptableObject
{
    public int gridX;
    public int gridY;
    public List<Vector2Int> activeCells;
}
