using System;
using System.Collections.Generic;

namespace ProductLibrary.Models
{
    public class ListNavigator<T>
    {
        private int currentPage;
        private readonly int pageSize;
        private readonly List<T> state;

        public ListNavigator(List<T> list, int pageSize = 5)
        {
            this.pageSize = pageSize;
            currentPage = 1;
            state = list;
        }

        private int lastPage
        {
            get
            {
                var val = state.Count / pageSize;

                if (state.Count % pageSize > 0)
                    //if there is a partial page at the end, that is the actual last page.
                    val++;

                return val;
            }
        }

        public bool HasPreviousPage => currentPage > 1;

        public bool HasNextPage => currentPage < lastPage;

        public Dictionary<int, T> GoForward()
        {
            if (currentPage + 1 > lastPage)
                throw new PageFaultException("Cannot navigate to the right of the last page in the list!");
            currentPage++;
            return GetWindow();
        }

        public Dictionary<int, T> GoBackward()
        {
            if (currentPage - 1 <= 0)
                throw new PageFaultException("Cannot navigate to the left of the first page in the list!");
            currentPage--;
            return GetWindow();
        }

        public Dictionary<int, T> GoToPage(int page)
        {
            if (page <= 0 || page > lastPage)
                throw new PageFaultException("Cannot navigate to a page outside of the bounds of the list!");
            currentPage = page;
            return GetWindow();
        }

        public Dictionary<int, T> GetCurrentPage()
        {
            return GoToPage(currentPage);
        }

        public Dictionary<int, T> GoToFirstPage()
        {
            currentPage = 1;
            return GetWindow();
        }

        public Dictionary<int, T> GoToLastPage()
        {
            currentPage = lastPage;
            return GetWindow();
        }

        private Dictionary<int, T> GetWindow()
        {
            //(currentPage*pageSize) + pageSize
            var window = new Dictionary<int, T>();
            for (var i = (currentPage - 1) * pageSize;
                 i < (currentPage - 1) * pageSize + pageSize && i < state.Count;
                 i++) window.Add(i + 1, state[i]);
            return window;
        }

        public void Navigate()
        {
            while (true)
            {
                Console.WriteLine($"PAGE {currentPage}");
                foreach (var p in GetCurrentPage())
                    Console.WriteLine(p.Value);
                Console.WriteLine("Press 1 to move to forwards.");
                Console.WriteLine("Press 2 to move to backwards.");
                Console.WriteLine("Press 3 to exit");
                int choice;
                while (!int.TryParse(Console.ReadLine() ?? "0", out choice))
                    Console.WriteLine("Please enter a valid choice.");
                if (choice == 1)
                {
                    if (HasNextPage)
                        GoForward();
                    else
                        Console.WriteLine("Already on the last page!");
                }

                if (choice == 2)
                {
                    if (HasPreviousPage)
                        GoBackward();
                    else
                        Console.WriteLine("Already on the first page!");
                }

                if (choice == 3)
                    return;
            }
        }
    }


    public class PageFaultException : Exception
    {
        public PageFaultException(string message) : base(message)
        {
        }
    }
}