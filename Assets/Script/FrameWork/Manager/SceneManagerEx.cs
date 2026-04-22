using UnityEngine;
using UnityEngine.SceneManagement;
using DefinesEnum;

public class SceneManagerEx
{
    // 현재 활성화된 씬의 BaseScene 스크립트를 참조
    public BaseScene CurrentScene { get; private set; }

    // 외부에서 SceneManagerEx.Instance.CurrentSceneType으로 바로 접근 가능
    public EScene CurrentSceneType => (CurrentScene != null) ? CurrentScene.SceneType : EScene.TestScene;

    public void SetCurrentScene(BaseScene scene)
    {
        CurrentScene = scene;
        Debug.Log($"현재 씬이 {CurrentSceneType}으로 설정되었습니다.");
    }

    public void LoadScene(EScene type)
    {
        // 1. 이벤트 매니저를 통한 알림 (EEventType 활용)
        Managers.Event.TriggerEvent(EEventType.OnSceneExit, CurrentSceneType);

        // 씬 전환 시 기존 데이터 정리 (선택 사항)
        if (CurrentScene != null) CurrentScene.Clear();

        // 2. 씬 전환
        SceneManager.LoadScene(type.ToString());

        // 3. 전환 후 알림
        Managers.Event.TriggerEvent(EEventType.OnSceneEnter, type);
    }
}
