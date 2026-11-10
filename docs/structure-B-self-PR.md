# B안 — 개인 레포에서 셀프 PR (먼저 진행)

한 사람, 한 레포. 작업 브랜치에서 변경을 만들고 **자기 `main`을 향해 PR**을 열어, 스스로 리뷰 코멘트를 달고 머지합니다. 1인 프로젝트의 정석 GitHub Flow와 동일합니다.

## 왜 B안으로 먼저?
- 계정 1개, 레포 1개 — **마찰 제로**로 즉시 시작
- "엣지 케이스 1개 = 브랜치 1개 = PR 1개" 로 히스토리가 깔끔하게 쌓임
- PR 메커니즘(branch/push/diff/머지/삭제)을 온전히 경험

## 셋업
```bash
# 로컬 레포 초기화 (이 폴더에서)
git init
git add .
git commit -m "chore: GitLearn 초기 프로젝트 (버그 포함)"

# GitHub에 빈 레포 생성 후 연결  (gh CLI 사용 예시)
gh repo create GitLearn --private --source=. --remote=origin --push

# main 보호(선택) — 셀프 머지라도 "PR을 통해서만 머지" 규칙을 들이면 습관이 든다
# GitHub > Settings > Branches > Add rule > Require a pull request before merging
```

## 반복 리듬 (매 차시)
```bash
git switch -c fix/<엣지케이스>
# 1) 실패 테스트 추가 → commit (red)
# 2) 소스 수정 → commit (green)
git push -u origin fix/<엣지케이스>
# 3) GitHub에서 PR 열기 (base: main ← fix/...)
# 4) 셀프 리뷰: "Files changed"에서 직접 코멘트 달기 (왜 이렇게 고쳤는지)
# 5) Merge → 브랜치 삭제
```

## 셀프 PR 잘 쓰는 법 (PR 작성 실전)

셀프 PR이라도 **"6개월 뒤의 내가 읽는다" + "남이 리뷰한다"** 를 가정하고 씁니다. 이게 PR 작성 실력의 핵심입니다.

### PR 본문 — 4칸 템플릿 (`.github/PULL_REQUEST_TEMPLATE.md`로 자동 적용)
```markdown
## 무엇을 (What)      ← 한 줄 요약
## 왜 / 문제 (Why)    ← 재현 방법 + 원인
## 어떻게 (How)       ← 핵심 해결 방식과 선택 이유
## 테스트            ← 무엇으로 검증했는가 (dotnet test)
```

### 좋은 PR의 조건
- **작게**: PR 하나 = 변경 의도 하나. "버그 수정 + 리팩터 + 포맷"을 한 PR에 섞지 않기
- **제목 컨벤션**: `fix:` / `feat:` / `docs:` / `test:` / `refactor:` 접두어로 역할을 드러냄
- **커밋 분리**: `test:`(red) → `fix:`(green) 처럼 역할별로 커밋을 나눔 → 히스토리가 교재가 됨
- **diff 자체 검토**: PR 열기 전 `gh pr diff`로 **의도한 변경만** 담겼는지 확인(불필요 파일·포맷 변경 제거)

### 셀프 리뷰 루틴 (리뷰어가 없을 때의 대체)
```bash
gh pr create --fill                 # 또는 템플릿으로 본문 작성
gh pr diff                          # 1) 전체 diff를 '남의 코드처럼' 읽기
gh pr comment <N> -b "왜 이렇게 고쳤는지 / 아쉬운 점"   # 2) 셀프 코멘트 남기기
# 3) 하루 뒤 다시 보기(가능하면) → 머지
gh pr merge <N> --squash --delete-branch
```

### 머지 전략
- **Squash merge 권장**: `main`에는 "변경 1개 = 커밋 1개"로 깔끔하게, 지저분한 중간 커밋은 PR 안에만 보존.
- `Settings > General > Pull Requests`에서 "Allow squash merging"만 켜두면 헷갈릴 일이 없음.

## B안의 유일한 약점과 보완
- **약점**: 진짜 리뷰어가 없어 "피드백 대응"이 시뮬레이션에 그침
- **보완 1**: 셀프 리뷰를 "남이 본다"는 가정으로 코멘트까지 남기기
- **보완 2**: 강사 / Claude 가 PR에 코멘트로 개입해 리뷰 왕복을 1회 이상 강제
- **졸업**: 리뷰 협업을 제대로 배우려면 → [A안 Fork](structure-A-fork-PR.md) / [Fork 분담](structure-A-fork-분담.md) 으로 이동
