# Állásinterjú Platform

> 🇭🇺 Magyar leírás | 🇬🇧 [English description below](#job-interview-platform)

---

## 🇭🇺 Magyar

### Mi ez a projekt?

Az **Állásinterjú Platform** egy teljes körű, webalapú alkalmazás, amely az IT-s (és általánosan informatikai) álláskeresési és toborzási folyamat egészét lefedi – az álláshirdetés feladásától kezdve a jelöltek kiértékeléséig. A platform egyszerre szolgálja a cégeket és a munkakeresőket, mindezt egy modern, strukturált interjúfolyamaton keresztül.

---

### ⚙️ Technológiai stack

| Réteg | Technológia |
|---|---|
| **Frontend** | Angular 18, TypeScript |
| **Backend** | ASP.NET Core (.NET 8), C# |
| **Adatbázis** | Microsoft SQL Server (Entity Framework Core 8) |
| **AI integráció** | OpenAI API |
| **Kódfuttatás** | Piston API (sandbox code execution) |
| **Autentikáció** | JWT (JSON Web Token), 30 napos érvényességgel |

---

### 👥 Felhasználói szerepkörök

A rendszer négy szerepkört különböztet meg:

- **Cég (`Ceg`)** – Állásokat hirdet, interjúköröket állít össze, jelölteket értékel.
- **Dolgozó (`Dolgozo`)** – Céges alkalmazott, meghívókóddal regisztrál; segíti a toborzási folyamatot.
- **Álláskereső (`Munkakereso`)** – Állásokra jelentkezik, teszteket tölt ki.
- **Admin** – Rendszerszintű jogosultságokkal rendelkezik.

---

### 🏢 Cégek számára elérhető funkciók

#### Álláshirdetés kezelése
- Új álláshirdetés létrehozása (cím, leírás, munkarend, helyszín, határidő megadásával)
- Meglévő hirdetések szerkesztése, törlése
- Hirdetések listázása a céges profilon

#### Interjúkörök összeállítása
- Egy álláshoz **maximum 5 interjúkör** adható hozzá
- Körök sorrendje drag & drop alapon rendezhető
- Az alábbi interjúkör-típusok közül lehet választani:
  - **Programming** – programozási feladat tesztesetekkel és kódsablonnal, automatikus futtatással
  - **Algorithm** – algoritmusfeladat, megkötésekkel, példákkal, hint rendszerrel
  - **Design** – rendszertervezési / szoftverarchitektúra feladat
  - **Testing** – tesztelési feladat
  - **DevOps** – infrastruktúra, CI/CD, cloud-feladatok

#### Jelöltek kezelése
- Az összes beérkező jelentkezés megtekintése
- Jelölt részletes profiljának megtekintése (végzettség, kompetenciák, LeetCode statisztikák, dokumentumok)
- **AI alapú automatikus kiértékelés**: az OpenAI API segítségével a rendszer százalékos pontszámot ad a jelölteknek és javaslatot tesz a továbbjutásra
- **Manuális kiértékelés**: a cég manuálisan is megadhatja a pontszámot és dönthet a továbbjutásról
- Jelöltek nominálása (direct invite) állásra

#### Dolgozók kezelése
- Meghívókód generálása és kezelése céges alkalmazottak regisztrációjához

---

### 👤 Álláskereső számára elérhető funkciók

#### Profil
- Személyes adatok szerkesztése (név, születési dátum, hely, adószám, anyja neve)
- Kompetenciák megadása típus és szint szerint
- Dokumentumok feltöltése (pl. önéletrajz)
- **LeetCode fiók összekapcsolása**: a rendszer lekéri és megjeleníti a felhasználó LeetCode statisztikáit (megoldott feladatok, nehézségi szint szerinti bontás)

#### Álláskeresés és jelentkezés
- Elérhető állások böngészése
- Állásra való jelentkezés
- Cég által kiküldött nomináció elfogadása

#### Tesztek kitöltése
- A feltöltési határidőn belül a jelentkező kitöltheti az interjúköröket
- **Programming feladatnál**: valódi kód megírása és futtatása a böngészőben (Piston API)
- Feladatok időkorláttal vannak ellátva; a rendszer automatikusan lezárja a lejárt feladatokat
- A kitöltés előrehaladása elmenthető

---

### 🤖 AI és automatizáció

- **AI-alapú kiértékelés**: a cég kérésére az OpenAI API elemzi az összes jelölt megoldását, százalékos pontszámot generál, és megjelöli, kik jussanak tovább
- A prompt tartalmazza a jelölt kompetenciáit és megoldásait, így kontextuális értékelés valósul meg
- A cég elfogadhatja vagy elutasíthatja az AI javaslatát, és manuálisan felülbírálhatja

---

### 🗂️ Projekt struktúra

```
/
├── ffrontend/                  # Angular 18 frontend alkalmazás
│   └── src/app/
│       ├── components/         # UI komponensek
│       │   ├── home/           # Főoldal, álláslista
│       │   ├── new-job/        # Álláshirdetés létrehozása
│       │   ├── add-rounds/     # Interjúkörök hozzáadása
│       │   ├── turns/          # Körök szerkesztői (programming, devops, stb.)
│       │   ├── job-tests/      # Tesztek kitöltése (jelölt nézetben)
│       │   ├── user-results/   # Eredmények megtekintése (cég nézetben)
│       │   ├── profile/        # Álláskereső profil
│       │   ├── c-profile/      # Céges profil
│       │   └── ...
│       └── services/           # API hívások, auth kezelés
│
└── allasinterju-backend/       # ASP.NET Core backend
    ├── Allasinterju.API/       # Controller réteg (REST API végpontok)
    ├── Allasinterju.Core/      # Üzleti logika, service-ek
    └── Allasinterju.Database/  # Entity Framework modellek, adatbázis-kontextus
```

---

### 🚀 Fejlesztői indítás

#### Frontend
```bash
cd ffrontend
npm install
ng serve
# Elérhető: http://localhost:4200
```

#### Backend
```bash
cd allasinterju-backend
dotnet restore
dotnet run --project Allasinterju.API
```

> Az adatbázis kapcsolatát az `appsettings.json` fájlban kell konfigurálni (SQL Server connection string).

---

### 📌 Fejlesztési állapot

| Funkció | Állapot |
|---|---|
| Regisztráció / Bejelentkezés | ✅ Kész |
| Céges profil szerkesztése | ✅ Kész |
| Álláskereső profil szerkesztése | ✅ Kész |
| Álláshirdetés létrehozása | ✅ Kész |
| Programming interjúkör | ✅ Kész |
| Kód automatikus futtatása | ✅ Kész |
| AI alapú kiértékelés | ✅ Kész |
| DevOps / Design / Testing körök (kitöltő nézet) | 🔄 Részben kész |
| Algorithm interjúkör | 🔄 Részben kész |
| LeetCode integráció | ✅ Kész |

---
---

## 🇬🇧 English

# Job Interview Platform

### What is this project?

The **Job Interview Platform** is a full-stack web application that covers the entire IT recruitment process — from posting a job listing to evaluating candidates. The platform serves both companies and job seekers through a modern, structured multi-round interview system.

---

### ⚙️ Technology Stack

| Layer | Technology |
|---|---|
| **Frontend** | Angular 18, TypeScript |
| **Backend** | ASP.NET Core (.NET 8), C# |
| **Database** | Microsoft SQL Server (Entity Framework Core 8) |
| **AI Integration** | OpenAI API |
| **Code Execution** | Piston API (sandboxed execution) |
| **Authentication** | JWT (JSON Web Token), 30-day validity |

---

### 👥 User Roles

The system supports four distinct roles:

- **Company (`Ceg`)** – Posts jobs, creates interview rounds, evaluates candidates.
- **Employee (`Dolgozo`)** – A company staff member who registers via invite code and assists in recruitment.
- **Job Seeker (`Munkakereso`)** – Applies to jobs and completes interview tests.
- **Admin** – Has system-level privileges.

---

### 🏢 Features for Companies

#### Job Listing Management
- Create new job listings (title, description, schedule, location, deadline)
- Edit and manage existing listings
- View all listings on the company profile page

#### Interview Round Configuration
- Attach up to **5 interview rounds** per job listing
- Reorder rounds using drag & drop
- Choose from the following round types:
  - **Programming** – coding task with test cases, code template, and automatic execution
  - **Algorithm** – algorithmic challenge with constraints, examples, and hints
  - **Design** – system design / software architecture task
  - **Testing** – software testing task
  - **DevOps** – infrastructure, CI/CD, and cloud-related tasks

#### Candidate Management
- View all submitted applications for a job
- Access detailed candidate profiles (education, competencies, LeetCode stats, uploaded documents)
- **AI-powered evaluation**: the OpenAI API analyzes all submissions, assigns percentage scores, and recommends which candidates should advance
- **Manual evaluation**: companies can override AI scores and make manual pass/fail decisions
- Nominate (direct invite) job seekers to apply for a position

#### Employee Management
- Generate and manage invite codes for onboarding company employees

---

### 👤 Features for Job Seekers

#### Profile
- Edit personal information (name, birth date, birthplace, tax number, mother's maiden name)
- Add competencies (type and skill level)
- Upload documents (e.g. CV/resume)
- **LeetCode account integration**: connect a LeetCode username to display solved problem statistics (total solved, by difficulty)

#### Job Search & Application
- Browse available job listings
- Apply to jobs
- Accept company-sent nominations

#### Completing Tests
- Fill in interview rounds before the application deadline
- **For Programming rounds**: write and execute real code in the browser (via Piston API)
- Rounds have time limits; the system auto-closes expired sessions
- Progress can be saved mid-session

---

### 🤖 AI & Automation

- **AI evaluation**: on demand, the OpenAI API reviews all candidates' solutions for a given round, generates percentage scores, and flags which candidates should advance
- Prompts include the candidate's competencies and submitted answers for contextual scoring
- Companies can accept or reject AI recommendations and override scores manually

---

### 🗂️ Project Structure

```
/
├── ffrontend/                  # Angular 18 frontend application
│   └── src/app/
│       ├── components/         # UI components
│       │   ├── home/           # Landing page, job listings
│       │   ├── new-job/        # Create job listing
│       │   ├── add-rounds/     # Add interview rounds
│       │   ├── turns/          # Round editors (programming, devops, etc.)
│       │   ├── job-tests/      # Test-taking view (candidate side)
│       │   ├── user-results/   # Results view (company side)
│       │   ├── profile/        # Job seeker profile
│       │   ├── c-profile/      # Company profile
│       │   └── ...
│       └── services/           # API calls, auth handling
│
└── allasinterju-backend/       # ASP.NET Core backend
    ├── Allasinterju.API/       # Controller layer (REST API endpoints)
    ├── Allasinterju.Core/      # Business logic, services
    └── Allasinterju.Database/  # Entity Framework models, DB context
```

---

### 🚀 Getting Started (Development)

#### Frontend
```bash
cd ffrontend
npm install
ng serve
# Available at: http://localhost:4200
```

#### Backend
```bash
cd allasinterju-backend
dotnet restore
dotnet run --project Allasinterju.API
```

> Configure the database connection string in `appsettings.json` (SQL Server).

---

### 📌 Development Status

| Feature | Status |
|---|---|
| Registration / Login | ✅ Done |
| Company profile editing | ✅ Done |
| Job seeker profile editing | ✅ Done |
| Job listing creation | ✅ Done |
| Programming interview round | ✅ Done |
| Automatic code execution | ✅ Done |
| AI-based evaluation | ✅ Done |
| DevOps / Design / Testing rounds (candidate view) | 🔄 Partial |
| Algorithm round | 🔄 Partial |
| LeetCode integration | ✅ Done |
