using UnityEngine;

public class UIManager : GenericSingleton<UIManager>
{
    private int _order = 10; // Sorting Order 관리

    // 팝업 생성 예시
    public T ShowPopupUI<T>(string name = null) where T : PopUp_UI
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = ResourceManager.Instance.Instantiate($"UI/Popup/{name}");
        T popup = go.GetComponent<T>();

        // 캔버스 설정 및 Order 조절 로직 추가 가능
        return popup;
    }
}
