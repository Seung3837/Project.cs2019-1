using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team.View
{
    public partial class ChangeTeacherForm : Form // 강의의 담당 선생을 교체하는 폼
    {
        private Controller cont; // 컨트롤러
        private Lecture lecture; // 해당 강의
        private Teacher selectedTea; // 선택된 선생
        private List<Teacher> teachers; // 전체 선생들 리스트
        
        private int index; // 해당 폼이 삽입으로 생성된것인지, 수정으로 생성된것인지 구분
        

        public ChangeTeacherForm(Controller cont, Lecture lecture) // 강의 수정시 ListForm에서 사용
        {
            InitializeComponent();
            this.cont = cont; // 컨트롤러
            this.lecture = lecture; // 해당 강의 
            selectedTea = lecture.Tea; // 선택된 선생
            GetTeachersView(); // 왼쪽의 데이터 그리드 뷰 선택된 선생을 제외한 모든 선생 출력
            index = 0; // 수정으로 생성된 폼
            GetSelectedTeacherView(); // 선택된 선생을 출력
        }

        public ChangeTeacherForm(Controller cont, Lecture lecture, int index) // 강의 삽입시 InsertLecture에서 사용
        { // 위 생성자와 동일
            InitializeComponent();
            this.cont = cont;
            this.lecture = lecture;
            this.index = index;
            selectedTea = lecture.Tea;
            GetTeachersView();

            GetSelectedTeacherView();
        }

        public void GetTeachersView() // 선택되지 않은 모든 선생을 출력
        { 
            string[] row; // 그리드 뷰의 행에 들어갈 값
            TeachersView.Rows.Clear(); // 행 모두 삭제
            TeachersView.Columns.Clear(); // 열 모두 삭제
            TeachersView.ReadOnly = true; // 읽기 전용
            TeachersView.AllowUserToAddRows = false; // 행 삽입 제한
            TeachersView.RowHeadersVisible = false; // 행 헤더 보기 제한


            TeachersView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // 셀 선택 금지, 행 단위로 선택 가능
            row = new string[4]; // string 배열 생성
            TeachersView.ColumnCount = 4; // 열 4개
            TeachersView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 중앙정렬
            TeachersView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 중앙정렬
            for (int i = 0; i < 4; i++)
                TeachersView.Columns[i].Width = 55; // 열 너비 55

            // 열 이름 지정
            TeachersView.Columns[0].Name = "교번";
            TeachersView.Columns[1].Name = "이름";
            TeachersView.Columns[2].Name = "직책";
            TeachersView.Columns[3].Name = "전공";

      
            teachers = cont.Teachers; // 컨트롤러에서 선생 목록 받음

            for (int i = 0; i < teachers.Count; i++) // 모든 선생을
            {
                if (teachers[i] == selectedTea) // 선택된 선생일 시 넘어감
                    continue;
                // 정보 string 배열에 옮김
                row[0] = teachers[i].Id;
                row[1] = teachers[i].Name;
                row[2] = teachers[i].Position;
                row[3] = teachers[i].Major;

                TeachersView.Rows.Add(row); // 행 추가
            }


        }

        public void GetSelectedTeacherView()
        { // 위의 GetTeachersView()와 동일하지만 선택된 선생만 출력
            string[] row;
            selectedTeacherView.Rows.Clear();
            selectedTeacherView.Columns.Clear();
            selectedTeacherView.ReadOnly = true;
            selectedTeacherView.AllowUserToAddRows = false;
            selectedTeacherView.RowHeadersVisible = false;


            selectedTeacherView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            row = new string[4];
            selectedTeacherView.ColumnCount = 4;
            selectedTeacherView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            selectedTeacherView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < 4; i++)
                selectedTeacherView.Columns[i].Width = 55;

            selectedTeacherView.Columns[0].Name = "교번";
            selectedTeacherView.Columns[1].Name = "이름";
            selectedTeacherView.Columns[2].Name = "직책";
            selectedTeacherView.Columns[3].Name = "전공";

            if (selectedTea != null)
            {
                row[0] = selectedTea.Id;
                row[1] = selectedTea.Name;
                row[2] = selectedTea.Position;
                row[3] = selectedTea.Major;

                selectedTeacherView.Rows.Add(row);
            }
        }
        
        private void ChangeButton_Click(object sender, EventArgs e)
        { // 변경 버튼 클릭시 
            string id; // id string 선언
            id = TeachersView.Rows[TeachersView.CurrentCellAddress.Y].Cells[0].Value.ToString(); // 선택된 행의 id부분 추출
            selectedTea = cont.FindOnlyTeacher(id); // FindOnlyTeacher 메소드 호출하여 선생 아이디로 검색
            // 두 개의 데이터 그리드 뷰 변경 값으로 다시 출력
            GetSelectedTeacherView(); 
            GetTeachersView();
        }

        private void OKButton_Click(object sender, EventArgs e)
        { // 확인 버튼 클릭시
            if(lecture.Tea != null) // 해당 강의가 원래 담당 선생이 없었다면
                lecture.Tea.DeleteLecture(lecture); // 담당 선생 DeleteLecture 메소드 호출
            lecture.Tea = selectedTea; // 선택된 강의의 담당 선생 변경
            if (index == 0) // 수정으로 생성된 폼이면
                cont.GetViews(); // ListForm의 데이터 그리드 뷰와 textbox 설정
            else // 삽입으로 생성된 폼이면
                cont.SetLectureTeaTextBox(); // InsertLecture의 textbox 설정
            Close(); // 폼 종료
        }
    }
}
