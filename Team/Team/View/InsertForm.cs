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

    public partial class InsertForm : Form // 선생, 학생의 삽입 폼
    {
        private string basicRoute = @"C:\Team8ProjectForder";
        Controller cont; // 컨트롤러
        int schoolIndex; // 초, 중, 고 구별 
        int index; // 학생, 선생 구별
        private List<Lecture> lectures; // 데이터 그리드 뷰에 뿌려줄 강의 리스트
        private Student newStudent; // 신규 학생
        private Teacher newTeacher; // 신규 선생
        

        public InsertForm(Controller cont, int index)
        {
            InitializeComponent();
            this.index = index; // 학생, 선생 여부
            this.cont = cont; // 컨트롤러

            string studentImageRoute = basicRoute + @"\TeamProjectImageSaveForder\default.png";
            System.IO.FileInfo studentImageFileInfo = new System.IO.FileInfo(studentImageRoute);
            if (studentImageFileInfo.Exists)
            {
                insertPictureBox.Load(studentImageRoute);
                insertPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            SetTool(); // index 값에 따른 Box들 셋팅

        }

        private void SetTool()
        {
            string[] year, month; // 연도, 월 string배열
            year = cont.GetYearComboboxItems(); // 콤보박스에 넣을 연도 string 배열 받아옴
            month = cont.GetMonthComboBoxItems(); // 콤보박스에 넣을 월 string 배열 받아옴
            yearComboBox.Items.AddRange(year); // 콤보박스에 넣음
            monthComboBox.Items.AddRange(month); // 콤보박스에 넣음
            yearComboBox.Text = "2000"; // 연도 설정
            monthComboBox.Text = "01"; // 월 설정
            //전화번호 길이 설정
            PhoneTxtBox1.MaxLength = 3; 
            PhoneTxtBox2.MaxLength = 4;
            PhoneTxtBox3.MaxLength = 4;
            parentTextBox1.MaxLength = 3;
            parentTextBox2.MaxLength = 4;
            parentTextBox3.MaxLength = 4;
            if (index == 0) // 학생
            {
                string[] school = { "초", "중", "고" }; // 학교 콤보박스에 넣을 string배열


                newStudent = new Student(); // 신규 학생 생성
                schoolComboBox.Items.AddRange(school); // 콤보박스에 넣음
                schoolComboBox.Text = "초"; // 학교 설정
                gradeComboBox.Text = "1"; // 학년 설정

                GetLectureView(); // 수강신청할 수 있도록 데이터그리드뷰로 강의목록 보여줌
            }

            else // 선생
            {
                string[] major = { "국어", "수학", "영어" }; // 과목
                string[] position = { "신입", "전문", "부원장", "원장" }; // 직급

                schoolComboBox.Items.AddRange(major); // 콤보 박스 삽입
                gradeComboBox.Items.AddRange(position); // 콤보 박스 삽입
                schoolComboBox.Text = "국어"; // 과목 설정
                gradeComboBox.Text = "신입"; // 직급 설정


                newTeacher = new Teacher(); // 신규 선생 생성
                schoolLabel.Text = "과      목"; // 라벨 이름 변경
                gradeLabel.Text = "직      책"; // 라벨 이름 변경
                schoolComboBox.Width = 50; // 과목 콤보박스 너비 설정
                schoolComboBox.Location = new System.Drawing.Point(82, 51); // 과목 위치 설정
                gradeComboBox.Width = 65; // 직급 콤보박스 너비 설정
                gradeComboBox.Location = new System.Drawing.Point(190, 51); // 직급 위치 설정

                gradeLabel.Location = new System.Drawing.Point(135, 51); // 직급 라벨 위치 설정

                //보호자(학생에 대한)전화번호 관련 정보 보이지 않게 표시
                parentLabel.Visible = false; 
                parentTextBox1.Visible = false;
                parentTextBox2.Visible = false;
                parentTextBox3.Visible = false;
                label12.Visible = false;
                label13.Visible = false;

                SchoolTxtBox.Visible = false; // 학교 이름 텍스트 박스 보이지 않게 

                GetLectureView(); // 강의를 담당할 수 있도록 강의 리스트 데이터 그리드 뷰로 출력
            }
        }
        

        private void schoolComboBox_SelectedIndexChanged(object sender, EventArgs e) // 학교 콤보박스 값이 변경되면
        {
            schoolIndex = schoolComboBox.SelectedIndex; // 초, 중, 고 중 어떤 것인지
            if (index == 0) // 학생일 때만
            {
                if (schoolIndex == 0) // 초 이면
                {
                    gradeComboBox.Items.Clear(); // 기존 콤보박스 내용 삭제
                    for (int i = 1; i <= 6; i++) // 1~6학년 콤보박스에 삽입
                        gradeComboBox.Items.Add(i);
                }
                else // 중, 고이면
                {
                    gradeComboBox.Items.Clear(); // 기존 콤보박스 내용 삭제
                    for (int i = 1; i <= 3; i++) // 1~3학년 콤보박스에 삽입
                        gradeComboBox.Items.Add(i);
                }
            }
        }

        public void GetLectureView() // 강의 리스트를 데이터 그리드 뷰로 출력해주는 메소드
        { // ChageTeacherForm에서의 메소드와 형식 동일
            string[] row; // 행에 들어갈 데이터 string배열

             
            LectureView.Width = 418; // 데이터 그리드 뷰 너비 418
            LectureView.AllowUserToAddRows = false; // 행 추가 기능 제한
            LectureView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // 행단위 선택 가능, 셀 단위 제한
            LectureView.RowHeadersVisible = false; // 행 헤더 보이지 않게

            LectureView.ColumnCount = 10; // 열 개수 10개
            // 열 이름 지정
            LectureView.Columns[0].Name = "강의명";
            LectureView.Columns[1].Name = "담당선생";
            LectureView.Columns[2].Name = "월";
            LectureView.Columns[3].Name = "화";
            LectureView.Columns[4].Name = "수";
            LectureView.Columns[5].Name = "목";
            LectureView.Columns[6].Name = "금";
            LectureView.Columns[7].Name = "토";
            LectureView.Columns[8].Name = "시작시간";
            LectureView.Columns[9].Name = "종료시간";
            LectureView.ReadOnly = true; // 읽기 전용
            LectureView.Columns[1].Frozen = true; // 좌우 드래그 시 속성 0~1은 고정
            row = new string[10]; // string배열 생성
            for (int i = 2; i < 8; i++)
                LectureView.Columns[i].Width = 25; // 2~8인덱스의 열은 너비 25
            lectures = cont.Lectures; // 전체 강의 리스트 받음

            if (index == 0) // 학생
            {
                for (int i = 0; i < lectures.Count; i++) // 전체 강의
                {
                    // 열에 맞는 값 설정
                    row[0] = lectures[i].ClassName; 
                    if (lectures[i].Tea != null) // 해당 강의 담당 선생이 있으면
                        row[1] = lectures[i].Tea.Name; // 이름으로
                    else // 없으면
                        row[1] = "미배정"; // 미배정
                    for (int j = 0; j < 6; j++) // 월요일부터 토요일까지
                    {
                        if (lectures[i].Day[j] == 1) // 1이면 해당 요일에 강의 존재
                            row[2 + j] = "○";
                        else // 0이면 해당 요일 강의 없음
                            row[2 + j] = "";
                    }
                    row[8] = lectures[i].StartTime; // 시작시간
                    row[9] = lectures[i].FinishTime; // 종료시간

                    LectureView.Rows.Add(row); // 행 추가

                    if (lectures[i].Tea == null) // 만약 담당 선생이 없으면
                        LectureView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow; // 노란색 표시

                }
            }

            else if (index == 1) // 선생
            {
                for (int i = 0; i < lectures.Count; i++) // 전체 강의 목록
                {
                    if (lectures[i].Tea != null) // 담당 선생이 이미 있는 강의는 건너뜀
                        continue;
                    // 나머진 위와 동일
                    row[0] = lectures[i].ClassName;
                    row[1] = "미배정";
                    for (int j = 0; j < 6; j++)
                    {
                        if (lectures[i].Day[j] == 1)
                            row[2 + j] = "○";
                        else
                            row[2 + j] = "";
                    }
                    row[8] = lectures[i].StartTime;
                    row[9] = lectures[i].FinishTime;

                    LectureView.Rows.Add(row);

                }
            }
        }

        private void YesButton_Click(object sender, EventArgs e) // 확인 버튼 클릭
        {
            string saveImageRoute = string.Empty;
            string phone;
            phone = PhoneTxtBox1.Text + PhoneTxtBox2.Text + PhoneTxtBox3.Text; // 전화번호 미리 조합
            if (index == 0) // 학생
            {
                string pPhone;
                pPhone = parentTextBox1.Text + parentTextBox2.Text + parentTextBox3.Text; // 보호자 번호 조합
                if (NameTxtBox.Text == "" || SchoolTxtBox.Text == "" || phone.Length != 11 || pPhone.Length != 11) // 콤보박스는 무조건 선택되므로 나머지 값 존재 유뮤 파악
                    MessageBox.Show("입력되지 않은 값이 있습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error); // 없을시 메시지 박스
                else // 모두 입력시
                {
                    string school; // 학교
                    newStudent.Name = NameTxtBox.Text; // 이름 받음

                    school = SchoolTxtBox.Text; // 학교명
                    
                    if (schoolComboBox.SelectedIndex == 0) // 초, 중, 고 따로 저장
                        school += "초등학교";
                    else if (schoolComboBox.SelectedIndex == 1)
                        school += "중학교";
                    else
                    {
                        school += "고등학교";
                    }
                    newStudent.School = school; // 학교 받음

                    newStudent.Grade = gradeComboBox.SelectedItem.ToString(); // 학년 정보 받음


                    newStudent.PhoneNumber = phone; // 전화번호 
                    newStudent.ParentPhone = pPhone; // 보호자 번호

                    // 모의 고사 성적은 삽입시에는 입력 받지 않고 이후에 수정에서 할 수 있음
                    // 0으로 입력 시 조회에서 볼 때는 - 로 표시됨
                    newStudent.KorGrade[0] = 0;
                    newStudent.KorGrade[1] = 0;
                    newStudent.EngGrade[0] = 0;
                    newStudent.EngGrade[1] = 0;
                    newStudent.MathGrade[0] = 0;
                    newStudent.MathGrade[1] = 0;

                    newStudent.Id = cont.GetNewStudentID(newStudent.School, newStudent.Grade); // 아이디 배정

                    newStudent.Birth = yearComboBox.SelectedItem.ToString() + monthComboBox.SelectedItem.ToString() + dayComboBox.SelectedItem.ToString(); // 생년월일 받음

                    saveImageRoute = basicRoute + @"\TeamProjectImageSaveForder\studentImage";//폴더 위치 저장

                    if (!System.IO.Directory.Exists(saveImageRoute))//폴더가 존재하지 않을 시
                    {
                        System.IO.Directory.CreateDirectory(saveImageRoute);//폴더가 저장 경로 생성
                    }
                    insertPictureBox.Image.Save(saveImageRoute + "\\" + newStudent.Id + ".png", System.Drawing.Imaging.ImageFormat.Png);//이미지 박스에 이미지 불러오기

                    if (newStudent.Lect.Count == 0) // 수강할 강의를 선택하지 않았을 시
                    {
                        if (MessageBox.Show("수강할 강의를 선택하지 않았습니다.\n수강 강의 없이 등록하시겠습니까?", "학생 등록", MessageBoxButtons.YesNo) == DialogResult.Yes) // 그래도 등록할 지 여부 
                        {
                            cont.Students.Add(newStudent); // 학생 목록에 추가
                            MessageBox.Show("등록되었습니다.\n배정된 학번은 " + newStudent.Id + "입니다.", "등록 완료"); // 배정된 학번을 고지하는 메시지박스
                            Close();
                        }
                        else // 수강을 하고 등록할거면 취소
                        {
                            MessageBox.Show("취소되었습니다.", "등록 취소");
                        }
                    }
                    else // 수강할 강의 선택했을 시
                    {
                        cont.Students.Add(newStudent); // 학생 목록에 추가됨
                        MessageBox.Show("등록되었습니다.\n배정된 학번은 " + newStudent.Id + "입니다.", "등록 완료"); // 배정된 학번을 고지하는 메시지박스
                        Close(); // 폼 닫음
                    }
                }
            }

            else if (index == 1) // 선생
            {
                if (NameTxtBox.Text == "" || phone.Length != 11) // 공백 있을 시
                    MessageBox.Show("입력되지 않은 값이 있습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else // 없을 시
                {
                    newTeacher.Name = NameTxtBox.Text; // 이름 받음
                    newTeacher.Major = (string)schoolComboBox.SelectedItem; // 과목 받음
                    newTeacher.Position = (string)gradeComboBox.SelectedItem; // 직급 받음
                    newTeacher.PhoneNumber = phone; // 전화번호 받음
                    newTeacher.Id = cont.GetNewTeacherID(); // id 배정받음

                    newTeacher.Birth = yearComboBox.SelectedItem.ToString() + monthComboBox.SelectedItem.ToString() + dayComboBox.SelectedItem.ToString(); // 생년월일 받음

                    saveImageRoute = basicRoute + @"\TeamProjectImageSaveForder\teacherImage";//폴더 경로 저장

                    if (!System.IO.Directory.Exists(saveImageRoute))//폴더가 존재하지 않을 시
                    {
                        System.IO.Directory.CreateDirectory(saveImageRoute);//폴더가 저장 경로 생성
                    }
                    insertPictureBox.Image.Save(saveImageRoute + "\\" + newTeacher.Id + ".png", System.Drawing.Imaging.ImageFormat.Png);

                    if (newTeacher.Lect.Count == 0) // 담당할 강의 선택 안 했을 시
                    {
                        if (MessageBox.Show("담당할 강의를 선택하지 않았습니다.\n수강 강의 없이 등록하시겠습니까?", "학생 등록", MessageBoxButtons.YesNo) == DialogResult.Yes) // 담당 강의 없이 등록할지 여부
                        {
                            cont.Teachers.Add(newTeacher); // 선생 목록에 추가
                            MessageBox.Show("등록되었습니다.\n배정된 교번은 " + newTeacher.Id + "입니다.", "등록 완료"); // 배정된 id 고지하는 메시지 박스
                            Close(); // 폼 닫음
                        }
                        else // 담당 강의 등록하고 가입할거면
                        {
                            MessageBox.Show("취소되었습니다.", "등록 취소");
                        }
                    }
                    else // 담당 강의 선택했을 시
                    {
                        cont.Teachers.Add(newTeacher); // 선생 목록에 추가
                        MessageBox.Show("등록되었습니다.\n배정된 교번은 " + newTeacher.Id + "입니다.", "등록 완료"); // 배정된 id 고지하는 메시지 박스
                        Close();
                    }
                }
            }



        }

        private void NoButton_Click(object sender, EventArgs e) // 취소 버튼 클릭
        {
            if (index == 0) // 학생이면
                newStudent.DeleteAllLecture(); // 학생이 선택한 강의들 모두 삭제
            else if (index == 1) // 선생이면
                newTeacher.DeleteAllLecture(); // 선생이 담당한 강의들 모두 삭제
            Close(); // 폼 닫음
        }

        private void ImageInsertButton_Click(object sender, EventArgs e)
        {
            // 이미지등록 버튼 눌렀을 때
            // pictureBox 변수에 이미지 넣으면 됨
            string imageFileRoute = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                imageFileRoute = openFileDialog.FileName;
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            insertPictureBox.Image = Bitmap.FromFile(imageFileRoute);
            insertPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void LectureView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // 강의 목록이 나오는 데이터 그리드 뷰의 셀 두번 클릭 시
        {
            Lecture lect; // 더블 클릭된 강의 받을 객체
            string[] send = new string[4]; // 데이터 그리드 뷰 내용으로 해당 강의를 검색하기 위한 string 배열
            for (int i = 0; i < 4; i++) // 검색을 위한 정보를 받음
            {
                if (i <= 1)
                    send[i] = LectureView.Rows[LectureView.CurrentCellAddress.Y].Cells[i].Value.ToString();
                else
                {
                    send[i] = LectureView.Rows[LectureView.CurrentCellAddress.Y].Cells[i + 6].Value.ToString();
                }
            }
            if (index == 0)//학생
            {
                string[] row = new string[4]; // 메시지 박스에서의 이용을 위한 string 배열
                
                // 배열 초기화
                row[0] = "강의명 : " + send[0] + "\n";
                row[1] = "담당선생 : " + send[1] + "\n";
                row[2] = "시작시간 : " + send[2] + "\n";
                row[3] = "종료시간 : " + send[3] + "\n";
                lect = cont.FindInsertLecture(send); // send 배열 값으로 강의 검색

                if (newStudent.Lect.Contains(lect)) // 만약 이미 학생이 수강하기로한 강의이면 취소로 받아들임
                {
                    if (MessageBox.Show
                    ("해당 강의를 취소하시겠습니까?\n\n" + row[0] + row[1] + row[2] + row[3], "수강취소", MessageBoxButtons.YesNo) == DialogResult.Yes) // 취소 여부를 물어봄
                    {
                        LectureView.Rows[LectureView.CurrentCellAddress.Y].DefaultCellStyle.BackColor = Color.White; // 해당 강의를 취소했다는 의미로 데이터 그리드 뷰에서의 행을 다시 흰색으로 바꿈
                        newStudent.DeleteLecture(lect); // 학생 강의 리스트에서 강의 삭제
                    }
                }
                else // 수강 신청하는 강의라면
                {
                    if (MessageBox.Show
                    ("해당 강의를 수강하시겠습니까?\n\n" + row[0] + row[1] + row[2] + row[3], "수강신청", MessageBoxButtons.YesNo) == DialogResult.Yes) // 수강 여부를 물어봄
                    {
                        if (lect.LectNum <= lect.Students.Count) // 강의 총 인원 수가 다 찼으면
                            MessageBox.Show("해당 강의는 여석이 없습니다.", "강의 여석 없음");
                        else if (cont.lecturePersonRedundancyCheck(lect, newStudent)) // 학생이 현재 수강하기로 한 강의와 시간이 겹치는 강의인지 여부 판단
                        {
                            LectureView.Rows[LectureView.CurrentCellAddress.Y].DefaultCellStyle.BackColor = Color.GreenYellow; // 해당 강의를 수강한다는 의미로 데이터 그리드 뷰에서의 행의 색 변경
                            newStudent.AddLecture(lect); // 학생 강의 리스트에 추가
                        }
                        else // 겹치는 시간이 있으면
                            MessageBox.Show("해당 강의 시간과 겹치는 강의를 수강중입니다.", "수강 오류");
                    }
                }


            }
             
            else if (index == 1) // 선생
            { // 위와 동일
                string[] row = new string[3];

                row[0] = "강의명 : " + send[0] + "\n";
                row[1] = "시작시간 : " + send[2] + "\n";
                row[2] = "종료시간 : " + send[3] + "\n";
                lect = cont.FindInsertLecture(send);

                if (newTeacher.Lect.Contains(lect))
                {
                    if (MessageBox.Show
                    ("해당 강의를 취소하시겠습니까?\n\n" + row[0] + row[1] + row[2], "수강취소", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        LectureView.Rows[LectureView.CurrentCellAddress.Y].DefaultCellStyle.BackColor = Color.White;
                        newTeacher.DeleteLecture(lect);
                    }
                }
                else
                {
                    if (MessageBox.Show
                    ("해당 강의를 담당하시겠습니까?\n\n" + row[0] + row[1] + row[2], "수강신청", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (cont.lecturePersonRedundancyCheck(lect, newTeacher))
                        {
                            LectureView.Rows[LectureView.CurrentCellAddress.Y].DefaultCellStyle.BackColor = Color.GreenYellow;
                            newTeacher.AddLecture(lect);
                        }
                        else
                            MessageBox.Show("해당 강의 시간과 겹치는 강의를 수강중입니다.", "수강 오류");
                    }
                }
            }
        }

        private void monthComboBox_SelectedIndexChanged(object sender, EventArgs e) // 생년월일에서 월 콤보박스가 변경되면
        {
            int month, year; // 연, 월 선택되어 있는 정보
            string[] day; // 일 콤보 박스에 저장할 string 배열
            month = Int32.Parse(monthComboBox.SelectedItem.ToString()); // 선택된 값 int 로 변환
            year = Int32.Parse(yearComboBox.SelectedItem.ToString()); // 선택된 값 int 로 변환
            day = cont.GetDayComboBoxItems(year, month); // 월에 따른 일의 기간(1~31, 1~30, 1~28, 1~29) 받아옴, 윤년계산 포함
            dayComboBox.Items.Clear(); // 기존 내용 삭제
            dayComboBox.Items.AddRange(day); // 새로운 아이템들 삽입
            dayComboBox.Text = "01"; // 1일로 설정
        }

        private void yearComboBox_SelectedIndexChanged(object sender, EventArgs e) // 생년월일에서 연 콤보박스가 변경되면
        {
            monthComboBox.Text = "01";  // 월 값을 01로 초기화
        }

        private void PhoneTxtBox1_TextChanged(object sender, EventArgs e) // 전화번호 텍스트박스 값이 변경되면
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(PhoneTxtBox1.Text, out tmpSecondText);
            if (PhoneTxtBox1.Text != "") // 내용이 아예 없을 때를 제외(백스페이스할 때 메시지 박스가 출력되는 것을 방지)
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false) // 숫자 외의 값이 입력되면
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error); // 경고창 출력 후 텍스트 박스 초기화
                    PhoneTxtBox1.Text = "";
                }
                if (PhoneTxtBox1.TextLength == 3) // 입력이 다되면 자동으로 다음 텍스트 박스로 포커스 이동
                    this.ActiveControl = PhoneTxtBox2;
            }
        }
        // 아래는 모두 위와 동일한 방식
        private void PhoneTxtBox2_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(PhoneTxtBox2.Text, out tmpSecondText);
            if (PhoneTxtBox2.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PhoneTxtBox2.Text = "";
                }
                if (PhoneTxtBox2.TextLength == 4)
                    this.ActiveControl = PhoneTxtBox3;
            }
        }

        private void PhoneTxtBox3_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(PhoneTxtBox3.Text, out tmpSecondText);
            if (PhoneTxtBox3.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PhoneTxtBox3.Text = "";
                }
                if (PhoneTxtBox3.TextLength == 4)
                    this.ActiveControl = parentTextBox1;
            }
        }

        private void parentTextBox1_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(parentTextBox1.Text, out tmpSecondText);
            if (parentTextBox1.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    parentTextBox1.Text = "";
                }
                if (parentTextBox1.TextLength == 3)
                    this.ActiveControl = parentTextBox2;
            }
        }

        private void parentTextBox2_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(parentTextBox2.Text, out tmpSecondText);
            if (parentTextBox2.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    parentTextBox2.Text = "";
                }
                if (parentTextBox2.TextLength == 4)
                    this.ActiveControl = parentTextBox3;
            }
        }

        private void parentTextBox3_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(parentTextBox3.Text, out tmpSecondText);
            if (parentTextBox3.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    parentTextBox3.Text = "";
                }
                if (parentTextBox3.TextLength == 4)
                    this.ActiveControl = YesButton;
            }
        }

        
    }
}