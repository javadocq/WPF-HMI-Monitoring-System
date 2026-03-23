🏭 Industrial HMI & Real-time Monitoring System
.NET 10.0 / WPF 기반의 제조 현장 설비 제어 및 상태 시각화를 위한 고성능 HMI(Human-Machine Interface) 시스템 프로젝트입니다.

🎯 프로젝트 목적 (WHY)
설비에서 발생하는 로우 레벨(Low-level) 데이터를 실시간으로 수집·가공하여 현장 작업자의 의사결정을 돕고, 장비의 이상 징후를 즉각 감지하여 공정 정지 시간을 최소화(Zero Downtime) 하는 신뢰성 높은 HMI 소프트웨어 설계를 목적으로 합니다.

🛠 핵심 문제 해결 (Problem Solving)
1️⃣ 실시간 데이터 스트리밍 및 UI 최적화
Problem: 초 단위로 인입되는 설비 데이터로 인해 UI 스레드 병목 현상이 발생하여 모니터링 화면의 반응성이 저하됨.

Solution: Async/Await 비동기 패턴과 Dispatcher를 최적화하여 데이터 수신 로직과 UI 갱신 스레드를 분리. 대량의 로그 유입 시에도 60fps 이상의 부드러운 대시보드 환경 유지.

2️⃣ 데이터 시각적 직관성 및 알람 시스템
Problem: 단순 수치 나열만으로는 관리자가 설비의 임계치 초과나 이상 상태를 즉각 파악하기 어려움.

Solution: IValueConverter 및 DataTrigger를 활용하여 데이터 값에 따라 색상과 애니메이션이 변하는 동적 상태 모니터링 기능 구현. 이상 수치 감지 시 즉각적인 시각적 피드백 제공.

3️⃣ 설비 이력 추적 및 로컬 데이터 무결성
Problem: 간헐적으로 발생하는 설비 에러의 원인을 분석하기 위한 과거 데이터 추적 장치가 부재함.

Solution: SQLite Embedded DB를 연동하여 초단위 생산 이력 및 설비 상태 로그를 로컬에 저장. 사용자 정의 필터링 기능을 통해 특정 설비의 과거 상태를 즉시 추적(Traceability)할 수 있는 아키텍처 구축.

💻 Tech Stack
Framework: .NET 10.0 (WPF)

Language: C# 14.0

Database: SQLite (Embedded Persistent Storage)

Architecture: MVVM Pattern (View-ViewModel decoupling을 통한 유지보수성 확보)

🚧 Roadmap & Upcoming Features (WIP)
🛰️ 산업용 표준 프로토콜 기반 설비 통신 구현
단순 시뮬레이션을 넘어 실제 산업 현장의 장비와 대화하기 위한 통신 레이어 고도화를 진행 중입니다.

[ ] TCP/IP 소켓 통신: 장비와의 직접적인 데이터 송수신 로직 구현

[ ] OPC UA Client 연동: Opc.Ua.Client 표준 라이브러리를 활용한 스마트 팩토리 표준 통신 구현

[ ] Fault Management: 실시간 Alarm/Condition 이벤트 감지 시 경고 팝업 및 알림 시스템 강화
