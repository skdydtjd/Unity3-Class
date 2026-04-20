using UnityEngine;
using TMPro;

public class TS_UI : Scene_UI
{
    // 프리팹 내의 실제 오브젝트 이름과 일치시킵니다.
    enum Texts
    {
        LoadingText,
        InfoText
    }

    protected override void Init()
    {
        base.Init(); // Scene_UI의 초기화 로직 실행

        // 1. TextMeshProUGUI 타입의 모든 자식을 바인딩
        Bind<TextMeshProUGUI>();

        // 2. 초기 값 설정 (이름 기반으로 호출)
        var loadingText = Get<TextMeshProUGUI>(Texts.LoadingText.ToString());
        if (loadingText != null) loadingText.text = "시스템 초기화 중...";

        var infoText = Get<TextMeshProUGUI>(Texts.InfoText.ToString());
        if (infoText != null) infoText.text = "";
    }

    public void UpdateProgress(int step, int total, string message)
    {
        if (_objects.Count == 0) Init();

        // 3. 갱신 시에도 이름으로 호출
        TextMeshProUGUI loading = Get<TextMeshProUGUI>(Texts.LoadingText.ToString());
        TextMeshProUGUI info = Get<TextMeshProUGUI>(Texts.InfoText.ToString());

        if (loading != null)
        {
            loading.text = $"진행 상황: {step} / {total}";
            Canvas.ForceUpdateCanvases();
        }

        if (info != null)
        {
            info.text = message;

            if (step == total)
            {
                info.text = $"<color=green>{message}</color>\n<b>모든 초기화 작업이 완료되었습니다.<b>\n<b>'F' 키를 눌러 씬을 전환하세요.</b>";
            }
        }
    }
}
