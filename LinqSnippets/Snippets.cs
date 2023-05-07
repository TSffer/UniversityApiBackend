using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LinqSnippets
{

    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            // 1. SELECT * of cars (SELECT ALL CARS)
            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE is Audi (SELECT AUDIs)
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }

        // Number Examples
        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //Each Number multiplied by 3

            //Take all number, but 9

            //Order numbers by ascending value

            var processedNumberList =
                numbers
                    .Select(num => num * 3)
                    .Where(num => num != 9)
                    .OrderBy(num => num);

        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            //1. First of all elements
            var first = textList.First();

            //2. First element that is "c"
            var cText = textList.First(text => text.Equals("c"));

            //3. First element that contains "j"

            var jText = textList.First(text => text.Contains("j"));

            //4. First element that contains "z" or default
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z"));

            //5. LAst element that contains "z" or default
            var lastOrDefault = textList.LastOrDefault(text => text.Contains("z"));

            //6. Single Values
            var uniqueText = textList.Single();
            var uniqueOrDefaultText = textList.SingleOrDefault();

            int[] evenNumver = { 0, 2, 4, 6, 8 };
            int[] otherEventNumber = { 0, 2, 6 };

            var myEvent = evenNumver.Except(otherEventNumber);
        }

        static public void MultipleSelect()
        {
            // SELECT MANY
            string[] myOpinions =
            {
                "Opinion 1, text 1",
                 "Opinion 2, text 2",
                 "Opinion 3, text 3"
            };

            var myOpiniosSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprise = new[]
            {
                new Enterprise()
                {
                    Id =1,
                    Name = "Enterprise 1",
                    Employees =new[]
                    {
                        new Employee
                        {
                            Id=1,
                            Name="Luis",
                            Email = "email",
                            Salary = 300
                        },
                         new Employee
                        {
                            Id=2,
                            Name="Pepe",
                            Email = "email",
                            Salary = 10300
                        },
                          new Employee
                        {
                            Id=3,
                            Name="Juan",
                            Email = "email",
                            Salary = 3000
                        }
                    }
                },
                 new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees =new[]
                    {
                        new Employee
                        {
                            Id=4,
                            Name="Ana",
                            Email = "email",
                            Salary = 300
                        },
                         new Employee
                        {
                            Id=5,
                            Name="Maria",
                            Email = "email",
                            Salary = 10300
                        },
                          new Employee
                        {
                            Id=6,
                            Name="Marta",
                            Email = "email",
                            Salary = 4000
                        }
                    }
                }
            };

            //Obtain all Employees of all enterprices
            var employeeList = enterprise.SelectMany(enterprise => enterprise.Employees);

            //Know if a list is empty
            bool hasEmterprises = enterprise.Any();

            bool hasEmployees = enterprise.Any(enterprise => enterprise.Employees.Any());

            // All enterprises at least has an employee with more than 1000 of salary
            bool hasEmployeeWithSalaryMoreThan1000 =
                enterprise.Any(enterprise =>
                    enterprise.Employees.Any(employee => employee.Salary >= 1000));
        }

        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            //INNER JOIN
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                        secondList,
                        element => element,
                        secondElement => secondElement,
                        (element, secondElement) => new { element, secondElement }
                        );

            //OUTER JOIN - LEFT
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };


            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };

            //OUTER JOIN - RIGHT
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            //UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3, 4,5, 6, 7,8, 9, 10
            };

            //SKIP
            var skipTwhoFirstValue = myList.Skip(2); // 3, 4, 5 ...
            var skipLastTwoValue = myList.SkipLast(2); //.. 7,8
            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); //4, 5 ,6 ...

            //TAKE

            var takeFistTwoValues = myList.Take(2);
            var takeLastTwoValues = myList.TakeLast(2);
            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4);
        }

        //Paging with Skip and Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pagenumber, int resultPerPage)
        {
            int startIndex = (pagenumber - 1) * resultPerPage;
            return collection.Skip(startIndex).Take(resultPerPage);
        }

        //Variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAvarage = from number in numbers
                               let average = numbers.Average()
                               let nSquare = Math.Pow(number, 2)
                               where nSquare > average
                               select number;

            Console.WriteLine("Avarare: {0}", aboveAvarage);

            foreach (int number in aboveAvarage)
            {
                Console.WriteLine("Query: Number: {0}  Square: {1}", number, Math.Pow(number, 2));
            }
        }

        // ZIP
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumber = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumber, (number, word) => number + " = " + word);
        }

        //Repeat & Range
        static public void RepeatRangeLinq()
        {
            //Generate collections from 1 - 1000 --> RANGE
            IEnumerable<int> firts1000 = Enumerable.Range(1, 1000);

            //Repeat a value N times
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5);
        }

        static public void studentLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Juan",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Ana",
                    Grade = 97,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Alvaro",
                    Grade = 10,
                    Certified = false,
                },
                new Student
                {
                    Id = 4,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true,
                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == false
                                       select student;

            var appovedStudents = from student in classRoom
                                  where student.Grade >= 50 && student.Certified == true
                                  select student;

        }

        //ALL
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };
            bool allAreSmallerThan10 = numbers.All(x => x < 10);

            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2);

            var emptyList = new List<int>();

            bool allNumbersAreGreaterThan0 = numbers.All(x => x >= 0); //true
        }

        //Aggregate
        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //sun all numbers

            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            //0, 1 => 1
            //1, 2 => 3
            //3, 4 => 7
            //..

            string[] words = { "hello,", "my", "name", "is", "luis" };
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);
        }

        //Distinct
        static public void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> distinctValues = numbers.Distinct();
        }

        //GroupBy
        static public void groupByExamples()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //Obtain only even numbers and generate two group

            var grouped = numbers.GroupBy(x => x % 2 == 0);

            foreach( var group in grouped )
            {
                foreach(var value in group)
                {
                    Console.WriteLine(value);
                }
            }

            var classRoom = new[]
           {
                new Student
                {
                    Id = 1,
                    Name = "Juan",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Ana",
                    Grade = 97,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Alvaro",
                    Grade = 10,
                    Certified = false,
                },
                new Student
                {
                    Id = 4,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true,
                }
            };


            var certifiedQuery = classRoom.GroupBy(student => student.Certified);
            //We obtain two groups
            //1. Not certified students
            //2. Certified Students

            foreach(var group in certifiedQuery)
            {
                Console.WriteLine("-------------- {0} ------------", group.Key);
                foreach(var student in group)
                {
                    Console.WriteLine(student.Name);  
                }
            }
        }

        static public void relationsLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "Mu first comentent",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My Content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My a comment",
                            Content = "My Content"
                        },
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My b comment",
                            Content = "My Content"
                        }
                    }
                }
            };

            var commentsContent = posts.SelectMany(
                                        post => post.Comments,
                                            (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });
        
        }
    }
}