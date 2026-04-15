using UnityEngine;
using DefinesEnum;

public class MainScene : BaseScene
{
    // 1. 이 클래스가 담당하는 씬 타입 정의
    public override EScene SceneType => EScene.MainScene;

    protected override void Awake()
    {
        base.Awake(); // 부모의 Awake를 호출하여 싱글톤에 등록되게 함
        Init();
    }

    protected override void Init()
    {
        // 씬 전환 후 싱글톤에 자신이 잘 등록되었는지 확인
        var current = SceneManagerEx.Instance.CurrentScene;
        Debug.Log($"[검증] 현재 싱글톤에 등록된 씬: {current.SceneType}");

        if (current == this)
            Debug.Log("<color=green>[성공] MainScene이 싱글톤에 정상 등록되었습니다.</color>");
    }

    // 예: 게임 시작 버튼 클릭 이벤트
    public void OnStartGame()
    {
        // 싱글톤을 활용하여 다음 씬으로 이동
        SceneManagerEx.Instance.LoadScene(EScene.GameScene);
    }

    public override void Clear()
    {
        base.Clear();
        Debug.Log("MainScene 전용 자원 해제");
    }
}
