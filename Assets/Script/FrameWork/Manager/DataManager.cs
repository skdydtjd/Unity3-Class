using DefinesEnum;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static MasterSO;
using static SpriteData;

// 데이터 파싱을 위한 인터페이스
public interface IDataLoader<Key, Item>
{
    // JSON 리스트를 Dictionary로 변환하여 반환
    Dictionary<Key, Item> MakeDict();
}

// 데이터 검증을 위한 인터페이스
public interface IValidation
{
    // 로드된 데이터가 유효한지 확인 (에러 방지)
    bool Validate();
}

public class DataManager
{
    // 런타임에 사용할 데이터 캐시 (예: 몬스터 스탯 사전)
    public Dictionary<int, MonsterData> _monsters = new Dictionary<int, MonsterData>();
    public Dictionary<int, SpriteData> _sprites = new Dictionary<int, SpriteData>();
    public Dictionary<EStatType, StatData> _stats = new Dictionary<EStatType, StatData>();

    // 설정 데이터 캐시
    public MasterSO _masterConfig;

    // 1. JSON 데이터 로드 및 가공
    public void LoadJson<Loader, Key, Item>(string path, ref Dictionary<Key, Item> dict)
        where Loader : IDataLoader<Key, Item>
    {
        TextAsset textAsset = Managers.Resource.Get<TextAsset>($"Data/{path}");
        if (textAsset == null) return;

        Loader loader = JsonUtility.FromJson<Loader>(textAsset.text);
        dict = loader.MakeDict();

        // 검증 로직 실행
        ValidateData(dict);
    }

    // 2. ScriptableObject 로드
    public T LoadScriptableObject<T>(string path) where T : ScriptableObject, IValidation
    {
        T so = Managers.Resource.Get<T>($"Data/{path}");
        if (so != null && so.Validate())
        {
            Debug.Log($"[DataManager] SO 로드 완료: {path}");
            return so;
        }
        return null;
    }

    // 3. GameObject 및 프리팹 관련 데이터 로드 (컴포넌트 검증 포함)
    public GameObject LoadData(string path)
    {
        GameObject go = Managers.Resource.Get<GameObject>(path);
        if (go == null) return null;

        // 예: 프리팹에 필수 컴포넌트가 있는지 검증하는 로직 추가 가능
        return go;
    }

    // 데이터 검증 헬퍼
    private void ValidateData<K, V>(Dictionary<K, V> dict)
    {
        foreach (var item in dict.Values)
        {
            if (item is IValidation validationItem)
            {
                if (!validationItem.Validate())
                    Debug.LogWarning($"[DataManager] 데이터 검증 실패 항목 발견!");
            }
        }
    }

    public void Init()
    {
        // 1. MasterSO 로드
        _masterConfig = LoadScriptableObject<MasterSO>("SO/Monsters");

        // 2. SO를 통해 Stats 딕셔너리 초기화
        if (_masterConfig != null)
        {
            _stats = _masterConfig.MakeDict();
            Debug.Log($"[DataManager] {_stats.Count}개의 스탯 데이터 로드 완료.");
        }

        // 2. JSON 로드 및 가공
        // Resources/Data/MonsterJson.json 파일을 읽어와 Monsters 딕셔너리를 채움
        LoadJson<MonsterDataLoader, int, MonsterData>("MonsterData", ref _monsters);
        //LoadJson<SpriteDataLoader, int, SpriteData>("SpriteData", ref _sprites);


        Debug.Log($"[DataManager] {_monsters.Count}개의 몬스터 데이터 로드 완료.");
    }
}
