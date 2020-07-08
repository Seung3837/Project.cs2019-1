using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team
{
    public class Lecture
    {
        
        private string teaId;
        
        private List<string> stuIds;

        public int[] Day { get; set; } // 월, 화, 수, 목, 금, 토 로 나눈 크기 6의 int 배열로 1이면 강의 해당 요일이고 0이면 해당 요일이 아님
        public string Subject { get; set; } // 해당 강의가 국어, 수학, 영어 중 어떤 것인지
        public string Classroom { get; set; } // 강의실 
        public string StartTime { get; set; } // 시작 시간
        public string FinishTime { get; set; } // 종료 시간
        public Teacher Tea { get; set; }  // 담당 선생
        public string ClassName { get; set; } // 강의명
        public string Id { get; set; } // 강의번호
        public string TeaId { get { return teaId; } set { this.teaId = value; } }
        public List<Student> Students { get; set; } // 강의를 수강하는 학생 목록
        public int LectNum { get; set; }
        public Lecture()
        { // 공백값으로 초기화하는 생성자
            Classroom = "";
            StartTime = "";
            FinishTime = "";
            Day = new int[6];
            Subject = "";
            Tea = null;
            Id = "";
            Students = new List<Student>();

        }
       
        public Lecture(string id, string classroom, int[] day, string startTime, string finishTime, string teaId, string classname, string subject, string stuIdList, string lectNum)
        { //생성자 들어온 값을 저장
            Id = id;
            Classroom = classroom;
            Day = day;
            StartTime = startTime;
            FinishTime = finishTime;
            TeaId = teaId;
            Subject = subject;
            ClassName = classname;
            string[] tmpStudentId = stuIdList.Split(',');//학생 정보 나누기
            stuIds = new List<string>();
            foreach (string tmpStdId in tmpStudentId)
            {
                stuIds.Add(tmpStdId);
                stuIds.Sort();
            }
            LectNum = Int32.Parse(lectNum);
            Students = new List<Student>();
        }
        
        public void RemoveLecture()
        { // 강의를 삭제하는 메소드
            for(int i = Students.Count - 1; i >= 0; i--) // 해당 강의를 수강하는 학생의 마지막 인덱스부터 0 인덱스까지
                Students[i].DeleteLecture(this); // 리스트에 있는 학생의 DeleteLecture메소드 호출

            Tea.DeleteLecture(this); // 담당 선생의 DeleteLecture메소드 호출
        }
        
        ~Lecture()
        {

        }


    }
}
