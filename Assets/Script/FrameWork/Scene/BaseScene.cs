using UnityEngine;
using DefinesEnum;

public abstract class BaseScene : MonoBehaviour
{
    public abstract EScene SceneType { get; }

    protected virtual void Awake()
    {
        // 싱글톤 매니저를 활용하여 현재 씬 등록
        Managers.Scene.SetCurrentScene(this);
    }

    protected abstract void Init();
    public virtual void Clear() => Debug.Log($"{SceneType} 자원 정리 중...");
}
