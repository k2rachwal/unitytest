using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Create Game Settings", order = 1)]
public class GameSettingsDatabase : ScriptableObject
{ 
    [Header("Prefabs")]
    public GameObject TargetPrefab;
    public List<Vector3> Positions;
}