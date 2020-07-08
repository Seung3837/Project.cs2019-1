using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team
{
    public partial class ListForm : Form
    {
        private string basicRoute = @"C:\Team8ProjectForder";//기본 저장 경로
        private Controller cont; // 컨트롤러
        public int index; // 0 : 학생, 1 : 선생, 2 : 강의
        public int searchIndex; // 검색 내용 콤보박스의 인덱스 정보, 0 : 전체(검색 불가), 1 : 번호, 2 : 이름 등
        public string searchText; // 검색 텍스트
        public List<Student> students; // 학생 리스트
        public List<Teacher> teachers; // 선생 리스트
        public List<Lecture> lectures; // 강의 리스트
        public Student selectedStudent; // 데이터 그리드 뷰에서 선택된 학생
        public Teacher selectedTeacher; // 데이터 그리드 뷰에서 선택된 선생
        public Lecture selectedLecture; // 데이터 그리드 뷰에서 선택된 강의
        private Teacher tmpTea; // 강의 수정 시 해당 강의의 담당 선생을 잠시 저장하는 객체
        private List<Lecture> tmpLec; // 학생, 선생 수정 시 수강하거나 담당하고 있는 강의 목록을 잠시 저장하는 객체
        bool updateState; // 수정 중인지 아닌지 판별하는 변수
        

        public ListForm(Controller cont)
        {
            this.cont = cont; // 컨트롤러
            InitializeComponent();
            index = 0; // 학생으로 설정
            searchIndex = 0; // 전체로 설정
            updateState = false; // 수정 상태 아님
            selectedStudent = null; // 선택된 학생 없음
            selectedTeacher = null; // 선택된 선생 없음
            selectedLecture = null; // 선택된 강의 없음
            searchText = ""; // 검색 텍스트 없음
            SelectTextBox.ReadOnly = true; // 검색 텍스트 박스 읽기 전용
            SetChooseGroupBox(); // 0 : 학생, 1 : 선생, 2 : 강의 중 어떤 목록을 보여줄지 정하는 콤보박스
            SetChooseAttributeBox(); // 어떤 속성으로 검색할지 정하는 콤보박스
            GetInfoListView(); // 그룹박스에서 선택된 정보에 따른 리스트를 데이터그리드뷰에 뿌려주는 메소드 (0 : 학생, 1 : 선생, 2 : 강의)
            SetLectureTimeComboBox(); // 강의가 선택됐을 시 보이는 박스들 세팅
            SetDateComboBox(); // 학생이 선택됐을 시 보이는 박스 세팅
            SetStuTeaBoxTool(); // 학생, 선생이 선택됐을 시 보이는 박스 세팅
            SetStuTeaBox(); // 학생, 선생 초기 정보 설정
            SetImageBox(); // 이미지 박스 설정
            cont.GetListForm(this); // 컨트롤러에 해당 리스트폼 객체를 보냄
        }

        private void SetStuTeaBoxTool() // 학생, 선생 박스 세팅
        {
            // 박스 초기값 공백 설정
            IDTextBox.Text = "";
            NameTextBox.Text = "";
            SchoolTextBox.Text = "";
            PhoneTextBox1.Text = "";
            PhoneTextBox2.Text = "";
            PhoneTextBox3.Text = "";
            
            LectureBox.Visible = false; // 강의 그룹박스 안 보이게 설정
            StuTeaBox.Visible = true; // 학생, 선생 그룹박스 보이게 설정
            
            // 업데이트 확인, 취소 버튼 안보이게 설정
            StuTeaUpdateOKButton.Visible = false; 
            StuTeaUpdateNoButton.Visible = false;

            // 각종 정보 박스들 사용 할 수 없게 설정(수정 버튼 안 누를 시 수정 불가)
            IDTextBox.Enabled = false;
            NameTextBox.Enabled = false;
            SchoolTextBox.Enabled = false;
            SchoolComboBox.Enabled = false;
            GradeComboBox.Enabled = false;
            PhoneTextBox1.Enabled = false;
            PhoneTextBox2.Enabled = false;
            PhoneTextBox3.Enabled = false;
            ImageButton.Visible = false; // 이미지 수정 버튼 보이지 않게 설정
            yearComboBox.Enabled = false;
            monthComboBox.Enabled = false;
            dayComboBox.Enabled = false;

            myLecture.CheckState = CheckState.Checked; // 내 강의만 보기 체크박스 체크된 상태 설정
            myLecture.Enabled = false; // 체크박스도 이용 불가
            myLecture.Visible = true; // 보이게 설정

            if (index == 0) // 학생
            {
                // 선생과 공유하고 있는 그룹박스 이름, 텍스트 박스 및 라벨 이름 설정
                SelectBox.Text = "학생 목록";
                StuTeaBox.Text = "학생 정보";
                IDLabel.Text = "학      번";
                SchoolLabel.Text = "학      교";
                GradeLabel.Text = "학      년";
                ParentPhone1.Text = "";
                ParentPhone2.Text = "";
                ParentPhone3.Text = "";

                // 보호자 번호, 6월, 9월 모의고사 정보 사용 불가
                ParentPhone1.Enabled = false;
                ParentPhone2.Enabled = false;
                ParentPhone3.Enabled = false;
                korComboBox1.Enabled = false;
                mathComboBox1.Enabled = false;
                engComboBox1.Enabled = false;
                korComboBox2.Enabled = false;
                mathComboBox2.Enabled = false;
                engComboBox2.Enabled = false;

                UsingTimeLabel.Visible = true; // 독서실 이용 시간 라벨 보이게 설정
                LibraryUsingTimeLabel.Visible = true; // 독서실 이용 시간 보이게 설정
                
                // 보호자 번호 보이게 설정
                ParentPhone1.Visible = true; 
                ParentPhone2.Visible = true;
                ParentPhone3.Visible = true;
                
                // 6월, 9월 모의고사 등급 관련 정보 보이게 설정
                korComboBox1.Visible = true; 
                mathComboBox1.Visible = true;
                engComboBox1.Visible = true;
                korComboBox2.Visible = true;
                mathComboBox2.Visible = true;
                engComboBox2.Visible = true;
                korGradeLabel1.Visible = true;
                mathGradeLabel1.Visible = true;
                engGradeLabel1.Visible = true;
                label7.Visible = true;
                label6.Visible = true;
                ExamScoreLabel.Visible = true;
                ParentNumLabel.Visible = true;
                korGradeLabel2.Visible = true;
                mathGradeLabel2.Visible = true;
                engGradeLabel2.Visible = true;
                label13.Visible = true;
                label15.Visible = true;

                // 선생과 학생이 공유하고 있는 콤보 박스, 그룹박스, 데이터 그리드 뷰의 크기 및 위치 재설정
                SchoolComboBox.Width = 77;
                SchoolComboBox.Location = new Point(172, 96);
                GradeComboBox.Width = 35;
                StuTeaUpdateOKButton.Location = new Point(346, 313);
                StuTeaUpdateNoButton.Location = new Point(427, 313);
                StuTeaBox.Height = 344;
                GroupDataTable.Height = 175;
                GroupDataTable.Location = new Point(367, 407);
                myLecture.Location = new Point(773, 385);
            }

                else
            {
                // 학생과 공유하고 있는 그룹박스 및 라벨의 텍스트 재설정
                SelectBox.Text = "선생 목록";
                StuTeaBox.Text = "선생 정보";
                IDLabel.Text = "교      번";
                SchoolLabel.Text = "직      급";
                GradeLabel.Text = "과      목";

                // 위의 학생의 설정과 반대되는 설정
                UsingTimeLabel.Visible = false;
                LibraryUsingTimeLabel.Visible = false;
                ParentPhone1.Visible = false;
                ParentPhone2.Visible = false;
                ParentPhone3.Visible = false;
                korComboBox1.Visible = false;
                mathComboBox1.Visible = false;
                engComboBox1.Visible = false;
                korComboBox2.Visible = false;
                mathComboBox2.Visible = false;
                engComboBox2.Visible = false;
                korGradeLabel1.Visible = false;
                mathGradeLabel1.Visible = false;
                engGradeLabel1.Visible = false;
                label7.Visible = false;
                label6.Visible = false;
                ExamScoreLabel.Visible = false;
                ParentNumLabel.Visible = false;
                korGradeLabel2.Visible = false;
                mathGradeLabel2.Visible = false;
                engGradeLabel2.Visible = false;
                label13.Visible = false;
                label15.Visible = false;

                SchoolComboBox.Width = 64;
                SchoolComboBox.Location = new Point(101, 96);
                GradeComboBox.Width = 64;
                StuTeaUpdateOKButton.Location = new Point(346, 249);
                StuTeaUpdateNoButton.Location = new Point(427, 249);
                StuTeaBox.Height = 278;
                GroupDataTable.Height = 249;
                GroupDataTable.Location = new Point(367, 341);
                myLecture.Location = new Point(773, 319);
            }
        }

        private void SetDateComboBox() // 학생의 초기 박스 설정
        {
            string[] year, month, grade; // 생년월일의 연, 월, 모의고사 성적 콤보박스에 넣을 string 배열

            // 컨트롤러에서 정보 받아옴
            year = cont.GetYearComboboxItems();
            month = cont.GetMonthComboBoxItems();
            grade = cont.GetGradeComboBoxItems();
            
            // 연, 월 콤보박스에 삽입
            yearComboBox.Items.AddRange(year);
            monthComboBox.Items.AddRange(month);

            // 초기값 설정
            yearComboBox.Text = "2000";
            monthComboBox.Text = "01";

            // 모의고사 성적 콤보박스에 삽입
            korComboBox1.Items.AddRange(grade);
            korComboBox2.Items.AddRange(grade);
            engComboBox1.Items.AddRange(grade);
            engComboBox2.Items.AddRange(grade);
            mathComboBox1.Items.AddRange(grade);
            mathComboBox2.Items.AddRange(grade);

            // 전화번호, 보호자번호 텍스트박스 길이 제한
            PhoneTextBox1.MaxLength = 3;
            PhoneTextBox2.MaxLength = 4;
            PhoneTextBox3.MaxLength = 4;
            ParentPhone1.MaxLength = 3;
            ParentPhone2.MaxLength = 4;
            ParentPhone3.MaxLength = 4;
        }

        private void SetLectureTimeComboBox() // 강의의 초기 박스 설정
        { 
            string[] min = { "00", "30" }; // 시작, 종료 분 설정
            string[] subject = { "국어", "수학", "영어" }; // 과목 설정
            for (int i = 10; i <= 22; i++) // 시작 시간 및 종료 시간 설정
            {
                if(i <= 21)
                    StartHourComboBox.Items.Add(i);
                if(i >= 11)
                    EndHourComboBox.Items.Add(i);
            }
            // 시작, 종료 분 콤보 박스에 삽입
            StartMinComboBox.Items.AddRange(min); 
            EndMinComboBox.Items.AddRange(min);

            for (int i = 1; i <= 3; i++) // 강의실 콤보 박스에 삽입
                classroomComboBox.Items.Add("강의실" + i);
            subjectComboBox.Items.AddRange(subject); // 과목 콤보 박스에 삽입
        }

        private void SetLectureBoxTool() // 강의 박스 세팅
        {
            // 강의 텍스트 박스 및 콤보박스, 그룹박스, 체크 박스 초기값 설정
            SelectBox.Text = "강의 목록";
            StuTeaBox.Visible = false;
            LectureBox.Visible = true;
            LectureNameTextBox.Text = "";
            LectureNumTextBox.Text = "";
            LectureTeaTextBox.Text = "";
            StartHourComboBox.Text = "";
            StartMinComboBox.Text = "";
            EndHourComboBox.Text = "";
            EndMinComboBox.Text = "";
            MondayCheckBox.CheckState = CheckState.Unchecked;
            TuesdayCheckBox.CheckState = CheckState.Unchecked;
            WednesdayCheckBox.CheckState = CheckState.Unchecked;
            ThursdayCheckBox.CheckState = CheckState.Unchecked;
            FridayCheckBox.CheckState = CheckState.Unchecked;
            SaturdayCheckBox.CheckState = CheckState.Unchecked;

            
            myLecture.Visible = false; // 학생, 선생에서 쓰이는 내 강의만 보기 체크박스 안보이게 설정

            // 수정 버튼 클릭 시 나오는 확인, 취소, 선생 변경 버튼 안보이게 설정
            LectureUpdateNoButton.Visible = false; 
            LectureUpdateOKButton.Visible = false;
            TeacherChangeButton.Visible = false;

            // 수정 버튼 클릭 전에는 정보를 볼 수만 있게 수정 제한
            LectureNameTextBox.Enabled = false;
            LectureNumTextBox.Enabled = false;
            LectureTeaTextBox.Enabled = false;
            classroomComboBox.Enabled = false;
            StartHourComboBox.Enabled = false;
            StartMinComboBox.Enabled = false;
            EndHourComboBox.Enabled = false;
            EndMinComboBox.Enabled = false;
            subjectComboBox.Enabled = false;
            MondayCheckBox.Enabled = false;
            TuesdayCheckBox.Enabled = false;
            WednesdayCheckBox.Enabled = false;
            ThursdayCheckBox.Enabled = false;
            FridayCheckBox.Enabled = false;
            SaturdayCheckBox.Enabled = false;

            // 데이터 그리드 뷰 및 크기 재설정
            GroupDataTable.Location = new Point(367, 333);
            GroupDataTable.Height = 248;
        }

        private void SetChooseGroupBox() // 어떤 정보를 출력할 지 선택하는 콤보박스 설정
        {
            string[] menu = { "학생", "선생", "강의" }; // 학생, 선생, 강의 삽입
            ChooseGroupBox.Items.AddRange(menu);
            ChooseGroupBox.Text = "학생";
            ChooseGroupBox.DropDownStyle = ComboBoxStyle.DropDownList; // 콤보박스 내의 정보 삽입 수정 불가하게 설정
        }
        public void GetInfoListView() // 학생, 선생, 강의의 전체 리스트를 출력해주는 데이터 그리드 뷰 설정
        { // 앞선 Form 들에서의 설명과 같으므로 생략합니다.
            string[] row; 
            InfoListView.Width = 330;
            InfoListView.Rows.Clear();
            InfoListView.Columns.Clear();
            InfoListView.ReadOnly = true;
            InfoListView.AllowUserToAddRows = false;
            InfoListView.RowHeadersVisible = false;


            InfoListView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            row = new string[5];
            InfoListView.ColumnCount = 5;
            InfoListView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            InfoListView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < 5; i++)
                InfoListView.Columns[i].Width = 66;

            if (index == 0) // 학생
            {
                InfoListView.Columns[0].Name = "학번";
                InfoListView.Columns[1].Name = "이름";
                InfoListView.Columns[2].Name = "학교";
                InfoListView.Columns[3].Name = "학년";
                InfoListView.Columns[4].Name = "생년월일";
                InfoListView.Columns[3].Width = 40;
                InfoListView.Columns[4].Width = 92;

                students = cont.FindStudents(searchText, searchIndex);

                for (int i = 0; i < students.Count; i++)
                {
                    row[0] = students[i].Id;
                    row[1] = students[i].Name;
                    row[2] = students[i].School;
                    row[3] = students[i].Grade;
                    row[4] = students[i].Birth;

                    InfoListView.Rows.Add(row);
                }

            }

            else if (index == 1) // 선생
            {
                InfoListView.Columns[0].Name = "교번";
                InfoListView.Columns[1].Name = "이름";
                InfoListView.Columns[2].Name = "생년월일";
                InfoListView.Columns[3].Name = "직책";
                InfoListView.Columns[4].Name = "과목";

                teachers = cont.FindTeachers(searchText, searchIndex);


                for (int i = 0; i < teachers.Count; i++)
                {
                    row[0] = teachers[i].Id;
                    row[1] = teachers[i].Name;
                    row[2] = teachers[i].Birth;
                    row[3] = teachers[i].Position;
                    row[4] = teachers[i].Major;
                    InfoListView.Rows.Add(row);
                }

            }

            else // 강의
            {
                InfoListView.ColumnCount = 6;
                row = new string[6];
                InfoListView.Columns[0].Name = "강의번호";
                InfoListView.Columns[1].Name = "강의명";
                InfoListView.Columns[2].Name = "담당선생";
                InfoListView.Columns[3].Name = "강의실";
                InfoListView.Columns[4].Name = "시작시간";
                InfoListView.Columns[5].Name = "종료시간";
                InfoListView.Columns[5].Width = 66;
                lectures = cont.FindLectures(searchText, searchIndex);

                for (int i = 0; i < lectures.Count; i++)
                {
                    row[0] = lectures[i].Id;
                    row[1] = lectures[i].ClassName;
                    if (lectures[i].Tea != null)
                        row[2] = lectures[i].Tea.Name;
                    else
                        row[2] = "미배정";

                    row[3] = lectures[i].Classroom;
                    row[4] = lectures[i].StartTime;
                    row[5] = lectures[i].FinishTime;

                    InfoListView.Rows.Add(row);
                    if (lectures[i].Tea == null)
                    {
                        InfoListView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }
        }


        private void GetGroupDataTable() // 학생, 선생이면 해당 학생, 선생이 수강하거나 담당중인 강의리스트, 강의면 해당 강의를 수강하는 학생 리스트를 보여주는 데이터 그리드 뷰를 설정
        { // 앞선 Form들과 비슷하므로 생략하겠습니다.
            string[] row;
            GroupDataTable.Rows.Clear();
            GroupDataTable.Columns.Clear();
            GroupDataTable.RowHeadersVisible = false;
            GroupDataTable.ReadOnly = true;
            GroupDataTable.AllowUserToAddRows = false;
            GroupDataTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            row = new string[8];
            GroupDataTable.ColumnCount = 8;
            GroupDataTable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GroupDataTable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < 8; i++)
                GroupDataTable.Columns[i].Width = 100;

            if (index == 0) // 학생
            {
                row = new string[13];
                GroupDataTable.ColumnCount = 13;
                for (int i = 0; i < 13; i++)
                    GroupDataTable.Columns[i].Width = 70;
                GroupDataTable.Columns[1].Width = 100;
                GroupDataTable.Columns[3].Frozen = true;
                GroupDataTable.Columns[0].Name = "강의번호";
                GroupDataTable.Columns[1].Name = "강의명";
                GroupDataTable.Columns[2].Name = "담당선생";
                GroupDataTable.Columns[3].Name = "강의실";
                GroupDataTable.Columns[4].Name = "월";
                GroupDataTable.Columns[5].Name = "화";
                GroupDataTable.Columns[6].Name = "수";
                GroupDataTable.Columns[7].Name = "목";
                GroupDataTable.Columns[8].Name = "금";
                GroupDataTable.Columns[9].Name = "토";
                GroupDataTable.Columns[10].Name = "시작시간";
                GroupDataTable.Columns[11].Name = "종료시간";
                GroupDataTable.Columns[12].Name = "비고";

                for (int i = 4; i <= 9; i++)
                    GroupDataTable.Columns[i].Width = 25;

                if (myLecture.CheckState == CheckState.Checked) // 내 강의만 보기 체크 박스가 클릭되면
                    lectures = selectedStudent.Lect; // 해당 학생의 강의 리스트로 설정
                else // 체크 박스가 해제되면
                    lectures = cont.Lectures; // 전체 강의 리스트 설정

                for (int i = 0; i < lectures.Count; i++)
                {
                    row[0] = lectures[i].Id;
                    row[1] = lectures[i].ClassName;
                    if (lectures[i].Tea != null)
                        row[2] = lectures[i].Tea.Name;
                    else
                        row[2] = "미배정";

                    row[3] = lectures[i].Classroom;
                    for(int j = 0; j < 6; j++)
                    {
                        if (lectures[i].Day[j] == 1)
                            row[4 + j] = "○";
                        else
                            row[4 + j] = "";
                    }
                    row[10] = lectures[i].StartTime;
                    row[11] = lectures[i].FinishTime;
                    row[12] = "";
                    GroupDataTable.Rows.Add(row);
                    if (selectedStudent.Lect.Contains(lectures[i])) // 수강하고 있는 강의이면
                    {
                        GroupDataTable.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow; // 색 설정
                    }
                }
            }

            else if (index == 1) // 선생
            {
                row = new string[12];
                GroupDataTable.ColumnCount = 12;
                for (int i = 0; i < 12; i++)
                    GroupDataTable.Columns[i].Width = 70;
                GroupDataTable.Columns[1].Width = 100;
                GroupDataTable.Columns[2].Frozen = true;
                GroupDataTable.Columns[0].Name = "강의번호";
                GroupDataTable.Columns[1].Name = "강의명";
                GroupDataTable.Columns[2].Name = "강의실";
                GroupDataTable.Columns[3].Name = "월";
                GroupDataTable.Columns[4].Name = "화";
                GroupDataTable.Columns[5].Name = "수";
                GroupDataTable.Columns[6].Name = "목";
                GroupDataTable.Columns[7].Name = "금";
                GroupDataTable.Columns[8].Name = "토";
                GroupDataTable.Columns[9].Name = "시작시간";
                GroupDataTable.Columns[10].Name = "종료시간";
                GroupDataTable.Columns[11].Name = "비고";
                for (int i = 3; i < 9; i++)
                    GroupDataTable.Columns[i].Width = 25;
                if (myLecture.CheckState == CheckState.Checked)
                    lectures = selectedTeacher.Lect;
                else
                    lectures = cont.Lectures;

                for (int i = 0; i < lectures.Count; i++)
                {
                    row[0] = lectures[i].Id;
                    row[1] = lectures[i].ClassName;
                    row[2] = lectures[i].Classroom;
                    for (int j = 0; j < 6; j++)
                    {
                        if (lectures[i].Day[j] == 1)
                            row[3 + j] = "○";
                        else
                            row[3 + j] = "";
                    }
                    row[9] = lectures[i].StartTime;
                    row[10] = lectures[i].FinishTime;
                    row[11] = "";
                    GroupDataTable.Rows.Add(row);
                    if (selectedTeacher.Lect.Contains(lectures[i]))
                        GroupDataTable.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                    if (lectures[i].Tea == null)
                        GroupDataTable.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }

            else // 강의
            {
                row = new string[6];
                GroupDataTable.ColumnCount = 6;
                for (int i = 0; i < 6; i++)
                    GroupDataTable.Columns[i].Width = 100;
                GroupDataTable.Columns[3].Width = 40;
                GroupDataTable.Columns[0].Name = "학번";
                GroupDataTable.Columns[1].Name = "이름";
                GroupDataTable.Columns[2].Name = "학교";
                GroupDataTable.Columns[3].Name = "학년";
                GroupDataTable.Columns[4].Name = "생년월일";
                GroupDataTable.Columns[5].Name = "비고";

                students = selectedLecture.Students;

                for (int i = 0; i < students.Count; i++)
                {
                    row[0] = students[i].Id;
                    row[1] = students[i].Name;
                    row[2] = students[i].School;
                    row[3] = students[i].Grade;
                    row[4] = students[i].Birth;
                    row[5] = "";
                    GroupDataTable.Rows.Add(row);
                }
            }
        }


        private void SelectButton_Click(object sender, EventArgs e) // 검색 버튼 클릭
        {
            searchText = SelectTextBox.Text; // searchText에 검색 값 옮김
            if (searchText == "") // 공백으로 버튼 누를시
                MessageBox.Show("값을 입력하세요.", "입력오류");
            SelectTextBox.Clear(); // 텍스트 박스 공백 설정
            GetInfoListView(); // 검색 값에 대한 정보를 보기 위해 데이터 그리드 뷰 다시 출력
        }

        private void ChooseGroupBox_SelectedIndexChanged(object sender, EventArgs e) // 학생, 선생, 강의 중 콤보 박스에서 선택이 바뀌면
        {
            string id = ""; // 검색을 위한 id
            index = ChooseGroupBox.SelectedIndex; // 0 : 학생, 1 : 선생, 2 : 강의
            SetChooseAttributeBox(); // 선택된 정보에 맞게 검색 속성 박스 설정
            searchIndex = 0; // 검색 정보 전체로 설정
            SelectTextBox.ReadOnly = true; // 검색 텍스트 박스 읽기 전용

            GetInfoListView(); // 선택된 정보에 맞게 리스트 출력
            if(InfoListView.RowCount > 0) // 데이터 그리드 뷰가 1개 이상이면
                id = InfoListView.Rows[0].Cells[0].Value.ToString(); // 아이디 설정

            if (index == 0) // 학생
            {
                selectedStudent = cont.FindOnlyStudent(id); // 학생 검색
                // 학생, 선생 박스 재설정
                SetStuTeaBoxTool(); 
                SetStuTeaBox();
            }
            else if (index == 1) // 선생
            {
                selectedTeacher = cont.FindOnlyTeacher(id); // 선생 검색
                // 학생, 선생 박스 재설정
                SetStuTeaBoxTool();
                SetStuTeaBox();
            }
            else // 강의
            {
                selectedLecture = cont.FindOnlyLecture(id); // 강의 검색
                // 강의 박스 재설정
                SetLectureBoxTool();
                SetLectureBox();
            }
            SetImageBox(); // 이미지 박스 재설정
            GetGroupDataTable(); // 데이터 그리드 뷰 재설정
        }

        private void ChooseAttributeBox_SelectedIndexChanged(object sender, EventArgs e) // 검색 속성이 변경될 시
        {
            int ind;
            searchIndex = ChooseAttributeBox.SelectedIndex; // 검색 속성 인덱스 받음
            UpdateButton.Enabled = false; // 수정 버튼 사용 불가
            if (searchIndex == 0) // 전체이면
            {
                SelectTextBox.ReadOnly = true; // 읽기 전용으로
                GetInfoListView(); // 전체 정보 다시 출력
            }
            else // 그 외의 것이면
            {
                ind = searchIndex; // 전체 인 것 처럼
                searchIndex = 0;
                GetInfoListView(); // 모든 리스트를 출력해두고
                searchIndex = ind; // 검색을 할 수 있게 속성 값 지정
                SelectTextBox.ReadOnly = false; // 읽기전용 없앰
            }
            if (index == 0 || index == 1) // 학생, 선생은 
            {
                SetStuTeaBoxTool(); // 학생, 선생 박스 설정
            }
            else // 강의는
            {
                SetLectureBoxTool(); // 강의 박스 설정
            }
        }

        private void SetChooseAttributeBox() // 검색 속성 콤보 박스 설정
        {
            string[] menu = new string[4];
            if (index == 0) // 학생
            {
                menu[0] = "전체";
                menu[1] = "학번";
                menu[2] = "이름";
                menu[3] = "학교";

            }

            else if (index == 1) // 선생
            {
                menu[0] = "전체";
                menu[1] = "교번";
                menu[2] = "이름";
                menu[3] = "과목";
            }

            else // 강의
            {
                menu[0] = "전체";
                menu[1] = "강의번호";
                menu[2] = "강의명";
                menu[3] = "담당선생";
            }
            // 이전 검색 속성 삭제하고 새롭게 검색 속성 부여 및 초기값 전체로 설정
            ChooseAttributeBox.Items.Clear();
            ChooseAttributeBox.Items.AddRange(menu);
            ChooseAttributeBox.Text = "전체"; 
            ChooseAttributeBox.DropDownStyle = ComboBoxStyle.DropDownList; // 아이템 삽입, 수정 불가하게 설정
        }

        private void InsertButton_Click(object sender, EventArgs e) // 삽입 버튼 클릭시
        { 
            if (index == 2) // 강의는
            { // 강의삽입 폼 생성 및 출력
                InsertLecture insertLecture = new InsertLecture(cont);

                insertLecture.ShowDialog();
            }
            else // 학생, 선생은
            { // 학생, 선생 삽입 폼 생성 및 출력
                InsertForm insert = new Team.InsertForm(cont, index);

                insert.ShowDialog();
            }
            GetInfoListView(); // 이후 정보 재출력
        }

        private void DeleteButton_Click(object sender, EventArgs e) // 삭제 버튼 클릭시
        {
            if (MessageBox.Show("선택하신 정보를 삭제하시겠습니까?", "정보 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes) // 삭제 여부 물어보고
            {
                string id;

                id = InfoListView.Rows[InfoListView.CurrentCellAddress.Y].Cells[0].Value.ToString(); // 아이디 데이터 그리드 뷰에서 받아서

                cont.DeleteInfo(id, index); // index (학생, 선생, 강의) 정보와 id를 함께 보내서 삭제

                GetInfoListView(); // 리스트 재 출력
                if(index == 0 || index == 1) // 학생, 선생은
                    SetStuTeaBoxTool(); // 학생, 선생 박스 설정
                else // 강의는
                    SetLectureBoxTool(); // 강의 박스 설정
                UpdateButton.Enabled = false; // 수정 버튼 사용 불가
                MessageBox.Show("삭제가 완료되었습니다."); // 메시지 박스 
            }
            
        }

        private void GroupDataTable_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) // 학생, 선생일 시 강의 목록 데이터 그리드 뷰 더블클릭시
        {
            if (updateState == true) // 수정 중일 경우
            {
                string id;
                string[] str, row;
                Lecture lec;
                id = GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[0].Value.ToString(); // 해당 강의 id

                if (index == 0 || index == 1) // 학생, 선생일 경우
                {
                    lec = cont.FindOnlyLecture(id); // 강의 검색

                    if (index == 0) // 학생
                    {
                        // 정보 데이터 그리드 뷰로 부터 받아옴
                        str = new string[6];
                        row = new string[6];
                        str[3] = "";
                        str[0] = GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[1].Value.ToString();
                        str[1] = GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[2].Value.ToString();
                        str[2] = GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[3].Value.ToString();
                        str[4] = GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[10].Value.ToString();
                        str[5] = GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[11].Value.ToString();
                        for (int i = 4; i < 10; i++)
                        {

                            if (GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[i].Value.ToString() == "○")
                            {
                                str[3] += ", " + GroupDataTable.Columns[i].HeaderText;
                            }
                        }
                        str[3] = str[3].Substring(2);


                        row[0] = "강의명 : " + str[0] + "\n";
                        row[1] = "담당선생 : " + str[1] + "\n";
                        row[2] = "강의실 : " + str[2] + "\n";
                        row[3] = "요일 : " + str[3] + "\n";
                        row[4] = "시작시간 : " + str[4] + "\n";
                        row[5] = "종료시간 : " + str[5] + "\n";

                        // insertForm에서와 같으므로 생략하겠습니다.
                        if (selectedStudent.Lect.Contains(lec))
                        {
                            if (MessageBox.Show
                            ("해당 강의의 수강을 취소하시겠습니까?\n\n" + row[0] + row[1] + row[2] + row[3] + row[4] + row[5], "수강 취소", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].DefaultCellStyle.BackColor = Color.White;
                                selectedStudent.DeleteLecture(lec);
                                MessageBox.Show("수강 취소 완료", "취소");
                                GetGroupDataTable();
                            }
                        }
                        else
                        {
                            if (MessageBox.Show
                            ("해당 강의를 수강하시겠습니까?\n\n" + row[0] + row[1] + row[2] + row[3] + row[4] + row[5], "수강 신청", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                if (lec.LectNum <= lec.Students.Count) // 강의 총 인원 수가 다 찼으면
                                    MessageBox.Show("해당 강의는 여석이 없습니다.", "강의 여석 없음");
                                else if (cont.lecturePersonRedundancyCheck(lec, selectedStudent))//강의 중복 수정 완료
                                {
                                    GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].DefaultCellStyle.BackColor = Color.GreenYellow;
                                    selectedStudent.AddLecture(lec);
                                    MessageBox.Show("수강 신청 완료", "신청");
                                    GetGroupDataTable();
                                }
                                else
                                    MessageBox.Show("해당 강의 시간과 겹치는 강의를 수강중입니다.", "수강 오류");
                            }
                        }
                    }

                    else if (index == 1) // 선생
                    {
                        // InsertForm에서와 같으므로 생략하겠습니다.
                        str = new string[5];
                        row = new string[5];
                        str[2] = "";
                        str[0] = GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[1].Value.ToString();
                        str[1] = GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[2].Value.ToString();
                        str[3] = GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[9].Value.ToString();
                        str[4] = GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[10].Value.ToString();
                        for (int i = 3; i < 9; i++)
                        {

                            if (GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].Cells[i].Value.ToString() == "○")
                            {
                                str[2] += ", " + GroupDataTable.Columns[i].HeaderText;
                            }
                        }
                        str[2] = str[2].Substring(2);


                        row[0] = "강의명 : " + str[0] + "\n";
                        row[1] = "강의실 : " + str[1] + "\n";
                        row[2] = "요일 : " + str[2] + "\n";
                        row[3] = "시작시간 : " + str[3] + "\n";
                        row[4] = "종료시간 : " + str[4] + "\n";
                        if (selectedTeacher.Lect.Contains(lec))
                        {
                            if (MessageBox.Show
                            ("해당 강의의 담당을 취소하시겠습니까?\n\n" + row[0] + row[1] + row[2] + row[3] + row[4], "담당 취소", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].DefaultCellStyle.BackColor = Color.White;
                                selectedTeacher.DeleteLecture(lec);
                                MessageBox.Show("담당 취소 완료", "취소");
                                GetGroupDataTable();
                            }
                        }
                        else
                        {
                            if (lec.Tea != null)
                                MessageBox.Show("해당 강의는 이미 다른 선생님이 담당중입니다.", "담당 중복 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                if (MessageBox.Show
                                ("해당 강의를 담당하시겠습니까?\n\n" + row[0] + row[1] + row[2] + row[3] + row[4], "담당 신청", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    if (cont.lecturePersonRedundancyCheck(lec, selectedTeacher))
                                    {
                                        GroupDataTable.Rows[GroupDataTable.CurrentCellAddress.Y].DefaultCellStyle.BackColor = Color.GreenYellow;
                                        selectedTeacher.AddLecture(lec);
                                        MessageBox.Show("담당 신청 완료", "신청");
                                        GetGroupDataTable();

                                    }
                                    else
                                        MessageBox.Show("해당 강의 시간과 겹치는 강의를 수강중입니다.", "수강 오류");
                                }
                            }
                        }
                    }
                }
                else // 강의 일 경우
                {
                    // 해당 학생의 정보를 출력하는 폼 출력
                    Student loginStudent;
                    StudentForm newForm;
                    loginStudent = cont.FindOnlyStudent(id);
                    newForm = new StudentForm(loginStudent, cont);
                    newForm.Show();
                }
            }
        }
        
        private void SelectTextBox_TextChanged(object sender, EventArgs e) // 검색 텍스트 박스 정보가 변경시
        {
            searchText = SelectTextBox.Text; // 검색 텍스트에 정보 받음

            GetInfoListView(); // 리스트 데이터 그리드 뷰 재 출력
        }

        private void InfoListView_CellContentClick(object sender, DataGridViewCellEventArgs e) // 목록을 출력해주는 데이터 그리드 뷰 더블 클릭시
        {
            if (updateState == false) // 업데이트 중이 아닐시에
            {
                string id;
                 // 아이디 받아서
                id = InfoListView.Rows[InfoListView.CurrentCellAddress.Y].Cells[0].Value.ToString();
                
                if (index == 0) // 학생
                {
                    selectedStudent = cont.FindOnlyStudent(id); // 선택된 학생으로 설정
                    SetStuTeaBoxTool(); // 학생, 선생 박스 초기화 및 재설정(이 학생 정보로)
                    SetStuTeaBox();
                }

                else if (index == 1) // 선생
                {
                    selectedTeacher = cont.FindOnlyTeacher(id); // 선택된 선생으로 설정
                    SetStuTeaBoxTool(); // 학생, 선생 박스 초기화 및 재설정(이 선생 정보로)
                    SetStuTeaBox();
                }

                else // 강의
                {
                    selectedLecture = cont.FindOnlyLecture(id); // 선택된 강의로 설정
                    SetLectureBoxTool(); // 강의 박스 초기화 및 재설정(이 강의 정보로)
                    SetLectureBox();
                }
                SetImageBox(); // 이미지 박스 설정
                GetGroupDataTable(); // 학생, 선생이면 강의 리스트, 강의이면 수강 학생 리스트 출력
            }
        }

        public void SetLectureBox() // 강의 박스 선택된 강의로 설정
        {
            LectureNumTextBox.Text = selectedLecture.Id; // 강의 아이디로 텍스트 박스 설정
            LectureNameTextBox.Text = selectedLecture.ClassName; // 강의명으로 텍스트 박스 설정
            if (selectedLecture.Tea != null) // 담당 선생 있으면
                LectureTeaTextBox.Text = selectedLecture.Tea.Name; // 선생 이름으로 설정
            else // 없으면
                LectureTeaTextBox.Text = "미배정"; // 미배정으로 설정
            classroomComboBox.Text = selectedLecture.Classroom; // 강의실명으로 설정
            StartHourComboBox.Text = selectedLecture.StartTime.Substring(0, 2); // 시작 시간
            StartMinComboBox.Text = selectedLecture.StartTime.Substring(3); // 시간 분
            EndHourComboBox.Text = selectedLecture.FinishTime.Substring(0, 2); // 종료 시간
            EndMinComboBox.Text = selectedLecture.FinishTime.Substring(3); // 종료 분
            subjectComboBox.Text = selectedLecture.Subject; // 과목으로 설정
            // 요일 체크박스로 설정
            if (selectedLecture.Day[0] == 0)
                MondayCheckBox.CheckState = CheckState.Unchecked;
            else
                MondayCheckBox.CheckState = CheckState.Checked;
            if (selectedLecture.Day[1] == 0)
                TuesdayCheckBox.CheckState = CheckState.Unchecked;
            else
                TuesdayCheckBox.CheckState = CheckState.Checked;
            if (selectedLecture.Day[2] == 0)
                WednesdayCheckBox.CheckState = CheckState.Unchecked;
            else
                WednesdayCheckBox.CheckState = CheckState.Checked;
            if (selectedLecture.Day[3] == 0)
                ThursdayCheckBox.CheckState = CheckState.Unchecked;
            else
                ThursdayCheckBox.CheckState = CheckState.Checked;
            if (selectedLecture.Day[4] == 0)
                FridayCheckBox.CheckState = CheckState.Unchecked;
            else
                FridayCheckBox.CheckState = CheckState.Checked;
            if (selectedLecture.Day[5] == 0)
                SaturdayCheckBox.CheckState = CheckState.Unchecked;
            else
                SaturdayCheckBox.CheckState = CheckState.Checked;
            UpdateButton.Enabled = true; // 수정 버튼 사용 가능

        }

        private void SetStuTeaBox() // 학생, 선생 박스 선택된 정보로 설정
        {
            if (index == 0) // 학생
            {
                string[] school = { "초등학교", "중학교", "고등학교" }; // 학교 콤보박스 아이템
                IDTextBox.Text = selectedStudent.Id; // 아이디 설정
                NameTextBox.Text = selectedStudent.Name; // 이름 설정
                SchoolComboBox.Items.Clear(); // 학교 콤보박스 내용 삭제
                SchoolComboBox.Items.AddRange(school); // 아이템 삽입
                if (selectedStudent.School.Contains("중학교")) // 중학교면
                { 
                    SchoolComboBox.Text = "중학교"; // 중학교로 설정
                    SchoolTextBox.Text = selectedStudent.School.Substring(0, selectedStudent.School.Length - 3); // 제외한 학교 명 설정
                }
                else if (selectedStudent.School.Contains("고등학교")) // 고등학교면
                {
                    SchoolComboBox.Text = "고등학교"; // 고등학교로 설정
                    SchoolTextBox.Text = selectedStudent.School.Substring(0, selectedStudent.School.Length - 4); // 제외한 학교 명 설정
                }
                else // 초등학교면
                {
                    SchoolComboBox.Text = "초등학교"; // 초등학교로 설정
                    SchoolTextBox.Text = selectedStudent.School.Substring(0, selectedStudent.School.Length - 4); // 제외한 학교 명 설정
                }
                GradeComboBox.Text = selectedStudent.Grade; // 학년 설정
                // 전화번호 설정
                PhoneTextBox1.Text = selectedStudent.PhoneNumber.Substring(0, 3);
                PhoneTextBox2.Text = selectedStudent.PhoneNumber.Substring(3, 4);
                PhoneTextBox3.Text = selectedStudent.PhoneNumber.Substring(7);

                LibraryUsingTimeLabel.Text = (selectedStudent.UsingTime / 60).ToString() + " 분"; // 독서실 이용 시간 설정
                
                // 보호자 번호 설정
                ParentPhone1.Text = selectedStudent.ParentPhone.Substring(0, 3);
                ParentPhone2.Text = selectedStudent.ParentPhone.Substring(3, 4);
                ParentPhone3.Text = selectedStudent.ParentPhone.Substring(7);
                
                // 생년월일 설정
                yearComboBox.Text = selectedStudent.Birth.Substring(0, 4);
                monthComboBox.Text = selectedStudent.Birth.Substring(4, 2);
                dayComboBox.Text = selectedStudent.Birth.Substring(6, 2);

                // 6월, 9월 모의고사 설정
                if (selectedStudent.KorGrade[0] == 0)
                    korComboBox1.Text = "-";
                else
                    korComboBox1.Text = "" + selectedStudent.KorGrade[0];
                if (selectedStudent.KorGrade[1] == 0)
                    korComboBox2.Text = "-";
                else
                    korComboBox2.Text = "" + selectedStudent.KorGrade[1];
                if(selectedStudent.MathGrade[0] == 0)
                    mathComboBox1.Text = "-";
                else
                    mathComboBox1.Text = "" + selectedStudent.MathGrade[0];
                if (selectedStudent.MathGrade[1] == 0)
                    mathComboBox2.Text = "-";
                else
                    mathComboBox2.Text = "" + selectedStudent.MathGrade[1];
                if(selectedStudent.EngGrade[0] == 0)
                    engComboBox1.Text = "-";
                else
                    engComboBox1.Text = "" + selectedStudent.EngGrade[0];
                if(selectedStudent.EngGrade[1] == 0)
                    engComboBox2.Text = "-";
                else
                    engComboBox2.Text = "" + selectedStudent.EngGrade[1];

            }

            else if (index == 1) // 선생
            {
                string[] major = { "국어", "수학", "영어" }; // 과목 
                string[] position = { "신입", "전문", "부원장", "원장" }; // 직급
                // 위의 학생과 같으므로 생략합니다.
                IDTextBox.Text = selectedTeacher.Id; 
                NameTextBox.Text = selectedTeacher.Name;
                SchoolComboBox.Items.Clear();
                SchoolComboBox.Items.AddRange(position);
                SchoolComboBox.Text = selectedTeacher.Position;
                GradeComboBox.Items.Clear();
                GradeComboBox.Items.AddRange(major);
                GradeComboBox.Text = selectedTeacher.Major;
                PhoneTextBox1.Text = selectedTeacher.PhoneNumber.Substring(0, 3);
                PhoneTextBox2.Text = selectedTeacher.PhoneNumber.Substring(3, 4);
                PhoneTextBox3.Text = selectedTeacher.PhoneNumber.Substring(7);

                yearComboBox.Text = selectedTeacher.Birth.Substring(0, 4);
                monthComboBox.Text = selectedTeacher.Birth.Substring(4, 2);
                dayComboBox.Text = selectedTeacher.Birth.Substring(6);
            }
            UpdateButton.Enabled = true; // 수정 버튼 사용 가능
        }

        private void SetImageBox()
        {
            string setImageRoute = string.Empty;
            PictureBox pic;
            Image newImage;
            pic = null;
            if (index == 0)
            {
                pic = pictureBox1;
                setImageRoute = basicRoute + @"\TeamProjectImageSaveForder\studentImage\" + selectedStudent.Id + ".png";//이미지 경로 저장
            }
            else if (index == 1)
            {
                pic = pictureBox1;
                setImageRoute = basicRoute + @"\TeamProjectImageSaveForder\teacherImage\" + selectedTeacher.Id + ".png";//이미지 경로 저장
            }
            else
            {
                pic = pictureBox2;
                if (selectedLecture.Tea != null)
                    setImageRoute = basicRoute + @"\TeamProjectImageSaveForder\teacherImage\" + selectedLecture.Tea.Id + ".png";//이미지 경로 저장
                else
                    setImageRoute = basicRoute + @"\TeamProjectImageSaveForder\default.png";//선생님이 없다면 기본 이미지로 설정
            }
            System.IO.FileInfo studentImageFileInfo = new System.IO.FileInfo(setImageRoute);//파일 정보 저장

            if (studentImageFileInfo.Exists)//파일 존재시
            {
                using (var bmpTemp = new Bitmap(setImageRoute))
                {
                    newImage = new Bitmap(bmpTemp);
                }
                pic.Image = newImage;
                pic.SizeMode = PictureBoxSizeMode.StretchImage;//피쳐박스에 이미지 불러오기
            }
            else
            {
                using (var bmpTemp = new Bitmap(basicRoute + @"\TeamProjectImageSaveForder\default.png"))//이미지 불러오기
                {
                    newImage = new Bitmap(bmpTemp);
                }
                pic.Image = newImage;
                pic.SizeMode = PictureBoxSizeMode.StretchImage;//피쳐박스에 이미지 불러오기
            }
        }


        private void SchoolComboBox_SelectedIndexChanged(object sender, EventArgs e) // 학교 콤보 박스 수정시
        {
            int schoolIndex;
            schoolIndex = SchoolComboBox.SelectedIndex; // 초, 중, 고 
            if (index == 0) // 학생일 때
            {
                if (schoolIndex == 0) // 초 이면
                {
                    GradeComboBox.Items.Clear(); // 이전 값 삭제
                    for (int i = 1; i <= 6; i++) // 1~6학년
                        GradeComboBox.Items.Add(i);
                    GradeComboBox.Text = "1"; // 초기값 1
                }
                else // 중, 고 이면
                {
                    GradeComboBox.Items.Clear(); // 이전 값 삭제
                    for (int i = 1; i <= 3; i++) // 1~3학년
                        GradeComboBox.Items.Add(i);
                    GradeComboBox.Text = "1"; // 초기값 1
                }
            }
        }

        private void ImageButton_Click(object sender, EventArgs e)//이미지 찾기 버튼
        {
            string imageFileRoute = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";//시작 경로 설정
            DialogResult dialogResult = openFileDialog.ShowDialog();//파일 경로 열기
            if (dialogResult == DialogResult.OK)
            {
                imageFileRoute = openFileDialog.FileName;
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            pictureBox1.Image = Bitmap.FromFile(imageFileRoute);//이미지 불러오기
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;//사이즈 설정
        }

        private void UpdateButton_Click(object sender, EventArgs e) // 수정 버튼 클릭
        {
            if (index == 0 || index == 1) // 학생, 선생이면
            {
                if (index == 0) // 학생
                    tmpLec = new List<Lecture>(selectedStudent.Lect); // 수강 중인 강의 리스트 잠시 따로 저장
                else // 선생
                    tmpLec = new List<Lecture>(selectedTeacher.Lect); // 담당 중인 강의 리스트 잠시 따로 저장
                
                // 박스들 수정가능하게 설정
                NameTextBox.Enabled = true;
                SchoolTextBox.Enabled = true;
                SchoolComboBox.Enabled = true;
                GradeComboBox.Enabled = true;
                PhoneTextBox1.Enabled = true;
                PhoneTextBox2.Enabled = true;
                PhoneTextBox3.Enabled = true;
                ImageButton.Visible = true;
                yearComboBox.Enabled = true;
                monthComboBox.Enabled = true;
                dayComboBox.Enabled = true;
                ParentPhone1.Enabled = true;
                ParentPhone2.Enabled = true;
                ParentPhone3.Enabled = true;
                korComboBox1.Enabled = true;
                mathComboBox1.Enabled = true;
                engComboBox1.Enabled = true;
                korComboBox2.Enabled = true;
                mathComboBox2.Enabled = true;
                engComboBox2.Enabled = true;
                myLecture.Enabled = true;
                
                // 수정 완료, 취소 버튼 보이게 설정
                StuTeaUpdateOKButton.Visible = true;
                StuTeaUpdateNoButton.Visible = true;
            }
            else // 강의
            {

                if (selectedLecture.Tea != null) // 담당선생이 있으면
                {
                    tmpTea = selectedLecture.Tea; // 담당선생을 잠시 저장
                    tmpTea.DeleteLecture(selectedLecture); // 강의를 삭제 해두고
                    selectedLecture.Tea = tmpTea; // 강의에서는 담당선생으로 설정 (담당선생은 해당 강의가 리스트에 없음, 강의 중복 여부 검사로 인해)
                }
                // 수정 확인, 취소, 선생 변경 버튼 보이게 설정
                LectureUpdateNoButton.Visible = true;
                LectureUpdateOKButton.Visible = true;
                TeacherChangeButton.Visible = true;

                // 박스 정보 수정 가능하게 설정
                LectureNameTextBox.Enabled = true;
                classroomComboBox.Enabled = true;
                StartHourComboBox.Enabled = true;
                StartMinComboBox.Enabled = true;
                EndHourComboBox.Enabled = true;
                EndMinComboBox.Enabled = true;
                subjectComboBox.Enabled = true;
                MondayCheckBox.Enabled = true;
                TuesdayCheckBox.Enabled = true;
                WednesdayCheckBox.Enabled = true;
                ThursdayCheckBox.Enabled = true;
                FridayCheckBox.Enabled = true;
                SaturdayCheckBox.Enabled = true;
            }
            updateState = true; // 수정 중으로 설정
        }

        private void LectureUpdateOKButton_Click(object sender, EventArgs e) // 강의 수정 완료 버튼 클릭
        {
            //InsertLecture 폼과 동일하므로 생략하겠습니다.
            int cnt = 0; 
            int[] day = { 0, 0, 0, 0, 0, 0 };
            if (MondayCheckBox.CheckState == CheckState.Checked)
            {
                cnt++;
                day[0] = 1;
            }
            if (TuesdayCheckBox.CheckState == CheckState.Checked)
            {
                cnt++;
                day[1] = 1;
            }
            if (WednesdayCheckBox.CheckState == CheckState.Checked)
            {
                cnt++;
                day[2] = 1;
            }
            if (ThursdayCheckBox.CheckState == CheckState.Checked)
            {
                cnt++;
                day[3] = 1;
            }
            if (FridayCheckBox.CheckState == CheckState.Checked)
            {
                cnt++;
                day[4] = 1;
            }
            if (SaturdayCheckBox.CheckState == CheckState.Checked)
            {
                cnt++;
                day[5] = 1;
            }
            if (MessageBox.Show("정보를 수정하시겠습니까?", "강의 수정", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (LectureNameTextBox.Text == "" || cnt == 0 || LectureTeaTextBox.Text == "")
                {
                    MessageBox.Show("정보를 모두 입력해주십시오.", "공백 존재", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int flag = 0;
                    string[] str = new string[5];
                    str[0] = LectureNameTextBox.Text;
                    str[1] = classroomComboBox.Text;
                    str[2] = StartHourComboBox.SelectedItem.ToString() + ":" + StartMinComboBox.SelectedItem.ToString();
                    str[3] = EndHourComboBox.SelectedItem.ToString() + ":" + EndMinComboBox.SelectedItem.ToString();
                    str[4] = subjectComboBox.SelectedItem.ToString();

                    if (cont.lecturePersonRedundancyCheck(str[2], str[3], day, selectedLecture.Tea))
                        flag = 1;
                    else
                    {
                        selectedLecture.Tea = null;
                        LectureTeaTextBox.Text = "";
                        MessageBox.Show("선택하신 선생님은 해당시간에 다른 강의를 담당하고 있습니다.", "시간 중복");
                    }

                    if (flag == 1)
                    {
                        if (cont.lectureRoomRedundancyCheck(day, str[2], str[3], selectedLecture.Id, str[1]))
                        {
                            selectedLecture.Tea.AddLecture(selectedLecture);
                            selectedLecture.ClassName = str[0];
                            selectedLecture.Classroom = str[1];
                            selectedLecture.StartTime = str[2];
                            selectedLecture.FinishTime = str[3];
                            selectedLecture.Subject = str[4];
                            selectedLecture.Day = day;
                            tmpTea = null;
                            updateState = false;
                            SetLectureBoxTool();
                            SetLectureBox();
                            GetInfoListView();
                        }
                        else
                            MessageBox.Show("해당 시간의 강의실이 이미 사용중입니다.", "강의실 시간 중복");
                    }
                }
            }
        }

        private void LectureUpdateNoButton_Click(object sender, EventArgs e) // 취소 버튼 클릭시
        { 
            if (MessageBox.Show("수정을 취소하시겠습니까?", "수정 취소", MessageBoxButtons.YesNo) == DialogResult.Yes) // 취소 여부
            {
                if (selectedLecture.Tea == tmpTea) // 담당 선생이 동일하면
                    tmpTea.AddLecture(selectedLecture); // 담당 선생 강의 리스트에 추가
                else if (selectedLecture.Tea != null) // 동일하지 않은 선생이 있으면
                    selectedLecture.Tea.ChangeTeacher(selectedLecture, tmpTea); // 담당 선생 변경
                tmpTea = null; // 임시 저장 초기화
                updateState = false; // 수정 종료
                SetLectureBoxTool(); // 강의 박스 재설정
                SetLectureBox();
            }
        }

        private void TeacherChangeButton_Click(object sender, EventArgs e) // 선생 변경 버튼 클릭시
        {
            Team.View.ChangeTeacherForm changeTeaForm = new View.ChangeTeacherForm(cont, selectedLecture); // ChangeTeacherForm생성 및 출력
            changeTeaForm.Show();
        }

        private void monthComboBox_SelectedIndexChanged(object sender, EventArgs e) // 생년월일 중 월 콤보박스 변경시
        {
            // InsertForm과 동일하므로 생략하겠습니다.
            int month, year;
            string[] day;
            year =  Int32.Parse(yearComboBox.SelectedItem.ToString());
            month = Int32.Parse(monthComboBox.SelectedItem.ToString());

            day = cont.GetDayComboBoxItems(year, month);

            dayComboBox.Items.Clear();
            dayComboBox.Items.AddRange(day);
            dayComboBox.Text = "01";
        }

        private void StuTeaUpdateOKButton_Click(object sender, EventArgs e) // 학생, 선생 수정 완료 버튼 클릭시
        {
            // InsertForm과 동일하므로 생략하겠습니다.
            string phone, pPhone, imageRoute;
            phone = PhoneTextBox1.Text + PhoneTextBox2.Text + PhoneTextBox3.Text;
            pPhone = ParentPhone1.Text + ParentPhone2.Text + ParentPhone3.Text;
            if (MessageBox.Show("정보를 수정하시겠습니까?", "수정 완료", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (index == 0)
                {
                    if (phone.Length != 11 || pPhone.Length != 11 || NameTextBox.Text == "" || SchoolTextBox.Text == "")
                    {
                        MessageBox.Show("정보를 모두 입력해주십시오.", "공백 존재", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        selectedStudent.ParentPhone = pPhone;
                        selectedStudent.PhoneNumber = phone;
                        selectedStudent.Name = NameTextBox.Text;
                        selectedStudent.School = SchoolTextBox.Text + SchoolComboBox.SelectedItem.ToString();

                        selectedStudent.KorGrade[0] = korComboBox1.SelectedIndex + 1;
                        selectedStudent.KorGrade[1] = korComboBox2.SelectedIndex + 1;
                        selectedStudent.MathGrade[0] = mathComboBox1.SelectedIndex + 1;
                        selectedStudent.MathGrade[1] = mathComboBox2.SelectedIndex + 1;
                        selectedStudent.EngGrade[0] = engComboBox1.SelectedIndex + 1;
                        selectedStudent.EngGrade[1] = engComboBox2.SelectedIndex + 1;
                        selectedStudent.Grade = GradeComboBox.SelectedItem.ToString();
                        selectedStudent.Birth = yearComboBox.SelectedItem.ToString() + monthComboBox.SelectedItem.ToString() + dayComboBox.SelectedItem.ToString();
                        imageRoute = basicRoute + @"\TeamProjectImageSaveForder\studentImage";
                        if (!System.IO.Directory.Exists(imageRoute))
                        {
                            System.IO.Directory.CreateDirectory(imageRoute);
                        }
                        imageRoute = imageRoute + @"\" + selectedStudent.Id + ".png";
                        pictureBox1.Image.Save(imageRoute, System.Drawing.Imaging.ImageFormat.Png);
                        updateState = false;
                    }

                }

                else
                {
                    if (phone.Length != 11 || NameTextBox.Text == "")
                    {
                        MessageBox.Show("정보를 모두 입력해주십시오.", "공백 존재", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        selectedTeacher.Name = NameTextBox.Text;
                        selectedTeacher.Position = SchoolComboBox.SelectedItem.ToString();
                        selectedTeacher.Major = GradeComboBox.SelectedItem.ToString();
                        selectedTeacher.PhoneNumber = phone;
                        selectedTeacher.Birth = yearComboBox.SelectedItem.ToString() + monthComboBox.SelectedItem.ToString() + dayComboBox.SelectedItem.ToString();
                        imageRoute = basicRoute + @"\TeamProjectImageSaveForder\teacherImage";
                        if (!System.IO.Directory.Exists(imageRoute))
                        {
                            System.IO.Directory.CreateDirectory(imageRoute);
                        }
                        imageRoute = imageRoute + @"\" + selectedTeacher.Id + ".png";
                        pictureBox1.Image.Save(imageRoute, System.Drawing.Imaging.ImageFormat.Png);
                        updateState = false;
                    }
                }
                tmpLec = null;
                GetInfoListView();
                SetStuTeaBoxTool();
                SetStuTeaBox();
            }
        }

        private void StuTeaUpdateNoButton_Click(object sender, EventArgs e) // 수정 취소 버튼 클릭시
        {
            if (MessageBox.Show("수정을 취소하시겠습니까?", "수정 취소", MessageBoxButtons.YesNo) == DialogResult.Yes) // 여부 파악
            {
                if(index == 0) // 학생
                {
                    for (int i = selectedStudent.Lect.Count - 1; i >= 0; i--) // 수정했던 강의 리스트와 기존 강의 리스트와의 차이나는 것을 삭제
                    {
                        if (!(tmpLec.Contains(selectedStudent.Lect[i])) && tmpLec != null)
                        {
                            selectedStudent.DeleteLecture(selectedStudent.Lect[i]);
                        }
                    }
                    for (int i = 0; i < tmpLec.Count; i++) // 기존 리스트에 있던 강의 삽입
                    {
                        if (!(selectedStudent.Lect.Contains(tmpLec[i])))
                            selectedStudent.AddLecture(tmpLec[i]);
                    }
                    
                }
                if (index == 1) // 선생
                { // 위와 같으므로 생략합니다.
                    for (int i = selectedTeacher.Lect.Count - 1; i >= 0; i--)
                    {
                        if (!(tmpLec.Contains(selectedTeacher.Lect[i])))
                            selectedTeacher.DeleteLecture(selectedTeacher.Lect[i]);
                    }
                    for (int i = 0; i < tmpLec.Count; i++)
                    {
                        if (!(selectedTeacher.Lect.Contains(tmpLec[i])))
                            selectedTeacher.AddLecture(tmpLec[i]);
                    }
                }
                tmpLec = null;
                updateState = false;
                GetGroupDataTable();
                SetStuTeaBoxTool();
                SetStuTeaBox();
            }

        }

        private void yearComboBox_SelectedIndexChanged(object sender, EventArgs e) // 연도 콤보 박스 변경시
        {
            monthComboBox.Text = "01"; // 값 설정
        }

        private void myLecture_CheckedChanged(object sender, EventArgs e) // 내 강의만 보기 체크박스 체크 또는 해제 시
        {
            if((index == 0 && selectedStudent != null) || (index == 1 && selectedTeacher != null)) // 선택된 정보가 있을 때만
                GetGroupDataTable(); // 강의 목록 재출력
        }
        
        // 전화번호 입력시 숫자만 입력 가능 및 다 입력시 다음 박스로 포커스 이동
        private void PhoneTextBox1_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(PhoneTextBox1.Text, out tmpSecondText);
            if (updateState && PhoneTextBox1.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);//숫자가 아닐시 오류
                    PhoneTextBox1.Text = "";//초기화
                }
                if (PhoneTextBox1.TextLength == 3)
                    this.ActiveControl = PhoneTextBox2;//길이에 맞으면 입력 다한것으로 간주하고 이동
            }
        }

        private void PhoneTextBox2_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(PhoneTextBox2.Text, out tmpSecondText);
            if (updateState && PhoneTextBox2.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);//숫자가 아닐시 오류
                    PhoneTextBox2.Text = "";//초기화
                }
                if (PhoneTextBox2.TextLength == 4)
                    this.ActiveControl = PhoneTextBox3;//길이에 맞으면 입력 다한것으로 간주하고 이동
            }
        }

        private void PhoneTextBox3_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(PhoneTextBox3.Text, out tmpSecondText);
            if (updateState && PhoneTextBox3.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);//숫자가 아닐시 오류
                    PhoneTextBox3.Text = "";//초기화
                }
                if (PhoneTextBox3.TextLength == 4)
                    this.ActiveControl = ParentPhone1;//길이에 맞으면 입력 다한것으로 간주하고 이동
            }
        }

        private void ParentPhone1_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(ParentPhone1.Text, out tmpSecondText);
            if (updateState && ParentPhone1.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ParentPhone1.Text = "";
                }
                if (ParentPhone1.TextLength == 3)
                    this.ActiveControl = ParentPhone2;
            }
        }

        private void ParentPhone2_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(ParentPhone2.Text, out tmpSecondText);
            if (updateState && ParentPhone2.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ParentPhone2.Text = "";
                }
                if (ParentPhone2.TextLength == 4)
                    this.ActiveControl = ParentPhone3;
            }
        }

        private void ParentPhone3_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(ParentPhone3.Text, out tmpSecondText);
            if (updateState && ParentPhone3.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ParentPhone2.Text = "";
                }
                if (ParentPhone3.TextLength == 4)
                    this.ActiveControl = StuTeaUpdateOKButton;
            }
        }
    }
}
