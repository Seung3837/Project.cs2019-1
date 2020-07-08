using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Team.View;

namespace Team
{
    public partial class Form1 : Form
    {
        private const bool boolForStudent = false;
        private const bool boolForTeacher = true;
        private string basicRoute = @"C:\Team8ProjectForder";//기본 경로
        Controller cont; // 컨트롤러
        ListForm listForm; // 리스트 폼
        LoginForm loginForm; // 로그인 폼
        View.LectureForm lectureForm; // 강의 목록 폼
        View.NoticeForm noticeForm;

        public Form1(Controller cont)
        {
            InitializeComponent();
            this.cont = cont; // 컨트롤러 받음
            listForm = new Team.ListForm(cont); // 리스트 폼 생성

            string defaultImageRoute = basicRoute + @"\TeamProjectImageSaveForder\MainPageImage\backtest.png";//배경 이미지 불러오기
            System.IO.FileInfo studentImageFileInfo = new System.IO.FileInfo(defaultImageRoute);//파일 정보저장
            this.BackgroundImage = Bitmap.FromFile(defaultImageRoute);//파일 배경 설정
            this.BackgroundImageLayout = ImageLayout.Stretch;//크기 맞게 적용

            buttonSet();

            loginForm = new LoginForm(); // 로그인 폼 생성
        }

        private void startStudentButton_Click(object sender, EventArgs e) // 학생 버튼 클릭
        {
            loginForm.SetAuthority(cont, boolForStudent); 
            loginForm.ShowDialog(); 
        }

        private void startTeacherButton_Click(object sender, EventArgs e) // 선생 버튼 클릭
        {
            loginForm.SetAuthority(cont, boolForTeacher);
            loginForm.ShowDialog();
        }

        private void buttonSet()//버튼 이미지 저장
        {
            // 이미지 불러오기
            string startTeacherImageRoute = basicRoute + @"\TeamProjectImageSaveForder\MainPageImage\startTeacher.png";//이미지 불러오기
            string startStudentImageRoute = basicRoute + @"\TeamProjectImageSaveForder\MainPageImage\startStudent.png";
            string startLectureImageRoute = basicRoute + @"\TeamProjectImageSaveForder\MainPageImage\startLecture.png";
            string startPostImageRoute = basicRoute + @"\TeamProjectImageSaveForder\MainPageImage\startPost.png";
            // tap 못하게 설정
            startLectureButton.TabStop = false;
            startTeacherButton.TabStop = false;
            startStudentButton.TabStop = false;
            startPostButton.TabStop = false;
            //각 파일에 관한 정보 저장
            System.IO.FileInfo startLectureImageFileInfo = new System.IO.FileInfo(startLectureImageRoute);
            System.IO.FileInfo startTeacherImageFileInfo = new System.IO.FileInfo(startTeacherImageRoute);
            System.IO.FileInfo startStudentImageFileInfo = new System.IO.FileInfo(startStudentImageRoute);
            System.IO.FileInfo startPostImageFileInfo = new System.IO.FileInfo(startPostImageRoute);
            //파일이 존재 할 시
            if (startLectureImageFileInfo.Exists)
            {
                startLectureButton.Text = "";
                startLectureButton.FlatStyle = FlatStyle.Flat;//버튼 스타일 설정
                startLectureButton.FlatAppearance.BorderSize = 2;
                startLectureButton.BackgroundImage = Bitmap.FromFile(startLectureImageRoute);//이미지 버튼에 입히기
                startLectureButton.BackgroundImageLayout = ImageLayout.Stretch;//이미지 크기 맞게 적용
            }
            if (startTeacherImageFileInfo.Exists)
            {
                startTeacherButton.Text = "";
                startTeacherButton.FlatStyle = FlatStyle.Flat;
                startTeacherButton.FlatAppearance.BorderSize = 2;
                startTeacherButton.BackgroundImage = Bitmap.FromFile(startTeacherImageRoute);
                startTeacherButton.BackgroundImageLayout = ImageLayout.Stretch;
            }
            if (startStudentImageFileInfo.Exists)
            {
                startStudentButton.Text = "";
                startStudentButton.FlatStyle = FlatStyle.Flat;
                startStudentButton.FlatAppearance.BorderSize = 2;
                startStudentButton.BackgroundImage = Bitmap.FromFile(startStudentImageRoute);
                startStudentButton.BackgroundImageLayout = ImageLayout.Stretch;
            }
            if (startPostImageFileInfo.Exists)
            {
                startPostButton.Text = "";
                startPostButton.FlatStyle = FlatStyle.Flat;
                startPostButton.FlatAppearance.BorderSize = 2;
                startPostButton.BackgroundImage = Bitmap.FromFile(startPostImageRoute);
                startPostButton.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void startLectureButton_Click(object sender, EventArgs e) // 강의 버튼 클릭
        {
            lectureForm = new View.LectureForm(cont); // 강의 폼 생성
            lectureForm.ShowDialog(); // 강의 폼 보여주기
        }

        private void startPostButton_Click(object sender, EventArgs e) //공지사항 버튼 클릭
        {
            noticeForm = new View.NoticeForm(cont); // 공지사항 폼 생성
            noticeForm.ShowDialog(); // 공지사항 폼 보여주기
        }
    }
}
