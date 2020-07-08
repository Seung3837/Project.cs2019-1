using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Team.Model;
//이거다-------------------------------------------
namespace Team
{
    class FileIO
    {
        private string basicRoute = @"C:\Team8ProjectForder";// 폴더를 읽기 위한 기본 위치
        public int[] LoadIDCntFile(string filename)//ID값 txt에서 불러오기
        {
            int[] idCnt = new int[16];//// 신규 가입시 아이디 배정 참조 카운트 배열
            string[] loadRow;//한 행 정보를 split해서 저장
            using (StreamReader idReader = new StreamReader(filename, Encoding.Default))//한글 깨지지 않기 위해 Encoding.Default
            {
                string loadLine;//한 행씩 읽는다
                while ((loadLine = idReader.ReadLine()) != null)//끝까지 읽기
                {
                    loadRow = loadLine.Split('@');//@기준으로 나눠 저장
                    for (int i = 0; i < 16; i++)
                    {
                        idCnt[i] = Int32.Parse(loadRow[i]);//string을 정수로 저장
                    }
                }
            }
            return idCnt;
        }
        public void SaveIDCntFile(int[] IDCnt)//ID값 txt에 저장
        {
            string filename = basicRoute + @"\TeamProjectFileSaveForder\IDCnt.txt";//저장 경로
            using (StreamWriter idWriter = new StreamWriter(filename, false, Encoding.Default))//한글 깨지지 않기 위해 Encoding.Default
            {
                string saveRow = "";
                for (int i = 0; i < 15; i++)//ID값 @로 구분
                {
                    saveRow = saveRow + IDCnt[i] + "@";
                }
                saveRow = saveRow + IDCnt[15];
                idWriter.WriteLine(saveRow);//txt에 적기
            }
        }
        public List<Lecture> LoadLectureFile(string filename)//txt에서 lecture 불러오기
        {
            List<Lecture> loadLectureList = new List<Lecture>();//반환할 Lecture리스트
            string[] loadRow, tmpDayString;//한 행 정보를 split해서 저장, 날짜 정보 저장
            using (StreamReader lectureReader = new StreamReader(filename, Encoding.Default))//한글 깨지지 않기 위해 Encoding.Default
            {
                string loadLine;
                while((loadLine = lectureReader.ReadLine()) != null)
                {
                    loadRow = loadLine.Split('@');
                    int[] tmpDay = new int[6];
                    tmpDayString = loadRow[2].Split(',');//날짜 정보만 따로 저장
                    for (int i = 0; i < 6; i++)
                    {
                        tmpDay[i] = Int32.Parse(tmpDayString[i]);//날짜 정보를 저장.
                    }
                    loadLectureList.Add(new Lecture(loadRow[0], loadRow[1], tmpDay, loadRow[3], loadRow[4], loadRow[5], loadRow[6], loadRow[7], loadRow[8], loadRow[9]));//생성자로 생성 후 리스트에 추가
                }
            }
            return loadLectureList;
        }
        public void SaveLectureFile(List<Lecture> lectureList)//txt에 lecture 저장
        {
            string filename = basicRoute + @"\TeamProjectFileSaveForder\Lecture.txt";//저장 경로
            using (StreamWriter lectureWriter = new StreamWriter(filename, false, Encoding.Default))//한글 깨지지 않기 위해 Encoding.Default
            {
                for (int i = 0; i < lectureList.Count; i++)//각 @에 순서에 맞게 한 줄로 만들어 적기
                {
                    string saveRow = "";
                    saveRow = lectureList[i].Id + "@" + lectureList[i].Classroom + "@";
                    for (int j = 0; j < 5; j++)
                        saveRow = saveRow + lectureList[i].Day[j] + ",";
                    saveRow = saveRow + lectureList[i].Day[5] + "@";
                    saveRow = saveRow + lectureList[i].StartTime + "@" + lectureList[i].FinishTime + "@" + lectureList[i].TeaId + "@" + lectureList[i].ClassName + "@" + lectureList[i].Subject + "@";
                    if (lectureList[i].Students.Count != 0)
                    {
                        lectureList[i].Students.Sort((firstStu, secondStu) => Int32.Parse(firstStu.Id.Substring(2)).CompareTo(Int32.Parse(secondStu.Id.Substring(2))));//학생 학번 순으로 정렬
                        for (int j = 0; j < lectureList[i].Students.Count - 1; j++)
                            saveRow = saveRow + lectureList[i].Students[j].Id + ",";
                        saveRow = saveRow + lectureList[i].Students[lectureList[i].Students.Count - 1].Id;
                    }
                    saveRow = saveRow + "@" + lectureList[i].LectNum;
                    lectureWriter.WriteLine(saveRow);//txt에 적기
                }
            }
        }

