# Unity3-Class

# GameFrameWork 구성 연습

# 기간

2026.04.13 ~ 

# 인원

1인

# 개발 환경

Unity6 (6000.4.2f1), URP, Windows, C#, 2D

# 개발 배경

기존까지 구조적인 프레임워크 없이 GameManager 한 곳에 공통기능을 구성하여 개발했던 점에 파편화 등의 문제점을 느꼈다.

AI툴킷을 활용하여 SingletonClass를 필두로 게임에 구성단계를 SceneManager -> ResourceManager -> DataManager -> UIManager 로 이어지는 유기적인 프레임워크 구성을 구축 하고 Scene전환 시 
데이터 구성 및 무결성 검증, IntroScene -> MainScene으로 넘어가는 로딩 창 등을 테스트해보고자 하였다.
