using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 사용 시

public class TestSceneUI : Scene_UI
{
    // 이미지의 각 요소들을 Enum으로 정의 (이름은 프리팹 자식 오브젝트명과 일치해야 함)
    enum Texts
    {
        TitleText,          // "Leaderboard"

    }

    enum Images
    {
        ProfilePreview,     // 캐릭터 미리보기 이미지
    }

    enum Buttons
    {
        BackButton,         // 상단 뒤로가기 버튼
    }

    protected override void Init()
    {
        base.Init(); // Scene_UI의 캔버스 설정 등 실행

        // 1. UI_Base의 Bind 로직을 통해 각 컴포넌트 자동 바인딩
        Bind<TextMeshProUGUI>();
        Bind<Image>();
        Bind<Button>();

        // 2. 초기 상태 설정
        GetTMP(Texts.TitleText.ToString()).text = "Title";

        // 3. 버튼 이벤트 연결 예시
        GetButton(Buttons.BackButton.ToString()).onClick.AddListener(() => {
            Debug.Log("뒤로 가기 버튼이 눌러졌습니다.");
        });
    }

    // 로딩 상황 갱신 메서드
    //public void UpdateProgress(int step, int total, string message)
    //{
    //    var title = GetTMP(Texts.TitleText.ToString());
    //    if (title == null) return;

    //    // 메인 타이틀에 진행도와 메시지를 통합해서 표시
    //    title.text = $"({step}/{total})\n{message}";

    //    if (step == total)
    //    {
    //        title.text = $"<color=green>READY!</color>\nPRESS 'Any Key'";
    //    }
    //}
}
