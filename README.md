# 🏭 Smart Factory HMI & Data Management System

**.NET 10.0** 기반의 WPF 환경에서 생산 현장의 복잡한 데이터를 체계적으로 관리하고, 실시간 설비 데이터를 시각화하는 **HMI(Human-Machine Interface) 기초 시스템** 프로젝트입니다.

---

## 🎯 프로젝트 목적 (WHY)
생산 현장의 대규모 데이터를 무결하게 관리하여 **생산 효율을 극대화**하고, 관리자가 공정 상태를 즉각적으로 파악할 수 있는 **신뢰성 높은 소프트웨어 설계 능력**을 배양하는 데 목적이 있습니다.

---

## 🛠 핵심 문제 해결 (Problem Solving)

### 1️⃣ 실시간 데이터 처리 및 UI 응답성 확보
* **Problem:** 쉴 새 없이 들어오는 설비 데이터로 인해 UI 스레드가 병목 현상을 일으켜 화면이 멈추거나 데이터 반영이 지연됨.
* **Solution:** **Async/Await(비동기)** 처리를 도입하여 데이터 수집 로직과 UI 갱신 스레드를 분리. 수천 개의 데이터 유입 시에도 부드러운 대시보드 환경 구현.

### 2️⃣ 데이터 시각적 직관성 및 모니터링
* **Problem:** 단순 수치 나열만으로는 관리자가 공정 내 병목 구간이나 이상 징후를 즉각 파악하기 어려움.
* **Solution:** **DataBinding 및 IValueConverter**를 활용하여 데이터 임계치에 따라 색상이 동적으로 변하는 상태 모니터링 기능 구현.

### 3️⃣ 생산 이력 추적 및 데이터 무결성
* **Problem:** 공정 에러 발생 시 로그 기록이 부재하면 원인 파악 및 재발 방지가 불가능함.
* **Solution:** **SQLite**를 연동하여 생산 이력 및 설비 로그를 실시간 저장. 사용자 중심의 필터링 기능을 통해 과거 이력을 즉시 추적할 수 있는 시스템 구축.

---

## 💻 Tech Stack
* **Framework:** .NET 10.0 (WPF)
* **Language:** C# 14.0
* **Database:** SQLite (Entity Persistence)
* **Architecture:** MVVM Pattern (Learning & Applying)

---

## 📈 발전 방향
* **OPC UA / MQTT** 프로토콜 연동을 통한 실제 설비 통신 구현
* **LiveCharts** 라이브러리를 활용한 생산 수율 통계 그래프 고도화

  ---

## 🚧 Roadmap & Upcoming Features (WIP)

### 🛰️ OPC UA 기반 설비 통신 구현
현재 단순 데이터 관리를 넘어, 실제 산업 현장의 표준 프로토콜인 **OPC UA** 연동을 준비 중입니다.
- [ ] **OPC UA Client 구현:** `Opc.Ua.Client` 라이브러리를 활용한 가상 서버 접속
- [ ] **Real-time Data Tagging:** 설비의 온도, 압력 등 시뮬레이션 데이터를 실시간 태그로 받아오기
- [ ] **Error Event Handling:** 설비에서 발생하는 Alarm/Condition 이벤트를 HMI에 즉각 표시

---
