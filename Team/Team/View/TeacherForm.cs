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
    public partial class TeacherForm : Form
    {
        private string basicRoute = @"C:\Team8ProjectForder";//기본 저장 경로
        Controller cont; // 컨트롤러
        Teacher loginTeacher; // 로그인 선생
        ListForm listForm; // 리스트폼
        List<Lecture> lectures; // 강의 리스트
        bool teacherInfoModifiyState = false; 
        public TeacherForm(Teacher loginTeacher, Controller cont)
        {
            InitializeComponent();
            this.cont = cont; // 컨트롤러
            this.loginTeacher = loginTeacher; // 로그인 선생

            SetComboBoxes(); // 콤보 박스 초기 설정

            SetOriginTeacherValue(false);
            GetLectureView(); // 선생 담당 강의 리스트 출력

            string teacherImageRoute = basicRoute + @"\TeamProjectImageSaveForder\teacherImage\" + loginTeacher.Id + ".png";//이미지 경로 저장
            System.IO.FileInfo TeacherImageFileInfo = new System.IO.FileInfo(teacherImageRoute);//파일 정보 저장
            if (TeacherImageFileInfo.Exists)//파일 존재 시
            {
                Image teacherImage;
                using (var bmpTemp = new Bitmap(teacherImageRoute))//이미지 저장
                {
                    teacherImage = new Bitmap(bmpTemp);
                }
                teacherPictureBox.Image = teacherImage;//이미지 불러오기
                teacherPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;//크기에 맞게 저장
            }
            UnvalidBoxes();
            //GetOneStudentLectureView();
        }
        private void SetOriginTeacherValue(bool saveImage)
        {
            // 해당 선생 정보 텍스트 박스 및 콤보 박스에 출력
            teacherSetNameTextBox.Text = loginTeacher.Name;
            teacherSetMajorCombo.Text = loginTeacher.Major;
            teacherSetPositionCombo.Text = loginTeacher.Position;
            teacherSetFirstPhoneNumberTextBox.Text = loginTeacher.PhoneNumber.Substring(0, 3);
            teacherSetSecondPhoneNumberTextBox.Text = loginTeacher.PhoneNumber.Substring(3, 4);
            teacherSetThirdPhoneNumberTextBox.Text = loginTeacher.PhoneNumber.Substring(7, 4);
            teacherSetYearCombo.Text = loginTeacher.Birth.Substring(0, 4);
            teacherSetMonthCombo.Text = loginTeacher.Birth.Substring(4, 2);
            teacherSetDayCombo.Text = loginTeacher.Birth.Substring(6);
            teacherSetIdLabel.Text = loginTeacher.Id;

            if (saveImage == true)
            {
                string saveImageRoute;
                saveImageRoute = basicRoute + @"\TeamProjectImageSaveForder\teacherImage";
                if (!System.IO.Directory.Exists(saveImageRoute))
                {
                    System.IO.Directory.CreateDirectory(saveImageRoute);
                }
                teacherPictureBox.Image.Save(saveImageRoute + "\\" + loginTeacher.Id + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void SetComboBoxes()
        {
            // 다른 폼들과 똑같으므로 생략하겠습니다.
            string[] major = { "국어", "수학", "영어" };
            teacherSetMajorCombo.Items.AddRange(major);

            string[] position = { "신입", "전문", "부원장", "원장" };
            teacherSetPositionCombo.Items.AddRange(position);

            string[] year, month;
            year = cont.GetYearComboboxItems();
            month = cont.GetMonthComboBoxItems();

            teacherSetYearCombo.Items.AddRange(year);
            teacherSetMonthCombo.Items.AddRange(month);

        }

        private void GetLectureView()
        {
            // 다른 폼들과 동일하므로 생략하겠습니다.
            string[] row;
            oneTeacherLectureView.Rows.Clear();
            oneTeacherLectureView.Columns.Clear();
            oneTeacherLectureView.RowHeadersVisible = false;
            oneTeacherLectureView.ReadOnly = true;
            oneTeacherLectureView.AllowUserToAddRows = false;
            oneTeacherLectureView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oneTeacherLectureView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oneTeacherLectureView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

            row = new string[12];
            oneTeacherLectureView.ColumnCount = 12;
            for (int i = 0; i < 6; i++)
                oneTeacherLectureView.Columns[i].Width = 70;
            oneTeacherLectureView.Columns[0].Name = "강의번호";
            oneTeacherLectureView.Columns[1].Name = "강의명";
            oneTeacherLectureView.Columns[2].Name = "강의실";
            oneTeacherLectureView.Columns[3].Name = "월";
            oneTeacherLectureView.Columns[4].Name = "화";
            oneTeacherLectureView.Columns[5].Name = "수";
            oneTeacherLectureView.Columns[6].Name = "목";
            oneTeacherLectureView.Columns[7].Name = "금";
            oneTeacherLectureView.Columns[8].Name = "토";
            oneTeacherLectureView.Columns[9].Name = "시작시간";
            oneTeacherLectureView.Columns[10].Name = "종료시간";
            oneTeacherLectureView.Columns[11].Name = "비고";
            oneTeacherLectureView.Columns[2].Frozen = true;
            for (int i = 3; i < 9; i++)
                oneTeacherLectureView.Columns[i].Width = 25;

            lectures = loginTeacher.Lect;

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
                oneTeacherLectureView.Rows.Add(row);
            }
        }

        private void UnvalidBoxes()
        {
            //필요 없는 박스는 활용 못하게 설정
            teacherSetNameTextBox.Enabled = false;
            teacherSetPositionCombo.Enabled = false;
            teacherSetMajorCombo.Enabled = false;
            teacherSetYearCombo.Enabled = false;
            teacherSetMonthCombo.Enabled = false;
            teacherSetDayCombo.Enabled = false;
            teacherSetFirstPhoneNumberTextBox.Enabled = false;
            teacherSetSecondPhoneNumberTextBox.Enabled = false;
            teacherSetThirdPhoneNumberTextBox.Enabled = false;
            imageRegisterButton.Visible = false;
        }
        private void ValidBoxes()
        {
            //필요 있는 박스는 활용 하게 설정
            teacherSetNameTextBox.Enabled = true;
            teacherSetPositionCombo.Enabled = true;
            teacherSetMajorCombo.Enabled = true;
            teacherSetYearCombo.Enabled = true;
            teacherSetMonthCombo.Enabled = true;
            teacherSetDayCombo.Enabled = true;
            teacherSetFirstPhoneNumberTextBox.Enabled = true;
            teacherSetSecondPhoneNumberTextBox.Enabled = true;
            teacherSetThirdPhoneNumberTextBox.Enabled = true;
            imageRegisterButton.Visible = true;
        }
        
        private void ModificationTeacherInformation()
        {
            //수정된 정보로 저장
            loginTeacher.Name = teacherSetNameTextBox.Text;
            loginTeacher.Position = teacherSetPositionCombo.SelectedItem.ToString();
            loginTeacher.Birth = teacherSetYearCombo.SelectedItem.ToString() + teacherSetMonthCombo.SelectedItem.ToString() + teacherSetDayCombo.SelectedItem.ToString();
            loginTeacher.PhoneNumber = teacherSetFirstPhoneNumberTextBox.Text + teacherSetSecondPhoneNumberTextBox.Text + teacherSetThirdPhoneNumberTextBox.Text;
        }
        private void teacherInfoModifiyButton_Click(object sender, EventArgs e)//수정 정보
        {
            if(teacherInfoModifiyState == false)//수정 하려고 할 시
            {
                SetOriginTeacherValue(false);
                ValidBoxes();
                teacherInfoModifiyButton.Text = "수정완료";
                teacherInfoModifiyState = true;
            }
            else //true
            {//수정 완료
                if (MessageBox.Show("수정하시겠습니까?", "수정확인", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    ModificationTeacherInformation();
                    SetOriginTeacherValue(true);
                }
                
                teacherInfoModifiyState = false;
                UnvalidBoxes();
                teacherInfoModifiyButton.Text = "내정보수정";
            }
        }

        private void allInformationButton_Click(object sender, EventArgs e)//모든 정보를 보기위한 폼 불러오기
        {
            listForm = new Team.ListForm(cont);
            listForm.ShowDialog();
        }

        private void teacherSetYearCombo_SelectedIndexChanged(object sender, EventArgs e) // 생년월일 중 연 콤보박스 선택시
        {
            teacherSetMonthCombo.Text = "01"; // 월 콤보박스 설정
        }

        private void teacherSetMonthCombo_SelectedIndexChanged(object sender, EventArgs e) // 생년월일 중 월 콤보박스 변경시
        {
            // 다른 폼과 같으므로 생략하겠습니다.
            string[] day;
            int year, month;
            year = Int32.Parse(teacherSetYearCombo.SelectedItem.ToString());
            month = Int32.Parse(teacherSetMonthCombo.SelectedItem.ToString());

            day = cont.GetDayComboBoxItems(year, month);

            teacherSetDayCombo.Items.AddRange(day);
            teacherSetDayCombo.Text = "01";
        }

        //전화번호 텍스트 박스에 숫자만 입력 가능 및 전화번호 입력 완료 시 다음 박스로 포커스 이동
        private void teacherSetFirstPhoneNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(teacherSetFirstPhoneNumberTextBox.Text, out tmpSecondText);
            if (teacherInfoModifiyState && teacherSetFirstPhoneNumberTextBox.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    teacherSetFirstPhoneNumberTextBox.Text = "";
                }
                if (teacherSetFirstPhoneNumberTextBox.TextLength == 3)
                    this.ActiveControl = teacherSetSecondPhoneNumberTextBox;
            }
        }

        private void teacherSetSecondPhoneNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(teacherSetSecondPhoneNumberTextBox.Text, out tmpSecondText);
            if (teacherInfoModifiyState && teacherSetSecondPhoneNumberTextBox.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    teacherSetSecondPhoneNumberTextBox.Text = "";
                }
                if (teacherSetSecondPhoneNumberTextBox.TextLength == 4)
                    this.ActiveControl = teacherSetThirdPhoneNumberTextBox;
            }
        }

        private void teacherSetThirdPhoneNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(teacherSetThirdPhoneNumberTextBox.Text, out tmpSecondText);
            if (teacherInfoModifiyState && teacherSetThirdPhoneNumberTextBox.Text != "")
            {
                if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
                {
                    MessageBox.Show("숫자만 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    teacherSetThirdPhoneNumberTextBox.Text = "";
                }
                if (teacherSetThirdPhoneNumberTextBox.TextLength == 4)
                    this.ActiveControl = teacherInfoModifiyButton;
            }
        }

        private void imageRegisterButton_Click(object sender, EventArgs e)//이미지 버튼 수정하기 위한 함수
        {
            string imageFileRoute = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";//기본 경로 설정
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)//확인 시 이미지 저장
            {
                imageFileRoute = openFileDialog.FileName;
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            teacherPictureBox.Image = Bitmap.FromFile(imageFileRoute);
            teacherPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
