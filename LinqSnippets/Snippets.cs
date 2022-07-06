using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "Wv Golf",
                "Wv California",
                "Audi a3",
                "Audi a4",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            // 1. Select * of cars (Select all cars)

            var carList = from car in cars select car;

            foreach ( var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. Select where car is Audi (Select Audis)

            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }

            // Number examples

            



        }

        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4 ,5 ,6 ,7, 8, 9};

            // Each number multiplied by 3 
            // take all numbers, but 9
            // order numbers by ascending values

            var processedNumberList =
                numbers
                .Select(num => num * 3) // {3, 6, 9...}
                .Where(num => num != 9) // {3, 6, 12...}
                .OrderBy(num => num); // at the end, we order ascending





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

            // 1. First of all elements

            var first = textList.First();

            // 2. First element thas is "c"

            var cText = textList.First(text => text.Equals("c"));

            // 3. First element that contains "j"

            var jText = textList.First(text => text.Contains("j"));

            // 4. First element that contains z or default 

            var firstOrDefaultText = textList.FirstOrDefault(text => text.Equals("z")); // "" or first element that contains "z"

            // 5. Last element that contains z or default

            var lastOrDefaultText = textList.LastOrDefault(text => text.Equals("z")); // "" or last element that contains "z

            // 6. Single values

            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // obtain { 4, 8 }

            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers);
        }

        static public void MultipleSelects()
        {
            // select many

            string[] myOpinions =
                {
                    "Opinion1, text1",
                    "Opinion2, text2",
                    "Opinion3, text3"

                };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Erick",
                            Email = "erickgall@outlook.es",
                            Salary = 3000

                        },

                        new Employee
                        {
                            Id = 2,
                            Name = "Juan",
                            Email = "juankgall@outlook.es",
                            Salary = 2000

                        },

                        new Employee
                        {
                            Id = 3,
                            Name = "Ana",
                            Email = "erickgall@outlook.es",
                            Salary = 1000

                        }
                    }
                },

                 new Enterprise
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 3,
                            Name = "Elena",
                            Email = "erickgall@outlook.es",
                            Salary = 3000

                        },

                        new Employee
                        {
                            Id = 4,
                            Name = "Juana",
                            Email = "juankgall@outlook.es",
                            Salary = 1500

                        },

                        new Employee
                        {
                            Id = 5,
                            Name = "Salamandra",
                            Email = "erickgall@outlook.es",
                            Salary = 4000

                        }
                    }
                }

            };

            // obtain all employees of all enterprises

            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // know if any list is empty

            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprises => enterprises.Employees.Any());

            // All enterprises has at least an employee with at least than 1000 euros of salary

            bool hasEmployeeWithSalaryMoreThanOrEqual1000 =
                enterprises.Any(enterprises => enterprises.Employees.Any(employee => employee.Salary > 1000));

        }

        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // INNER JOIN (interseccion)

            var commonResult = from element in firstList
                               join element2 in secondList
                               on element equals element2
                               select new { element, element2 };

            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element,secondElement) => new {element,secondElement });


            // OUTER JOIN- LEFT

            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element  equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };


            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where( s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };
            // OUTER JOIN-RIGHT

            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            // UNION 

            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10

            };

            // SKIP

            var skipTwoFirstValues = myList.Skip(2); // {3,4,5,6,7,8,9,10}

            var skipLastTwoValues = myList.SkipLast(2); // {1,2,3,4,5,6,7,8}

            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); // {4,5,6,7,8,9,10}

            // TAKE

            var takeTwoFirstValues = myList.Take(2); // {1,2}

            var takeLastTwoValues = myList.TakeLast(2); // {9,10}

            var TakeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // {1,2,3}
            


        }

        // Paging wwith Skip and Take

        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage); // pueden ser anidados juntos, el resultado de una lo aplica la siguiente

        }

        static public void LinqVariables() // declarar variables dentro de las consultas
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aboveAverage = from number in numbers
                               let average = numbers.Average()  // uso de variables locales
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach(int number in aboveAverage)
            {
                Console.WriteLine("Number {0} Square {1}",number,Math.Pow(number,2));

            }

        }

        //  ZIP

        static public void ZipLinq()  // cremallera, vamos a intercalar las dos listas
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5 };
            string[] stringNumbers = new string[] { "uno", "dos", "tres", "cuatro", "cinco" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word);
            // { "1 = uno", "2 = dos", ...}

        }

        // REPEAT & RANGE

        static public void repeatRangeLinq()
        {
            // generate colletion from 1 to 1000 -- Range

            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);

            // repeat a value N times 

            IEnumerable<string> fiveX = Enumerable.Repeat("X",5); // { "X","X","X","X","X" }



            
        }

        static public void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {

                    Id = 1,
                    Name = "Erick",
                    Grade = 90,
                    Certified = true,
                },

                 new Student
                {

                    Id = 2,
                    Name = "Pedro",
                    Grade = 80,
                    Certified = false,
                },

                  new Student
                {

                    Id = 3,
                    Name = "Erick",
                    Grade = 90,
                    Certified = true,
                },

                   new Student
                {

                    Id = 4,
                    Name = "Erick",
                    Grade = 90,
                    Certified = true,
                },

                    new Student
                {

                    Id = 5,
                    Name = "Erick",
                    Grade = 90,
                    Certified = true,
                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertified = from student in classRoom
                               where student.Certified == false
                               select student;

            var approvedStudents = from student in classRoom
                                   where student.Grade >= 50 && student.Certified == true
                                   select student;

            var approvedStudentsName = from student in classRoom
                                   where student.Grade >= 50 && student.Certified == true
                                   select student.Name;




        }

        // ALL

        static public void allLinq()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x => x <= 10); // true

            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // false

            var emptyList = new List<int>();

            var allNumbersAreGreaterThan0 = numbers.All(x => x >= 0); // true


        }

        // AGGREGATE

        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // sum all numbers

            int sum = numbers.Aggregate((prevSum,current) => prevSum + current);
            // 0,1 => 1
            // 1,2 => 3
            // 3,3 => 6
            // etc.

            string[] words = { "hello", "my", "name", "is", "Erick" };

            string greeting = words.Aggregate((prevGreeting,current) => prevGreeting + current);
            // hello my name is Erick






        }

        // DISTINCT

        static public void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> distinctValues = numbers.Distinct();
        }

        // GROUPBY

        static public void groupByExamples()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9 };

            // obtain only even numbers (pares) and generate two groups

            var grouped = numbers.GroupBy(x => x%2 ==0);

            // we will have two groups:
            // 1. the group that doesn't fit the condition ( odd numbers )
            // 2. the grup that fits the condition ( even numbers )
            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value); // 1,3,5,7,9....2,4,6,8 (first the odds and then the even)
                }

            }

            // Another example 

            var classRoom = new[]
            {
                new Student
                {

                    Id = 1,
                    Name = "Erick",
                    Grade = 90,
                    Certified = true,
                },

                 new Student
                {

                    Id = 2,
                    Name = "Pedro",
                    Grade = 80,
                    Certified = false,
                },

                  new Student
                {

                    Id = 3,
                    Name = "Erick",
                    Grade = 90,
                    Certified = true,
                },

                   new Student
                {

                    Id = 4,
                    Name = "Erick",
                    Grade = 90,
                    Certified = true,
                },

                    new Student
                {

                    Id = 5,
                    Name = "Erick",
                    Grade = 90,
                    Certified = true,
                }
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified);

            // we obtain two groups 
            // 1. not certified students
            // 2. certified students

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("----------- {0} ---------- " , group.Key);

                foreach (var student in group)
                {
                    Console.WriteLine(student.Name); // 
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
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id=1,
                            Title = "My first comment",
                            Created = DateTime.Now,
                            Content = "My content"

                            
                        },

                        new Comment()
                        {
                            Id=2,
                            Title = "My second comment",
                            Created = DateTime.Now,
                            Content = "My other  content"


                        }
                    }



                },

                 new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id=3,
                            Title = "My other comment",
                            Created = DateTime.Now,
                            Content = "My new content"


                        },

                        new Comment()
                        {
                            Id=4,
                            Title = "My other new  comment",
                            Created = DateTime.Now,
                            Content = "My new  content"


                        }
                    }



                }
            };

            var commentsContent = posts.SelectMany(
                post => post.Comments, (post, comment) => new {PostId = post.Id,CommentContent = comment.Content});


        }
    }

    
}