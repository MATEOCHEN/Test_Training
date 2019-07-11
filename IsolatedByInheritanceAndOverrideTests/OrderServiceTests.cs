using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IsolatedByInheritanceAndOverride.Tests
{
    public class FakeBookDao : BookDao
    {
        public override void Insert(Order order)
        {
            Count++;
        }
    }

    public class MokeOrderService : OrderService
    {
        private readonly FakeBookDao _fakeBookDao = new FakeBookDao();

        public override BookDao GetBookDao()
        {
            return _fakeBookDao;
        }

        protected override List<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    ProductName = "product1",
                    Type = "Book",
                    Price =100,
                    CustomerName = "Kyo"
                },
                new Order
                {
                ProductName = "product2",
                Type = "DVD",
                Price =200,
                CustomerName = "Kyo"
                },
                new Order
                {
                    ProductName = "product3",
                    Type = "Book",
                    Price =300,
                    CustomerName = "Joey"
                }
            };
        }
    }

    [TestClass()]
    public class OrderServiceTests
    {
        /// <summary>
        /// Tests the synchronize book orders 3 orders only 2 book order.
        /// ProductName, Type, Price, CustomerName
        /// 商品1,        Book,  100, Kyo
        /// 商品2,        DVD,   200, Kyo
        /// 商品3,        Book,  300, Joey
        /// </summary>
        [TestMethod()]
        public void Test_SyncBookOrders_3_Orders_Only_2_book_order()
        {
            //Try to isolate dependency to unit test

            //var target = new OrderService();
            //target.SyncBookOrders();
            //verify bookDao.Insert() twice

            var orderService = new MokeOrderService();
            orderService.SyncBookOrders();

            Assert.AreEqual(2, orderService.GetBookDao().Count);
        }
    }
}