using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Castle.Core.Logging;

namespace BaseClassCoupling
{
    public interface IRecorder
    {
        void DebugInfoRecord(string value);
    }

    public static class DebugHelper
    {
        public static void Info(string message)
        {
            //you can't modified this function
            throw new NotImplementedException();
        }
    }

    public static class SalaryRepo
    {
        internal static decimal Get(int id)
        {
            //you can't modified this function
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class BaseClassCouplingTests
    {
        [TestMethod]
        public void calculate_half_year_employee_bonus()
        {
            //if my monthly salary is 1200, working year is 0.5, my bonus should be ㄈ600
            var lessThanOneYearEmployee = new FakeLessThanOneYear()
            {
                Id = 91,
                //Console.WriteLine("your StartDate should be :{0}", DateTime.Today.AddDays(365/2*-1));
                Today = new DateTime(2018, 1, 27),
                StartWorkingDate = new DateTime(2017, 7, 29)
            };

            var actual = lessThanOneYearEmployee.GetYearlyBonus();
            Assert.AreEqual(600, actual);
        }
    }

    public abstract class Employee
    {
        protected IRecorder Recorder;

        protected Employee()
        {
            Recorder = new InfoRecorder();
        }

        public int Id { get; set; }

        public IRecorder Logger
        {
            get => Recorder;
            set => Recorder = value;
        }

        public DateTime StartWorkingDate { get; set; }

        public DateTime Today { get; set; }

        public abstract decimal GetYearlyBonus();

        protected virtual decimal GetMonthlySalary()
        {
            Recorder.DebugInfoRecord($"query monthly salary id:{Id}");
            return SalaryRepo.Get(this.Id);
        }
    }

    public class FakeLessThanOneYear : LessThanOneYearEmployee
    {
        public FakeLessThanOneYear()
        {
            Logger = new FakeRecorder();
        }

        protected override decimal GetMonthlySalary()
        {
            return 1200;
        }
    }

    public class FakeRecorder : IRecorder
    {
        public void DebugInfoRecord(string value)
        {
        }
    }

    public class InfoRecorder : IRecorder
    {
        public void DebugInfoRecord(string value)
        {
            DebugHelper.Info(value);
        }
    }

    public class LessThanOneYearEmployee : Employee
    {
        public override decimal GetYearlyBonus()
        {
            Logger.DebugInfoRecord("--get yearly bonus--");
            var salary = GetMonthlySalary();
            Logger.DebugInfoRecord($"id:{Id}, his monthly salary is:{salary}");
            return Convert.ToDecimal(this.WorkingYear()) * salary;
        }

        private double WorkingYear()
        {
            Logger.DebugInfoRecord("--get working year--");
            var year = (Today - StartWorkingDate).TotalDays / 365;
            return year > 1 ? 1 : Math.Round(year, 2);
        }
    }
}