using UnityEngine;
using System.Collections;
using DefinesEnum;

public class IntroScene : MonoBehaviour
{
    private IntroUI _ui; // 로딩 전용 UI로 활용
    private bool _isLoaded = false;

    void Start()
    {
        // 1. 인트로 전용 UI 생성 (ShowSceneUI 사용)
        _ui = UIManager.Instance.ShowSceneUI<IntroUI>("Intro_UI");

        // 2. 초기화 프로세스 시작
        StartCoroutine(LoadingProcess());
    }

    private IEnumerator LoadingProcess()
    {
        ResourceManager rm = ResourceManager.Instance;
        DataManager dm = DataManager.Instance;

        // [Step 1] 리소스 로드
        rm.LoadAll<TextAsset>("Data");
        rm.LoadAll<GameObject>("Prefabs");

        _ui.UpdateProgress(1, 2, "Resources Loaded...");
        yield return new WaitForSeconds(0.5f);

        // [Step 2] 데이터 가공
        dm.Init();

        _ui.UpdateProgress(2, 2, "Data Initialized...");
        yield return new WaitForSeconds(0.5f);

        _isLoaded = true;
        Debug.Log("<color=cyan>[Intro] Ready! Press Any Key to Start.</color>");
    }

    private void Update()
    {
        // 로딩 완료 후 입력 대기
        if (_isLoaded && Input.anyKeyDown && !IsMouseClick())
        {
            SceneManagerEx.Instance.LoadScene(EScene.MainScene);
        }
    }

    private bool IsMouseClick() => Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
}
