using UnityEngine;

// 인벤토리, 설정창, 경고창 등 필요할 때 띄우고 닫는 UI입니다.
// 스택(Stack) 구조로 관리되어, '뒤로 가기' 버튼을 누르면 가장 최근에 뜬 팝업부터 닫히는 로직이 들어갑니다.

public class PopUp_UI : UI_Base
{
    protected override void Init()
    {

    }

    public virtual void ClosePopup()
    {
        // 팝업 닫기 로직 (보통 UIManager를 통해 삭제)
        Managers.Resource.Destroy(gameObject);
    }
}
