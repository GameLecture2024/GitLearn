# A안(심화) — Fork + 버그 분담 방식 (진짜 협업)

학생 전원이 **하나의 강사 레포(upstream)** 를 fork해서, **같은 코드베이스**를 함께 고치는 방식. 실무·오픈소스와 동일한 협업을 경험합니다. 기본 fork 메커니즘은 [A안 기본 문서](structure-A-fork-PR.md)를 먼저 참고하세요. 이 문서는 **다수 학생을 돌리기 위한 "분담 운영"** 에 집중합니다.

## 과제 자동 생성 (학생 수 N 입력)

분담표·버그·과제 카드를 손으로 만들 필요 없이, **학생 수만 입력하면 샘플이 생성**됩니다.
명세는 [과제 자동 생성기 설계안](과제생성기-설계안.md) 참고.

```
사용 예 (이 문서/설계안과 함께 명령):
  "7명에서 분담하여 만드는 계산기 샘플 만들어줘"
  "학생 12명, 날짜 유틸 과제로 생성해줘"
  "5명, 계산기, 난이도 쉬움 위주로"
```
생성 결과: 계산기 프로젝트 + `docs/assignments/student-01..NN.md`(학생별 과제 카드) + 분담표 + CI + PR 템플릿.
학생 수 > 버그 수이면 자동으로 **경쟁 모드**(여러 명이 같은 버그 → 베스트 PR 채택).

## 왜 "분담"이 필요한가
Fork 방식에서 학생 모두에게 같은 과제를 주면 **"30명이 똑같은 버그에 30개의 PR"** 을 보내는 혼란이 생깁니다.
→ **엣지 케이스(버그)를 학생마다 다르게 배정**하면, 각 PR이 서로 다른 부분을 고쳐서 실제 팀 협업처럼 굴러갑니다.

## 분담표 예시 (엣지 케이스 ↔ 담당)
| # | 엣지 케이스 | 대상 함수 | 담당(예) | 브랜치명 |
|---|---|---|---|---|
| 1 | 수량 0 → DivideByZeroException | `CalculateUnitPrice` | 학생 A | `fix/unit-price-divide-by-zero` |
| 2 | 음수 금액 입력 미검증 | `CalculateFinalPrice` | 학생 B | `fix/negative-amount-guard` |
| 3 | 할인율 1 초과 / 음수 | `CalculateFinalPrice` | 학생 C | `fix/discount-rate-range` |
| 4 | 반올림 정밀도(소수점) | `CalculateFinalPrice` | 학생 D | `fix/rounding-precision` |
| 5 | 쿠폰 중복 적용 방지 | (신규 로직) | 학생 E | `fix/coupon-duplication` |

> 인원이 많으면 **같은 버그를 2~3명이 각자 fork에서 풀고, 그중 베스트 PR을 함께 리뷰**하는 변형도 좋습니다(상호 리뷰 학습).

## 전체 흐름
```
upstream (강사: GameLecture2024/GitLearn)  ◄──── PR ────┐  (학생마다 다른 버그)
      │ fork                                            │
      ▼                                                 │
학생A/GitLearn   학생B/GitLearn   학생C/GitLearn ...     │
   (origin)         (origin)         (origin)           │
      └──── fix/... 브랜치에서 작업 → push ──────────────┘
```

## 1단계 — 강사(upstream) 준비
- [ ] upstream 레포 공개(또는 조직 내 학생 접근 권한) → 학생이 fork 가능해야 함
- [ ] `main` **브랜치 보호**: "Require a pull request before merging" + "Require approvals(1+)"
- [ ] **PR 템플릿**(`.github/PULL_REQUEST_TEMPLATE.md`) 제공
- [ ] **CI**(`.github/workflows`)로 PR마다 `dotnet test` 자동 실행 → 통과 전 리뷰 안 함
- [ ] `README`에 **분담표 공지**

## 2단계 — 학생 흐름 (각자)
```bash
# (1) 웹에서 upstream을 Fork → 내 계정에 복제본 생성

# (2) 내 fork를 clone
git clone https://github.com/<나>/GitLearn.git
cd GitLearn
git remote add upstream https://github.com/GameLecture2024/GitLearn.git

# (3) 배정받은 버그 브랜치 생성
git switch -c fix/negative-amount-guard

# (4) 실패 테스트 재현 → 커밋 → 수정 → 커밋 (red → green)
dotnet test
git add . && git commit -m "test: 음수 금액 입력 실패 케이스 추가"
# ...수정...
git add . && git commit -m "fix: 음수 금액 입력 시 예외 처리"

# (5) 내 fork(origin)로 push
git push -u origin fix/negative-amount-guard

# (6) PR 생성: base=GameLecture2024/GitLearn:main ← compare=<나>/GitLearn:fix/...
```

## 3단계 — 강사 리뷰·머지 (강사)
```bash
# upstream에 올라온 모든 PR 확인
gh pr list --repo GameLecture2024/GitLearn

# 특정 PR 리뷰
gh pr diff <번호> --repo GameLecture2024/GitLearn
gh pr review <번호> --repo GameLecture2024/GitLearn --comment -b "수정 요청: 경계값 테스트도 추가해 주세요"

# 통과하면 머지 (Squash 권장)
gh pr merge <번호> --repo GameLecture2024/GitLearn --squash --delete-branch
```

## 4단계 — 학생의 fork 최신화 (중요)
다른 학생 PR이 upstream에 머지되면, 내 fork도 동기화해야 충돌을 줄입니다.
```bash
git switch main
git fetch upstream
git merge upstream/main      # 또는 git rebase upstream/main
git push origin main         # 내 fork의 main도 최신화
```
> 💡 여기서 **머지 충돌이 나면** 그것 자체가 훌륭한 실습 교재입니다(실무 필수 스킬). 충돌 해결 → 커밋 → push.

## 리뷰 후속(코멘트 이후) 관리 원칙
- **새 PR 열지 말 것.** 같은 브랜치에 커밋 추가 → push하면 PR 자동 갱신.
- 학생: 코멘트에 답글 + 스레드 **Resolve** + **Re-request review**
- 강사: "Changes since last review"로 **바뀐 부분만** 재확인 → Approve
- 머지: **Squash merge** 권장 → `main`은 "버그 1개 = 커밋 1개", 왕복 과정은 PR 페이지에 보존

## 다수 학생 운영 체크리스트
- [ ] 버그 분담표 배포 (충돌·중복 PR 방지)
- [ ] `main` 브랜치 보호(PR 필수 + 승인 1+)
- [ ] CI 통과를 머지 조건으로 강제(status check)
- [ ] 상호 리뷰(peer review) 1회 의무화 → 강사 부하 분산
- [ ] 라벨(`리뷰대기`/`수정요청`/`승인`)로 상태 관리

## 장단점
| 장점 | 단점 |
|---|---|
| 실무·OSS와 100% 동일한 협업 경험 | remote 2개(origin/upstream) 개념 학습 필요 |
| 같은 코드 협업 + **충돌 해결** 학습 | 강사가 N개 PR 리뷰 (CI·상호리뷰로 완화) |
| 학생 수 사실상 무제한 | 분담/동기화 안내 필요 |
