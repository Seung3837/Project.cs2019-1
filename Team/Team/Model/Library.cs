using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team
{
    public class Library
    {
        public Desk[] desk; // 독서실의 책상
        

        public Library()
        { // 책상 20개를 생성하는 독서실 생성자
            desk = new Desk[20];
            for (int i = 0; i < 20; i++)
                desk[i] = new Desk();
        }
        public class Desk 
        {

            public Student Stu { get; set; } // 독서실 사용 중인 학생
            public DateTime StartTime { get; set; } // 학생이 해당 좌석을 사용 시작한 시간
            public DateTime FinishTime { get; set; } // 학생이 해당 좌석을 사용 종료한 시간
            public int State { get; set; } // 해당 좌석의 사용 여부, 0 : 미사용, 1 : 사용

            public Desk()
            { // 공백 상태로 좌석 생성
                Stu = null; 
                State = 0; 
            }
            ~Desk()
            {
                if (Stu != null) // 프로그램 종료 시 사용 중인 학생이 있으면
                    finishDesk(); // 해당 학생의 책상 사용 종료
            }

            public void startDesk(Student student) // 좌석 사용 시작
            { 
                this.Stu = student; // 사용 중인 학생 설정
                StartTime = DateTime.Now; // 시작 시간 저장
                State = 1; // 사용 
            }

            public void finishDesk() // 좌석 사용 종료
            { 
                State = 0; // 미사용
                FinishTime = DateTime.Now; // 종료 시간 저장
                TimeSpan diffTime = FinishTime - StartTime; // 시간 차이 계산
                Stu.UsingTime += diffTime.Hours * 3600 + diffTime.Minutes * 60 + diffTime.Seconds; // 해당 학생의 좌석 이용 시간 초단위로 저장
                Stu = null; // 학생 공백
            }
        }

    }
}
