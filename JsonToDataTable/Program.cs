using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;

namespace JsonToDataTable
{
    public class Program
    {
        static void Main(string[] args)
        {
            int id = 1;
            #region Students
            List<Student> students = new List<Student>
            {
                new Student
                {
                    Name = "TurtleMan",
                    StudentId = 12345,
                    Birthday = new DateTime(2000, 12, 25)
                },

                new Student
                {
                    Name = "MuffinMan",
                    StudentId = 54321,
                    Birthday = new DateTime(1999, 02, 14)
                },

                new Student
                {
                    Name = "TurkeyMan",
                    StudentId = 66666,
                    Birthday = new DateTime(1986, 10, 31)
                }
            };
            #endregion

            List<ClassRoom<JObject>> classRoom = new List<ClassRoom<JObject>>();

            foreach (var student in students)
            {
                var j = new ClassRoom<JObject>
                {
                    ClassId = id++,
                    Student = JObject.FromObject(student)
                };
                classRoom.Add(j);
            }

            foreach (var room in classRoom)
            {
                Console.WriteLine(room.ClassId);
                var jsonObject = JsonConvert.DeserializeObject<JObject>(room.Student.ToString());
                jsonObject.TryGetValue("Name", out JToken value);
                Console.WriteLine(value);
                jsonObject.TryGetValue("StudentId", out value);
                Console.WriteLine(value);
                jsonObject.TryGetValue("Birthday", out value);
                Console.WriteLine(value);                
            }

            Console.WriteLine("---------------------");

            foreach (var room in classRoom)
            {
                Console.WriteLine(room.ClassId);
                var jsonObject = JsonConvert.DeserializeObject<JObject>(room.Student.ToString());
                foreach (var j in jsonObject)
                {
                    Console.WriteLine(j.Value.ToString());
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("---------------------");

            foreach (var room in classRoom)
            {
                Console.WriteLine(JObject.Parse(room.Student.ToString())["Name"].ToString());
                Console.WriteLine(JObject.Parse(room.Student.ToString())["StudentId"].ToString());
                Console.WriteLine(JObject.Parse(room.Student.ToString())["Birthday"].ToString());
            }

            #region JObject
            ////This Does NOT Work
            //var jsonObject = JObject.FromObject(students);
            //var temp = JsonConvert.SerializeObject(jsonObject);
            ////DataTable temp2 = JsonConvert.DeserializeObject<DataTable>(temp);

            ////foreach (var row in temp2.AsEnumerable())
            ////{
            ////    Console.WriteLine(row.Field<string>("Name"));
            ////    Console.WriteLine(row.Field<long>("StudentId"));
            ////    Console.WriteLine(row.Field<DateTime?>("Birthday"));
            ////}
            #endregion

            #region StringJson
            Console.WriteLine("---------------------");
            string json = JsonConvert.SerializeObject(students);
            DataTable jsonDateTabe = JsonConvert.DeserializeObject<DataTable>(json);

            foreach (var row in jsonDateTabe.AsEnumerable())
            {
                Console.WriteLine(row.Field<string>("Name"));
                Console.WriteLine(row.Field<long>("StudentId"));
                Console.WriteLine(row.Field<DateTime?>("Birthday"));
            }
            #endregion

            Console.ReadLine();
        }
    }
}
