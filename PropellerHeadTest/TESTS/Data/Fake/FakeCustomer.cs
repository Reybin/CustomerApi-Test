using Bogus;
using DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTS.Data.Fake
{
    public class FakeCustomer : Customer
    {

        private static int firsId = SharedData.CUSTOMER_ID_FIRST;
        private static int minNotes = SharedData.MIN_NOTES;
        public static Faker<Customer> Data { get; } = new Faker<Customer>()
            .RuleFor(c => c.Id, f => firsId++)
            .RuleFor(c => c.Name, f => f.Name.FindName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Address, f => f.Address.StreetAddress());
            //.RuleFor(c => c.Notes, f => FakeNote.Data.Generate(new Random(minNotes).Next()));
            
    }
}
