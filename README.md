# RoadToGrowth

# https://www.youtube.com/playlist?list=PLMZ4SZ4Qaji3c0mmmiluDhWtS31DNp7yu
# 위 링크는 각 챕터별 영상이 있는 링크입니다.

스크립트는 MonoBehaviour을 상속받는 스크립트, 랜덤 던전 생성 알고리즘에 쓰이는 스크립트, 오브젝트용 스크립트 , 게임 매니징용 스크립트, 적 상태 스크립트로 나뉘어 있습니다.

챕터1은 타일 팔레트를 이용해 타일을 찍어 맵을 구성한 후 캐릭터와 적을 만들었습니다.

일정 반경안에 들어오면 적은 반응해서 공격을 하고 플레이어가 일정 범위를 벗어나면 제자리로 돌아갑니다.

오브젝트 풀을 이용해 적이 사용하는 탄환을 관리 했습니다.

관련 스크립트는 https://github.com/Yeondi/RoadToGrowth/tree/master/Assets/Scripts/MonoBehaviour 이곳에서 
Weapon.cs / Ammo.cs / Enemy.cs / Character.cs 입니다.

https://github.com/Yeondi/RoadToGrowth/tree/master/Assets/Scripts/States 이곳엔 배회 관련 스크립트가 있습니다.


챕터2는 https://github.com/a327ex/blog/issues/7
해당 깃허브 링크를 기초로 짠 알고리즘입니다.
랜덤으로 스폰장소와 상점,보스 리젠 장소를 정했으며
사다리와 중간 연결 골목을 이용해 보스가 있는 방까지 이동합니다.
보스는 총 세번 되살아나며 클리어 후 통과하면 다음 챕터로 넘어갑니다.

관련 스크립트는 https://github.com/Yeondi/RoadToGrowth/tree/master/Assets/Scripts/PDG 의 스크립트들이며 대부분 PDG.cs에 들어있습니다.


캐릭터의 기본 움직임은 movementController.cs를 이용해 움직입니다.

2020년 c++을 기반으로한 cocos2d-x를 공부하다 4월경부터 c#을 공부했고 6월부터 유니티를 독학한 후 제작한 게임입니다.

유니티의 초반 동작원리를 이해하지 못해서 가장 기초적인 코드부분에서 나눠진 부분이 있습니다.

개인적으로 랜덤 던전 생성 관련한 부분에서 많은 어려움을 느꼈고, 어느 github의 도움을 받아 제작 원리를 개인 노트에 적어가며 꽤 오랜시간 공들여 작업했습니다.

처음엔 게임 '스펠렁키'처럼 각 방의 방향을 설정한 후 템플릿화해서 랜덤으로 찍어내는 방식으로 진행했고,

더 다양한 방식의 방을 원해서 현재 방식으로 바꾸게 되었습니다.

가장 시행착오를 겪었던 부분은 초반으로 범위 안에 사각형 방을 만든 후 나누는 과정에서 다소 시행착오를 겪었습니다.

https://atli-yeondi.tistory.com/category/%23%20%EC%95%8C%EA%B3%A0%EB%A6%AC%EC%A6%98 이곳에 일부 작업 일지들을 적어놓았습니다.

