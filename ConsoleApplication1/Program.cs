using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "test";

            object obj = str;

            IEnumerable<string> strings = new List<string>();

            IEnumerable<object> objects = strings;

            DoUsing<int>(SetObject);
        }
        
        static int SetObject(object o)
        {
            return 5;
        }

       
        public static TResult DoUsing<TResult>(Func<object, TResult> action)
        {
           object o = new object();
           return action(o);
        }
    }



    interface IModel { }

    interface IRepo<T> where T : IModel { }

    class MyModel : IModel { }

    class Repo : IRepo<MyModel> { }
   


}
