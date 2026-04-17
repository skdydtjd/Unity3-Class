using UnityEngine;

// 각 씬마다 하나씩 존재하며, 게임의 배경이나 메인 HUD(체력 바, 미니맵 등)를 담당합니다.
// 보통 삭제되지 않고 씬과 운명을 같이 합니다.

public abstract class Scene_UI : UI_Base
{
    protected override void Init()
    {
        // Scene UI는 기본적으로 캔버스 설정을 잡습니다.
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
            canvas.sortingOrder = 0;
        }
    }
}
