using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team
{
    public class Student : People
    {

        public int[] KorGrade { get; set; } // 6월, 9월 모의고사 국어 등급
        public int[] MathGrade { get; set; } // 6월, 9월 모의고사 수학 등급
        public int[] EngGrade { get; set; } // 6월, 9월 모의고사 영어 등급
        public string Grade { get; set; } // 학년
        public string School { get; set; } // 학교
        public int UsingTime { get; set; } // 독서실 이용시간

        public string ParentPhone { get; set; } // 학부모 번호
        

        public Student(string name, string id, string phoneNumber, string birth, string grade, string school, string parentPhone, string allGrade) : base(name, id, phoneNumber, birth)
        { //들어온 값에 맞게 저장
            Grade = grade;
            School = school;
            ParentPhone = parentPhone;
            string[] tmpAllGrade = allGrade.Split(',');
            KorGrade = new int[2];
            MathGrade = new int[2];
            EngGrade = new int[2];
            for (int i = 0; i < 2; i++)//모든 성적을 string으로 저장하여 분해
            {
                KorGrade[i] = Int32.Parse(tmpAllGrade[0 + i]);
                MathGrade[i] = Int32.Parse(tmpAllGrade[2 + i]);
                EngGrade[i] = Int32.Parse(tmpAllGrade[4 + i]);
            }
        }
        public Student() : base()
        { // 공백값으로 초기화하는 생성자
            Grade = "";
            School = "";
            ParentPhone = "";
            KorGrade = new int[2];
            KorGrade[0] = 0;
            KorGrade[1] = 0;
            EngGrade = new int[2];
            EngGrade[0] = 0;
            EngGrade[1] = 0;
            MathGrade = new int[2];
            MathGrade[0] = 0;
            MathGrade[1] = 0;
            UsingTime = 0;
        }
        public Student(string name, string id, string phoneNumber, string birth, string grade, string school) : base(name, id, phoneNumber, birth)
        { // 넘겨받지 못한 정보는 공백으로 처리하고 넘겨받은 정보는 입력하는 생성자
            Grade = grade;
            School = school;
            UsingTime = 0;
            ParentPhone = "";
            KorGrade = new int[2];
            KorGrade[0] = 0;
            KorGrade[1] = 0;
            EngGrade = new int[2];
            EngGrade[0] = 0;
            EngGrade[1] = 0;
            MathGrade = new int[2];
            MathGrade[0] = 0;
            MathGrade[1] = 0;
        }

        public override void AddLecture(Lecture lecture) // 강의를 수강하는 메소드
        {
            Lect.Add(lecture); // 학생의 강의리스트에 해당 강의 추가
            lecture.Students.Add(this); // 해당 강의의 학생 리스트에 학생 추가
        }

        public override void DeleteLecture(Lecture lecture) // 강의를 삭제하는 메소드
        {
            Lect.Remove(lecture); // 학생의 강의리스트에서 해당 강의 삭제
            lecture.Students.Remove(this); // 해당 강의의 학생 리스트에서 학생 삭제
        }

        public override void DeleteAllLecture() // 수강 중인 모든 강의를 삭제하는 메소드
        {
            for (int i = Lect.Count - 1; i >= 0; i--) // 수강 중인 강의 리스트에서 가장 큰 인덱스부터 0인덱스까지
            {
                Lect[i].Students.Remove(this); // 해당 강의의 학생 리스트에서 학생 삭제
                Lect.RemoveAt(i); // 해당 인덱스의 강의 삭제
            }
        }


        public override void MappingLecture(string allLectures, List<Lecture> lectureList)
        {
            //누락된 정보 있는 예외처리 해야함
            string[] lectures = allLectures.Split(',');
            //Console.WriteLine("1234512335343434342353");
            Array.Sort(lectures);
            /*
            foreach (var i in lectures)
            {
                Console.Write(i);
            }
            Console.WriteLine();*/
            foreach (string lectureId in lectures)
            {
                foreach (Lecture lecture in lectureList)
                {
                    if (lectureId.Equals(lecture.Id))
                    {
                        Lect.Add(lecture);
                        lecture.Students.Add(this);
                        break;
                    }
                }
            }
        }

        ~Student()
        {

        }
    }
}
