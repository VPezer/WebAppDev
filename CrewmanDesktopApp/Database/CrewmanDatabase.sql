/* =====================================================================
   CREWMAN - baza podataka za upravljanje pomorcima (MS SQL Server, T-SQL)

   Skripta je idempotentna: moze se pokrenuti vise puta bez greske.
   - Kreira bazu Crewman ako ne postoji
   - Kreira tablice Ranks, Vessels i Seafarers ako ne postoje
   - Ubacuje pocetne (demo) podatke samo ako su tablice prazne

   Aplikacija istu skriptu pokrece automatski pri prvom startu,
   a moze se pokrenuti i rucno u SQL Server Management Studiju (SSMS).
   ===================================================================== */

IF DB_ID(N'Crewman') IS NULL
    CREATE DATABASE [Crewman];
GO

USE [Crewman];
GO

/* ---------- Ranks (rangovi) ---------- */
IF OBJECT_ID(N'dbo.Ranks', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Ranks
    (
        Id   INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Ranks PRIMARY KEY,
        Name NVARCHAR(100)     NOT NULL CONSTRAINT UQ_Ranks_Name UNIQUE
    );
END
GO

/* ---------- Vessels (brodovi) ---------- */
IF OBJECT_ID(N'dbo.Vessels', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Vessels
    (
        Id   INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Vessels PRIMARY KEY,
        Name NVARCHAR(100)     NOT NULL CONSTRAINT UQ_Vessels_Name UNIQUE
    );
END
GO

/* ---------- Seafarers (pomorci) ---------- */
IF OBJECT_ID(N'dbo.Seafarers', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Seafarers
    (
        Id              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Seafarers PRIMARY KEY,
        FirstName       NVARCHAR(100)     NOT NULL,
        LastName        NVARCHAR(100)     NOT NULL,
        DateOfBirth     DATE              NOT NULL,
        Nationality     NVARCHAR(100)     NOT NULL,
        Email           NVARCHAR(255)     NULL,
        RankId          INT               NOT NULL CONSTRAINT FK_Seafarers_Ranks   REFERENCES dbo.Ranks (Id),
        VesselId        INT               NULL     CONSTRAINT FK_Seafarers_Vessels REFERENCES dbo.Vessels (Id),
        EmbarkationDate DATE              NULL,

        /* datum ukrcaja ima smisla samo ako je pomorac dodijeljen brodu */
        CONSTRAINT CK_Seafarers_Embarkation CHECK (EmbarkationDate IS NULL OR VesselId IS NOT NULL)
    );

    CREATE INDEX IX_Seafarers_LastName_FirstName ON dbo.Seafarers (LastName, FirstName);
    CREATE INDEX IX_Seafarers_RankId             ON dbo.Seafarers (RankId);
    CREATE INDEX IX_Seafarers_VesselId           ON dbo.Seafarers (VesselId);
END
GO

/* ---------- Pocetni podaci: rangovi ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.Ranks)
BEGIN
    INSERT INTO dbo.Ranks (Name) VALUES
        (N'Master'),
        (N'Chief Officer'),
        (N'Second Officer'),
        (N'Third Officer'),
        (N'Chief Engineer'),
        (N'Second Engineer'),
        (N'Third Engineer'),
        (N'Electro-Technical Officer'),
        (N'Bosun'),
        (N'Able Seaman'),
        (N'Ordinary Seaman'),
        (N'Oiler'),
        (N'Cook'),
        (N'Steward'),
        (N'Deck Cadet'),
        (N'Engine Cadet');
END
GO

/* ---------- Pocetni podaci: brodovi ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.Vessels)
BEGIN
    INSERT INTO dbo.Vessels (Name) VALUES
        (N'MV Adriatic Star'),
        (N'MV Dubrovnik'),
        (N'MT Jadran'),
        (N'MV Split'),
        (N'MT Korčula'),
        (N'MV Hvar'),
        (N'MV Brač'),
        (N'MV Marjan');
END
GO

/* ---------- Pocetni podaci: pomorci (demo) ---------- */
IF NOT EXISTS (SELECT 1 FROM dbo.Seafarers)
BEGIN
    INSERT INTO dbo.Seafarers (FirstName, LastName, DateOfBirth, Nationality, Email, RankId, VesselId, EmbarkationDate)
    SELECT d.FirstName, d.LastName, d.DateOfBirth, d.Nationality, d.Email, r.Id, v.Id, d.EmbarkationDate
    FROM (VALUES
        (N'Ivan',     N'Horvat',      '1978-03-12', N'Hrvatska',  N'ivan.horvat@example.com',       N'Master',                    N'MV Adriatic Star', '2026-06-01'),
        (N'Marko',    N'Kovačević',   '1985-07-24', N'Hrvatska',  N'marko.kovacevic@example.com',   N'Chief Officer',             N'MV Adriatic Star', '2026-06-01'),
        (N'Jose',     N'Reyes',       '1990-08-14', N'Filipini',  N'jose.reyes@example.com',        N'Able Seaman',               N'MV Adriatic Star', '2026-06-01'),
        (N'Nikola',   N'Perić',       '1969-12-02', N'Crna Gora', N'nikola.peric@example.com',      N'Master',                    N'MV Dubrovnik',     '2026-05-15'),
        (N'Ana',      N'Babić',       '1992-11-05', N'Hrvatska',  N'ana.babic@example.com',         N'Second Officer',            N'MV Dubrovnik',     '2026-05-15'),
        (N'Josip',    N'Marić',       '1975-01-30', N'Hrvatska',  N'josip.maric@example.com',       N'Chief Engineer',            N'MT Jadran',        '2026-04-20'),
        (N'Petar',    N'Jurić',       '1988-09-17', N'Hrvatska',  N'petar.juric@example.com',       N'Second Engineer',           N'MT Jadran',        '2026-04-20'),
        (N'Luka',     N'Vuković',     '1995-02-08', N'Hrvatska',  N'luka.vukovic@example.com',      N'Third Officer',             N'MV Split',         '2026-07-03'),
        (N'Andrii',   N'Shevchenko',  '1983-05-21', N'Ukrajina',  N'andrii.shevchenko@example.com', N'Third Engineer',            N'MV Split',         '2026-07-03'),
        (N'Ramon',    N'Santos',      '1987-04-03', N'Filipini',  N'ramon.santos@example.com',      N'Bosun',                     N'MV Hvar',          '2026-03-10'),
        (N'Mihai',    N'Popescu',     '1993-03-09', N'Rumunjska', N'mihai.popescu@example.com',     N'Cook',                      N'MV Hvar',          '2026-03-10'),
        (N'Rajesh',   N'Kumar',       '1991-10-27', N'Indija',    N'rajesh.kumar@example.com',      N'Oiler',                     N'MT Korčula',       '2026-08-12'),
        (N'Piotr',    N'Nowak',       '1980-06-19', N'Poljska',   N'piotr.nowak@example.com',       N'Electro-Technical Officer', N'MV Brač',          '2026-02-28'),
        (N'Tomislav', N'Šarić',       '2003-09-01', N'Hrvatska',  N'tomislav.saric@example.com',    N'Deck Cadet',                NULL,                NULL),
        (N'Filip',    N'Knežević',    '2002-05-23', N'Hrvatska',  N'filip.knezevic@example.com',    N'Engine Cadet',              NULL,                NULL),
        (N'Dario',    N'Matić',       '1982-02-14', N'Hrvatska',  NULL,                             N'Steward',                   NULL,                NULL)
    ) AS d (FirstName, LastName, DateOfBirth, Nationality, Email, RankName, VesselName, EmbarkationDate)
    INNER JOIN dbo.Ranks   AS r ON r.Name = d.RankName
    LEFT  JOIN dbo.Vessels AS v ON v.Name = d.VesselName;
END
GO
