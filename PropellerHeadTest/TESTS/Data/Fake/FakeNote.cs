using Bogus;
using DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTS.Data.Fake
{
    public class FakeNote : Note
    {
        private static int customerId;
        public FakeNote(int id)
        {
            customerId = id;
        }
        private static int id= 1;
        public static Faker<Note> Data { get; } = new Faker<Note>()
            .RuleFor(n => n.Content, f => f.Random.String(10))
            .RuleFor(n => n.Id, f => id++)
            .RuleFor(n => n.CustomerId, f => customerId);

    }
}
