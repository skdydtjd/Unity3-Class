using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class IntroUI : Scene_UI
{
    // 프리팹 내의 실제 오브젝트 이름과 일치시킵니다.
    enum Texts
    {
        CompleteText
    }

    enum Images
    {
        ProgressBar
    }

    protected override void Init()
    {
        base.Init(); // Scene_UI의 초기화 로직 실행

        // 1. TextMeshProUGUI 타입의 모든 자식을 바인딩
        Bind<TextMeshProUGUI>();
        Bind<Image>();

        // 2. 초기 값 설정 (이름 기반으로 호출)
        var loadingBar = Get<Image>(Images.ProgressBar.ToString());
        if (loadingBar != null) loadingBar.fillAmount = 0f;

        var infoText = Get<TextMeshProUGUI>(Texts.CompleteText.ToString());
        if (infoText != null) infoText.text = "";
    }

    public void UpdateProgress(int step, int total, string message)
    {
        if (_objects.Count == 0) Init();

        // 3. 갱신 시에도 이름으로 호출
        Image loadingbar = Get<Image>(Images.ProgressBar.ToString());
        TextMeshProUGUI info = Get<TextMeshProUGUI>(Texts.CompleteText.ToString());

        float ratio = (float)step / total;

        if (loadingbar != null)
        {
            loadingbar.fillAmount = ratio;
            Canvas.ForceUpdateCanvases();
        }

        if (info != null)
        {
            info.text = $"{message} ({ Mathf.RoundToInt(ratio * 100)}%)";
            info.color = Color.green;

            if (step == total)
            {
                info.text = $"{message} ({Mathf.RoundToInt(ratio * 100)}%)\n<color=white><b>모든 초기화 작업이 완료되었습니다.<b>\n<b>아무 키를 눌러 씬을 전환하세요.</b></color>";
            }
        }
    }
}
