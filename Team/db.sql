CREATE DATABASE TestManagement;
USE TestManagement;

-- 학생 테이블
CREATE TABLE Student (              
    ID varchar(16) not null,        -- 학생 ID
    Password varchar(512) not null, -- 학생 PW
    Salt varchar(512) not null,     -- 학생 Salt
    Name varchar(32) not null,      -- 학생 이름
    Phone varchar(13) not null,     -- 학생 전화번호
    Email varchar(48) not null,     -- 학생 이메일
    PRIMARY KEY(ID)                 -- 기본키 (학생 ID)
);

-- 교수 테이블
CREATE TABLE Professor (            
    ID varchar(16) not null,        -- 교수 ID
    Password varchar(512) not null, -- 교수 PW
    Salt varchar(512) not null,     -- 교수 Salt
    Name varchar(32) not null,      -- 교수 이름
    Phone varchar(13) not null,     -- 교수 전화번호
    Email varchar(48) not null,     -- 교수 이메일
    PRIMARY KEY(ID)                 -- 기본키 (교수 ID)
);
-- 교수 연구실 번호 넣을까말까 고민중

-- 강의 테이블
CREATE TABLE Lecture (
    ID varchar(16) not null,        -- 강의 ID
    Name varchar(128) not null,     -- 강의 명
    ProID varchar(16) not null,     -- 강의 교수 ID 
    Year int not null,              -- 강의 년도 (2020, 2021, ...)
    Semester varchar(10) not null,  -- 강의 학기 (1, 2, 여름계절, 겨울계절)
    PRIMARY KEY(ID),                -- 기본키 (강의 ID)
    FOREIGN KEY(ProID) REFERENCES Professor(ID) ON UPDATE CASCADE ON DELETE CASCADE 
                                    -- 외래키 (강의 교수 ID) from Professor(교수 테이블)
);
-- 강의 ID는 어떤 식으로 만들어야 될까? 
-- AUTO_INCREMENT로 해야할지 고민중
-- Semester은 계절학기까지 고려해서 varchar으로 만듬

-- 강의 시험 테이블
CREATE TABLE LectureTest (
    ID varchar(16) not null,        -- 시험 ID
    LecID varchar(16) not null,     -- 강의 ID
    Name varchar(16) not null,      -- 시험 명 (중간고사, 기말고사, Quiz1, Quiz2, ...)
    TotalScore float not null,      -- 시험 총 점 
    Percent int not null,           -- 전체 점수 중 시험 퍼센트
    StartYear int not null,         -- 시험 시작 시간
    StartMonth int not null,        -- 시험 시작 월
    StartDay int not null,          -- 시험 시작 일
    StartHour int not null,         -- 시험 시작 시각
    StartMin int not null,          -- 시험 시작 분
    EndYear int not null,           -- 시험 종료 시간
    EndMonth int not null,          -- 시험 종료 월
    EndDay int not null,            -- 시험 종료 일
    EndHour int not null,           -- 시험 종료 시각
    EndMin int not null,            -- 시험 종료 분
    PRIMARY KEY (ID),               -- 기본키 (시험 ID)
    FOREIGN KEY(LecID) REFERENCES Lecture(ID) ON UPDATE CASCADE ON DELETE CASCADE
                                    -- 외래키 (강의 ID) from Lecture(강의 테이블)
);
-- 시험 ID를 int형으로 바꿔서 AUTO_INCREMENT 할까 고민중

-- 시험 문제 테이블
CREATE TABLE TestQuestion (
    ID int not null,                -- 문제 ID
    TestID varchar(16) not null,    -- 시험 ID
    Score float not null,           -- 문제 배점
    Title varchar(1024) not null,   -- 문제 제목
    Text varchar(4096),             -- 문제 설명 또는 보기 
    Image varchar(64),              -- 문제 이미지
    Audio varchar(64),              -- 문제 오디오
    PRIMARY KEY(ID, TestID),        -- 기본키 (문제 ID, 시험 ID)
    FOREIGN KEY(TestID) REFERENCES LectureTest(ID) ON UPDATE CASCADE ON DELETE CASCADE
                                    -- 외래키 (시험 ID) from LectureTest(강의 시험 테이블)
);
-- 메인 문제는 항상 서브 문제를 가짐.
-- 문제와 보기를 json형태로 묶어서 Text 파일로 저장할지 따로 테이블을 만들지 고민 중.
-- 서브 문제가 있는 경우 Score 변수에는 C#에서 서브 문제들의 점수 합으로 넣어줌.

