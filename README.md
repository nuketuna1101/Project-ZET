# ProjectZET
 Unity2D Topdown Miniproject (2025-04-18 ~ 2025-04-20)

# 과제 명세 주요 요구사항
- 하드코딩 지양 (데이터 변수에 직접할당 X)
- 씬에 선 배치 지양 (프리팹과 Resource Load 등을 활용)

(1) 2D Top-Down 방식의 게임 로직 구현
- 기본적인 WASD 컨트롤
- 자동 사냥 무기
- 몬스터 감지 공격 (Physics.Overlap)

(2) Object Pooling

(3) Monster 도감 UI 호출

## 주요 구현 사항

- 몬스터 (Enemy) 오브젝트는 풀링을 통해 관리됨
- DataManager : CSVReader를 통해 data table을 SO 파싱 및 id에 맞는 이미지 스프라이트 관리
- 본 과제의 특성을 고려하여, 서버와 관련한 데이터 직렬화/역직렬화 과정이 필요가 없기 때문에 고정된 데이터 자원 관리하는데 더 적합한 SO를 이용하도록 의사결정함.
- BaseSingleton 추상 클래스를 통한 핵심 매니저 클래스의 싱글턴 패턴 적용
- ZETUtils 정적 클래스를 통한 일부 벡터 연산 처리