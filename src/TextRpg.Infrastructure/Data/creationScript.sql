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
  Id              TEXT PRIMARY KEY,
  MinRelationship INTEGER,
  MaxRelationship INTEGER,
  HasTrait        TEXT REFERENCES Traits (Id) ON DELETE CASCADE,
  SpokenText      TEXT,
  EndChat         BOOLEAN DEFAULT 0 NOT NULL,
  CHECK (MaxRelationship IS NULL OR MinRelationship <= MaxRelationship)
);

CREATE TABLE DialoguesOptions
(
  Id            TEXT PRIMARY KEY,
  DisplayedText TEXT,
  ParentId      TEXT REFERENCES DialoguesOptions (Id) ON DELETE CASCADE,
  TimeOfDay     TEXT,
  Location      TEXT,
  HasFollowUp   BOOLEAN DEFAULT 0 NOT NULL
);

CREATE TABLE DialogueOptionPersonalities
(
  DialogueOptionId TEXT NOT NULL REFERENCES DialoguesOptions (Id) ON DELETE CASCADE,
  PersonalityId    TEXT NOT NULL REFERENCES Traits (Id) ON DELETE CASCADE,
  PRIMARY KEY (DialogueOptionId, PersonalityId)
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
  MinRelationship    INTEGER,
  MaxRelationship    INTEGER,
  HasTrait           TEXT REFERENCES Traits (Id) ON DELETE CASCADE,
  Description        TEXT,
  AdvanceTime        INTEGER DEFAULT 0,
  ChangeMoney        INTEGER DEFAULT 0,
  RelationshipChange INTEGER DEFAULT 0,
  Action             TEXT,
  EndChat            BOOLEAN DEFAULT 0 NOT NULL,
  CHECK (MaxRelationship IS NULL OR MinRelationship <= MaxRelationship)
);

CREATE TABLE DialogueResultLines
(
  Id               TEXT PRIMARY KEY,
  DialogueResultId TEXT NOT NULL REFERENCES DialogueResults (Id) ON DELETE CASCADE,
  TraitId          TEXT REFERENCES Traits (Id) ON DELETE CASCADE,
  SpokenText       TEXT NOT NULL
);
