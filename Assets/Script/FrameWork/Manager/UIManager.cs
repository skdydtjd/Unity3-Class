using UnityEngine;

public class UIManager : GenericSingleton<UIManager>
{
    private int _order = 10; // 팝업 등의 소팅 오더 관리용
    private GameObject _uiRoot = null;

    // UI가 붙을 최상위 루트 오브젝트 (없으면 생성)
    public GameObject Root
    {
        get
        {
            if (_uiRoot == null)
            {
                _uiRoot = GameObject.Find("@UI_Root");

                if (_uiRoot == null)
                {
                    // 1. 루트 생성 시 이름을 바로 고정하여 Find 중복 방지
                    _uiRoot = ResourceManager.Instance.Instantiate("Prefabs/UI/TestSceneUI/@UI_Root");

                    if (_uiRoot != null) 
                        _uiRoot.name = "@UI_Root";
                }
            }
            return _uiRoot;
        }
    }

    // Scene UI를 생성하고 관리하는 핵심 함수
    public T ShowSceneUI<T>(string name = null) where T : Scene_UI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name; // 클래스 이름을 프리팹 이름으로 간주

        // 리소스 매니저를 통해 UI 프리팹 생성
        // 경로 규칙: UI/Scene/프리팹이름
        string path = $"Prefabs/UI/TestSceneUI/SceneUI/{name}";

        GameObject go = ResourceManager.Instance.Instantiate(path);
        T sceneUI = go.GetComponent<T>();

        // UI_Root 하위로 배치
        if (Root != null)
        {
            // [핵심 1] 부모를 설정할 때 worldPositionStays를 false로 주어 
            // 프리팹에 설정된 로컬 좌표(0,0,0)를 강제 적용합니다.
            go.transform.SetParent(Root.transform, false);

            // [핵심 2] RectTransform 초기화
            RectTransform rect = go.GetComponent<RectTransform>();
            if (rect != null)
            {
                // 앵커(Anchor)를 전체 채우기(Stretch)로 설정하고 싶을 때 주로 쓰는 코드
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.offsetMin = Vector2.zero; // Left, Bottom
                rect.offsetMax = Vector2.zero; // Right, Top

                // 일반적인 중앙 정렬 시 로컬 좌표 초기화
                rect.localPosition = Vector3.zero;
                rect.localScale = Vector3.one;
            }
        }

        return sceneUI;
    }

    // (참고) 팝업 UI용 함수도 미리 구성
    public T ShowPopupUI<T>(string name = null) where T : PopUp_UI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = ResourceManager.Instance.Instantiate($"UI/Popup/{name}");
        T popup = go.GetComponent<T>();

        go.transform.SetParent(Root.transform);

        // 팝업은 소팅 오더를 높여서 다른 UI보다 위에 오게 설정 가능
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.sortingOrder = _order++;

        return popup;
    }
}
