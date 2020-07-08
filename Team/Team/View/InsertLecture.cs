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
    public partial class InsertLecture : Form // 강의를 삽입하는 폼
    {
        Controller cont; // 컨트롤러
        Lecture newLecture; // 신규 강의

        public InsertLecture(Controller cont)
        {
            InitializeComponent();
            this.cont = cont; // 컨트롤러
            cont.SetLectureInsertForm(this); // 컨트롤러가 해당 인서트 폼을 넘겨받도록 메소드 호출
            newLecture = new Lecture(); // 신규 강의 생성
            SetComboBoxItems(); // 콤보박스 값 생성 및 설정
        }
        
        private void SetComboBoxItems() // 콤보박스 값 생성 및 설정
        {
            string[] room = { "강의실1", "강의실2", "강의실3" }; // 강의실 목록
            string[] subject = { "국어", "수학", "영어" }; // 과목
            string[] min = { "00", "30" }; // 강의시간에서 분

            classroomComboBox.Items.AddRange(room); // 강의실 콤보박스
            subjectComboBox.Items.AddRange(subject); // 과목 콤보박스

            for (int i = 10; i <= 21; i++)
                StartHourComboBox.Items.Add(i); // 시작시간 콤보박스 10~21시까지
            for (int i = 11; i <= 22; i++) 
                EndHourComboBox.Items.Add(i); // 종료시간 콤보박스 11~22시까지
            StartMinComboBox.Items.AddRange(min); // 시작 분 콤보박스 
            EndMinComboBox.Items.AddRange(min); // 종료 분 콤보박스

            // 초기값 설정
            classroomComboBox.Text = "강의실1";
            subjectComboBox.Text = "국어";
            StartHourComboBox.Text = "10";
            StartMinComboBox.Text = "00";
            EndHourComboBox.Text = "11";
            EndMinComboBox.Text = "00";

            LectureTeaTextBox.Enabled = false; // 담당 선생 텍스트 박스는 변경 불가, ChangeTeacherForm에서 변경됨

        }

        private void LectureUpdateOKButton_Click(object sender, EventArgs e) // 확인 버튼 클릭
        {
            int cnt = 0; // 요일 체크 박스가 몇 개 체크 되었는 지 갯수 확인
            int[] day = { 0, 0, 0, 0, 0, 0 }; // 요일 정보를 신규 강의에 저장하기 위한 배열

            // 체크된 요일 체크 박스에 따른 cnt 증가와 해당 요일 배열 값 변경
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
            if (MessageBox.Show("강의를 등록하시겠습니까?", "강의 등록", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (newLecture.Tea == null || LectureNameTextBox.Text == "" || cnt == 0) // 만약 공백값, 선택되지 않은 값이 있다면
                {
                    MessageBox.Show("정보를 모두 입력해주십시오.", "공백 존재", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else // 모두 채워졌다면
                {
                    newLecture.ClassName = LectureNameTextBox.Text; // 이름 받음
                    newLecture.Classroom = classroomComboBox.SelectedItem.ToString(); // 강의실 받음
                    newLecture.Subject = subjectComboBox.SelectedItem.ToString(); // 과목 받음
                    newLecture.StartTime = StartHourComboBox.SelectedItem.ToString() + ":" + StartMinComboBox.SelectedItem.ToString(); // 시작 시간 받음
                    newLecture.FinishTime = EndHourComboBox.SelectedItem.ToString() + ":" + EndMinComboBox.SelectedItem.ToString(); // 종료 시간 받음
                    newLecture.Day = day; // 강의 요일 받음
                    newLecture.Id = cont.GetNewLectureID(newLecture.Subject); // 아이디 배정 받음
                    if (!(cont.lecturePersonRedundancyCheck(newLecture, newLecture.Tea))) // 담당 선생이 해당 시간에 이미 강의가 있는 지 검사
                    {
                        newLecture.Tea = null; // 담당 선생을 없앰
                        LectureTeaTextBox.Text = ""; // 텍스트 박스 공백
                        MessageBox.Show("선택하신 선생님은 해당시간에 다른 강의를 담당하고 있습니다.", "시간 중복");
                    }
                    else // 없다면
                    {
                        if (cont.lectureRoomRedundancyCheck(newLecture.Day, newLecture.StartTime, newLecture.FinishTime, newLecture.Id, newLecture.Classroom))
                        { // 해당 강의실의 해당 시간에 강의가 있는 지 여부 검사
                            newLecture.Tea.AddLecture(newLecture); // 선생의 담당 강의 리스트에 강의 추가
                            cont.Lectures.Add(newLecture); // 전체 강의 목록에 강의 추가
                            cont.GetViews(); // 삽입 후 ListForm 에서 수정된 정보가 데이터 그리드 뷰에 적용되게 출력
                            Close(); // 폼 닫음
                        }
                        else // 중복이면
                            MessageBox.Show("해당 시간의 강의실이 이미 사용중입니다.", "강의실 시간 중복");
                    }
                }
            }
        }

        private void LectureUpdateNoButton_Click(object sender, EventArgs e) // 취소 버튼 클릭
        {
            if(newLecture.Tea != null) // 만약 담당 선생이 있다면
                newLecture.Tea.DeleteLecture(newLecture); // 해당 강의를 삭제
            Close(); // 폼 닫음
        }

        public void SetLectureTeaTextBox() // 선생 텍스트 박스 설정
        {
            if(newLecture.Tea != null) // 담당 선생이 있다면 이름 넣음
                LectureTeaTextBox.Text = newLecture.Tea.Name; 
        }

        private void TeacherChangeButton_Click(object sender, EventArgs e) // 선생 변경 버튼 클릭
        {
            Teacher Tea;
            Tea = newLecture.Tea; // 현재 선생 정보 저장해 둠

            Team.View.ChangeTeacherForm changeTeaForm = new View.ChangeTeacherForm(cont, newLecture, 1); // ChangeTeacherForm 생성 및 호출
            changeTeaForm.Show();
        }
    }
}
