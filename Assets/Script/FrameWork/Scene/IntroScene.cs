using DefinesEnum;
using System.Collections;
using System.Text;
using UnityEngine;

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
            VerifyMonsterData();

            SceneManagerEx.Instance.LoadScene(EScene.MainScene);
        }
    }

    private bool IsMouseClick() => Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);

    private void VerifyMonsterData()
    {
        StringBuilder report = new StringBuilder();
        report.AppendLine("<color=orange><b>===== DataManager Full Inventory Report =====</b></color>");

        // 1. 데이터 로드 여부 및 전체 개수 확인
        var monsters = DataManager.Instance.Monsters;
        bool isLoaded = (monsters != null && monsters.Count > 0);

        report.AppendLine($"[시스템 상태] 로드 완료: {(isLoaded ? "<color=green>YES</color>" : "<color=red>NO</color>")}");
        report.AppendLine($"[데이터 통계] 총 몬스터 수: {monsters.Count}개");
        report.AppendLine("--------------------------------------------------");

        if (isLoaded)
        {
            // 2. 모든 데이터 전수 출력
            foreach (var item in monsters)
            {
                int id = item.Key;
                MonsterData data = item.Value;

                // 데이터 무결성 체크 (IValidation 활용)
                string validTag = data.Validate() ? "<color=green>[정상]</color>" : "<color=red>[결함]</color>";

                report.AppendLine($"{validTag} <b>ID: {id}</b> | <b>Name: {data.name}</b>");
                report.AppendLine($"      - Type: {data.type}");
                report.AppendLine($"      - Stats: HP({data.hp}), ATK({data.attack}), SPD({data.speed}), RNG({data.range})");
                report.AppendLine("--------------------------------------------------");
            }
        }
        else
        {
            report.AppendLine("<color=red>표시할 데이터가 없습니다. JSON 파일이나 경로를 확인하세요.</color>");
        }

        report.AppendLine("<color=orange><b>===========================================</b></color>");

        // 최종 결과 출력
        Debug.Log(report.ToString());

        // 데이터가 정상 로드되었을 때만 다음 씬으로 전환 가능하게 처리
        if (isLoaded)
        {
            Debug.Log("<color=cyan>전체 데이터 검증 완료. MainMenu으로 이동할 준비가 되었습니다.</color>");
        }
    }
}
