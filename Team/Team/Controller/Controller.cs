using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Model;

namespace Team
{
    public class Controller
    {
        private ListForm listForm; // 리스트 폼
        private InsertLecture insert; // 강의 삽입 폼
        private string basicRoute = @"C:\Team8ProjectForder";// 폴더를 읽기 위한 기본 위치

        public Library Library { get; set; } // 독서실
        public List<Student> Students { get; set; } // 전체 학생 리스트
        public List<Teacher> Teachers { get; set; } // 전체 선생 리스트
        public List<Lecture> Lectures { get; set; } // 전체 강의 리스트
        public List<Post> Posts { get; set; }
        private int[] IDCnt; // 신규 가입시 아이디 배정 참조 카운트 배열
        private int IDCntIndex; // 인덱스
        public Controller()
        {
             List<Lecture> list = new List<Lecture>();
             Library = new Library(); // 독서실
             FileIO fileIO = new FileIO();//FilIO
            Lectures = fileIO.LoadLectureFile(basicRoute + @"\TeamProjectFileSaveForder\Lecture.txt");//파일에서 Lecture 읽어오기
            Teachers = fileIO.LoadTeacherFile(basicRoute + @"\TeamProjectFileSaveForder\Teacher.txt", Lectures);//파일에서 Teacher 읽어오기와 강의 맵핑
            Students = fileIO.LoadStudentFile(basicRoute + @"\TeamProjectFileSaveForder\Student.txt", Lectures);//파일에서 Student 읽어오기와 강의 맵핑
            IDCnt = fileIO.LoadIDCntFile(basicRoute + @"\TeamProjectFileSaveForder\IDCnt.txt");
            Posts = fileIO.LoadPostFile(basicRoute + @"\TeamProjectFileSaveForder\Post.txt");


        }

        ~Controller()
        {
            // 컨트롤러 소멸 시 파일 저장
            SaveStudents();
            SaveTeacher();
            SaveLectures();
            SaveIDCnt();
            SavePostFile();
        }

