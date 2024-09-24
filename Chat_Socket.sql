Create database chatSocket;

CREATE TABLE Messages (

  Id INT PRIMARY KEY,

  Sender VARCHAR(50) NOT NULL,

  Receiver VARCHAR(50) NOT NULL,

  MessageText NVARCHAR(MAX),

  SentAt DATETIME2(3) NOT NULL

);
