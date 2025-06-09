# 사이트 디자인 순서
1. CSS Reset
   - [CSS Reset](https://meyerweb.com/eric/tools/css/reset/)
   - [브라우저별 Normalize](https://necolas.github.io/normalize.css/)
2. Font 확인 및 변환
3. 공통 요소 CSS 스타일
4. 주요 파트별 CSS 작성성

# Docker
```
# 빌드
docker build -t juhyung1021/my-html-server .

# 실행 (8080 포트로 외부 노출)
docker run -d -p 8080:80 juhyung1021/my-html-server
```