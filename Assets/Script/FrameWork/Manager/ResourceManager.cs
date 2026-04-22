using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    // 로드된 리소스를 관리하는 캐시 (Key: 경로 또는 이름)
    private Dictionary<string, Object> _resources = new Dictionary<string, Object>();

    public void Init()
    {
        LoadAll<TextAsset>("Data");
        LoadAll<GameObject>("Prefabs");
    }

    // 1. 특정 폴더 내의 모든 리소스 로드
    public void LoadAll<T>(string path) where T : Object
    {
        T[] loadedResources = Resources.LoadAll<T>(path);

        foreach (T resource in loadedResources)
        {
            // 중복 키 체크 후 등록 (Key는 "폴더명/리소스명")
            string key = string.IsNullOrEmpty(path) ? resource.name : $"{path}/{resource.name}";

            if (!_resources.ContainsKey(key))
            {
                _resources.Add(key, resource);
            }
        }

        Debug.Log($"[ResourceManager] {path} 내 {loadedResources.Length}개 리소스 로드 완료.");
    }

    // 2. 단일 리소스 가져오기 (캐시에 없으면 즉시 로드)
    public T Get<T>(string path) where T : Object
    {
        if (_resources.TryGetValue(path, out Object resource))
        {
            return resource as T;
        }

        // 캐시에 없으면 Resources.Load 시도
        T loadedResource = Resources.Load<T>(path);
        if (loadedResource != null)
        {
            _resources.Add(path, loadedResource);
            return loadedResource;
        }

        Debug.LogError($"[ResourceManager] 리소스를 찾을 수 없습니다: {path}");
        return null;
    }

    // 3. 프리팹 생성 (Instantiate)
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Get<GameObject>(path);
        if (prefab == null) return null;

        return Object.Instantiate(prefab, parent);
    }

    // 4. 오브젝트 파괴 (Destroy)
    public void Destroy(GameObject go)
    {
        if (go == null) return;
        Object.Destroy(go);
    }

    // 5. 특정 리소스 혹은 전체 캐시 비우기 (Clear)
    public void Clear()
    {
        _resources.Clear();
        Debug.Log("[ResourceManager] 모든 캐시가 비워졌습니다.");
    }

    // 6. 사용하지 않는 리소스까지 메모리에서 완전히 해제 (ReleaseAll)
    public void ReleaseAll()
    {
        Clear();
        // Resources.Load로 불러왔던 에셋들을 메모리에서 해제
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        Debug.Log("[ResourceManager] 모든 리소스가 메모리에서 해제되었습니다.");
    }
}
