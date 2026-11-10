# GitLearn — PR로 배우는 Git 협업 (C#)

작은 C# 프로젝트(**장바구니 할인 계산기**)에 **엣지 케이스 버그를 의도적으로 심어 두고**,
그 버그를 **Pull Request 단위로 하나씩 해결**하며 Git/PR 워크플로를 배우는 튜토리얼입니다.
PR 히스토리 자체가 "문제 → 원인 → 해결"의 교재가 됩니다.

## 학습 목표
1. 브랜치 → PR → 리뷰 → 머지의 한 바퀴를 손에 익힌다
2. 엣지 케이스를 **실패하는 테스트(red)** 로 재현하고, 고쳐서 **초록(green)** 으로 만든다
3. 읽기 좋은 **PR 작성법**(제목·설명·범위·리뷰 대응)을 체득한다

## 진행 구조 (3가지 — 상황에 맞게 선택)
먼저 **[운영 방식 가이드](docs/운영방식-가이드.md)** 에서 비교·선택하세요.
- **B안 — 셀프 PR** — 개인 레포에서 자기 자신에게 PR. 입문·자습에 최적. → [`docs/structure-B-self-PR.md`](docs/structure-B-self-PR.md)
- **A안 — Fork 분담** — 강사 레포 ↔ 학생 fork, 버그를 학생별 배정해 진짜 협업. → [`docs/structure-A-fork-분담.md`](docs/structure-A-fork-분담.md) (기본: [`structure-A-fork-PR.md`](docs/structure-A-fork-PR.md))
- **C안 — GitHub Classroom** — 학생별 레포 자동 생성 + CI 자동 채점. 다수 수업용. → [`docs/structure-C-github-classroom.md`](docs/structure-C-github-classroom.md)

## 프로젝트 구조
```
GitLearn.slnx
├─ src/DiscountCalculator/          # 할인 계산 로직 (버그가 심어져 있음)
│   └─ DiscountService.cs
└─ tests/DiscountCalculator.Tests/  # xUnit 테스트
    └─ DiscountServiceTests.cs
```

## 빠른 시작
```bash
dotnet test        # 현재는 2개 통과(green) 상태
```

## 차시 교안
- [0차시 — 준비와 멘탈 모델](docs/00-준비.md)
- [1차시 — 첫 PR 왕복](docs/01-첫PR.md)
- [2차시 — 엣지 케이스 #1: 0으로 나누기](docs/02-엣지케이스-0나누기.md)

> 3차시 이후(음수 금액, 반올림 오차, 쿠폰 중복 …)는 2차시와 동일한 리듬으로 확장합니다.

## 수강 노트
- 1차시: 첫 PR 한 바퀴 — branch → commit → push → PR → diff → merge → 브랜치 삭제
