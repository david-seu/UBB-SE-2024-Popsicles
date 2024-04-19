Create database GroupPlatform
go
use GroupPlatform

-- Create the Group Members table



CREATE TABLE GroupMembers (
    GroupMemberId UNIQUEIDENTIFIER NOT NULL,
    UserName NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Phone NVARCHAR(13) UNIQUE,
    CONSTRAINT PK_GroupMembers PRIMARY KEY(GroupMemberId)
);

-- Create the Groups table
CREATE TABLE Groups (
    GroupId UNIQUEIDENTIFIER NOT NULL,
    OwnerId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Icon VARCHAR(255),
    Banner VARCHAR(255),
    MaxPostsPerHour INT DEFAULT 5,
    GroupCode NVARCHAR(255),
    IsPublic BIT NOT NULL DEFAULT 1,
    CanPostByDefault BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME,
    CONSTRAINT PK_Groups PRIMARY KEY(GroupId),
    CONSTRAINT FK_Groups_GroupMembers FOREIGN KEY (OwnerId) REFERENCES GroupMembers(GroupMemberId)
        ON DELETE CASCADE ON UPDATE CASCADE
);

-- Create the Requests table
CREATE TABLE Requests(
    RequestId UNIQUEIDENTIFIER NOT NULL,
    GroupMemberId UNIQUEIDENTIFIER NOT NULL,
    GroupId UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_Requests PRIMARY KEY(RequestId),
    CONSTRAINT FK_Requests_Groups FOREIGN KEY (GroupId) REFERENCES Groups(GroupId),
    CONSTRAINT FK_Requests_GroupMembers FOREIGN KEY (GroupMemberId) REFERENCES GroupMembers(GroupMemberId)
);

-- Create the GroupPosts table
CREATE TABLE GroupPosts (
    GroupPostId UNIQUEIDENTIFIER NOT NULL,
    GroupMemberId UNIQUEIDENTIFIER NOT NULL,
    GroupId UNIQUEIDENTIFIER NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    ImageData IMAGE,
    IsPinned BIT NOT NULL DEFAULT 0,
    Tags NVARCHAR(255),
    SpecificToRole NVARCHAR(255),
    CONSTRAINT PK_GroupPosts PRIMARY KEY(GroupPostId),
    CONSTRAINT FK_GroupPosts_GroupMembers FOREIGN KEY (GroupMemberId) REFERENCES GroupMembers(GroupMemberId),
    CONSTRAINT FK_GroupPosts_Groups FOREIGN KEY (GroupId) REFERENCES Groups(GroupId)
);

-- Create the Polls table
CREATE TABLE Polls (
    PollId UNIQUEIDENTIFIER NOT NULL,
    GroupMemberId UNIQUEIDENTIFIER NOT NULL,
    GroupId UNIQUEIDENTIFIER NOT NULL,
    ResultsVisible BIT NOT NULL DEFAULT 1,
    Options NVARCHAR(500),
    Description NVARCHAR(500) NOT NULL,
    IsPinned BIT NOT NULL DEFAULT 0,
    IsAnonymous BIT NOT NULL DEFAULT 0,
    IsLive BIT NOT NULL DEFAULT 1,
    IsMultipleChoice BIT NOT NULL DEFAULT 0,
    EndTime DATETIME,
    Tags NVARCHAR(255),
    SpecificToRole NVARCHAR(255),
    CONSTRAINT PK_Polls PRIMARY KEY(PollId),
    CONSTRAINT FK_Polls_GroupMembers FOREIGN KEY (GroupMemberId) REFERENCES GroupMembers(GroupMemberId),
    CONSTRAINT FK_Polls_Groups FOREIGN KEY (GroupId) REFERENCES Groups(GroupId)
);

-- Create the Votes table
CREATE TABLE Votes(
    VoteId UNIQUEIDENTIFIER NOT NULL,
    GroupMemberId UNIQUEIDENTIFIER NOT NULL,
    PollId UNIQUEIDENTIFIER NOT NULL,
    Choices NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_Votes PRIMARY KEY(VoteId),
    CONSTRAINT FK_Votes_GroupMembers FOREIGN KEY (GroupMemberId) REFERENCES GroupMembers(GroupMemberId),
    CONSTRAINT FK_Votes_Polls FOREIGN KEY (PollId) REFERENCES Polls(PollId)
);