        public List<Teacher> LoadTeacherFile(string filename, List<Lecture> lectureList)//txt에서 teacher 불러오기
        {
            List<Teacher> loadTeacherList = new List<Teacher>();
            string[] loadRow;
            int listCnt = 0;
            using (StreamReader teacherReader = new StreamReader(filename, Encoding.Default))
            {
                string loadLine;
                while((loadLine = teacherReader.ReadLine()) != null)//@기준으로 분해 후 순서에 맞게 생성자 호출
                {
                    loadRow = loadLine.Split('@');
                    loadTeacherList.Add(new Teacher(loadRow[0], loadRow[1], loadRow[2], loadRow[3], loadRow[4], loadRow[5]));
                    loadTeacherList[listCnt].MappingLecture(loadRow[6], lectureList);//미리 저장한 강의를 맵핑
                    listCnt++;
                }
            }
            return loadTeacherList;
        }
        public void SaveTeacherFile(List<Teacher> teacherList)//txt에 선생님 정보 저장
        {
            string filename = basicRoute + @"\TeamProjectFileSaveForder\Teacher.txt";//저장 경로
            using (StreamWriter teacherWriter = new StreamWriter(filename, false, Encoding.Default))
            {
                for (int i = 0; i < teacherList.Count; i++)//각 @에 순서에 맞게 한 줄로 만들어 적기
                {
                    string saveRow = "";
                    teacherList.Sort((firstLect, secondLect) => Int32.Parse(firstLect.Id.Substring(2)).CompareTo(Int32.Parse(secondLect.Id.Substring(2))));
                    saveRow = saveRow + teacherList[i].Name + "@" + teacherList[i].Id + "@" + teacherList[i].PhoneNumber + "@" + teacherList[i].Birth + "@" + teacherList[i].Position + "@" + teacherList[i].Major + "@";
                    if (teacherList[i].Lect.Count != 0)
                    {
                        for (int j = 0; j < teacherList[i].Lect.Count - 1; j++)
                            saveRow = saveRow + teacherList[i].Lect[j].Id + ",";
                        saveRow = saveRow + teacherList[i].Lect[teacherList[i].Lect.Count - 1].Id;
                    }
                    teacherWriter.WriteLine(saveRow);
                }
            }
        }

        public List<Student> LoadStudentFile(string filename, List<Lecture> lectureList)//txt에서 학생 정보 불러오기
        {
            List<Student> loadStudentList = new List<Student>();
            string[] loadRow;
            int listCnt = 0;
            using (StreamReader studentReader = new StreamReader(filename, Encoding.Default))
            {
                string loadLine;
                while((loadLine = studentReader.ReadLine()) != null)//@기준으로 분해 후 순서에 맞게 생성자 호출
                {
                    loadRow = loadLine.Split('@');
                    loadStudentList.Add(new Student(loadRow[0], loadRow[1], loadRow[2], loadRow[3], loadRow[4], loadRow[5], loadRow[7], loadRow[8]));
                    loadStudentList[listCnt].MappingLecture(loadRow[6], lectureList);//미리 저장한 강의를 맵핑
                    listCnt++;
                }
            }
            return loadStudentList;
        }
        public void SaveStudentFile(List<Student> studentList)//txt에 학생 정보 저장
        {
            string filename = basicRoute + @"\TeamProjectFileSaveForder\Student.txt";
            using (StreamWriter studentWriter = new StreamWriter(filename, false, Encoding.Default))
            {
                for (int i = 0; i < studentList.Count; i++)//각 @에 순서에 맞게 한 줄로 만들어 적기
                {
                    string saveRow = "";
                    studentList.Sort((firstLect, secondLect) => Int32.Parse(firstLect.Id.Substring(2)).CompareTo(Int32.Parse(secondLect.Id.Substring(2))));
                    saveRow = saveRow + studentList[i].Name + "@" + studentList[i].Id + "@" + studentList[i].PhoneNumber + "@" + studentList[i].Birth + "@" + studentList[i].Grade + "@" + studentList[i].School + "@";
                    if (studentList[i].Lect.Count != 0)
                    {
                        for (int j = 0; j < studentList[i].Lect.Count - 1; j++)
                            saveRow = saveRow + studentList[i].Lect[j].Id + ",";
                        saveRow = saveRow + studentList[i].Lect[studentList[i].Lect.Count - 1].Id + "@";
                    }
                    else
                        saveRow = saveRow + "@";
                    saveRow = saveRow + studentList[i].ParentPhone + "@";
                    saveRow = saveRow + studentList[i].KorGrade[0] + "," + studentList[i].KorGrade[1] + "," + studentList[i].MathGrade[0] + "," + studentList[i].MathGrade[1] + "," + studentList[i].EngGrade[0] + "," + studentList[i].EngGrade[1];
                    studentWriter.WriteLine(saveRow);
                }
            }
        }
        public List<Post> LoadPostFile(string filename)//txt에서 공지사항 불러오기
        {
            List<Post> loadPostList = new List<Post>();
            string[] loadRow;
            using (StreamReader studentReader = new StreamReader(filename, Encoding.Default))
            {
                string loadLine;
                while ((loadLine = studentReader.ReadLine()) != null)//@기준으로 분해 후 순서에 맞게 생성자 호출
                {
                    loadRow = loadLine.Split('@');
                    loadPostList.Add(new Post(loadRow[0], loadRow[1], loadRow[2], loadRow[3]));
                }
            }
            return loadPostList;
        }
        public void SavePostFile(List<Post> postList)//txt에서 공지사항 저장
        {
            string filename = basicRoute + @"\TeamProjectFileSaveForder\Post.txt";//저장 경로
            using (StreamWriter postWriter = new StreamWriter(filename, false, Encoding.Default))
            {
                for (int i = 0; i < postList.Count; i++)//각 @에 순서에 맞게 한 줄로 만들어 적기
                {
                    string saveRow = "";
                    saveRow = saveRow + postList[i].DateTime + "@" + postList[i].Poster + "@" + postList[i].Title + "@" + postList[i].Content;
                    postWriter.WriteLine(saveRow);
                }
            }
        }
    }
}
