DROP SCHEMA IF EXISTS student_incubator CASCADE;
CREATE SCHEMA student_incubator;
SET SCHEMA 'student_incubator';

CREATE DOMAIN username AS VARCHAR CHECK (LENGTH(VALUE) > 2) NOT NULL;
CREATE DOMAIN password AS VARCHAR CHECK (LENGTH(VALUE) > 5) NOT NULL;
CREATE DOMAIN status AS INTEGER CHECK ( VALUE < 101) CHECK ( -1 < VALUE ) DEFAULT 100;
CREATE DOMAIN avatarName AS VARCHAR CHECK ( LENGTH(VALUE) < 17) NOT NULL;
CREATE DOMAIN timeBlock AS INTEGER CHECK ( VALUE > -1 ) DEFAULT 80;
CREATE DOMAIN bio AS VARCHAR CHECK ( LENGTH(VALUE) <= 500 ) DEFAULT 'I am very generic, so I do not need a bio.';
CREATE DOMAIN score AS INTEGER CHECK ( VALUE > -1 ) DEFAULT 0;
CREATE DOMAIN online AS bool DEFAULT 'false';
CREATE DOMAIN cost AS INTEGER CHECK (VALUE < 90) CHECK ( VALUE > -90) DEFAULT 0;

CREATE TABLE student_user
(
    username username UNIQUE,
    password password,
    bio      bio,
    highscore score DEFAULT 0,
    online bool DEFAULT FALSE,
    has_hug   boolean DEFAULT FALSE,
    PRIMARY KEY (username)
);

CREATE TABLE favorites
(
    username username,
    favorite username,
    PRIMARY KEY (username, favorite),
    FOREIGN KEY (username) REFERENCES student_user (username),
    FOREIGN KEY (favorite) REFERENCES student_user (username)
);

CREATE TABLE avatar
(
    username  varchar,
    name      avatarName,
    academic  status,
    financial status,
    health    status,
    social    status,
    time      timeBlock,
    PRIMARY KEY (username),
    FOREIGN KEY (username) REFERENCES student_user (username)
);

CREATE TABLE suggestions
(
    username   username,
    suggestion varchar,
    PRIMARY KEY (username, suggestion),
    FOREIGN KEY (username) REFERENCES student_user (username)
);

CREATE TABLE suggested_actions
(
    creator   username,
    activity  varchar,
    academic  cost,
    financial cost,
    health    cost,
    social    cost,
    time      timeBlock,
    PRIMARY KEY (creator, activity),
    FOREIGN KEY (creator) REFERENCES student_user (username)
);


INSERT INTO student_user
VALUES ('Cranberry', 'abc123');
INSERT INTO student_user
VALUES ('Bob', 'abc123');
INSERT INTO student_user
VALUES ('Alice', 'abc123');
INSERT INTO student_user
VALUES ('Sekiro', 'abc123');
INSERT INTO student_user
VALUES ('Amara', 'abc123');
INSERT INTO student_user
VALUES ('Maze', 'abc123');

INSERT INTO avatar
VALUES ('Cranberry', 'Jody', 100, 100, 100, 100);
INSERT INTO avatar
VALUES ('Bob', 'ABob', 100, 100, 30, 30);
INSERT INTO avatar
VALUES ('Alice', 'NotAlice', 100, 100, 100, 100);
INSERT INTO avatar
VALUES ('Amara', 'Slam', 100, 100, 100, 100);


INSERT INTO suggestions VALUES ('Cranberry', 'Have a live community meeting');

INSERT INTO suggested_actions VALUES ('Cranberry', 'Travel', 20, -50, 20, 30, 6);





