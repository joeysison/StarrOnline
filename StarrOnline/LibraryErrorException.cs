using System;


namespace StarrOnline    
{
    public class LibraryErrorException : Exception
    {
        public LibraryErrorException()
        {
        }

        public LibraryErrorException(string message)
            : base(message)
        {
        }

        public LibraryErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}