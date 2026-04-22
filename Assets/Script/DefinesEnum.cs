using UnityEngine;

namespace DefinesEnum
{
    public enum EScene
    {
        TestScene,
        IntroScene,
        MainScene,
        GameScene,
        ResultScene
    }

    public enum EEventType
    {
        OnSceneEnter,
        OnSceneExit,
        LevelUpEvent,
        LearnSkillEvent,
        AttackEvent,
        DieEvent
    }
}
