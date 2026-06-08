USE [dbi570496]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artworks](
 [Id] [int] IDENTITY(1,1) NOT NULL,
 [Title] [nvarchar](200) NOT NULL,
 [Artist] [nvarchar](150) NOT NULL,
 [Museum] [nvarchar](150) NOT NULL,
 [ImageUrl] [nvarchar](500) NOT NULL,
 [Description] [nvarchar](max) NOT NULL,
 [Year] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
 [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalEntries](
 [Id] [int] IDENTITY(1,1) NOT NULL,
 [UserId] [int] NOT NULL,
 [ArtworkId] [int] NOT NULL,
 [Reflection] [nvarchar](max) NOT NULL,
 [Date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
 [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
 [Id] [int] IDENTITY(1,1) NOT NULL,
 [Name] [nvarchar](100) NOT NULL,
 [Email] [nvarchar](255) NOT NULL,
 [PasswordHash] [nvarchar](255) NOT NULL,
 [CreatedAt] [datetime] NOT NULL,
 [IsAdmin] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
 [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitedArtworks](
 [Id] [int] IDENTITY(1,1) NOT NULL,
 [UserId] [int] NOT NULL,
 [ArtworkId] [int] NOT NULL,
 [VisitedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
 [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishlistItems](
 [Id] [int] IDENTITY(1,1) NOT NULL,
 [UserId] [int] NOT NULL,
 [ArtworkId] [int] NOT NULL,
 [SavedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
 [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Artworks] ON 
GO
INSERT [dbo].[Artworks] ([Id], [Title], [Artist], [Museum], [ImageUrl], [Description], [Year]) VALUES (1, N'Mona Lisa', N'Leonardo da Vinci', N'Louvre Museum, Paris', N'https://upload.wikimedia.org/wikipedia/commons/6/6a/Mona_Lisa.jpg', N'One of the most famous paintings in the world.', 1503)
GO
INSERT [dbo].[Artworks] ([Id], [Title], [Artist], [Museum], [ImageUrl], [Description], [Year]) VALUES (2, N'The Starry Night', N'Vincent van Gogh', N'Museum of Modern Art, New York', N'https://commons.wikimedia.org/wiki/Special:FilePath/Van%20Gogh%20-%20Starry%20Night%20-%20Google%20Art%20Project.jpg', N'Van Gogh''s iconic night sky masterpiece.', 1889)
GO
INSERT [dbo].[Artworks] ([Id], [Title], [Artist], [Museum], [ImageUrl], [Description], [Year]) VALUES (3, N'The Kiss', N'Gustav Klimt', N'Belvedere Palace, Vienna', N'https://commons.wikimedia.org/wiki/Special:FilePath/The%20Kiss%20-%20Gustav%20Klimt%20-%20Google%20Cultural%20Institute.jpg', N'Golden romantic painting by Klimt.', 1908)
GO
INSERT [dbo].[Artworks] ([Id], [Title], [Artist], [Museum], [ImageUrl], [Description], [Year]) VALUES (4, N'Girl with a Pearl Earring', N'Johannes Vermeer', N'Mauritshuis, The Hague', N'https://upload.wikimedia.org/wikipedia/commons/d/d7/Meisje_met_de_parel.jpg', N'Known as the Dutch Mona Lisa.', 1665)
GO
INSERT [dbo].[Artworks] ([Id], [Title], [Artist], [Museum], [ImageUrl], [Description], [Year]) VALUES (5, N'The Birth of Venus', N'Sandro Botticelli', N'Uffizi Gallery, Florence', N'https://commons.wikimedia.org/wiki/Special:FilePath/Sandro%20Botticelli%20-%20La%20nascita%20di%20Venere%20-%20Google%20Art%20Project%20-%20edited.jpg', N'Famous Renaissance painting of Venus.', 1486)
GO
INSERT [dbo].[Artworks] ([Id], [Title], [Artist], [Museum], [ImageUrl], [Description], [Year]) VALUES (6, N'Water Lilies', N'Claude Monet', N'Musée de l''Orangerie, Paris', N'https://commons.wikimedia.org/wiki/Special:FilePath/Claude%20Monet%20-%20Water%20Lilies%20-%20Google%20Art%20Project.jpg', N'One of Monet''s famous water lily paintings.', 1916)
GO
INSERT [dbo].[Artworks] ([Id], [Title], [Artist], [Museum], [ImageUrl], [Description], [Year]) VALUES (7, N'Las Meninas', N'Diego Velazquez', N'Museo del Prado, Madrid', N'https://upload.wikimedia.org/wikipedia/commons/9/99/Las_Meninas_01.jpg', N'One of the greatest Spanish paintings.', 1656)
GO
INSERT [dbo].[Artworks] ([Id], [Title], [Artist], [Museum], [ImageUrl], [Description], [Year]) VALUES (8, N'The Scream', N'Edvard Munch', N'National Museum, Oslo', N'https://commons.wikimedia.org/wiki/Special:FilePath/Edvard%20Munch%20-%20The%20Scream%20-%20Google%20Art%20Project.jpg', N'Expressionist painting showing anxiety and emotion.', 1893)
GO
INSERT [dbo].[Artworks] ([Id], [Title], [Artist], [Museum], [ImageUrl], [Description], [Year]) VALUES (9, N'American Gothic', N'Grant Wood', N'Art Institute of Chicago', N'https://commons.wikimedia.org/wiki/Special:FilePath/Grant%20Wood%20-%20American%20Gothic%20-%20Google%20Art%20Project.jpg', N'Famous American painting of a farmer and his daughter.', 1930)
GO
SET IDENTITY_INSERT [dbo].[Artworks] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Name], [Email], [PasswordHash], [CreatedAt], [IsAdmin]) VALUES (2, N'Anita Ivanova', N'anitaivanova2006@abv.bg', N'JoDywuR4Bx68uNFIksHaRg==.1tF9McjZCl4eUcURaGOvY9bWMmeP6hN9SgSI7u7mnNQ=', CAST(N'2026-04-11T00:57:52.267' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[VisitedArtworks] ON 
GO
INSERT [dbo].[VisitedArtworks] ([Id], [UserId], [ArtworkId], [VisitedDate]) VALUES (3, 2, 4, CAST(N'2026-06-03T13:22:48.903' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[VisitedArtworks] OFF
GO
SET IDENTITY_INSERT [dbo].[WishlistItems] ON 
GO
INSERT [dbo].[WishlistItems] ([Id], [UserId], [ArtworkId], [SavedDate]) VALUES (8, 2, 7, CAST(N'2026-05-12T11:44:44.613' AS DateTime))
GO
INSERT [dbo].[WishlistItems] ([Id], [UserId], [ArtworkId], [SavedDate]) VALUES (9, 2, 9, CAST(N'2026-05-25T10:50:28.120' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[WishlistItems] OFF
GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
 [Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VisitedArtworks] ADD  DEFAULT (getdate()) FOR [VisitedDate]
GO
ALTER TABLE [dbo].[JournalEntries]  WITH CHECK ADD  CONSTRAINT [FK_JournalEntries_Artworks] FOREIGN KEY([ArtworkId])
REFERENCES [dbo].[Artworks] ([Id])
GO
ALTER TABLE [dbo].[JournalEntries] CHECK CONSTRAINT [FK_JournalEntries_Artworks]
GO
ALTER TABLE [dbo].[JournalEntries]  WITH CHECK ADD  CONSTRAINT [FK_JournalEntries_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[JournalEntries] CHECK CONSTRAINT [FK_JournalEntries_Users]
GO
ALTER TABLE [dbo].[VisitedArtworks]  WITH CHECK ADD FOREIGN KEY([ArtworkId])
REFERENCES [dbo].[Artworks] ([Id])
GO
ALTER TABLE [dbo].[VisitedArtworks]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[WishlistItems]  WITH CHECK ADD FOREIGN KEY([ArtworkId])
REFERENCES [dbo].[Artworks] ([Id])
GO
ALTER TABLE [dbo].[WishlistItems]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO