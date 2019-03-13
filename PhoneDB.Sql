CREATE TABLE [dbo].[User] (
    [Id]        INT           NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Email]     NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Subscription] (
    [SubscriptionId] NVARCHAR (50) NOT NULL,
    [Name]           NVARCHAR (50) NOT NULL,
    [Price]          DECIMAL (18)  NOT NULL,
    [VatAmount]      DECIMAL (18)  NOT NULL,
    [CallMinutes]    DECIMAL (18)  NOT NULL,
    PRIMARY KEY CLUSTERED ([SubscriptionId] ASC)
);

CREATE TABLE [dbo].[UserSubscriptions] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [UserId]         INT           NOT NULL,
    [SubscriptionId] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    FOREIGN KEY ([SubscriptionId]) REFERENCES [dbo].[Subscription] ([SubscriptionId])
);

INSERT INTO [dbo].[User] ([Id], [FirstName], [LastName], [Email]) VALUES (5, N'rastogi', N'lastname', N'testlast1@mail.com')

INSERT INTO [dbo].[Subscription] ([SubscriptionId], [Name], [Price], [VatAmount], [CallMinutes]) VALUES (N'2712e86a-d519-48af-b50b-194e9a2102dg', N'118 min deal', CAST(24 AS Decimal(18, 0)), CAST(30 AS Decimal(18, 0)), CAST(50 AS Decimal(18, 0)))

SET IDENTITY_INSERT [dbo].[UserSubscriptions] ON
INSERT INTO [dbo].[UserSubscriptions] ([Id], [UserId], [SubscriptionId]) VALUES (1, 5, N'2712e86a-d519-48af-b50b-194e9a2102dg')
SET IDENTITY_INSERT [dbo].[UserSubscriptions] OFF
