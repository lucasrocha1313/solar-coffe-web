using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SolarCoffe.Data;
using SolarCoffe.Data.Models;
using SolarCoffe.Services.Customer.Services;
using Xunit;

namespace SolarCoffe.Test
{
    //Tests created only for practice
    public class CustomerServiceTest
    {
        DbContextOptions<SolarDbContext> options = new DbContextOptionsBuilder<SolarDbContext>()
                .UseInMemoryDatabase("gets_all").Options;
        
        [Fact]
        public void GetAllCustomers_GivenTheyExist()
        {
            using var context = new SolarDbContext(options);
            var sut =  new CustomerService(context);

            var allCustomers = sut.GetAllCustomers();
            context.Customers.RemoveRange(allCustomers);
            sut.CreateCustomer(MockCustomer());
            sut.CreateCustomer(MockCustomer());

            allCustomers = sut.GetAllCustomers();

            allCustomers.Count().Should().Be(2);
        }

        [Fact]
        public void CreateCustomer_GivenNewCustomerObject()
        {
            using var context = new SolarDbContext(options);
            var sut = new CustomerService(context);

            sut.CreateCustomer(MockCustomer(123));

            var customers = sut.GetAllCustomers();
            customers.Single().Id.Should().Be(123);
        }

        [Fact]
        public void DeleteCustomer_GivenId()
        {
            using var context = new SolarDbContext(options);
            var sut = new CustomerService(context);
            var id = 123;
            var allCustomers = sut.GetAllCustomers();
            context.Customers.RemoveRange(allCustomers);
            sut.CreateCustomer(MockCustomer(id));
            sut.CreateCustomer(MockCustomer());
            
            var customers = sut.GetAllCustomers();
            customers.Where(c => c.Id == id).Count().Should().Be(1);
            var countCustomers = customers.Count();
            sut.DeleteCustomer(id);
            customers = sut.GetAllCustomers();
            customers.Count().Should().Be(countCustomers - 1);
            customers.Where(c => c.Id == id).Count().Should().Be(0);
        }

        [Fact]
        public void OrderByLast_WhenGetAllCustomerInvoked()
        {
            //Arange
            var customer1 = MockCustomer(1);
            customer1.LastName = "Xtom";
            var customer2 = MockCustomer(2);
            customer2.LastName = "Btom";
            var customer3 = MockCustomer(3);
            customer3.LastName = "Ztom";

            var data = new List<Customer>() {customer1, customer2, customer3}.AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(data.Provider);
            
            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            var mockContext = new Mock<SolarDbContext>();

            mockContext.Setup(c => c.Customers)
                .Returns(mockSet.Object);

            //Act

            var sut = new CustomerService(mockContext.Object);
            var customers = sut.GetAllCustomers();

            //Asset
            customers.Count().Should().Be(3);
            customers[0].Id.Should().Be(2);
            customers[1].Id.Should().Be(1);
            customers[2].Id.Should().Be(3);
        }

        private Customer MockCustomer(int? id = null) {
        Random rnd = new Random();
        return new Customer
            {
                Id = id ?? rnd.Next(1, 9999),
                FirstName = $"Test{rnd.Next(1, 100)}",
                LastName = $"Test{rnd.Next(1, 100)}",
                PrimaryAddress =  new CustomerAddress
                    {
                        Id = rnd.Next(1, 9999),
                        AddressLine1 = "",
                        AddressLine2 = "",
                        City = "",
                        Country = "",
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                        PostalCode = "",
                        State = ""
                    }
            };
    }
    }    
}