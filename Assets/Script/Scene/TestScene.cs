using UnityEngine;
using DefinesEnum;

public class TestScene : MonoBehaviour
{
    void Start()
    {
        // 1. ResourceManager 준비 (Instance 호출 시 자동 생성)
        ResourceManager rm = ResourceManager.Instance;

        // 2. LoadAll을 사용하여 'Resources/Data' 폴더 내의 모든 TextAsset(JSON 포함)을 미리 로드
        // 이렇게 하면 폴더 내의 모든 파일이 ResourceManager의 Dictionary 캐시에 저장됩니다.
        rm.LoadAll<TextAsset>("Data");

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            // 3. 데이터 로드 및 검증 (이미 LoadAll로 캐싱되었으므로 Get은 내부 캐시에서 바로 반환함)
            VerifyMonsterData();

            // 4. 메인 씬으로 이동
            SceneManagerEx.Instance.LoadScene(EScene.MainScene);
        }
    }

    private void VerifyMonsterData()
    {
        // Get<T>를 통해 로드된 TextAsset을 가져옵니다. 
        // 경로는 Resources/Data/MonsterJson.json 기준입니다.
        TextAsset jsonAsset = ResourceManager.Instance.Get<TextAsset>("Data/MonsterJson");

        if (jsonAsset != null)
        {
            // 색상을 넣어 눈에 띄게 출력합니다.
            Debug.Log("<color=green><b>[JSON 로드 성공]</b></color>");
            Debug.Log($"<color=white>내용물:</color>\n{jsonAsset.text}");
        }
        else
        {
            // 로드 실패 시 에러 메시지
            Debug.LogError("<color=red>[JSON 로드 실패]</color> 'Resources/Data/MonsterJson' 파일을 찾을 수 없습니다.");
        }
    }
}
