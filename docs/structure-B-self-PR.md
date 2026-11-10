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

## B안의 유일한 약점과 보완
- **약점**: 진짜 리뷰어가 없어 "피드백 대응"이 시뮬레이션에 그침
- **보완 1**: 셀프 리뷰를 "남이 본다"는 가정으로 코멘트까지 남기기
- **보완 2**: 강사 / Claude 가 PR에 코멘트로 개입해 리뷰 왕복을 1회 이상 강제
- **졸업**: 리뷰 협업을 제대로 배우려면 → **A안**으로 이동
