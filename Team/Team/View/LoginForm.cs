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
    public partial class LoginForm : Form
    {
        Controller cont;
        Student loginStudent;
        StudentForm studentForm;
        Teacher loginTeacher;
        TeacherForm teacherForm;
        bool authority;//authority true : 선생님 false : 학생
        public LoginForm()
        {
            InitializeComponent();
        }
        public void SetAuthority(Controller cont, bool authority)//선생 또는 학생에 따른 권한 받기
        {
            this.cont = cont;
            this.authority = authority;
            //권한에 맞는 텍스트 저장
            if(this.authority == true)
            {
                firstIdLoginLabel.Text = "te";
            }
            else if(this.authority == false)
            {
                firstIdLoginLabel.Text = "st";
            }
            secondIdErrorMessageLabel.Text = "";
        }

        private void idLoginCompleteButton_Click(object sender, EventArgs e)
        {
            if(authority == true)//선생일 시
            {
                loginTeacher = cont.FindOnlyTeacher(firstIdLoginLabel.Text + secondIdLoginTextBox.Text);//해당 선생님이 존재하는지 검색
                if (loginTeacher != null)
                {
                    secondIdLoginTextBox.Text = "";
                    this.Visible = false;
                    teacherForm = new TeacherForm(loginTeacher, cont);//선생폼 보여주기
                    teacherForm.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("선생 ID가 존재하지 않습니다.", "ID 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);//ID오류시 메시지 박스
                }
            }
            else if(authority == false)//학생일 시
            {
                loginStudent = cont.FindOnlyStudent(firstIdLoginLabel.Text + secondIdLoginTextBox.Text);//해당 학생이 존재하는지 검색
                if (loginStudent != null)
                {
                    secondIdLoginTextBox.Text = "";
                    this.Visible = false;
                    studentForm = new StudentForm(loginStudent, cont);
                    studentForm.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("학생 ID가 존재하지 않습니다.", "ID 입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);//ID오류시 메시지 박스
                }
            }
        }

        private void idLoginCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void secondIdLoginTextBox_TextChanged(object sender, EventArgs e)//숫자가 아닌 것을 입력하면 숫자를 입력해달라는 메세지 출력
        {
            int tmpSecondText;
            bool tmpTryParseSecondId = int.TryParse(secondIdLoginTextBox.Text, out tmpSecondText);
            if ((tmpTryParseSecondId == true && tmpSecondText < 0) || tmpTryParseSecondId == false)
            {
                secondIdErrorMessageLabel.Text = "숫자만 입력해주세요!";
            }
            else
            {
                secondIdErrorMessageLabel.Text = "";
            }
        }
    }
}
