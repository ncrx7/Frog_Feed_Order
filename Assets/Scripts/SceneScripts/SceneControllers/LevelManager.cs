using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int Level {get; private set;} = 1;
    private int _maxLevel = 3;

    //public Dictionary< int, List<GridGroup> > levelGridGroups = new Dictionary< int, List<GridGroup> >();
    public List<LevelData> LevelDatas= new List<LevelData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

       DontDestroyOnLoad(Instance);
    }

    private void IncreaseLevel()
    {
        if(Level++ >= _maxLevel)
            return;

        Level++;

        EventSystem.ChangeScene?.Invoke(1); //same scene
    }
}

[Serializable]
public class LevelData
{
    public int Level;
    public List<GridGroup> gridGroups;
}
