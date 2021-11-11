# RoadToGrowth
[![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2FYeondi%2FRoadToGrowth&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)

# https://youtu.be/wboRtnjx_nE
# 위 링크는 각 챕터별 영상이 있는 링크입니다.

스크립트는 MonoBehaviour을 상속받는 스크립트, 랜덤 던전 생성 알고리즘에 쓰이는 스크립트, 오브젝트용 스크립트 , 게임 매니징용 스크립트, 적 상태 스크립트로 나뉘어 있습니다.

챕터1은 타일 팔레트를 이용해 타일을 찍어 맵을 구성한 후 캐릭터와 적을 만들었습니다.

일정 반경안에 들어오면 적은 반응해서 공격을 하고 플레이어가 일정 범위를 벗어나면 제자리로 돌아갑니다.

오브젝트 풀링을 이용해 적이 사용하는 탄환을 관리 했습니다.

관련 스크립트는 https://github.com/Yeondi/RoadToGrowth/tree/master/Assets/Scripts/MonoBehaviour 이곳에서 
Weapon.cs / Ammo.cs / Enemy.cs / Character.cs 입니다.

https://github.com/Yeondi/RoadToGrowth/tree/master/Assets/Scripts/States
이곳엔 배회 관련 스크립트가 있습니다.

GameManager를 비롯한 일부 카메라 스크립트, 상점 등 이러한 경우엔 혼선을 방지하기 위해
싱글턴 디자인 패턴을 사용했습니다.

챕터2는 https://github.com/a327ex/blog/issues/7
해당 깃허브 링크를 기초로 짠 알고리즘입니다.

일정한 방들을 절차적으로 생성하는 알고리즘입니다.
설정된 크기 내의 방을 랜덤으로 생성한 후 서로의 위치를 계산해 퍼뜨려 준 후
해당 크기의 방 안에 타일맵을 생성해 찍어내는 방식입니다.

랜덤으로 스폰장소와 상점,보스 리젠 장소를 정했으며
사다리와 중간 연결 골목을 이용해 보스가 있는 방까지 이동합니다.
보스는 총 세번 되살아나며 클리어 후 통과하면 다음 챕터로 넘어갑니다.

사다리 오르는 애니메이션이 존재하지 않아 따로 설정해두지 않았습니다.

관련 스크립트는 https://github.com/Yeondi/RoadToGrowth/tree/master/Assets/Scripts/PDG 의 스크립트들이며 대부분 PDG.cs에 들어있습니다.


챕터3의 경우 해당 캐릭터의 과거모습이라고 설정을 한 후 만들었습니다.
방향키의 값에 따라 특수스킬의 방향이 결정됩니다.
3.5초 후 자동으로 사라지는 검이고 특수스킬 시전 후 점프 키 입력시 해당 검 위치로 순간이동이 가능합니다.


캐릭터의 기본 움직임은 movementController.cs를 이용해 움직입니다.

2020년 c++을 기반으로한 cocos2d-x를 공부하다 4월경부터 c#을 공부했고 6월부터 유니티를 독학한 후 제작한 게임입니다.

유니티의 초반 동작원리를 이해하지 못해서 가장 기초적인 코드부분에서 나눠진 부분이 있습니다.

개인적으로 랜덤 던전 생성 관련한 부분에서 많은 어려움을 느꼈고, 어느 github의 도움을 받아 제작 원리를 개인 노트에 적어가며 꽤 오랜시간 공들여 작업했습니다.

처음엔 게임 '스펠렁키'처럼 각 방의 방향을 설정한 후 템플릿화해서 랜덤으로 찍어내는 방식으로 진행했고,

더 다양한 방식의 방을 원해서 현재 방식으로 바꾸게 되었습니다.

가장 시행착오를 겪었던 부분은 초반으로 범위 안에 사각형 방을 만든 후 나누는 과정에서 다소 시행착오를 겪었습니다.

https://atli-yeondi.tistory.com/category/%23%20%EC%95%8C%EA%B3%A0%EB%A6%AC%EC%A6%98 이곳에 일부 작업 일지들을 적어놓았습니다.

개발일지 일차별
1. https://atli-yeondi.tistory.com/28 [완성] Procedural Dungeon Generation

2. https://atli-yeondi.tistory.com/29 Procedural Dungeon Generation in Unity #1

3. https://atli-yeondi.tistory.com/30 Procedural Dungeon Generation in Unity #2

4. https://atli-yeondi.tistory.com/31 유니티 개발일지 + Procedural Dungeon Generation in Unity #3







============================= ENG ==========================================


https://youtu.be/wboRtnjx_nE 
The link above is a link with images for each chapter.



Scripts are divided into scripts that inherit MonoBehavior, scripts used by random dungeon generation algorithms, scripts for objects, scripts for game management, and enemy state scripts.

Chapter 1 made characters and enemies by using tile palettes to form maps.

If it comes within a certain radius, the enemy reacts and attacks, and if the player is outside a certain range, it returns to its original position.

Object pooling was used to manage bullets used by enemies.

The related scripts are: https://github.com/Yeondi/RoadToGrowth/tree/master/Assets/Scripts/MonoBehaviour here: Weapon.cs / Enemy.cs / Character.cs / Character.cs

https://github.com/Yeondi/RoadToGrowth/tree/master/Assets/Scripts/States Here's a script about roaming.

Some camera scripts, such as GameManager, and stores, have used singleton design patterns to avoid confusion.

Chapter 2 is an algorithm based on https://github.com/a327ex/blog/issues/7's GitHub link.

It is an algorithm that procedurally generates certain rooms. It is a method of randomly creating a room within a set size, calculating and spreading each other's locations, and then creating and printing a tile map in a room of that size.

Randomly choose a sponsor, store, boss regen location, and use the ladder and the intermediate alley to get to the boss's room. The boss is revived three times, cleared and passed to the next chapter.

I didn't set the ladder climb separately because there was no animation.

The scripts are from https://github.com/Yeondi/RoadToGrowth/tree/master/Assets/Scripts/PDG, most of which are located at PDG.cs.

In the case of Chapter 3, I made it after setting it as the character's past appearance. The value of the rudder key determines the direction of the special skill. It automatically disappears after 3.5 seconds, and you can teleport to the relevant sword position when you enter the jump key after a special skill test.

The character's basic movements are made using movementController.cs.

I studied cocos2d-x based on c++ in 2020 and studied c# from around April, and it is a game that I produced after studying Unity by myself from June.

Unity's initial principles of motion are not understood, so there are some parts that are divided in the most basic code part.

Personally, I had a lot of difficulties in creating random dungeons, and with the help of a github, I worked hard for quite a long time, writing down the principles of production in my personal notebook.

At first, we set the direction of each room like the game 'Spelunki', and then we template and print it randomly.

I wanted a more diverse room, so I changed it to the current one.

The most trial-and-error part was the initial trial and error in the process of creating a square room within the range and dividing it.

https://atli-yeondi.tistory.com/category/%23%20%EC%95%8C%EA%B3%A0%EB%A6%AC%EC%A6%98 I've written down some work logs here.

