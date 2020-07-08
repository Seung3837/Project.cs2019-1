using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team.Model
{
    public class Post
    {
        public string DateTime { get; set; }//작성 날짜 저장
        public string Poster { get; set; }//작성자 저장
        public string Title { get; set; }//제목 저장
        public string Content { get; set; }//내용 저장
        public Post(string dt, string pt,string ti, string ct)//공지사항 정보를 저장하는 생성자
        {
            DateTime = dt;
            Poster = pt;
            Title = ti;
            Content = ct;
        }
    }
}
