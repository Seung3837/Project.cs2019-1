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
    public partial class StudentForm : Form
    {
        private string basicRoute = @"C:\Team8ProjectForder";//기본 저장 경로
        Controller cont;
        private List<Lecture> lectures;
        Student loginStudent;
        int useDeskNum;
        Button[] desk;
        public StudentForm(Student loginStudent, Controller cont)
        {
            InitializeComponent();
            this.cont = cont;
            this.loginStudent = loginStudent;
            
            useDeskNum = MakeDeskButton(); // 좌석 버튼 설정 및 사용 중인 좌석 번호
            // 학생 정보 라벨 설정
            studentSetNameLabel.Text = loginStudent.Name;
            studentSetSchoolLabel.Text = loginStudent.School;
            studentSetBirthLabel.Text = loginStudent.Birth.Substring(0, 4) + "년 " + loginStudent.Birth.Substring(4, 2) + "월 " + loginStudent.Birth.Substring(6, 2) + "일";
            studentSetPhoneNumberLabel.Text = loginStudent.PhoneNumber.Substring(0, 3) + "-" + loginStudent.PhoneNumber.Substring(3, 4) + "-" + loginStudent.PhoneNumber.Substring(7, 4);
            parentPhoneTextBox.Text = loginStudent.ParentPhone.Substring(0, 3) + "-" + loginStudent.ParentPhone.Substring(3, 4) + "-" + loginStudent.ParentPhone.Substring(7);
            studentSetIdLabel.Text = loginStudent.Id;

            string tmpFirstGrade = loginStudent.Grade;
            studentSetGradeLabel.Text = tmpFirstGrade + "학년";
            string studentImageRoute = basicRoute + @"\TeamProjectImageSaveForder\studentImage\" + loginStudent.Id + ".png";//이미지 정보 불러오기
            System.IO.FileInfo studentImageFileInfo = new System.IO.FileInfo(studentImageRoute);//파일 정보 저장
            if (studentImageFileInfo.Exists)//파일 존재할 시
            {
                studentPictureBox.Load(studentImageRoute);//불러오기
                studentPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;//이미지 크기 설정
            }
            GetOneStudentLectureView(); // 로그인 학생의 강의 리스트 출력
        }

        public int MakeDeskButton() // 독서실 좌석 버튼을 만드는 메소드
        {
            int index, num;
            num = -1; // 해당 학생의 좌석 이용 번호 정보
            desk = new Button[20]; // 좌석 버튼을 생성
            for(int i = 0; i < 5; i++) //5행
            {
                for (int j = 0; j < 4; j++) // 4열
                {
                    index = i * 4 + j; // index는 0~19
                    desk[index] = new Button();  // 버튼 생성
                    desk[index].Name = "desk" + index; // 이름 설정
                    desk[index].Text = "좌석" + (index + 1); // 텍스트 설정
                    panel1.Controls.Add(desk[index]); // 패널에add
                    desk[index].Size = new System.Drawing.Size(54, 50); // 사이즈 설정
                    desk[index].Location = new Point(78 + j * 74, 16 + i * 66); // 위치 설정
                    desk[index].Click += new EventHandler(btn_Click); // 이벤트 연결
                    if (cont.Library.desk[index].Stu == loginStudent) // 해당 학생이 좌석을 이용 중이면
                    {
                        desk[index].BackColor = Color.BlueViolet; // 색 설정
                        num = index; // 좌석 번호 설정
                    }
                    else if(cont.Library.desk[index].Stu == null) // 이용 중인 학생이 없으면
                    {
                        desk[index].BackColor = Color.GreenYellow; // 색 설정
                    } 
                    else // 다른 학생이 사용중이면
                    {
                        desk[index].BackColor = Color.OrangeRed; // 색 설정
                    }
                }
            }
            // 색의 의미 설명 위한 패널들
            panel2.BackColor = Color.BlueViolet; 
            panel3.BackColor = Color.OrangeRed;  
            panel4.BackColor = Color.GreenYellow;  
            return num;
        }
        public void GetOneStudentLectureView()
        {
            // InsertForm과 비슷하므로 생략하겠습니다.
            string[] row;
            lectures = cont.Lectures;
            oneStudentLectureView.Rows.Clear();
            oneStudentLectureView.Columns.Clear();
            oneStudentLectureView.AllowUserToAddRows = false;
            oneStudentLectureView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oneStudentLectureView.ColumnCount = 11;
            oneStudentLectureView.Columns[0].Name = "강의명";
            oneStudentLectureView.Columns[1].Name = "담당선생";
            oneStudentLectureView.Columns[2].Name = "강의실";
            oneStudentLectureView.Columns[3].Name = "월";
            oneStudentLectureView.Columns[4].Name = "화";
            oneStudentLectureView.Columns[5].Name = "수";
            oneStudentLectureView.Columns[6].Name = "목";
            oneStudentLectureView.Columns[7].Name = "금";
            oneStudentLectureView.Columns[8].Name = "토";
            oneStudentLectureView.Columns[9].Name = "시작시간";
            oneStudentLectureView.Columns[10].Name = "종료시간";
            oneStudentLectureView.ReadOnly = true;
            oneStudentLectureView.Columns[0].Width = 100;
            oneStudentLectureView.Columns[1].Width = 70;
            oneStudentLectureView.Columns[2].Width = 70;
            oneStudentLectureView.Columns[9].Width = 70;
            oneStudentLectureView.Columns[10].Width = 70;
            for (int i = 3; i <= 8; i++)
                oneStudentLectureView.Columns[i].Width = 25;
            oneStudentLectureView.Columns[2].Frozen = true;
            row = new string[11];

            lectures = loginStudent.Lect;

            for (int i = 0; i < lectures.Count; i++)
            {
                row[0] = lectures[i].ClassName;
                if (lectures[i].Tea != null)
                    row[1] = lectures[i].Tea.Name;
                else
                    row[1] = "미배정";

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
                oneStudentLectureView.Rows.Add(row);

                if (loginStudent.Lect[i].Tea == null)
                    oneStudentLectureView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

            }
            
        }
        
  
        private void btn_Click(object sender, EventArgs e) // 좌석 버튼 클릭시
        {
            Button btn = sender as Button; // 버튼으로 변경
            int i;
            for(i = 0; i < 20; i++)
            {
                if (desk[i] == btn) // 버튼을 찾고
                    break;
            }
            if (cont.Library.desk[i].Stu == loginStudent) // 로그인 학생이 자신이 사용중인 버튼을 누르면
            {
                if (MessageBox.Show("해당 좌석을 사용을 종료하시겠습니까?", "좌석 사용 종료", MessageBoxButtons.YesNo) == DialogResult.Yes) // 좌석 사용 종료 여부
                {
                    cont.Library.desk[i].finishDesk(); // finishDesk()메소드 호출
                    desk[i].BackColor = Color.GreenYellow; // 색 설정
                    useDeskNum = -1; // 사용중인 좌석 번호 초기화
                }
            }
            else if (cont.Library.desk[i].Stu == null) // 사용중이지 않은 좌석이면
            {
                if (useDeskNum == -1) // 로그인 학생도 사용중인 좌석이 없으면
                {
                    cont.Library.desk[i].startDesk(loginStudent); // 해당 좌석에 startDesk 메소드 호출
                    desk[i].BackColor = Color.BlueViolet; // 색 설정
                    useDeskNum = i; // 좌석 번호 저장
                    MessageBox.Show((i + 1) + "번 좌석이 지정되었습니다.", "좌석 신청 완료"); // 좌석 지정 완료 메시지 박스
                }
                else // 로그인 학생이 사용중인 좌석이 있으면
                {
                    MessageBox.Show("이미 " + (useDeskNum + 1) + "번 좌석을 사용중입니다.", "좌석 신청 오류", MessageBoxButtons.OK, MessageBoxIcon.Error); // 해당 좌석 번호 메시지 박스
                }
            }
            else // 다른 학생이 사용중인 좌석이면
            {
                MessageBox.Show("다른 학생이 사용중인 좌석입니다.", "좌석 신청 오류", MessageBoxButtons.OK, MessageBoxIcon.Error); // 메시지 박스 
            }
        }
        
    }
}
