# A안 — 강사 레포 ↔ 학생 Fork (나중에 진행)

강사가 **upstream 레포**를 소유하고, 학생은 그것을 **fork**(복제)해서 자기 fork의 브랜치에서 작업한 뒤 **강사 레포로 PR**을 보냅니다. 실제 오픈소스 기여 흐름과 100% 동일합니다.

## remote 두 개의 멘탈 모델
```
upstream (강사 소유, 원본)  ◄── PR ──┐
                                      │
origin (내 fork, 내 소유) ──push──► feature/...  ← 여기서 작업
```
```bash
# 학생: 강사 레포를 fork 한 뒤
git clone https://github.com/<나>/GitLearn.git
cd GitLearn
git remote add upstream https://github.com/<강사>/GitLearn.git

# 작업
git switch -c fix/unit-price-divide-by-zero
# ...commit...
git push -u origin fix/unit-price-divide-by-zero
# GitHub에서 PR: base = 강사/GitLearn:main  ←  compare = 나/GitLearn:fix/...

# 강사 레포가 갱신되면 내 로컬 동기화
git switch main
git fetch upstream
git merge upstream/main   # 또는 git rebase upstream/main
```

---

# ✅ 다중 학생 가능 여부 (요청하신 체크 항목)

**결론: 가능합니다. 오히려 A안은 "학생 여러 명"이 전제인 구조입니다.** 세 가지 운영 방식이 있고, 인원·관리 편의에 따라 고릅니다.

## 방식 1 — Fork 기반 (학생 수 무제한, 추천)
- 학생 각자 upstream을 **fork** → 자기 fork에서 작업 → upstream으로 PR
- **장점**
  - 학생 수에 **사실상 제한 없음** (수십~수백 명도 동일)
  - 학생끼리 **완전 격리** — 남의 브랜치를 볼 일도, 망칠 일도 없음
  - 학생에게 upstream 쓰기 권한을 줄 필요 없음 (안전)
  - 실제 OSS 기여와 동일한 경험
- **단점**
  - 학생마다 remote 2개(origin/upstream) 개념을 배워야 함
  - 강사가 N명의 PR을 리뷰해야 함 (관리 포인트)

## 방식 2 — 단일 공유 레포 + 브랜치 (소규모 팀, ~5명)
- 모두를 **collaborator**로 추가, 각자 브랜치 → 같은 레포 main으로 PR
- **장점**: fork 개념 없이 간단, 서로의 PR을 보고 배우기 쉬움
- **단점**
  - 전원에게 **쓰기 권한** 필요 → 실수로 남의 브랜치/ main 건드릴 위험
  - 브랜치가 한 레포에 몰려 **이름 충돌/혼잡**. 네이밍 규칙 필수(`<이름>/fix-...`)
  - 인원 많아지면 관리 급격히 어려워짐

## 방식 3 — GitHub Classroom (교육기관/정규 강의, 추천)
- 템플릿 레포로 **학생 1인당 레포를 자동 생성**, 과제 채점/자동 피드백 연동
- **장점**
  - 학생 N명 레포 생성·수집·채점 **완전 자동화**
  - 각 학생 격리 + 강사가 한 눈에 진행 현황 파악
  - CI(GitHub Actions)로 `dotnet test` 자동 실행 → 통과 여부 자동 확인
- **단점**
  - 초기 세팅(조직 생성, Classroom 연동) 학습 필요
  - GitHub 조직(무료 가능)과 교육용 설정 필요

## 추천 매트릭스
| 상황 | 추천 방식 |
|---|---|
| 혼자 자습 후 협업 맛보기 | 본인 계정에 upstream+fork 2레포 (방식1 축소판) |
| 스터디 3~5명 | 방식 2 (공유 레포) 또는 방식 1 |
| 수업/부트캠프 6명 이상 | **방식 1(fork)** 또는 **방식 3(Classroom)** |
| 정규 강의 + 자동 채점 | **방식 3 (GitHub Classroom)** |

## 다중 학생 운영 시 공통 체크리스트
- [ ] upstream `main`에 **브랜치 보호 규칙**(PR 필수, 리뷰 1명 이상 승인) 설정
- [ ] PR/이슈 **템플릿**(`.github/PULL_REQUEST_TEMPLATE.md`) 제공 → 작성 품질 균일화
- [ ] **CI**: PR마다 `dotnet test` 자동 실행 (학생 코드가 깨졌는지 자동 판별)
- [ ] 브랜치 **네이밍 규칙** 공지 (방식 2에서 특히 중요)
- [ ] 리뷰 부하 분산: 조교 지정 or 학생 상호 리뷰(peer review) 1회 의무화
