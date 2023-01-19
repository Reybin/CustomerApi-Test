using DATA.DTO;
using DATA.Entities;
using DATA.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TESTS.Data;
using TESTS.Data.Fake;
using Xunit;

namespace TESTS.Extensions
{
    
    public class TestExtensions
    {
        [Fact]
        public void TestFilterExtension_NoResult()
        {
            var customers = FakeCustomer.Data.Generate(20).ToArray();
            var conditions = new List<PaginationConditionDto>();

            conditions.Add(new PaginationConditionDto
            {
                Field = "Name",
                Method = "equal",
                Value = customers[0].Name
            });

            conditions.Add(new PaginationConditionDto
            {
                Field = "Name",
                Method = "equal",
                Value = customers[1].Name
            });

            var pagination = PaginatorExtension.GetPagination<Customer>(customers.AsQueryable(), conditions, null, false, 1, 10).Result;

            Assert.NotNull(customers);
            Assert.NotNull(pagination);           
            Assert.Equal(0, pagination.TotalItems);
            Assert.Equal(0, pagination.TotalPages);
        }

        [Fact]
        public void TestFilterExtension_Result()
        {
            var customers = FakeCustomer.Data.Generate(20).ToArray();
            var conditions = new List<PaginationConditionDto>();

            conditions.Add(new PaginationConditionDto
            {
                Field = "Name",
                Method = "equal",
                Value = customers[0].Name
            });           

            var pagination = PaginatorExtension.GetPagination<Customer>(customers.AsQueryable(), conditions, null, false, 1, 10).Result;

            Assert.NotNull(customers);
            Assert.NotNull(pagination);
            Assert.Equal(1, pagination.TotalItems);
            Assert.Equal(1, pagination.TotalPages);
        }

        [Fact]
        public void TestExtension_Pages()
        {
            var customers = FakeCustomer.Data.Generate(25).ToArray();
            var conditions = new List<PaginationConditionDto>();           

            var pagination = PaginatorExtension.GetPagination<Customer>(customers.AsQueryable(), conditions, null, false, 1, 5).Result;

            Assert.NotNull(customers);
            Assert.NotNull(pagination);
            Assert.Equal(25, pagination.TotalItems);
            Assert.Equal(5, pagination.TotalPages);
        }

    }
}
