using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int PlayerLevel { get; private set; } = 1;
    private int _maxPlayerLevel = 3;
    public int CellAmountCounter;

    //public Dictionary< int, List<GridGroup> > levelGridGroups = new Dictionary< int, List<GridGroup> >();
    public List<LevelData> LevelDatas = new List<LevelData>();

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

    public void IncreaseLevel()
    {
        if (PlayerLevel + 1 > _maxPlayerLevel)
            return;

        PlayerLevel++;

        EventSystem.ChangeScene?.Invoke(1); //same scene
    }

    public void ReduceCellAmount()
    {
        CellAmountCounter--;

        if (CellAmountCounter <= 0)
        {
            if (PlayerLevel == 3)
            {
                EventSystem.SwitchDefeatHudDisplay?.Invoke(HudType.FINISHEDGAME_HUD);
                EventSystem.PlaySoundClip?.Invoke(SoundType.WIN_ROUND);
            }
            else
            {
                EventSystem.SwitchDefeatHudDisplay?.Invoke(HudType.VICTORY_HUD);
                EventSystem.PlaySoundClip?.Invoke(SoundType.WIN_ROUND);
            }
        }
    }
}

[Serializable]
public class LevelData
{
    public int Level;
    public List<GridGroup> gridGroups;
}
