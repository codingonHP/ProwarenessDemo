using ProawarenessMeetupDemos.DataAccessLayer;
using ProawarenessMeetupDemos.Models;
using System;
using System.Collections.Generic;

namespace ProawarenessMeetupDemos.Business
{
    public class BookAuthorBusiness
    {
        BookAuthorDataAccessLayer bookAuthorDataAccess = new BookAuthorDataAccessLayer();

        public bool SaveBookAuthorData(BookAuthorModel bookAuthorVm)
        {
            try
            {
                var fs = bookAuthorVm.Image.InputStream;
                using (fs)
                {
                    if (fs.CanSeek && fs.CanRead)
                    {
                        bookAuthorVm.BookImageBytes = new byte[fs.Length];
                        fs.Read(bookAuthorVm.BookImageBytes, 0, Convert.ToInt32(fs.Length));
                    }
                }

                bookAuthorDataAccess.SaveBookAuthorData(bookAuthorVm);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BookAuthorModel> GetAllBookAuthorModel ()
        {
            return bookAuthorDataAccess.GetAllBookAuthorRecords();
        }
    }
}