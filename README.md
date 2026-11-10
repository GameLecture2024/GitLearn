# GitLearn — PR로 배우는 Git 협업 (C#)

작은 C# 프로젝트(**장바구니 할인 계산기**)에 **엣지 케이스 버그를 의도적으로 심어 두고**,
그 버그를 **Pull Request 단위로 하나씩 해결**하며 Git/PR 워크플로를 배우는 튜토리얼입니다.
PR 히스토리 자체가 "문제 → 원인 → 해결"의 교재가 됩니다.

## 학습 목표
1. 브랜치 → PR → 리뷰 → 머지의 한 바퀴를 손에 익힌다
2. 엣지 케이스를 **실패하는 테스트(red)** 로 재현하고, 고쳐서 **초록(green)** 으로 만든다
3. 읽기 좋은 **PR 작성법**(제목·설명·범위·리뷰 대응)을 체득한다

## 두 가지 진행 구조
- **B안 (먼저 진행)** — 개인 레포에서 **자기 자신에게 PR**. 마찰이 적어 메커니즘 익히기에 최적. → [`docs/structure-B-self-PR.md`](docs/structure-B-self-PR.md)
- **A안 (나중에 진행)** — **강사 레포 ↔ 학생 fork** 로 실제 협업. 다중 학생 지원. → [`docs/structure-A-fork-PR.md`](docs/structure-A-fork-PR.md)

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
