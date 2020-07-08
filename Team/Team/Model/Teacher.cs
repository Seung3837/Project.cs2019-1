using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team
{
    public class Teacher : People
    {

        public string Position { get; set; } // 직급
        public string Major { get; set; } // 수학, 영어, 국어 중 전공
        public Teacher() : base()
        { // 공백값으로 초기화하는 생성자
            Position = "";
            Major = "";
        }
        
        public Teacher(string name, string id, string phoneNumber, string birth, string position, string major) : base(name, id, phoneNumber, birth)
        { // 강의값 없이 초기화하는 생성자
            Major = major;
            Position = position;
        }

        public override void AddLecture(Lecture lecture)
        { // 강의를 추가하는 메소드
            Lect.Add(lecture); // 담당 중인 강의 리스트에 추가
            lecture.Tea = this; // 해당 강의의 선생으로 설정
        }

        public void ChangeTeacher(Lecture lecture, Teacher other_teacher) // 강의 담당 선생 변경
        {
            DeleteLecture(lecture); // 담당 중인 강의 리스트에서 해당 강의 삭제
            if (other_teacher != null) // 선생이 미배정인 강의의 수정 상황에서의 예외 처리
            {
                other_teacher.AddLecture(lecture); // 교체를 원하는 선생이 강의추가 메소를 호출
            }
        }

        public override void DeleteLecture(Lecture lecture) // 강의 삭제 
        {
            Lect.Remove(lecture); // 담당 중인 강의 리스트에서 해당 강의 삭제
            lecture.Tea = null; // 해당 강의 담당 선생 없앰
        }

        public override void DeleteAllLecture() // 모든 강의 삭제
        {
            for (int i = Lect.Count - 1; i >= 0; i--) // 담당 중인 강의 리스트의 가장 큰 인덱스부터 0인덱스까지
            {
                Lect[i].Tea = null; // 해당 강의의 선생 없앰
                Lect.RemoveAt(i); // 강의 리스트에서 해당 인덱스 강의 삭제
            }
        }

        public override void MappingLecture(string allLectures, List<Lecture> lectureList)
        {
            //누락된 정보 있는 예외처리 해야함
            string[] lectures = allLectures.Split(',');
            //Console.WriteLine("ABCD");
            Array.Sort(lectures);
            /*
            foreach(var i in lectures)
            {
                Console.Write(i);
            }
            Console.WriteLine();*/
            foreach(string lectureId in lectures)
            {
                foreach (Lecture lecture in lectureList)
                {
                    if (lectureId.Equals(lecture.Id))
                    {
                        Lect.Add(lecture);
                        lecture.Tea = this;
                        break;
                    }
                }
            }
        }

        ~Teacher()
        {

        }


    }
}
