using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExamples.Example2
{
    public class A
    {
        public int Id { get; set; }

        public B B { get; set; }

        public A()
        {
            B = new B();
        }
    }

    public class B
    {
        public int Id { get; set; }

        public B()
        {

        }
    }

}
