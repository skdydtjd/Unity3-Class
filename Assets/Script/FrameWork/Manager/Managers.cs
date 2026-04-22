using UnityEngine;

public class Managers : GenericSingleton<Managers>
{
    // --- [ 하위 매니저 인스턴스 ] ---
    // MonoBehaviour가 아닌 일반 클래스라면 직접 생성
    private readonly DataManager _data = new DataManager();
    private readonly ResourceManager _resource = new ResourceManager();
    private readonly SceneManagerEx _scene = new SceneManagerEx();
    private readonly UIManager _ui = new UIManager();
    private readonly EventManager _event = new EventManager();

    // --- [ 외부 접근용 프로퍼티 ] ---
    public static DataManager Data => Instance._data;
    public static ResourceManager Resource => Instance._resource;
    public static SceneManagerEx Scene => Instance._scene;
    public static UIManager UI => Instance._ui;
    public static EventManager Event => Instance._event;

    protected override void Awake()
    {
        // 부모의 Awake(싱글톤 설정 및 DontDestroyOnLoad) 호출
        base.Awake();
    }

    public void InitAllManagers()
    {
        // 각 매니저의 초기화 로직이 있다면 여기서 호출
        _resource.Init();
        _event.Init();
        _data.Init();

        Debug.Log("모든 매니저 초기화 완료");
    }

    public static void Clear()
    {
        Resource.Clear();
        // 기타 정리가 필요한 매니저들 호출
    }
}
