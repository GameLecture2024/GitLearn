# C안 — GitHub Classroom 방식 (강사 1 : 학생 다수, 자동화)

학생 1인당 **개별 레포를 자동 생성**하고, 과제 수집·CI 자동 채점·진행 현황을 한 화면에서 관리하는 교육 특화 방식. 폴더 분리 방식의 상위 호환이며, 다수 학생 운영에 가장 권장됩니다.

## 한눈에 보는 구조
```
Template Repo (강사, 과제 원본: GitLearn)
        │  Classroom이 학생마다 복제
        ▼
student-1-GitLearn   student-2-GitLearn   student-3-GitLearn   ...
   (각 학생 전용 레포 · 완전 격리 · 본인만 push)
        │  각 학생이 자기 레포에서 브랜치→PR→머지 (셀프 PR)
        ▼
강사: Classroom 대시보드에서 전원 진행 현황 + 자동 채점(CI) 확인
```

## 사전 준비물
- **GitHub 조직(Organization)** — 무료로 생성 가능 (`github.com/organizations/new` → Free)
- **GitHub Classroom** 접속 — `classroom.github.com` (조직 연결)
- **Template 레포** — 과제 원본. 이 `GitLearn`을 Template으로 지정하면 됨

## 1단계 — Template 레포 준비
1. 과제용 레포(예: `GameLecture2024/GitLearn`)를 연다
2. `Settings > General` → **Template repository** 체크 ✅
3. 학생이 받을 상태로 정리:
   - 버그가 심어진 소스 + 실패 테스트(또는 주석 처리된 테스트)
   - `README`에 과제 지시 + `docs/` 교안
   - `.github/workflows/`에 자동 채점용 CI (아래 4단계)

## 2단계 — Classroom 만들기
1. `classroom.github.com` → **New classroom**
2. 과제를 받을 **조직 선택** → Classroom 이름 지정 (예: `Git-PR-2026`)
3. (선택) 학생 명단(roster) 업로드 — 학번/이름과 GitHub 계정 매핑

## 3단계 — Assignment(과제) 생성
1. Classroom에서 **New assignment** → **Individual assignment**
2. 설정:
   - **Assignment title**: 예 `edge-case-0-divide`
   - **Repository visibility**: Private 권장 (학생끼리 답 안 보이게)
   - **Repository prefix**: 예 `edge0` → 학생 레포명이 `edge0-<github계정>` 로 생성됨
   - **Template repository**: 위에서 만든 `GitLearn` 선택
3. **Online IDE** (선택): "GitHub Codespaces" 켜면 학생이 로컬 설치 없이 브라우저에서 바로 작업 가능
4. **Autograding** 설정(핵심, 4단계)
5. 생성되면 **초대 링크(invitation URL)** 가 나옴 → 학생들에게 배포

## 4단계 — 자동 채점(Autograding) 설정
두 가지 방법:

### 방법 1 — Classroom UI에서 테스트 추가 (간단)
- Assignment 설정의 **Add test** → "Run command" 선택
- Setup command: `dotnet restore`
- Run command: `dotnet test`
- 통과 시 점수 부여 → 학생이 push할 때마다 자동 채점

### 방법 2 — Actions 워크플로 직접 작성 (유연, 권장)
Template 레포에 `.github/workflows/classroom.yml`:
```yaml
name: Autograding
on: [push]
jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '10.0.x'
      - name: Run tests
        run: dotnet test --nologo
```
> Classroom은 이 결과를 읽어 자동 채점 표에 반영합니다. "테스트 초록 = 과제 통과"가 자동으로 판정됩니다.

## 5단계 — 학생 흐름 (학생 입장)
1. 강사가 준 **초대 링크 클릭** → 본인 전용 레포 `edge0-<내계정>` 자동 생성
2. 레포 clone (또는 Codespaces로 열기)
3. **셀프 PR 방식**으로 작업: 브랜치 → 실패 테스트 재현 → 수정 → PR → 머지
   - (Classroom은 학생별 레포라, 그 안에서는 [B안 셀프 PR](structure-B-self-PR.md)과 동일하게 진행)
4. push하면 Autograding이 자동 실행 → 통과/실패 즉시 확인

## 6단계 — 강사 관리
- **Classroom 대시보드**: 학생별 제출 여부·채점 점수·마지막 커밋을 한 표로 확인
- **Download repositories**: 전원 레포를 일괄 다운로드(채점/백업)
- 개별 피드백은 각 학생 레포의 PR/커밋에 코멘트로 남김

## 장단점 요약
| 장점 | 단점 |
|---|---|
| 학생 N명 레포 생성·수집·채점 **완전 자동화** | 초기 세팅(조직·Classroom·CI) 학습 필요 |
| 학생 간 완전 격리 (답 공유 차단) | 진짜 "협업"은 아님(각자 사본) → 협업은 [A안 Fork](structure-A-fork-분담.md) 병행 |
| CI 자동 채점으로 강사 리뷰 부하 ↓ | GitHub 조직 필요 |
| Codespaces로 환경 설치 문제 제거 | - |

## 언제 쓰나
- **정규 수업 / 부트캠프 / 다수 인원 + 자동 채점**이 필요할 때 1순위.
- "각자 독립적으로 전 과정을 연습 + 진도·점수 중앙 관리"가 목표면 최적.
