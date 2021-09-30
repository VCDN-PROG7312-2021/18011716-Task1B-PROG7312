using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PROG3B_1B_Y3S2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PROG3B_1B_Y3S2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        List<Book> Books = new List<Book>()
        {
            new Book("https://firebasestorage.googleapis.com/v0/b/prog-task1-y3s2.appspot.com/o/book1.jpeg?alt=media&token=59a877cc-3dfb-4c61-9b3b-e7691ffe7f78", "001.123", "Guide to Computer Science"),
            new Book("https://firebasestorage.googleapis.com/v0/b/prog-task1-y3s2.appspot.com/o/book2.png?alt=media&token=37edd1c8-b02a-407e-8265-69ed47a12367", "200.234", "Computer Science: Unleashed"),
            new Book("https://firebasestorage.googleapis.com/v0/b/prog-task1-y3s2.appspot.com/o/book3.png?alt=media&token=0fb1fe91-2dc1-4163-a4ab-ee5325f3402b", "150.345", "Computer Science: Distilled"),
            new Book("https://firebasestorage.googleapis.com/v0/b/prog-task1-y3s2.appspot.com/o/book4.png?alt=media&token=db555227-45f4-45f5-8a64-1c5231223a2b", "300.567", "Step-by-Step Spanish"),
            new Book("https://firebasestorage.googleapis.com/v0/b/prog-task1-y3s2.appspot.com/o/book9.png?alt=media&token=bfa178d7-557d-4bc9-881e-81f80d2b9bac", "400.456", "Learn  Java"),
            new Book("https://firebasestorage.googleapis.com/v0/b/prog-task1-y3s2.appspot.com/o/book10.png?alt=media&token=cf4c9587-8fe9-43bb-80f3-2a74533ed538", "500.678", "Learn JavaScript"),
            new Book("https://firebasestorage.googleapis.com/v0/b/prog-task1-y3s2.appspot.com/o/book8.png?alt=media&token=0523455f-3be1-4b87-8b04-11fb7ff18964", "600.789", "Learn Python"),
            new Book("https://firebasestorage.googleapis.com/v0/b/prog-task1-y3s2.appspot.com/o/book7.png?alt=media&token=250a87a6-4526-45ba-a782-510e412d7536", "700.891", "Learn C"),
            new Book("https://firebasestorage.googleapis.com/v0/b/prog-task1-y3s2.appspot.com/o/book5.png?alt=media&token=4e198ea4-c1cf-4485-a1a1-5798496dfc32", "800.981", "Mughul Empire"),
            new Book("https://firebasestorage.googleapis.com/v0/b/prog-task1-y3s2.appspot.com/o/book6.png?alt=media&token=b8dc6a72-2ca5-41a4-b0b4-5b393708e106", "900.999", "Marco Polo"),
        };


        // GET api/values
        [HttpGet]
        [Route("api/values")]
        public List<Book> Get()
        {
            //Simply serializing all data before the client can ask for it.
            List<object> jsonBooks = new List<object>();

            foreach(Book i in Books)
            {
                Random random = new Random();
                i.Ddc = random.Next(100, 999).ToString();
            }

            foreach (Book i in Books)
            { 
                jsonBooks.Add(JsonConvert.SerializeObject(i, Settings));
            }
            return Books;
        }

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        // GET api/values/5
        [HttpGet]
        [Route("api/values/{id}")]
        public Book Get(int id)
        {
            //return a particular object
            return Books[id];
        }


        // POST api/values
        [HttpPost]
        [Route("api/values/save")]
        public bool Post([FromBody] List<string> ddcs)
        {
            //when the user clicks the 'check' icon, perform a test on the order of the numbers
            return HandleAlternateMethod(ddcs);
        }

        private static bool HandleFirstColumn(List<string> ddcs)
        {
            int[] sortedColumnOne = new int[10];
            int[] originalUserColumn1 = new int[10];

            int counter1 = 0;
            //slicing numbers
            foreach (string i in ddcs)
            {
                sortedColumnOne[counter1] = Convert.ToInt32(i.Substring(0, 3));
                originalUserColumn1[counter1] = Convert.ToInt32(i.Substring(0, 3));
                counter1++;
            }

            //check non-decimal numbers
            int temp;
            for (int j = 0; j <= sortedColumnOne.Length - 2; j++)
            {
                for (int i = 0; i <= sortedColumnOne.Length - 2; i++)
                {
                    if (sortedColumnOne[i] > sortedColumnOne[i + 1])
                    {
                        temp = sortedColumnOne[i + 1];
                        sortedColumnOne[i + 1] = sortedColumnOne[i];
                        sortedColumnOne[i] = temp;
                    }
                }
            }

            int[] orderedInAscColumOne = new int[10];
            int counter2 = 9;
            for (int i = 0; i < 10; i++)
            {
                orderedInAscColumOne[i] = sortedColumnOne[counter2];
                counter2--;
            }

            bool pass = true;
            for (int i = 0; i < 10; i++)
            {
                if (originalUserColumn1[i] != sortedColumnOne[i])
                {
                    pass = false;
                    break;
                }
                pass = true;
            }

            return pass;
        }

        private static bool HandleSecondColumn(List<string> ddcs)
        {
            int[] sortedColumnOne = new int[10];
            int[] originalUserColumn1 = new int[10];

            int counter1 = 0;
            //slicing numbers
            foreach (string i in ddcs)
            {
                int starting = i.IndexOf(".");
                sortedColumnOne[counter1] = Convert.ToInt32(i.Substring(starting+1));
                originalUserColumn1[counter1] = Convert.ToInt32(i.Substring(starting+1));
                counter1++;
            }

            //check non-decimal numbers
            int temp;
            for (int j = 0; j <= sortedColumnOne.Length - 2; j++)
            {
                for (int i = 0; i <= sortedColumnOne.Length - 2; i++)
                {
                    if (sortedColumnOne[i] > sortedColumnOne[i + 1])
                    {
                        temp = sortedColumnOne[i + 1];
                        sortedColumnOne[i + 1] = sortedColumnOne[i];
                        sortedColumnOne[i] = temp;
                    }
                }
            }

            int[] orderedInAscColumOne = new int[10];
            int counter2 = 9;
            for (int i = 0; i < 10; i++)
            {
                orderedInAscColumOne[i] = sortedColumnOne[counter2];
                counter2--;
            }

            bool pass = true;
            for (int i = 0; i < 10; i++)
            {
                if (originalUserColumn1[i] != sortedColumnOne[i])
                {
                    pass = false;
                    break;
                }
                pass = true;
            }

            return pass;

            //if (pass)
            //{
            //    return "First Column Done!";
            //}
            //else
            //{
            //    return "First Column Error!";
            //}
        }

        private static bool HandleAlternateMethod(List<string> ddcs)
        {
            int[] sortedColumnOne = new int[10];
            int[] originalUserColumn1 = new int[10];

            int counter1 = 0;
            //slicing numbers
            foreach (string i in ddcs)
            {
                int startPos1 = 0;
                int startPos2 = i.IndexOf(".");

                sortedColumnOne[counter1] = Convert.ToInt32(i.Substring(startPos1, 3) + i.Substring(startPos2+1));
                originalUserColumn1[counter1] = Convert.ToInt32(i.Substring(startPos1, 3) + i.Substring(startPos2+1));
                counter1++;
            }

            //simple sorting
            int temp;
            for (int j = 0; j <= sortedColumnOne.Length - 2; j++)
            {
                for (int i = 0; i <= sortedColumnOne.Length - 2; i++)
                {
                    if (sortedColumnOne[i] > sortedColumnOne[i + 1])
                    {
                        temp = sortedColumnOne[i + 1];
                        sortedColumnOne[i + 1] = sortedColumnOne[i];
                        sortedColumnOne[i] = temp;
                    }
                }
            }

            //determinig the player win state
            bool pass = true;
            for (int i = 0; i < 10; i++)
            {
                if (originalUserColumn1[i] != sortedColumnOne[i])
                {
                    pass = false;
                    break;
                }
                pass = true;
            }

            return pass;
        }

























        // POST api/values
        [HttpPost]
        [Route("api/values")]
        public string Posta([FromBody]object value)
        {
            string temp = "Value received from POST -> " + value;
            return temp;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
