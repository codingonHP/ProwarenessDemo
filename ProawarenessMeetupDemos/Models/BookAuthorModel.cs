using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProawarenessMeetupDemos.Models
{
    public class BookAuthorModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public byte[] BookImageBytes { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string ImageBase64 { get; set; }
    }
}