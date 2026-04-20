using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    // 타입별로 오브젝트들을 관리할 사전 (Key: 이름)
    protected Dictionary<Type, Dictionary<string, UnityEngine.Object>> _objects = new Dictionary<Type, Dictionary<string, UnityEngine.Object>>();

    protected abstract void Init();

    protected virtual void Awake()
    {
        Init();
    }

    // [핵심] 자식 오브젝트들을 타입별로 바인딩
    protected void Bind<T>() where T : UnityEngine.Object
    {
        if (!_objects.ContainsKey(typeof(T)))
            _objects.Add(typeof(T), new Dictionary<string, UnityEngine.Object>());

        // 자식들 중 해당 타입을 모두 찾음 (비활성화 된 것도 포함)
        T[] objects = GetComponentsInChildren<T>(true);

        foreach (T obj in objects)
        {
            if (!_objects[typeof(T)].ContainsKey(obj.name))
                _objects[typeof(T)].Add(obj.name, obj);
        }
    }

    // 이름으로 컴포넌트 가져오기
    protected T Get<T>(string name) where T : UnityEngine.Object
    {
        if (_objects.TryGetValue(typeof(T), out var dict))
        {
            if (dict.TryGetValue(name, out UnityEngine.Object obj))
                return obj as T;
        }
        return null;
    }

    // 자주 쓰는 래핑 함수들
    protected Text GetText(string name) => Get<Text>(name);
    protected Button GetButton(string name) => Get<Button>(name);
    protected Image GetImage(string name) => Get<Image>(name);

    protected TMPro.TextMeshProUGUI GetTMP(string name) => Get<TMPro.TextMeshProUGUI>(name);
}