-- Create the GroupMemberships table
CREATE TABLE GroupMemberships(
    GroupMembershipId UNIQUEIDENTIFIER NOT NULL,
    GroupId UNIQUEIDENTIFIER NOT NULL,
    GroupMemberId UNIQUEIDENTIFIER NOT NULL,
    Role NVARCHAR(100) NOT NULL,
    JoinDate DATETIME NOT NULL,
    IsBanned BIT NOT NULL DEFAULT 0,
    BypassPostSettings BIT NOT NULL DEFAULT 0,
    IsTimedOut BIT NOT NULL DEFAULT 0,
    CONSTRAINT PK_GroupMemberships PRIMARY KEY(GroupMembershipId),
    CONSTRAINT FK_GroupMemberships_GroupMembers FOREIGN KEY (GroupMemberId) REFERENCES GroupMembers(GroupMemberId),
    CONSTRAINT FK_GroupMemberships_Groups FOREIGN KEY (GroupId) REFERENCES Groups(GroupId)
);
-- Inserting into GroupMembers
INSERT INTO GroupMembers (GroupMemberId, UserName, Password, Description, Email, Phone) VALUES
(NEWID(), 'johndoe', 'securepassword123', 'A dedicated member of the community.', 'johndoe@example.com', '1234567890123'),
(NEWID(), 'janedoe', 'passwordsafe456', 'Loves painting and art galleries.', 'janedoe@example.com', '9876543210987'),
(NEWID(), 'aliceblue', 'pass23456789', 'Freelance photographer.', 'alice@example.com', '5678901234567'),
(NEWID(), 'bobsmith', 'bobssecurepass', 'Musician and tech enthusiast.', 'bobsmith@example.com', '3216549870321');

-- Inserting into Groups
INSERT INTO Groups (GroupId, OwnerId, Name, Description, Icon, Banner, MaxPostsPerHour, GroupCode, IsPublic, CanPostByDefault, CreatedAt) VALUES
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'johndoe'), 'Nature Lovers', 'A group for those who enjoy the outdoors.', 'icon_url1', 'banner_url1', 5, 'NATURE123', 1, 1, GETDATE()),
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'janedoe'), 'Art Fanatics', 'A place to discuss and share art.', 'icon_url2', 'banner_url2', 3, 'ARTLOVE', 1, 1, GETDATE()),
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'aliceblue'), 'Photo Corner', 'Community for sharing photography tips and gear.', 'icon_url3', 'banner_url3', 10, 'PHOTOSNAP', 0, 1, GETDATE()),
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'bobsmith'), 'Tech Trends', 'Latest discussions on technology and gadgets.', 'icon_url4', 'banner_url4', 7, 'TECHWORLD', 1, 0, GETDATE());

-- Inserting into Requests
INSERT INTO Requests (RequestId, GroupMemberId, GroupId) VALUES
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'aliceblue'), (SELECT GroupId FROM Groups WHERE Name = 'Nature Lovers')),
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'bobsmith'), (SELECT GroupId FROM Groups WHERE Name = 'Art Fanatics'));

-- Inserting into GroupPosts
INSERT INTO GroupPosts (GroupPostId, GroupMemberId, GroupId, Description, ImageData, IsPinned, Tags, SpecificToRole) VALUES
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'johndoe'), (SELECT GroupId FROM Groups WHERE Name = 'Nature Lovers'), 'Check out this beautiful landscape!', NULL, 1, 'Landscape,Photography', NULL),
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'janedoe'), (SELECT GroupId FROM Groups WHERE Name = 'Art Fanatics'), 'My latest oil painting.', NULL, 0, 'OilPainting,ModernArt', 'Artist');

-- Inserting into Polls
INSERT INTO Polls (PollId, GroupMemberId, GroupId, ResultsVisible, Options, Description, IsPinned, IsAnonymous, IsLive, IsMultipleChoice, EndTime, Tags, SpecificToRole) VALUES
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'johndoe'), (SELECT GroupId FROM Groups WHERE Name = 'Nature Lovers'), 1, 'Mountain;Beach;Forest', 'Where should our next group hike be?', 0, 0, 1, 1, DATEADD(day, 7, GETDATE()), 'Hiking,Locations', NULL),
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'janedoe'), (SELECT GroupId FROM Groups WHERE Name = 'Art Fanatics'), 1, 'Abstract;Realism;Impressionism', 'What is your favorite art style?', 1, 1, 1, 0, DATEADD(day, 7, GETDATE()), 'ArtStyle,Poll', NULL);

-- Inserting into Votes
INSERT INTO Votes (VoteId, GroupMemberId, PollId, Choices) VALUES
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'aliceblue'), (SELECT PollId FROM Polls WHERE Description = 'Where should our next group hike be?'), 'Mountain'),
(NEWID(), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'bobsmith'), (SELECT PollId FROM Polls WHERE Description = 'What is your favorite art style?'), 'Abstract');

-- Inserting into GroupMemberships
INSERT INTO GroupMemberships (GroupMembershipId, GroupId, GroupMemberId, Role, JoinDate, IsBanned, BypassPostSettings, IsTimedOut) VALUES
(NEWID(), (SELECT GroupId FROM Groups WHERE Name = 'Nature Lovers'), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'johndoe'), 'Admin', GETDATE(), 0, 1, 0),
(NEWID(), (SELECT GroupId FROM Groups WHERE Name = 'Art Fanatics'), (SELECT GroupMemberId FROM GroupMembers WHERE UserName = 'janedoe'), 'Moderator', GETDATE(), 0, 0, 0);
