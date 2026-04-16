using DefinesEnum;
using System.Text;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    void Start()
    {
        // 1. ResourceManager 준비 (Instance 호출 시 자동 생성)
        ResourceManager rm = ResourceManager.Instance;
        DataManager dm = DataManager.Instance;

        // 2. LoadAll을 사용하여 'Resources/Data' 폴더 내의 모든 TextAsset(JSON 포함)을 미리 로드
        // 이렇게 하면 폴더 내의 모든 파일이 ResourceManager의 Dictionary 캐시에 저장됩니다.
        rm.LoadAll<TextAsset>("Data");
        dm.Init();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 3. 데이터 로드 및 검증 (이미 LoadAll로 캐싱되었으므로 Get은 내부 캐시에서 바로 반환함)
            VerifyMonsterData();

            // 4. 메인 씬으로 이동
            SceneManagerEx.Instance.LoadScene(EScene.MainScene);
        }
    }

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
            Debug.Log("<color=cyan>전체 데이터 검증 완료. MainScene으로 이동할 준비가 되었습니다.</color>");
        }
    }
}
