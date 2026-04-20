using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData : IValidation
{
    public int id;
    public string name;
    public string type;
    public int hp;
    public int attack;
    public float speed;
    public float range;

    // 데이터 무결성 검사
    public bool Validate()
    {
        if (id <= 0) return false;
        if (string.IsNullOrEmpty(name)) return false;
        if (hp <= 0) return false;
        // 필요 시 공격력이나 사거리 등의 최소치 검증 로직 추가
        return true;
    }
}

[Serializable]
public class MonsterDataLoader : IDataLoader<int, MonsterData>
{
    // JSON의 "monsters" 키와 이름이 반드시 같아야 함
    public List<MonsterData> monsters = new List<MonsterData>();

    public Dictionary<int, MonsterData> MakeDict()
    {
        Dictionary<int, MonsterData> dict = new Dictionary<int, MonsterData>();
        foreach (MonsterData monster in monsters)
        {
            if (monster.Validate())
                dict.Add(monster.id, monster);
            else
                Debug.LogWarning($"[DataManager] 유효하지 않은 몬스터 데이터 발견: ID {monster.id}");
        }
        return dict;
    }
}
