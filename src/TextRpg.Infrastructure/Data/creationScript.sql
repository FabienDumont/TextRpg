﻿CREATE TABLE Traits
(
  Id   TEXT PRIMARY KEY,
  Name TEXT NOT NULL UNIQUE
);

CREATE TABLE IncompatibleTraits
(
  TraitId             TEXT REFERENCES Traits (Id) ON DELETE CASCADE,
  IncompatibleTraitId TEXT REFERENCES Traits (Id) ON DELETE CASCADE,
  PRIMARY KEY (TraitId, IncompatibleTraitId)
);


CREATE TABLE Greetings
(
  Id         TEXT PRIMARY KEY,
  SpokenText TEXT,
  EndChat    BOOLEAN DEFAULT 0 NOT NULL
);

CREATE TABLE Conditions
(
  Id            TEXT PRIMARY KEY,
  ContextType   TEXT              NOT NULL, -- 'Greeting', 'DialogueOption', etc.
  ContextId     TEXT              NOT NULL, -- Id of the Greeting or DialogueOption

  ConditionType TEXT              NOT NULL, -- 'HasTrait', 'Relationship', etc.
  OperandLeft   TEXT,
  Operator      TEXT              NOT NULL,
  OperandRight  TEXT,
  Negate        BOOLEAN DEFAULT 0 NOT NULL
);

CREATE TABLE DialoguesOptions
(
  Id            TEXT PRIMARY KEY,
  DisplayedText TEXT,
  ParentId      TEXT REFERENCES DialoguesOptions (Id) ON DELETE CASCADE,
  HasFollowUp   BOOLEAN DEFAULT 0 NOT NULL
);

CREATE TABLE DialogueLines
(
  Id               TEXT PRIMARY KEY,
  TraitId          TEXT REFERENCES Traits (Id) ON DELETE CASCADE,
  DialogueOptionId TEXT NOT NULL REFERENCES DialoguesOptions (Id) ON DELETE CASCADE,
  SpokenText       TEXT NOT NULL
);

CREATE TABLE DialogueResults
(
  Id                 TEXT PRIMARY KEY,
  DialogueOptionId   TEXT              NOT NULL REFERENCES DialoguesOptions (Id) ON DELETE CASCADE,
  Description        TEXT,
  AdvanceTime        INTEGER DEFAULT 0,
  ChangeMoney        INTEGER DEFAULT 0,
  RelationshipChange INTEGER DEFAULT 0,
  Action             TEXT,
  EndChat            BOOLEAN DEFAULT 0 NOT NULL
);

CREATE TABLE DialogueResultLines
(
  Id               TEXT PRIMARY KEY,
  DialogueResultId TEXT NOT NULL REFERENCES DialogueResults (Id) ON DELETE CASCADE,
  TraitId          TEXT REFERENCES Traits (Id) ON DELETE CASCADE,
  SpokenText       TEXT NOT NULL
);

CREATE TABLE Locations
(
  Id           TEXT PRIMARY KEY,
  Name         TEXT    NOT NULL,
  IsAlwaysOpen INTEGER NOT NULL
);

CREATE TABLE LocationOpeningHours
(
  Id         TEXT PRIMARY KEY,
  LocationId TEXT    NOT NULL REFERENCES Locations (Id) ON DELETE CASCADE,
  DayOfWeek  INTEGER NOT NULL,
  OpensAt    TEXT    NOT NULL,
  ClosesAt   TEXT    NOT NULL
);


CREATE TABLE Rooms
(
  Id            TEXT PRIMARY KEY,
  LocationId    TEXT    NOT NULL REFERENCES Locations (Id) ON DELETE CASCADE,
  Name          TEXT    NOT NULL,
  IsPlayerSpawn INTEGER NOT NULL
);

CREATE TABLE Movements
(
  Id             TEXT PRIMARY KEY,
  FromRoomId     TEXT REFERENCES Rooms (Id) ON DELETE CASCADE,
  FromLocationId TEXT NOT NULL REFERENCES Locations (Id) ON DELETE CASCADE,
  ToRoomId       TEXT REFERENCES Rooms (Id) ON DELETE CASCADE,
  ToLocationId   TEXT NOT NULL REFERENCES Locations (Id) ON DELETE CASCADE,
  RequiredItemId TEXT
);

CREATE TABLE MovementNarrations
(
  Id         TEXT PRIMARY KEY,
  MovementId TEXT NOT NULL REFERENCES Movements (Id) ON DELETE CASCADE,
  Text       TEXT NOT NULL
);

CREATE TABLE Narrations
(
  Id   TEXT PRIMARY KEY,
  Key  TEXT NOT NULL,
  Text TEXT NOT NULL
);

CREATE TABLE ExplorationActions
(
  Id            TEXT PRIMARY KEY,
  LocationId    TEXT    NOT NULL REFERENCES Locations (Id) ON DELETE CASCADE,
  RoomId        TEXT    REFERENCES Rooms (Id) ON DELETE SET NULL,
  Label         TEXT    NOT NULL,
  NeededMinutes INTEGER NOT NULL
);

CREATE TABLE ExplorationActionResults
(
  Id                  TEXT PRIMARY KEY,
  ExplorationActionId TEXT              NOT NULL REFERENCES ExplorationActions (Id) ON DELETE CASCADE,
  AddMinutes          BOOLEAN DEFAULT 0 NOT NULL,
  EnergyChange        INTEGER,
  MoneyChange         INTEGER
);

CREATE TABLE ExplorationActionResultNarrations
(
  Id                        TEXT PRIMARY KEY,
  ExplorationActionResultId TEXT NOT NULL REFERENCES ExplorationActionResults (Id) ON DELETE CASCADE,
  Text                      TEXT NOT NULL
);
