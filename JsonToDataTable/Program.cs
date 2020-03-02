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
            //Create a list of students
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
            
            //Create a list of ClassRooms
            List<ClassRoom<JObject>> classRooms = new List<ClassRoom<JObject>>();

            //Add the students to the class room and store the students as JObjects
            foreach (var student in students)
            {
                var j = new ClassRoom<JObject>
                {
                    ClassId = id++,
                    Student = JObject.FromObject(student)
                };
                classRooms.Add(j);
            }

            #region ParseClassRoom
            //Method 1 of getting the information of a class room
            Method1(classRooms);

            Console.WriteLine("---------------------");

            //Method 2 of getting the information of a class room
            Method2(classRooms);
            
            Console.WriteLine("---------------------");


            //Method 3 of getting the information of a class room
            Method3(classRooms);

            Console.WriteLine("=====================");

            //Method 4 of getting the information of a class room
            Method4(classRooms);
            #endregion

            #region StringJson
            //Basics of serializing object to JSON and Deserializing object to DateTable
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

        private static void Method4(List<ClassRoom<JObject>> classRooms)
        {
            //Is a dictonary so can index the JObject by its key
            foreach(var room in classRooms)
            {
                Console.WriteLine(room.ClassId);
                Console.WriteLine(room.Student["Name"]);
                Console.WriteLine(room.Student["StudentId"]);
                Console.WriteLine(room.Student["Birthday"]);
            }
        }

        private static void Method3(List<ClassRoom<JObject>> classRooms)
        {
            foreach (var room in classRooms)
            {
                Console.WriteLine(room.ClassId);
                Console.WriteLine(JObject.Parse(room.Student.ToString())["Name"].ToString());
                Console.WriteLine(JObject.Parse(room.Student.ToString())["StudentId"].ToString());
                Console.WriteLine(JObject.Parse(room.Student.ToString())["Birthday"].ToString());
            }
        }

        private static void Method2(List<ClassRoom<JObject>> classRooms)
        {
            foreach (var room in classRooms)
            {
                Console.WriteLine(room.ClassId);
                var jsonObject = JsonConvert.DeserializeObject<JObject>(room.Student.ToString());
                foreach (var j in jsonObject)
                {
                    Console.WriteLine(j.Value.ToString());
                }
                Console.WriteLine();
            }
        }

        private static void Method1(List<ClassRoom<JObject>> classRooms)
        {
            foreach (var room in classRooms)
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
        }
    }
}
