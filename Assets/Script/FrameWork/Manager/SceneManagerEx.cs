using UnityEngine;
using DefinesEnum;

public class SceneManagerEx : GenericSingleton<SceneManagerEx>
{
    // 현재 활성화된 씬의 BaseScene 스크립트를 참조
    public BaseScene CurrentScene { get; private set; }

    // 외부에서 SceneManagerEx.Instance.CurrentSceneType으로 바로 접근 가능
    public EScene CurrentSceneType => (CurrentScene != null) ? CurrentScene.SceneType : EScene.IntroScene;

    public void SetCurrentScene(BaseScene scene)
    {
        CurrentScene = scene;
        Debug.Log($"현재 씬이 {CurrentSceneType}으로 설정되었습니다.");
    }

    public void LoadScene(EScene type)
    {
        // 씬 전환 시 기존 데이터 정리 (선택 사항)
        if (CurrentScene != null) CurrentScene.Clear();

        UnityEngine.SceneManagement.SceneManager.LoadScene(type.ToString());
    }
}
