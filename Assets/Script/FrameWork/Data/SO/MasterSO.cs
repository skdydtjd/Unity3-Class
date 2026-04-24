using DefinesEnum;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "MasterConfig", menuName = "Configs/MasterConfig")]
public class MasterSO : ScriptableObject, IValidation, IDataLoader<EStatType, MasterSO.StatData>
{
    public string soLoadPath = "SO"; // 리소스 로드 경로
    // public string saveFileName = "SaveData.json"; // 세이브 파일명

    public List<StatData> statList = new List<StatData>();

    public bool Validate()
    {
        // 경로가 비어있지 않은지 검증
        bool isValid = !string.IsNullOrEmpty(soLoadPath);
        if(!isValid) 
            Debug.LogWarning("[MasterSO] soLoadPath가 비어있어 검증에 실패했습니다.");
        return isValid;
    }

    public Dictionary<EStatType, StatData> MakeDict()
    {
        Dictionary<EStatType, StatData> dict = new Dictionary<EStatType, StatData>();

        foreach (var stat in statList)
        {
            if (stat.Validate())
                dict.Add(stat.type, stat);
        }
        return dict;
    }

    [System.Serializable]
    public class StatData : IValidation
    {

        public EStatType type;
        public float value;
        public string description;

        public bool Validate() => type != EStatType.None;
    }
}
