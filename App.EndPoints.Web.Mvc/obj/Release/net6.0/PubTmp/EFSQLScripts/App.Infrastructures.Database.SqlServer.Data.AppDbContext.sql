IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [AppFile] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Extention] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_AppFile] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [Discriminator] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [Family] nvarchar(max) NULL,
        [FullAddress] nvarchar(max) NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [MainCategories] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_MainCategories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [SecondaryCategories] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        [MainCategoryId] int NOT NULL,
        CONSTRAINT [PK_SecondaryCategories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SecondaryCategories_MainCategories_MainCategoryId] FOREIGN KEY ([MainCategoryId]) REFERENCES [MainCategories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [ThirdCategories] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [Price] bigint NULL,
        [IsDeleted] bit NOT NULL,
        [SecondaryCategoryId] int NOT NULL,
        CONSTRAINT [PK_ThirdCategories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ThirdCategories_SecondaryCategories_SecondaryCategoryId] FOREIGN KEY ([SecondaryCategoryId]) REFERENCES [SecondaryCategories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [ExpertSkills] (
        [ThirdCategoryId] int NOT NULL,
        [ExpertId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_ExpertSkills] PRIMARY KEY ([ExpertId], [ThirdCategoryId]),
        CONSTRAINT [FK_ExpertSkills_ThirdCategories_ThirdCategoryId] FOREIGN KEY ([ThirdCategoryId]) REFERENCES [ThirdCategories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        [CustomerId] nvarchar(max) NOT NULL,
        [ExpertId] nvarchar(max) NULL,
        [BasePrice] bigint NULL,
        [IsShow] bit NULL,
        [ThirdCategoryId] int NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Orders_ThirdCategories_ThirdCategoryId] FOREIGN KEY ([ThirdCategoryId]) REFERENCES [ThirdCategories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [ThirdCategoryFiles] (
        [Id] int NOT NULL IDENTITY,
        [CreatedUserId] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [ThirdCategoryId] int NOT NULL,
        [AppFileId] int NOT NULL,
        CONSTRAINT [PK_ThirdCategoryFiles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ThirdCategoryFiles_AppFile_AppFileId] FOREIGN KEY ([AppFileId]) REFERENCES [AppFile] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ThirdCategoryFiles_ThirdCategories_ThirdCategoryId] FOREIGN KEY ([ThirdCategoryId]) REFERENCES [ThirdCategories] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [Comments] (
        [Id] int NOT NULL IDENTITY,
        [ThirdCategoryId] int NULL,
        [CommentText] nvarchar(max) NULL,
        [CreatedUserId] nvarchar(max) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [IsShow] bit NULL,
        [OrderId] int NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comments_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE TABLE [Suggestions] (
        [Id] int NOT NULL IDENTITY,
        [SuggestionDate] datetime2 NULL,
        [SuggestedPrice] bigint NOT NULL,
        [StartTime] nvarchar(max) NOT NULL,
        [Duration] nvarchar(max) NOT NULL,
        [IsApproved] bit NOT NULL,
        [ExpertId] nvarchar(max) NOT NULL,
        [IsShow] bit NULL,
        [OrderId] int NOT NULL,
        CONSTRAINT [PK_Suggestions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Suggestions_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_Comments_OrderId] ON [Comments] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_ExpertSkills_ThirdCategoryId] ON [ExpertSkills] ([ThirdCategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_Orders_ThirdCategoryId] ON [Orders] ([ThirdCategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_SecondaryCategories_MainCategoryId] ON [SecondaryCategories] ([MainCategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_Suggestions_OrderId] ON [Suggestions] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_ThirdCategories_SecondaryCategoryId] ON [ThirdCategories] ([SecondaryCategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_ThirdCategoryFiles_AppFileId] ON [ThirdCategoryFiles] ([AppFileId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    CREATE INDEX [IX_ThirdCategoryFiles_ThirdCategoryId] ON [ThirdCategoryFiles] ([ThirdCategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720052824_init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220720052824_init', N'6.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720075532_file')
BEGIN
    ALTER TABLE [ThirdCategoryFiles] DROP CONSTRAINT [FK_ThirdCategoryFiles_AppFile_AppFileId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720075532_file')
BEGIN
    ALTER TABLE [AppFile] DROP CONSTRAINT [PK_AppFile];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720075532_file')
BEGIN
    EXEC sp_rename N'[AppFile]', N'AppFiles';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720075532_file')
BEGIN
    ALTER TABLE [AppFiles] ADD CONSTRAINT [PK_AppFiles] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720075532_file')
BEGIN
    ALTER TABLE [ThirdCategoryFiles] ADD CONSTRAINT [FK_ThirdCategoryFiles_AppFiles_AppFileId] FOREIGN KEY ([AppFileId]) REFERENCES [AppFiles] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720075532_file')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220720075532_file', N'6.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720075653_file1')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppFiles]') AND [c].[name] = N'Extention');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AppFiles] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AppFiles] DROP COLUMN [Extention];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720075653_file1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220720075653_file1', N'6.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220720083450_file3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220720083450_file3', N'6.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220725113003_UserFile')
BEGIN
    CREATE TABLE [UserFiles] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NOT NULL,
        [AppFileId] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_UserFiles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserFiles_AppFiles_AppFileId] FOREIGN KEY ([AppFileId]) REFERENCES [AppFiles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220725113003_UserFile')
BEGIN
    CREATE INDEX [IX_UserFiles_AppFileId] ON [UserFiles] ([AppFileId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220725113003_UserFile')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220725113003_UserFile', N'6.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220728125254_beforseed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220728125254_beforseed', N'6.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220728171540_seedData')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] ON;
    EXEC(N'INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES (N''c7b013f0-5201-4317-abd8-c211f91b6540'', N''3'', N''Expert'', N''Expert''),
    (N''c7b013f0-5201-4317-abd8-c211f91b6822'', N''4'', N''Normal'', N''Normal''),
    (N''c7b013f0-5201-4317-abd8-c211f91b7330'', N''2'', N''Customer'', N''Customer''),
    (N''fab4fac1-c546-41de-aebc-a14da6895711'', N''1'', N''Admin'', N''Admin'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
        SET IDENTITY_INSERT [AspNetRoles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220728171540_seedData')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Discriminator', N'Email', N'EmailConfirmed', N'Family', N'FirstName', N'FullAddress', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] ON;
    EXEC(N'INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Discriminator], [Email], [EmailConfirmed], [Family], [FirstName], [FullAddress], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
    VALUES (N''b74ddd14-6340-4840-95c2-db12554843e5'', 0, N''d284b9fd-4dc4-4e9b-ad12-daa895d809b5'', N''ApplicationUser'', N''admin@gmail.com'', CAST(0 AS bit), N''Parsa'', N''Mahdi'', NULL, CAST(0 AS bit), NULL, NULL, NULL, NULL, N''09173229408'', CAST(0 AS bit), N''16176599-5986-4202-aa11-cca05b700854'', CAST(0 AS bit), N''Admin'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Discriminator', N'Email', N'EmailConfirmed', N'Family', N'FirstName', N'FullAddress', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
        SET IDENTITY_INSERT [AspNetUsers] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220728171540_seedData')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDeleted', N'Title') AND [object_id] = OBJECT_ID(N'[MainCategories]'))
        SET IDENTITY_INSERT [MainCategories] ON;
    EXEC(N'INSERT INTO [MainCategories] ([Id], [IsDeleted], [Title])
    VALUES (1, CAST(0 AS bit), N''لوازم خانگی''),
    (2, CAST(0 AS bit), N'' نظافت و بهداشت''),
    (3, CAST(0 AS bit), N'' خدمات اداری'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDeleted', N'Title') AND [object_id] = OBJECT_ID(N'[MainCategories]'))
        SET IDENTITY_INSERT [MainCategories] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220728171540_seedData')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] ON;
    EXEC(N'INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
    VALUES (N''fab4fac1-c546-41de-aebc-a14da6895711'', N''b74ddd14-6340-4840-95c2-db12554843e5'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
        SET IDENTITY_INSERT [AspNetUserRoles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220728171540_seedData')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDeleted', N'MainCategoryId', N'Title') AND [object_id] = OBJECT_ID(N'[SecondaryCategories]'))
        SET IDENTITY_INSERT [SecondaryCategories] ON;
    EXEC(N'INSERT INTO [SecondaryCategories] ([Id], [IsDeleted], [MainCategoryId], [Title])
    VALUES (1, CAST(0 AS bit), 1, N''لوازم آشپزخانه''),
    (2, CAST(0 AS bit), 1, N''لوازم صوتی و تصویری''),
    (3, CAST(0 AS bit), 2, N''خشکشویی و اتوشویی''),
    (4, CAST(0 AS bit), 2, N''قالی شویی و مبل شویی''),
    (5, CAST(0 AS bit), 3, N''ماشین اداری''),
    (6, CAST(0 AS bit), 3, N''مبلمان اداری'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsDeleted', N'MainCategoryId', N'Title') AND [object_id] = OBJECT_ID(N'[SecondaryCategories]'))
        SET IDENTITY_INSERT [SecondaryCategories] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220728171540_seedData')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'IsDeleted', N'Price', N'SecondaryCategoryId', N'Title') AND [object_id] = OBJECT_ID(N'[ThirdCategories]'))
        SET IDENTITY_INSERT [ThirdCategories] ON;
    EXEC(N'INSERT INTO [ThirdCategories] ([Id], [Description], [IsDeleted], [Price], [SecondaryCategoryId], [Title])
    VALUES (1, N''استادکار با معرفی بهترین تعمیرکارهای یخچال در منزل، به شما کمک می‌کند تا با سرعت بیشتری بتوانید مشکل یخچال فریزر را حل کنید.'', CAST(0 AS bit), CAST(500000 AS bigint), 1, N''یخچال'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'IsDeleted', N'Price', N'SecondaryCategoryId', N'Title') AND [object_id] = OBJECT_ID(N'[ThirdCategories]'))
        SET IDENTITY_INSERT [ThirdCategories] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220728171540_seedData')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'IsDeleted', N'Price', N'SecondaryCategoryId', N'Title') AND [object_id] = OBJECT_ID(N'[ThirdCategories]'))
        SET IDENTITY_INSERT [ThirdCategories] ON;
    EXEC(N'INSERT INTO [ThirdCategories] ([Id], [Description], [IsDeleted], [Price], [SecondaryCategoryId], [Title])
    VALUES (2, N''ظرفشویی‌ها هم مثل تمامی وسایل برقی و مکانیکی خراب می‌شوند و نیاز به سرویس دارند. گاهی سر‌و‌صدای ظرفشویی و گاهی نقص در شستشوی ظروف باعث می‌شود برای تعمیر ظرفشویی آن اقدام کنیم.'', CAST(0 AS bit), CAST(400000 AS bigint), 1, N''ماشین ظرفشویی'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'IsDeleted', N'Price', N'SecondaryCategoryId', N'Title') AND [object_id] = OBJECT_ID(N'[ThirdCategories]'))
        SET IDENTITY_INSERT [ThirdCategories] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220728171540_seedData')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220728171540_seedData', N'6.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220729053824_seed2')
BEGIN
    EXEC(N'UPDATE [AspNetUsers] SET [ConcurrencyStamp] = N''926ddc1a-24ca-4e23-8138-641163a6acff'', [EmailConfirmed] = CAST(1 AS bit), [NormalizedUserName] = N''ADMIN@GMAIL.COM'', [PasswordHash] = N''AQAAAAEAACcQAAAAEARw1J9g5Lq4Gh9CeP2URCJqRpTU3qYolKKVA3TjfE8I/iZRnBdcDC21aU9B8z9b/Q=='', [SecurityStamp] = N''cc0002ec-3ffe-4d19-924f-65f973e4f713''
    WHERE [Id] = N''b74ddd14-6340-4840-95c2-db12554843e5'';
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220729053824_seed2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220729053824_seed2', N'6.0.7');
END;
GO

COMMIT;
GO

