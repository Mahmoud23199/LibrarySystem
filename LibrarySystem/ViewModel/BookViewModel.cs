namespace LibrarySystem.ViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public int NumberOfCopies { get; set; }
        public List<BookCopyViewModel> BookCopies { get; set; }
    }
}
