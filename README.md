# RoadToGrowth

#### 2D Side-Scrolling RPG
1. 간단한 소개
2. 주요 기술


#### 1. 간단한 소개
장르 : 2D Side-Scrolling RPG
엔진 : Unity
플랫폼 : 모바일(Android)
개발기간 : 약 6개월

챕터를 총 3개의 챕터로 나누어 개인적으로 해보고싶었던 기술이나 방식들을 구현했습니다.

- 싱글턴 패턴
- 오브젝트 풀링
- 절차 맵 생성 알고리즘
- Finite State Machine

#### 2. 주요 기술

- 1 챕터
1 챕터의 경우 지금까지 학습한 유한상태기계, 매니저를 통한 오브젝트관리 ( 싱글턴 디자인패턴 )등을 구현했습니다.
또 오브젝트 풀링을 이용해 Enemy가 사용하는 탄환을 관리하여 불필요한 메모리 낭비를 방지했습니다.
GameManager를 비롯한 일부 카메라 스크립트, 상점 등 이러한 경우엔 혼선을 방지하기 위해
싱글턴 디자인 패턴을 사용했습니다.

![img](https://user-images.githubusercontent.com/51913393/158007793-d16ea8e0-b1a4-4c71-834d-dce1b5163950.gif)

![img (1)](https://user-images.githubusercontent.com/51913393/158007795-ac262d29-0868-4ffb-a47b-49e32bbc90d1.gif)

![img (2)](https://user-images.githubusercontent.com/51913393/158007797-4b480a68-c53e-4537-89e0-4f5816c8f33a.gif)

- 2챕터
2 챕터의 경우 로그라이크 방식의 던전에 흥미를 느껴 매 던전 입장시마다 고정된 패턴을 제외하곤 모두 랜덤으로 생성되는 방과 통로를 만들어 진행되게 했습니다.
고정되는 요소는 몹의 수, 보스의 수, 방의 개수 등입니다.
기존 기획은 마을에서 정비를하고 2챕터인 해당 던전에 들어가서 캐릭터를 성장시키며 나아가는 방식으로 매번 던전을 다르게 구성하여 유저에게 지루함이 느껴지지 않는 전투를 생각했습니다.

![img (3)](https://user-images.githubusercontent.com/51913393/158007798-42e0ccf0-94e3-471d-bb95-6a0002b5a73c.gif)


- 3챕터
해당 챕터는 단순히 해보고 싶었던 기능을 조촐하게 구성했습니다. 날아가는 오브젝트와 캐릭터의 위치를 바꾸는 것.





개발일지 일차별
1. https://atli-yeondi.tistory.com/28 [완성] Procedural Dungeon Generation

2. https://atli-yeondi.tistory.com/29 Procedural Dungeon Generation in Unity #1

3. https://atli-yeondi.tistory.com/30 Procedural Dungeon Generation in Unity #2

4. https://atli-yeondi.tistory.com/31 유니티 개발일지 + Procedural Dungeon Generation in Unity #3

5. https://atli-yeondi.tistory.com/35 Procedural Dungeon Generation in Unity #4

영상 : https://youtube.com/playlist?list=PLMZ4SZ4Qaji3c0mmmiluDhWtS31DNp7yu