-- 시험 서브 문제 테이블
CREATE TABLE SubQuestion (
    ID int not null,                -- 서브 문제 ID
    TestID varchar(16) not null,    -- 시험 ID
    QuestionID int not null,        -- 메인 시험 문제 ID
    Score float not null,           -- 서브 문제 배점
    Title varchar(1024) not null,   -- 서브 문제 제목 
    Text varchar(4096),             -- 서브 문제 설명 또는 보기
    Image varchar(64),              -- 서브 문제 이미지
    Audio varchar(64),              -- 서브 문제 오디오
    Type int not null,              -- 문제 유형 (객관식: 1, 단답형: 2, 서술형: 3)
    Answer varchar(4096),           -- 문제 정답
    PRIMARY KEY(ID, TestID, QuestionID),    -- 기본키 (서브 문제 ID, 시험 ID, 메인 시험 문제 ID)
    FOREIGN KEY(TestID, QuestionID) REFERENCES TestQuestion(TestID, ID) ON UPDATE CASCADE ON DELETE CASCADE
                                    -- 외래키 (시험 ID, 메인 시험 문제 ID) from TestQuestion(시험 문제 테이블)
);
-- 문제와 보기를 json형태로 묶어서 Text 파일로 저장할지 따로 테이블을 만들지 고민 중

-- 학생 답안지 테이블
CREATE TABLE AnswerSheet (
    StuID varchar(16) not null,     -- 시험 응시 학생 ID
    TestID varchar(16) not null,    -- 시험 ID
    QuestionID int not null,        -- 시험 문제 ID
    SubQuestionID int not null,     -- 시험 서브 문제 ID
    StuAnswer varchar(4096),        -- 학생 작성 답안
    Score float not null,           -- 문제 점수
    SubmitTime varchar(32),         -- 답안 제출 시간
    PRIMARY KEY(StuID, TestID, QuestionID, SubQuestionID),
                                    -- 기본키 (학생 ID, 시험 ID, 시험 문제 ID, 시험 서브 문제 ID)
    FOREIGN KEY(TestID, QuestionID, SubQuestionID) REFERENCES SubQuestion(TestID, QuestionID, ID) ON UPDATE CASCADE ON DELETE CASCADE,
                                    -- 외래키 (시험 ID, 시험 문제 ID, 서브 문제 ID) from SubQuestion(시험 서브 문제 테이블)
    FOREIGN KEY(StuID) REFERENCES Student(ID) ON UPDATE CASCADE ON DELETE CASCADE
                                    -- 외래키 (학생 ID) from Student(학생 테이블)
);

-- 수강 테이블
CREATE TABLE Course (
    LecID varchar(16) not null,     -- 강의 ID
    StuID varchar(16) not null,     -- 수강 학생 ID
    Score float not null,           -- 학생 강의 총 점
    PRIMARY KEY(LecID, StuID),       -- 기본키 (강의 ID, 수강 학생 ID)
    FOREIGN KEY(LecID) REFERENCES Lecture(ID) ON UPDATE CASCADE ON DELETE CASCADE,
                                    -- 외래키 (강의 ID) from Lecture(강의 테이블)
    FOREIGN KEY(StuID) REFERENCES Student(ID) ON UPDATE CASCADE ON DELETE CASCADE
                                    -- 외래키 (학생 ID) from Student(학생 테이블)
);

-- 학생 시험 점수 테이블
CREATE TABLE TestScore (
    TestID varchar(16) not null,    -- 시험 ID
    StuID varchar(16) not null,     -- 수강 학생 ID
    LecID varchar(16) not null,     -- 강의 ID
    Score float not null,           -- 시험 점수
    PRIMARY KEY(TestID, StuID),      -- 기본키 (시험 ID, 학생 ID)
    FOREIGN KEY(TestID) REFERENCES LectureTest(ID) ON UPDATE CASCADE ON DELETE CASCADE,
                                    -- 외래키 (시험 ID) from LectureTest(강의 시험 테이블)
    FOREIGN KEY(StuID, LecID) REFERENCES Course(StuID, LecID) ON UPDATE CASCADE ON DELETE CASCADE
                                    -- 외래키 (수강 학생 ID, 강의 ID) from Course(수강 테이블)                                
);


