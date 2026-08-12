-- Roller
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Roles')
BEGIN
    CREATE TABLE Roles (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Name NVARCHAR(50) NOT NULL
    );
END

-- Hesaplar
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Accounts')
BEGIN
    CREATE TABLE Accounts (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        Email NVARCHAR(100) NOT NULL UNIQUE,
        Password NVARCHAR(255) NOT NULL,
        RoleId UNIQUEIDENTIFIER NOT NULL,
        IsDeleted BIT NOT NULL DEFAULT 0,
        CONSTRAINT FK_Accounts_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id)
    );
END

-- Kullanıcı Profilleri
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'UserProfiles')
BEGIN
    CREATE TABLE UserProfiles (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        AccountId UNIQUEIDENTIFIER NOT NULL,
        Name NVARCHAR(50) NOT NULL,
        Surname NVARCHAR(50) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        PhoneNumber NVARCHAR(20) NULL,
        PhotoUrl NVARCHAR(255) NULL,
        CvUrl NVARCHAR(255) NULL,
        CONSTRAINT FK_UserProfiles_Accounts FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
    );
END

-- Mentorlar
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Mentors')
BEGIN
    CREATE TABLE Mentors (
        Id UNIQUEIDENTIFIER PRIMARY KEY,
        AccountId UNIQUEIDENTIFIER NOT NULL,
        ProfileId UNIQUEIDENTIFIER NOT NULL,
        InternCount INT NOT NULL DEFAULT 0,
        IsDeleted BIT NOT NULL DEFAULT 0,
        CONSTRAINT FK_Mentors_Accounts FOREIGN KEY (AccountId) REFERENCES Accounts(Id),
        CONSTRAINT FK_Mentors_UserProfiles FOREIGN KEY (ProfileId) REFERENCES UserProfiles(Id)
    );
END