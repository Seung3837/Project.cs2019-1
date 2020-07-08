using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team
{
    public class People
    {

        public string Name { get; set; } // 이름
        public string Id { get; set; } // ID
        public string PhoneNumber { get; set; } // 전화번호
        public string Birth { get; set; } // 생년월일
        

        public List<Lecture> Lect { get; set; } // 학생 : 수강 중인 강의 리스트, 선생 : 담당 중인 강의 리스트

        public People()
        { // 공백 값으로 초기화하는 생성자
            Name = ""; 
            Id = "";
            PhoneNumber = "";
            Birth = "";
            Lect = new List<Lecture>();
        }

        public People(string name, string id, string phoneNumber, string birth)
        { // 강의를 제외한 나머지 입력 받은 값으로 초기화하는 생성자
            Name = name;
            Id = id;
            PhoneNumber = phoneNumber;
            Birth = birth;
            Lect = new List<Lecture>();
        }

        public People(string name, string id, string phoneNumber, string birth, List<Lecture> lecture)
        { // 강의를 포함한 입력 받은 값으로 초기화하는 생성자
            Name = name;
            Id = id;
            PhoneNumber = phoneNumber;
            Birth = birth;
            Lect = lecture;
        }


        // 오버라이딩 사용 이유는 학생과 선생의 삭제 루틴이 다르기 때문
        public virtual void AddLecture(Lecture lecture) { } // 강의를 추가하는 가상함수

        public virtual void DeleteLecture(Lecture lecture) { } // 강의를 삭제하는 가상함수

        public virtual void DeleteAllLecture() { } // 모든 강의를 삭제하는 가상함수

        public virtual void MappingLecture(string AllLectures, List<Lecture> lectureList) { } // 파일을 Load한 후 강의와 선생, 학생을 매핑해주는 가상 함수
        ~People()
        {

        }



    }
}
