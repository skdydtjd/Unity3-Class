using System;
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

    public enum EStatType
    {
        None = 0,
        MaxHp,      // hp
        Attack,     // attack
        MoveSpeed,  // speed
        AttackRange // range
    }
}
