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
    public partial class LectureForm : Form
    {
        //datagrid 정보 저장
        private const int rowCount = 8, columnCount = 6;
        private const int cellWidth = 100, cellHeight = 60;
        Controller cont;
        List<Lecture> lectures;//전체 강의 저장
        string[] stdRoom;//존재하는 강의실 정보 저장
        int[][] roomTimeArray;//강의실 시간표 저장
        public LectureForm(Controller cont)
        {
            InitializeComponent();
            this.cont = cont;
            SetBasicState();
            
        }
        private void SetBasicState()//초기 상태 설정
        {
            GetLectureView();//강의 리스트 설정
            SetRoomComboBox();//강의실 이름 설정
            roomSelectCombo.Visible = false;//강의실 선택 콤보 비활성화
            roomSchedule.Visible = false;//강의실 시간표 비활성화
            lectureViewSelectButton.Text = "강의실\n시간표 보기";//텍스트 적용
            roomNameLabel.Text = "전체 강의실";
        }

        private void GetLectureView() // 전체 강의 리스트를 보여주는 데이터 그리드 뷰 설정
        {
            // 모든 설정은 View폴더의 순서상 앞선 다른 폼에서의 설정과 비슷하므로 생략합니다.
            string[] row;
            LectureView.Rows.Clear();
            LectureView.Columns.Clear();
            LectureView.RowHeadersVisible = false;
            LectureView.ReadOnly = true;
            LectureView.AllowUserToAddRows = false;
            LectureView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LectureView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            LectureView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            row = new string[13];
            LectureView.ColumnCount = 13;
            for (int i = 0; i < 13; i++)
                LectureView.Columns[i].Width = 90;
            LectureView.Columns[12].Width = 87;
            LectureView.Columns[3].Frozen = true;
            LectureView.Columns[0].Name = "강의번호";
            LectureView.Columns[1].Name = "강의명";
            LectureView.Columns[2].Name = "담당선생";
            LectureView.Columns[3].Name = "강의실";
            LectureView.Columns[4].Name = "월";
            LectureView.Columns[5].Name = "화";
            LectureView.Columns[6].Name = "수";
            LectureView.Columns[7].Name = "목";
            LectureView.Columns[8].Name = "금";
            LectureView.Columns[9].Name = "토";
            LectureView.Columns[10].Name = "시작시간";
            LectureView.Columns[11].Name = "종료시간";
            LectureView.Columns[12].Name = "비고";

            for (int i = 4; i <= 9; i++)
                LectureView.Columns[i].Width = 25;

            lectures = cont.Lectures;

            for (int i = 0; i < lectures.Count; i++)
            {
                row[0] = lectures[i].Id;
                row[1] = lectures[i].ClassName;
                if (lectures[i].Tea != null)
                    row[2] = lectures[i].Tea.Name;
                else
                    row[2] = "미배정";

                row[3] = lectures[i].Classroom;
                for (int j = 0; j < 6; j++)
                {
                    if (lectures[i].Day[j] == 1)
                        row[4 + j] = "○";
                    else
                        row[4 + j] = "";
                }
                row[10] = lectures[i].StartTime;
                row[11] = lectures[i].FinishTime;
                row[12] = "";
                LectureView.Rows.Add(row);
            }

        }

        private void roomSchedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)//각 시간표 더블 클릭 시 정포 출력
        {
            //MessageBox
            Teacher tmpTeacher;
            int lectureIndex = roomTimeArray[e.ColumnIndex - 1][e.RowIndex];//위치 가져오기
            if (lectureIndex != 0)
            {
                tmpTeacher = cont.FindOnlyTeacher(cont.Lectures[lectureIndex - 1].TeaId);//저장한 lecture에 index로 선생님 정보 찾기
                string result = "담당 : " + tmpTeacher.Name + "\n" + "과목 : " + cont.Lectures[lectureIndex - 1].Subject + "\n" + "학생 수 : " + cont.Lectures[lectureIndex - 1].Students.Count;
                MessageBox.Show(result, "강의 정보", MessageBoxButtons.OK, MessageBoxIcon.Information);//강의 정보 표시
            }
        }

        private void roomSelectCombo_SelectedIndexChanged(object sender, EventArgs e)//선택 강의 변경
        {
            SetSchedule(roomSelectCombo.Text);//스케쥴 변경
            roomNameLabel.Text = roomSelectCombo.Text;
        }

        private void lectureViewSelectButton_Click(object sender, EventArgs e)//강의실 시간표 보일지 리스트로 정보 보일지
        {
            //강의실 인지 리스트인지에 맞게 활성화 및 텍스트 변경.
            if(lectureViewSelectButton.Text == "강의실\n시간표 보기")
            {
                roomSelectCombo.Visible = true;
                roomSchedule.Visible = true;
                lectureViewSelectButton.Text = "전체\n시간표 보기";
                roomNameLabel.Text = roomSelectCombo.Text;
                SetSchedule(roomSelectCombo.Text);
                LectureView.Visible = false;
            }
            else
            {
                roomSelectCombo.Visible = false;
                roomSchedule.Visible = false;
                lectureViewSelectButton.Text = "강의실\n시간표 보기";
                roomNameLabel.Text = "전체 강의실";
                LectureView.Visible = true;
            }
        }

        private void SetSchedule(string roomName)//강의실 이름에 맞게 시간표 설정
        {
            //datagrid 초기화
            roomSchedule.Rows.Clear();
            roomSchedule.Columns.Clear();
            roomSchedule.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            roomSchedule.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            roomSchedule.ColumnCount = 7;
            //각 행에 텍스트 설정
            roomSchedule.Columns[1].Name = "월요일";
            roomSchedule.Columns[2].Name = "화요일";
            roomSchedule.Columns[3].Name = "수요일";
            roomSchedule.Columns[4].Name = "목요일";
            roomSchedule.Columns[5].Name = "금요일";
            roomSchedule.Columns[6].Name = "토요일";
            //강의실 이름에 맞는 배열 받기
            roomTimeArray = cont.GetRoomTimeSchedule(roomName);
            string[] row = new string[7];
            int startTime = 10;//시작 시간ㅈ
            for(int i = 0; i < 24; i++)//10~22시까지 정보 부러오기
            {
                if (i % 2 == 0)//분이 30시작이면 시가 넘어가는 것을 계산
                    row[0] = startTime + ":" + "00 ~ " + startTime + ":30";
                else
                {
                    row[0] = startTime + ":" + "30 ~ " + (startTime + 1) + ":00";
                    startTime++;
                }
                for (int j = 1; j < 7; j++)//저장된 2차원 배열에 강의 인덱스가 저장되어 있으므로 그정보로 시간표에 기입
                {
                    if (roomTimeArray[j - 1][i] != 0)
                    {
                        row[j] = cont.Lectures[roomTimeArray[j - 1][i] - 1].ClassName;
                    }
                    else
                        row[j] = "";
                }
                roomSchedule.Rows.Add(row);
            }
            roomSchedule.Columns[0].Width = 100;
            for (int i = 1; i < 7; i++)
                roomSchedule.Columns[i].Width = 110;
            roomSchedule.Columns[6].Frozen = true;//크기 변화 금지
        }

        private void SetRoomComboBox()//강의실 이름 저장
        {
            stdRoom = new string[3];
            for (int i = 0; i < 3; i++)
                stdRoom[i] = "강의실" + (i + 1);
            Array.Sort(stdRoom);
            roomSelectCombo.Items.AddRange(stdRoom);
            roomNameLabel.Text = roomSelectCombo.Text = stdRoom[0];
        }
    }
}
