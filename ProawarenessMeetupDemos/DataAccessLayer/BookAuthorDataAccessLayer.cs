using ProawarenessMeetupDemos.Models;
using ProawarenessMeetupDemos.Database;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ProawarenessMeetupDemos.DataAccessLayer
{
    public class BookAuthorDataAccessLayer
    {
        public bool SaveBookAuthorData(BookAuthorModel bookAuthorVm)
        {
            using (var context = new ProwarenessContext())
            {
                context.BookAuthors.Add(new BookAuthor
                {
                    Author = bookAuthorVm.AuthorName,
                    Book = bookAuthorVm.BookName,
                    Image = bookAuthorVm.BookImageBytes
                });

                context.SaveChanges();
                return true;
            }

        }

        public List<BookAuthorModel> GetAllBookAuthorRecords()
        {
            using (var context = new ProwarenessContext())
            {
                return context.BookAuthors.ToList()
                                          .Select(s => new BookAuthorModel
                                          {
                                              AuthorName = s.Author,
                                              BookName = s.Book,
                                              ImageBase64 = Convert.ToBase64String(s.Image)
                                          })
                                          .ToList();
            }
        }


    }
}