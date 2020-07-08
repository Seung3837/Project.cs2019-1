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
    public partial class NoticeForm : Form // 공지사항을 보여주는 폼
    {
        public Controller cont; // 컨트롤러
        private Model.Post post; // 공지사항 정보 객체
        private List<Model.Post> posts; // 컨트롤러에서 넘겨 받을 전체 공지사항 리스트
        public NoticeForm(Controller cont)
        {
            InitializeComponent();
            this.cont = cont; // 컨트롤러 받음
            post = null;  // 없는 것으로 설정
            posts = cont.Posts; // 컨트롤러에서 전체 공지사항 받음
            SetTextBoxTool(); // 버튼 및 텍스트 박스 초기 설정
            GetNoticeView(); // 전체 공지 리스트를 보여주는 데이터 그리드 뷰 출력
        }

        private void SetTextBoxTool() // 버튼 및 텍스트 박스 초기 설정
        {
            // 신규 공지사항 등록 완료, 취소 버튼 보이지 않게 설정
            InsertOKButton.Visible = false;
            InsertNoButton.Visible = false;
            // 삽입, 삭제 버튼 보이게 설정
            InsertButton.Visible = true;
            DeleteButton.Visible = true;
            // 공지사항 제목, 작성자, 작성일자, 내용 텍스트 공백
            titleTextBox.Text = "";
            writerTextBox.Text = "";
            insertTimeTextBox.Text = "";
            contentTextBox.Text = "";
            // 공지사항 제목, 작성자, 작성일자, 내용 수정 불가
            titleTextBox.Enabled = false;
            writerTextBox.Enabled = false;
            insertTimeTextBox.Enabled = false;
            contentTextBox.Enabled = false;
        }

        private void SetTextBox() // 선택된 정보로 텍스트 박스 채움
        {
            // 데이터 그리드 뷰에서 선택된 포스트 정보를 텍스트 박스에 설정
            titleTextBox.Text = post.Title;
            writerTextBox.Text = post.Poster;
            insertTimeTextBox.Text = post.DateTime;
            contentTextBox.Text = post.Content.Replace("뷁", "\n");
            Console.WriteLine();
        }

        private void GetNoticeView() // 전체 공지 리스트를 보여주는 데이터 그리드 뷰 출력
        {
            string[] row; // 추가 할 행 정보 받을 string 배열
            noticeView.Rows.Clear();
            noticeView.ReadOnly = true; // 읽기 전용
            noticeView.AllowUserToAddRows = false; // 행 추가 불가
            noticeView.RowHeadersVisible = false; // 행 헤더 보이지 않게


            noticeView.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // 행 전체 클릭 가능, 셀 단위 클릭 안되게
            row = new string[3];
            noticeView.ColumnCount = 3; // 열 3개 설정
            noticeView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 중앙 정렬
            noticeView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // 중앙 정렬

            // 열 별 너비 설정
            noticeView.Columns[0].Width = 80;
            noticeView.Columns[1].Width = 120;
            noticeView.Columns[2].Width = 184;

            // 열 별 이름 설정
            noticeView.Columns[0].Name = "작성자";
            noticeView.Columns[1].Name = "공지 제목";
            noticeView.Columns[2].Name = "공지 날짜";

            // 행 추가
            for (int i = 0; i < posts.Count; i++)
            {
                row[0] = posts[i].Poster;
                row[1] = posts[i].Title;
                row[2] = posts[i].DateTime;

                noticeView.Rows.Add(row);

            }
        }

        private void noticeView_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // 공지사항 데이터 그리드 뷰에서 한 행을 더블 클릭하면
        {
            string[] str;
            str = new string[2];
            // 작성자와 작성일자를 기준으로 전체 공지사항에서 해당 포스터 객체 받아옴
            str[0] = noticeView.Rows[noticeView.CurrentCellAddress.Y].Cells[0].Value.ToString();
            str[1] = noticeView.Rows[noticeView.CurrentCellAddress.Y].Cells[2].Value.ToString();
            post = cont.FindPost(str);
            SetTextBoxTool();
            SetTextBox(); // 텍스트 박스 재구성
        }

        private void InsertOKButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("공지사항을 등록하시겠습니까?", "공지 사항 등록", MessageBoxButtons.YesNo) == DialogResult.Yes) // 공지 등록 여부 확인
            {
                string[] textRow;
                textRow = contentTextBox.Text.Split('\n');//개행문자 txt저장하기 위한 저장소
                string convertText = "";
                for (int i = 0; i < textRow.Length - 1; i++)
                {
                    convertText = convertText + textRow[i] + "뷁";//\n을 다른 문자로 변환해서 저장
                }
                convertText = convertText + textRow[textRow.Length - 1];
                Model.Post newPost = new Model.Post(DateTime.Now.ToString(), writerTextBox.Text, titleTextBox.Text, convertText); // 새로운 포스터 객체 생성
                cont.AddPost(newPost); // 컨트롤러에서 전체 공지사항 리스트에 삽입
                MessageBox.Show("공지가 등록 되었습니다.", "등록 완료"); // 메시지 박스
                post = newPost; // 현재 선택된 포스터로 지정
                SetTextBoxTool(); // 해당 내용 수정 불가능하게 박스들 설정
                SetTextBox();
                GetNoticeView(); // 공지사항 리스트 다시 데이터 그리드 뷰에 출력
            }
            else // 취소 시
                MessageBox.Show("취소되었습니다.", "등록 취소"); // 메시지 박스
        }

        private void InsertNoButton_Click(object sender, EventArgs e)
        {
            SetTextBoxTool(); // 텍스트 박스 공백 및 버튼들 재 설정
        }

        private void InsertButton_Click(object sender, EventArgs e) // 등록 버튼 클릭
        {
            // 완료, 취소 버튼 보이게 설정
            InsertOKButton.Visible = true;
            InsertNoButton.Visible = true;
            // 삽입, 삭제 버튼 안 보이게 설정
            InsertButton.Visible = false;
            DeleteButton.Visible = false;
            // 텍스트 박스 공백으로 초기화
            titleTextBox.Text = "";
            writerTextBox.Text = "";
            insertTimeTextBox.Text = "";
            contentTextBox.Text = "";
            // 텍스트 박스 사용할 수 있게 설정
            titleTextBox.Enabled = true;
            writerTextBox.Enabled = true;
            contentTextBox.Enabled = true;
        }

        private void DeleteButton_Click(object sender, EventArgs e) // 삭제 버튼 클릭시
        {
            if (post == null) // 선택된 포스트가 없다면
                MessageBox.Show("선택된 공지사항이 없습니다.", "삭제 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else // 있다면
            {
                cont.DeletePost(post);
                SetTextBoxTool();
                GetNoticeView();
            }
        }
    }
}