        public Student FindOnlyStudent(string searchId) // 학생 아이디로 학생 검색
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].Id == searchId)
                    return Students[i];
            }
            return null;
        }
        public List<Student> FindStudents(string searchText, int searchIndex) // 검색 텍스트와 검색 속성을 통해 정보 검색
        {
            List<Student> list = new List<Student>();

            if (searchIndex == 0)
                list = Students;

            else if (searchIndex == 1) // 학번
            {
                for (int i = 0; i < Students.Count; i++)
                {
                    if (Students[i].Id.Contains(searchText))
                        list.Add(Students[i]);
                }
            }

            else if (searchIndex == 2) // 이름
            {
                for (int i = 0; i < Students.Count; i++)
                {
                    if (Students[i].Name.Contains(searchText))
                        list.Add(Students[i]);
                }
            }

            else // 학교
            {
                for (int i = 0; i < Students.Count; i++)
                {
                    if (Students[i].School.Contains(searchText))
                        list.Add(Students[i]);
                }
            }

            return list;
        }
        public Teacher FindOnlyTeacher(string searchid) // 선생 번호로 검색
        {
            for (int i = 0; i < Teachers.Count; i++)
            {
                if (Teachers[i].Id == searchid)
                    return Teachers[i];
            }
            return null;
        }
        public List<Teacher> FindTeachers(string searchText, int searchIndex) // 검색 텍스트와 검색 속성을 통한 선생 검색
        {
            List<Teacher> list = new List<Teacher>();

            if (searchIndex == 0) 
                list = Teachers;

            else if(searchIndex == 1) // 교번
            {
                for(int i = 0; i < Teachers.Count; i++)
                {
                    if (Teachers[i].Id.Contains(searchText))
                        list.Add(Teachers[i]);
                }
            }

            else if (searchIndex == 2) // 이름
            {
                for (int i = 0; i < Teachers.Count; i++)
                {
                    if (Teachers[i].Name.Contains(searchText))
                        list.Add(Teachers[i]);
                }
            }

            else // 과목
            {
                for (int i = 0; i < Teachers.Count; i++)
                {
                    if (Teachers[i].Major.Contains(searchText))
                        list.Add(Teachers[i]);
                }
            }

            return list;
        }          
        public Lecture FindOnlyLecture(string searchId) // 강의 번호를 통한 강의 검색
        {
            for (int i = 0; i < Lectures.Count; i++)
            {
                if (Lectures[i].Id == searchId)
                    return Lectures[i];
            }
            return null;
        }
        public List<Lecture> FindLectures(string searchText, int searchIndex) // 검색 텍스트와 검색 속성을 통한 강의 검색
        {
            List<Lecture> list = new List<Lecture>();

            if (searchIndex == 0)
                list = Lectures;

            else if (searchIndex == 1) // 강의번호
            {
                for (int i = 0; i < Lectures.Count; i++)
                {
                    if (Lectures[i].Id.Contains(searchText))
                        list.Add(Lectures[i]);
                }
            }

            else if (searchIndex == 2) // 강의명
            {
                for (int i = 0; i < Lectures.Count; i++)
                {
                    if (Lectures[i].ClassName.Contains(searchText))
                        list.Add(Lectures[i]);
                }
            }

            else // 담당선생
            {
                for (int i = 0; i < Lectures.Count; i++)
                {
                    if (Lectures[i].Tea.Name.Contains(searchText))
                        list.Add(Lectures[i]);
                }
            }

            return list;
        }

        public void DeleteInfo(string id, int index) // id와 학생, 선생, 강의에 따른 삭제 
        {
            if(index == 0) // 학생
            {
                for(int i = 0; i < Students.Count; i++)
                {
                    if (Students[i].Id == id) // 해당 아이디
                    {
                        Students[i].DeleteAllLecture(); //강의 리스트 모두 삭제 후
                        Students.RemoveAt(i); // 리스트에서 삭제
                    }
                }
            } 
            else if(index == 1) // 선생
            {
                for(int i = 0; i < Teachers.Count; i++)
                {
                    if(Teachers[i].Id == id) // 해당 아이디
                    {
                        Teachers[i].DeleteAllLecture(); // 강의 리스트 모두 삭제 후
                        Teachers.RemoveAt(i); // 리스트에서 삭제
                    }
                }
                
            } 
            else // 강의
            {
                for(int i = 0; i < Lectures.Count; i++) 
                {
                    if (Lectures[i].Id == id) // 해당 아이디
                    {
                        Lectures[i].RemoveLecture(); // 강의 삭제(관련 학생, 선생 정보 삭제)
                        Lectures.RemoveAt(i); // 리스트에서 삭제
                    }
                }
            }
        }

        public Lecture FindInsertLecture(string[] row) // InsertForm 에서 데이터 그리드 뷰에 id가 없기 때문에 나머지 속성으로 강의 검색
        {
            Lecture lect;
            lect = null;
            for(int i = 0; i < Lectures.Count; i++)
            {
                if (row[1] == "미배정")
                {
                    if (Lectures[i].ClassName == row[0] && Lectures[i].Tea == null && Lectures[i].StartTime == row[2] && Lectures[i].FinishTime == row[3])
                        lect = Lectures[i];
                }
                else
                {
                    if (Lectures[i].ClassName == row[0] && Lectures[i].Tea.Name == row[1] && Lectures[i].StartTime == row[2] && Lectures[i].FinishTime == row[3])
                        lect = Lectures[i];
                }
            }

            return lect;
        }

        public void GetListForm(ListForm listForm) //  ListForm 받아옴
        {
            this.listForm = listForm;
        }
        
        public void GetViews() // 받아온 ListForm을 이용하여 ListForm 내 데이터 그리드 뷰 및 박스들 수정
        {
            listForm.GetInfoListView();
            listForm.SetLectureBox();
        }
        
        public string[] GetYearComboboxItems() // 생년월일 중 연에 들어갈 아이템
        { 
            string[] str;
            int index = 0;

            str = new string[DateTime.Now.Year - 1950 + 1];

            for(int i = 1950; i <= DateTime.Now.Year; i++) // 1950년 부터 현재 년도까지
            {
                str[index++] = "" + i;
            }

            return str;
        }

        public string[] GetMonthComboBoxItems() // 생년월일 중 월에 들어갈 아이템
        {
            string[] str;
            str = new string[12];
            for (int i = 1; i <= 12; i++) // 1~12월
            {
                if (i < 10)
                    str[i - 1] = "0" + i;
                else
                    str[i - 1] = "" + i;
            }

            return str;
        }

        public string[] GetDayComboBoxItems(int year, int month) // 생년월일 중 일에 들어갈 아이템
        {
            string[] str;
            int end;
            // 월 별로 일 수 지정
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                str = new string[31];
                end = 31;
            }
            else
            {
                if (month == 2)
                {
                    if (DateTime.IsLeapYear(year)) // 윤년 처리
                    {
                        str = new string[29];
                        end = 29;
                    }
                    else
                    {
                        str = new string[28];
                        end = 28;
                    }
                }
                else
                {
                    str = new string[30];
                    end = 30;
                }
            }
            for (int i = 1; i <= end; i++)
            {
                if (i < 10)
                    str[i - 1] = "0" + i;
                else
                    str[i - 1] = "" + i;
            }
            return str;
        }

        public string[] GetGradeComboBoxItems() // 6월, 9월 콤보박스 아이템
        {
            string[] str;

            str = new string[10];
            str[0] = "-";
            for (int i = 1; i <= 9; i++)
                str[i] = "" + i;

            return str;
        }
        public void SetLectureInsertForm(InsertLecture form) // InsertLecture 받아옴
        {
            insert = form;
        }

        public void SetLectureTeaTextBox() // InsertLecture 텍스트 박스 설정
        {
            insert.SetLectureTeaTextBox();
        }

        public void AddLecture(Lecture newLecture) // 신규 강의 리스트에 추가
        {
            Lectures.Add(newLecture);
        }
       
        public bool lectureRoomRedundancyCheck(int[] day, string startTime, string finishTime, string id, string classroom)//강의실 중복 검사
        {
            bool[] stdTimeArray;
            bool[] otherTimeArray;
            stdTimeArray = getLectureTimeArray(lectureTimeConvertIndex(startTime), lectureTimeConvertIndex(finishTime)); // 강의실 시간을 배열로 저장
            for (int i = 0; i < 6; i++)
            {
                if (day[i] == 1)//당일 강의가 있다면
                {
                    for (int j = 0; j < Lectures.Count; j++)
                    {
                        if (id != "" && (Lectures[j].Id == id || Lectures[j].Day[i] != 1 || Lectures[j].Classroom != classroom))//강의실 이름이 다르고 id가 같거나 그 날 강의가 없는 강의라면 비교하지 않고
                            continue;
                        otherTimeArray = getLectureTimeArray(lectureTimeConvertIndex(Lectures[j].StartTime), lectureTimeConvertIndex(Lectures[j].FinishTime)); // 당일날 강의실 같고 강의 있는 것이라면 시간 배열 저장
                        if (!getValidLecture(stdTimeArray, otherTimeArray))//시간 배열 비교 해서 값반환
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        

        public bool lecturePersonRedundancyCheck(Lecture lect, Student student) // 학생 강의 중복 검사
        {
            bool[] stdTimeArray;
            bool[][] personTimeArray = new bool[6][] { new bool[25], new bool[25], new bool[25], new bool[25], new bool[25], new bool[25] };//선생에 모든 가으이 시간 저장
            int[] day = lect.Day;
            string startTime = lect.StartTime;
            string finishTime = lect.FinishTime;
            stdTimeArray = getLectureTimeArray(lectureTimeConvertIndex(startTime), lectureTimeConvertIndex(finishTime));//강의 시간을 배열에 저장 

            // new code
            for (int i = 0; i < student.Lect.Count; i++)//그 학생 강의 시간을 2차원 배열에 요일 별로 저장
            {
                getAllLectureTimeArray(lectureTimeConvertIndex(student.Lect[i].StartTime), lectureTimeConvertIndex(student.Lect[i].FinishTime), personTimeArray, student.Lect[i].Day);
            }
            
            for (int i = 0; i < 6; i++)
            {
                if (day[i] == 1)//그 날 강의 있으면
                {
                    if (!getValidLecture(stdTimeArray, personTimeArray[i]))//시간 배열 비교 해서 값반환
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool lecturePersonRedundancyCheck(Lecture lect, Teacher Tea) // 선생 강의 중복 검사
        {
            bool[] stdTimeArray;
            bool[][] personTimeArray = new bool[6][] { new bool[25], new bool[25], new bool[25], new bool[25], new bool[25], new bool[25] };//선생에 모든 가으이 시간 저장
            int[] day = lect.Day;
            string startTime = lect.StartTime;
            string finishTime = lect.FinishTime;
            stdTimeArray = getLectureTimeArray(lectureTimeConvertIndex(startTime), lectureTimeConvertIndex(finishTime));//강의 시간을 배열에 저장

            // new code
            for (int i = 0; i < Tea.Lect.Count; i++)//그 학생 강의 시간을 2차원 배열에 요일 별로 저장
            {
                getAllLectureTimeArray(lectureTimeConvertIndex(Tea.Lect[i].StartTime), lectureTimeConvertIndex(Tea.Lect[i].FinishTime), personTimeArray, Tea.Lect[i].Day);
            }

            for (int i = 0; i < 6; i++)
            {
                if (day[i] == 1)
                {
                    if (!getValidLecture(stdTimeArray, personTimeArray[i]))//시간 배열 비교 해서 값반환
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool lecturePersonRedundancyCheck(string startT, string finishT, int[] Day, Teacher Tea) // 새로운 선생 강의 중복 검사
        {
            bool[] stdTimeArray;
            bool[][] personTimeArray = new bool[6][] { new bool[25], new bool[25], new bool[25], new bool[25], new bool[25], new bool[25] };//선생에 모든 가으이 시간 저장
            int[] day = Day;
            string startTime = startT;
            string finishTime = finishT;
            stdTimeArray = getLectureTimeArray(lectureTimeConvertIndex(startTime), lectureTimeConvertIndex(finishTime));//강의 시간을 배열에 저장

            // new code
            for (int i = 0; i < Tea.Lect.Count; i++)//그 학생 강의 시간을 2차원 배열에 요일 별로 저장
            {
                getAllLectureTimeArray(lectureTimeConvertIndex(Tea.Lect[i].StartTime), lectureTimeConvertIndex(Tea.Lect[i].FinishTime), personTimeArray, Tea.Lect[i].Day);
            }

            for (int i = 0; i < 6; i++)
            {
                if (day[i] == 1)
                {
                    if (!getValidLecture(stdTimeArray, personTimeArray[i]))//시간 배열 비교 해서 값반환
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int lectureTimeConvertIndex(string time)//강의 시간을 배열에 인덱스로 변경
        {
            int tmpTimeIndex;
            tmpTimeIndex = Int32.Parse(time.Substring(0, 2) + time.Substring(3, 2));
            if (time.Substring(3, 2) == "30")//50단위로 변경해줘서 계산 편하게 하기 위한 것
                tmpTimeIndex += 20;
            tmpTimeIndex = (tmpTimeIndex - 1000) / 50;
            return tmpTimeIndex;
        }

        private bool[] getLectureTimeArray(int startIndex, int finishIndex)//강의 시간 배열을 반환
        {
            bool[] tmpTimeArray = new bool[25];
            for (int i = startIndex; i < finishIndex; i++)//시작 시간 부터 끝 시간 인덱스 까지 true 나머지 false
                tmpTimeArray[i] = true;
            return tmpTimeArray;
        }

        private void getAllLectureTimeArray(int startIndex, int finishIndex, bool[][] personTimeArray, int[] lectDay)//월~토까지 시간표 저장
        {
            for(int i = 0; i < 6; i++)
            {
                if(lectDay[i] == 1)
                {
                    for( int j = startIndex; j < finishIndex; j++)//시작 시간 부터 끝 시간 인덱스 까지 true 나머지 false
                    {
                        personTimeArray[i][j] = true;
                    }
                }
            }
        }

        private bool getValidLecture(bool[] stdTimeArray, bool[] otherTimeArray)//강의 중복 검사
        {
            for (int i = 0; i < 25; i++)
                if (stdTimeArray[i] && otherTimeArray[i])//강의가 겹치면
                    return false;
            return true;
        }

        public int[][] GetRoomTimeSchedule(string roomName) // 강의실 이름 같을 때 중복 검사
        {
            int[][] personTimeArray = new int[6][] { new int[24], new int[24], new int[24], new int[24], new int[24], new int[24] };//선생에 모든 가으이 시간 저장

            // new code
            for (int i = 0; i < Lectures.Count; i++)
            {
                if (Lectures[i].Classroom == roomName)//강의실 이름 같은거 검사
                    getAllRoomTimeArray(lectureTimeConvertIndex(Lectures[i].StartTime), lectureTimeConvertIndex(Lectures[i].FinishTime), personTimeArray, Lectures[i].Day, i);
            }
            return personTimeArray;
        }

        private void getAllRoomTimeArray(int startIndex, int finishIndex, int[][] personTimeArray, int[] lectDay, int lectIndex)//월~토까지 시간표 및 강의 정보 저장
        {
            for (int i = 0; i < 6; i++)
            {
                if (lectDay[i] == 1)
                {
                    for (int j = startIndex; j < finishIndex; j++)
                    {
                        personTimeArray[i][j] = lectIndex + 1;//강의 인덱스 정보 저장 하지만 0을 빈 강의로 처리하기 위해 +1
                    }
                }
            }
        }

        public string GetNewStudentID(string school, string grade) // 신규 학생 아이디 부여
        {
            string id = "st", sch;
            int year;

            year = DateTime.Now.Year; // 현재 년도
            
            // 초, 중, 고 나눔
            if (school.Substring(school.Length - 3, 1) == "중")
                sch = "중";
            else
                sch = school.Substring(school.Length - 4, 1);

            // 현재 학년에서 수능 보는 년도 계산
            // IDCntIndex는 IDCnt배열에서의 인덱스를 계산 몇 번째로 가입인지
            if (sch == "고")
            {
                if (grade == "3")
                    IDCntIndex = 0;
                if (grade == "2")
                {
                    year++;
                    IDCntIndex = 1;
                }
                else if (grade == "1")
                {
                    year += 2;
                    IDCntIndex = 2;
                }
            }
            else if (sch == "중")
            {
                if (grade == "3")
                {
                    year += 3;
                    IDCntIndex = 3;
                }
                else if (grade == "2")
                {
                    year += 4;
                    IDCntIndex = 4;
                }
                else
                {
                    year += 5;
                    IDCntIndex = 5;
                }
            }
            else
            {
                if (grade == "6")
                {
                    year += 6;
                    IDCntIndex = 6;
                }
                else if (grade == "5")
                {
                    year += 7;
                    IDCntIndex = 7;
                }
                else if (grade == "4")
                {
                    year += 8;
                    IDCntIndex = 8;
                }
                else if (grade == "3")
                {
                    year += 9;
                    IDCntIndex = 9;
                }
                else if (grade == "2")
                {
                    year += 10;
                    IDCntIndex = 10;
                } 
                else
                {
                    year += 11;
                    IDCntIndex = 11;
                }
            }
            // id = st + 연도 + 해당연도가입순서
            ++IDCnt[IDCntIndex];
            if (IDCnt[IDCntIndex] < 10)
                id += year + "00" +IDCnt[IDCntIndex];
            else if(IDCnt[IDCntIndex] < 100)
                id += year + "0" + IDCnt[IDCntIndex];
            else
                id += year + "" + IDCnt[IDCntIndex];


            return id;
        }

        public string GetNewTeacherID() // 신규 선생 아이디 부여
        {
            string id;

            // 선생은 구별없이 번호 부여
            // id = te + 해당연도가입순서
            IDCnt[15]++;
            if (IDCnt[15] < 10)
                id = "te0" + IDCnt[15];
            else
                id = "te" + IDCnt[15];

            return id;
        }

        public string GetNewLectureID(string subject) // 신규 강의 아이디 부여
        {
            string id;
            
            id = "le" + DateTime.Now.Year; // 해당 연도

            // 과목 별 번호와 인덱스
            if (subject == "국어")
            {
                id += "01";
                IDCntIndex = 12;
            }
            else if (subject == "수학")
            {
                id += "02";
                IDCntIndex = 13;
            }
            else
            {
                id += "03";
                IDCntIndex = 14;
            }
            // id = le + 해당연도 + 가입순서
            ++IDCnt[IDCntIndex];
            if(IDCnt[IDCntIndex] < 10)
                id += "00" + IDCnt[IDCntIndex];
            else if (IDCnt[IDCntIndex] < 100)
                id += "0" + IDCnt[IDCntIndex];
            else
                id += "" + IDCnt[IDCntIndex];


            return id;
        }

        public void SaveStudents() // 학생 전체 저장
        {
            FileIO file;
            file = new FileIO();

            file.SaveStudentFile(Students);
        }

        public void SaveTeacher() // 선생 전체 저장
        {
            FileIO file;
            file = new FileIO();

            file.SaveTeacherFile(Teachers);
        }

        public void SaveLectures() // 강의 전체 저장
        {
            FileIO file;
            file = new FileIO();

            file.SaveLectureFile(Lectures);
            
        }

        public void SaveIDCnt() // 갯수 배열 전체 저장
        {
            FileIO file;
            file = new FileIO();

            file.SaveIDCntFile(IDCnt);
        }
        
        public void SavePostFile()
        {
            FileIO file;
            file = new FileIO();
            file.SavePostFile(Posts);
        }

        public Post FindPost(string[] data) // 포스터 검색 메소드
        {
            for(int i = 0; i < Posts.Count; i++) // 전체 포스터 중에서
            {
                if (Posts[i].Poster == data[0] && Posts[i].DateTime == data[1]) // 전달받은 작성자와 작성일자가 동일한 것 검색
                    return Posts[i]; // 발견하면 리턴
            }
            return null; // 없으면 널값
        }

        public void AddPost(Post newPost) // 공지사항 리스트에 새로운 공지사항 삽입
        {
            Posts.Add(newPost);
        }

        public void DeletePost(Post post) // 공지사항 리스트에서 해당 공지사항 삭제
        {
            Posts.Remove(post);
        }
        
    }
}
