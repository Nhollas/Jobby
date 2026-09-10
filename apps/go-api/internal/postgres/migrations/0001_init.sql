CREATE TABLE boards (
    reference    TEXT PRIMARY KEY,
    owner_id     TEXT        NOT NULL,
    name         TEXT        NOT NULL,
    created_date TIMESTAMPTZ NOT NULL,
    last_updated TIMESTAMPTZ NOT NULL
);

CREATE TABLE job_lists (
    reference       TEXT PRIMARY KEY,
    board_reference TEXT        NOT NULL REFERENCES boards (reference) ON DELETE CASCADE,
    owner_id        TEXT        NOT NULL,
    name            TEXT        NOT NULL,
    position        INTEGER     NOT NULL,
    created_date    TIMESTAMPTZ NOT NULL,
    last_updated    TIMESTAMPTZ NOT NULL
);

CREATE TABLE jobs (
    reference          TEXT PRIMARY KEY,
    job_list_reference TEXT        NOT NULL REFERENCES job_lists (reference) ON DELETE CASCADE,
    board_reference    TEXT        NOT NULL REFERENCES boards (reference) ON DELETE CASCADE,
    owner_id           TEXT        NOT NULL,
    company            TEXT        NOT NULL,
    title              TEXT        NOT NULL,
    position           INTEGER     NOT NULL,
    created_date       TIMESTAMPTZ NOT NULL,
    last_updated       TIMESTAMPTZ NOT NULL
);

CREATE TABLE activities (
    reference       TEXT PRIMARY KEY,
    board_reference TEXT        NOT NULL REFERENCES boards (reference) ON DELETE CASCADE,
    -- NULL when the activity is not linked to a job.
    job_reference   TEXT        NULL     REFERENCES jobs (reference) ON DELETE SET NULL,
    owner_id        TEXT        NOT NULL,
    title           TEXT        NOT NULL,
    type            SMALLINT    NOT NULL,
    start_date      TIMESTAMPTZ NOT NULL,
    end_date        TIMESTAMPTZ NOT NULL,
    note            TEXT        NOT NULL,
    completed       BOOLEAN     NOT NULL,
    created_date    TIMESTAMPTZ NOT NULL,
    last_updated    TIMESTAMPTZ NOT NULL
);

CREATE INDEX job_lists_board_reference_idx  ON job_lists (board_reference);
CREATE INDEX jobs_board_reference_idx       ON jobs (board_reference);
CREATE INDEX activities_board_reference_idx ON activities (board_reference);
CREATE INDEX boards_owner_id_idx            ON boards (owner_id);
