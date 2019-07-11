using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IsolatedByInheritanceAndOverride
{
    /// <summary>
    /// not complete yet
    /// </summary>
    public class BookDao
    {
        public int Count { get; set; }

        public virtual void Insert(Order order)
        {
            // directly depend on some web service
            //var client = new HttpClient();
            //var response = client.PostAsync<Order>("http://api.joey.io/Order", order, new JsonMediaTypeFormatter()).Result;
            //response.EnsureSuccessStatusCode();
            throw new NotImplementedException();
        }
    }

    public class Order
    {
        public string CustomerName { get; set; }
        public int Price { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
    }

    public class OrderService
    {
        /// <summary>
        /// The file path
        /// </summary>
        private string _filePath = @"C:\temp\testOrders.csv";

        public virtual BookDao GetBookDao()
        {
            var bookDao = new BookDao();
            return bookDao;
        }

        public virtual void SyncBookOrders()
        {
            var orders = GetOrders();

            // only get orders of book
            var ordersOfBook = orders.Where(x => x.Type == "Book");

            var bookDao = GetBookDao();
            foreach (var order in ordersOfBook)
            {
                bookDao.Insert(order);
            }
        }

        protected virtual List<Order> GetOrders() //fake
        {
            // parse csv file to get orders
            var result = new List<Order>();

            // directly depend on File I/O
            using (StreamReader sr = new StreamReader(this._filePath, Encoding.UTF8))
            {
                int rowCount = 0;

                while (sr.Peek() > -1)
                {
                    rowCount++;

                    var content = sr.ReadLine();

                    // Skip CSV header line
                    if (rowCount > 1)
                    {
                        string[] line = content.Trim().Split(',');

                        result.Add(this.Mapping(line));
                    }
                }
            }

            return result;
        }

        private Order Mapping(string[] line)
        {
            var result = new Order
            {
                ProductName = line[0],
                Type = line[1],
                Price = Convert.ToInt32(line[2]),
                CustomerName = line[3]
            };

            return result;
        }
    }
}