using System.Reflection;

namespace Lista7

{
    public class Department(int id, string name)
    {
        public int Id { get; set; } = id;
        public String Name { get; set; } = name;

        public override string ToString()
        {
            return $"{Id,2}), {Name,16}";
        }

    }

    public enum Gender
    {
        Female,
        Male
    }

    public class StudentWithTopics(int id, int index, string name, Gender gender, bool active,
        int departmentId, List<string> topics)
    {
        public int Id { get; set; } = id;
        public int Index { get; set; } = index;
        public string Name { get; set; } = name;
        public Gender Gender { get; set; } = gender;
        public bool Active { get; set; } = active;
        public int DepartmentId { get; set; } = departmentId;

        public List<string> Topics { get; set; } = topics;

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "no active"),9},{DepartmentId,2}, topics: ";
            foreach (var str in Topics)
                result += str + ", ";
            return result;
        }
    }
    
    public class Topic(int id, string name)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;

        public override string ToString()
        {
            return $"{Id}) {Name}";
        }
    }
    
    public class Student(int id, int index, string name, Gender gender, bool active, int departmentId, List<int> topicIds)
    {
        public int Id { get; set; } = id;
        public int Index { get; set; } = index;
        public string Name { get; set; } = name;
        public Gender Gender { get; set; } = gender;
        public bool Active { get; set; } = active;
        public int DepartmentId { get; set; } = departmentId;

        public List<int> TopicIds { get; set; } = topicIds;

        public override string ToString()
        {
            var result = $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "not active"),9},{DepartmentId,2}, topics: ";
            result += string.Join(",", TopicIds);
            return result;
        }
    }

    public class Student2(int id, int index, string name, Gender gender, bool active, int departmentId)
    {
        public int Id { get; set; } = id;
        public int Index { get; set; } = index;
        public string Name { get; set; } = name;
        public Gender Gender { get; set; } = gender;
        public bool Active { get; set; } = active;
        public int DepartmentId { get; set; } = departmentId;

        public override string ToString()
        {
            return $"{Id,2}) {Index,5}, {Name,11}, {Gender,6},{(Active ? "active" : "not active"),9},{DepartmentId,2}";
        }

        public void ChangeNameAndActiveStatus(string name, bool active)
        {
            Name = name;
            Active = active;
        }
    }
    
    public class StudentToTopic(int studentId, int topicId)
    {
        public int StudentId { get; set; } = studentId;
        public int TopicId { get; set; } = topicId;

        public override string ToString()
        {
            return $"({StudentId}, {TopicId})";
        }
    }

    public static class Generator
    {
        public static List<StudentWithTopics> GenerateStudentsWithTopicsEasy()
        {
            return [
            new StudentWithTopics(1,12345,"Nowak", Gender.Female,true,1,
                    ["C#","PHP","algorithms"]),
            new StudentWithTopics(2, 13235, "Kowalski", Gender.Male, true,1,
                    ["C#","C++","fuzzy logic"]),
            new StudentWithTopics(3, 13444, "Schmidt", Gender.Male, false,2,
                    ["Basic","Java", "algorithms"]),
            new StudentWithTopics(4, 14000, "Newman", Gender.Female, false,3,
                    ["JavaScript","neural networks"]),
            new StudentWithTopics(5, 14001, "Bandingo", Gender.Male, true,3,
                    ["Java","C#"]),
            new StudentWithTopics(6, 14100, "Miniwiliger", Gender.Male, true,2,
                    ["algorithms","web programming"]),
            new StudentWithTopics(11,22345,"Nowaczyk", Gender.Female,true,2,
                    ["C#","JavaScript","web programming"]),
            new StudentWithTopics(12, 23235, "Newdon", Gender.Male, false,1,
                    ["C#","C++","fuzzy logic"]),
            new StudentWithTopics(13, 23444, "Showner", Gender.Male, true,2,
                    ["algorithms","C#"]),
            new StudentWithTopics(13, 29844, "Wilson", Gender.Male, true,2,
                ["Basic","C#"]),
            new StudentWithTopics(14, 24000, "Newman", Gender.Female, false,3,
                    ["JavaScript","neural networks"]),
            new StudentWithTopics(15, 24001, "Rocky", Gender.Male, true,2,
                    ["fuzzy logic","C#"]),
            new StudentWithTopics(16, 24100, "Bruno", Gender.Female, false,2,
                    ["algorithms","JavaScript", "neural networks"]),
            ];
        }
        
        public static List<Topic> GenerateTopicsFromStudents(List<StudentWithTopics> students)
        {
            return students
                .SelectMany(s => s.Topics)
                .Distinct()
                .Select((name, index) => new Topic(index + 1, name))
                .ToList();
        }
    }
    class Program
    {
        public static IEnumerable<IEnumerable<StudentWithTopics>> GroupStudents(
            List<StudentWithTopics> students,
            int n)
        {
            return students
                .OrderBy(s => s.Name)
                .ThenBy(s => s.Index)
                .Select((student, index) => new { student, index })
                .GroupBy(x => x.index / n)
                .Select(g => g.Select(x => x.student));
        }

        public static void Zad1()
        {
            var students = Generator.GenerateStudentsWithTopicsEasy();
            var groups = GroupStudents(students, 3);

            int groupNumber = 1;
            foreach (var group in groups)
            {
                Console.WriteLine($"--- Grupa {groupNumber++} ---");
                foreach (var s in group)
                    Console.WriteLine(s);
                Console.WriteLine();
            }

        }

        public static IEnumerable<dynamic> SortStudentsTopics(
            List<StudentWithTopics> students)
        {
            return students
                .SelectMany(s => s.Topics)
                .GroupBy(t => t)
                .Select(g => new { Topic = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.Topic);
        }

        public static IEnumerable<dynamic> SortStudentsTopicsByGender(
            List<StudentWithTopics> students)
        {
            return students
                .GroupBy(s => s.Gender)
                .Select(g => new
                {
                    Gender = g.Key,
                    Topics =
                        g.SelectMany(s => s.Topics)
                            .GroupBy(t => t)
                            .Select(tg => new
                            {
                                Topic = tg.Key,
                                Count = tg.Count()
                            })
                            .OrderByDescending(x => x.Count)
                            .ThenBy(x => x.Topic)
                });
        }

        public static void Zad2()
        {
            var students = Generator.GenerateStudentsWithTopicsEasy();
            var sortedTopics = SortStudentsTopics(students);
            var sortedTopicsByGender = SortStudentsTopicsByGender(students);
            
            Console.WriteLine("   Podpunkt A");
            foreach (var t in sortedTopics)
                Console.WriteLine($"{t.Topic}: {t.Count}");
            Console.WriteLine("\n-------------------------------------\n");
            
            Console.WriteLine("   Podpunkt B");
            foreach (var genderGroup in sortedTopicsByGender)
            {
                Console.WriteLine($"\n--- {genderGroup.Gender} ---");
                foreach (var t in genderGroup.Topics)
                    Console.WriteLine($"{t.Topic}: {t.Count}");
            }
        }
        
        public static Dictionary<string, int> CreateTopicMap(List<Topic> topics)
        {
            return topics.ToDictionary(t => t.Name, t => t.Id);
        }


        public static List<Student> ConvertStudents(
            List<StudentWithTopics> studentsWithTopics,
            Dictionary<string, int> topicMap)
        {
            return studentsWithTopics
                .Select(s => new Student(
                    s.Id,
                    s.Index,
                    s.Name,
                    s.Gender,
                    s.Active,
                    s.DepartmentId,
                    s.Topics.Select(t => topicMap[t]).ToList()
                ))
                .ToList();
        }

        public static void Zad3()
        {
            var studentsWithTopics = Generator.GenerateStudentsWithTopicsEasy();
            
            Console.WriteLine("--- Tematy ---");
            var topics = Generator.GenerateTopicsFromStudents(studentsWithTopics);
            foreach (var t in topics)
            {
                Console.WriteLine(t);
            }
            
            Console.WriteLine("\n--------------------------------------------------\n");
            
            Console.WriteLine("--- Studenci z tematami ---");
            foreach (var s in studentsWithTopics)
            {
                Console.WriteLine(s);
            }
            
            Console.WriteLine("\n--- Przekonwertowani studenci ---");
            var topicMap = CreateTopicMap(topics);
            var students = ConvertStudents(studentsWithTopics, topicMap);

            foreach (var s in students)
            {
                Console.WriteLine(s);
            }
        }
        
        public static void ConvertStudentsToRelation(
            List<StudentWithTopics> studentsWithTopics,
            out List<Student2> students,
            out List<StudentToTopic> studentToTopics)
        {
            var topics = Generator.GenerateTopicsFromStudents(studentsWithTopics);
            var topicMap = CreateTopicMap(topics);

            students = studentsWithTopics
                .Select(s => new Student2(
                    s.Id,
                    s.Index,
                    s.Name,
                    s.Gender,
                    s.Active,
                    s.DepartmentId
                ))
                .ToList();

            studentToTopics = studentsWithTopics
                .SelectMany(s => s.Topics.Select(t => new StudentToTopic(
                    s.Id,
                    topicMap[t]
                )))
                .ToList();
        }

        public static void Zad3Ver2()
        {
            var studentsWithTopics = Generator.GenerateStudentsWithTopicsEasy();
            var topics = Generator.GenerateTopicsFromStudents(studentsWithTopics);
            
            ConvertStudentsToRelation(studentsWithTopics, out var students, out var studentToTopics);
            
            Console.WriteLine("--- Studenci ---");
            foreach (var s in students)
            {
                Console.WriteLine(s);
            }
            
            Console.WriteLine("\n--- Relacje ---");
            foreach (var r in studentToTopics)
            {
                Console.WriteLine(r);
            }
            
            Console.WriteLine("\n--- Tematy ---");
            foreach (var t in topics)
            {
                Console.WriteLine(t);
            }
        }

        public static void Zad4()
        {
            // podpunkt a
            string className = "Lista7.Student2";
            Type t = Type.GetType(className)!;

            object? student1 = Activator.CreateInstance(
                t,
                1, 12345, "Nowak", Gender.Female, true, 1
            );

            object? student2 = Activator.CreateInstance(
                t,
                2, 23456, "Kowalski", Gender.Male, false, 2
            );

            Console.WriteLine(student1);
            Console.WriteLine(student2);

            // podpunkt b
            string methodName = "ChangeNameAndActiveStatus";
            MethodInfo method = t.GetMethod(methodName)!;

            object[] args = { "Dandelion", false };

            object? result = method.Invoke(student1, args);

            Console.WriteLine($"Wynik: {result ?? "null"}");
            Console.WriteLine($"Obiekt 1 po wywołaniu metody {methodName}:\n{student1}");

            methodName = "get_Gender";
            method = t.GetMethod(methodName)!;
            result = method.Invoke(student2, null)!;

            Console.WriteLine($"Wywołanie metody {methodName}");
            Console.WriteLine($"Wynik: {result}");
        }
        
        static void Main()
        {
            Console.WriteLine("\tZADANIE 1");
            Zad1();
            Console.WriteLine("____________________________________________________________________\n");
            Console.WriteLine("\tZADANIE 2");
            Zad2();
            Console.WriteLine("____________________________________________________________________\n");
            Console.WriteLine("\tZADANIE 3");
            Zad3();
            Console.WriteLine("____________________________________________________________________\n");
            Console.WriteLine("\tZADANIE 3 WERSJA 2");
            Zad3Ver2();
            Console.WriteLine("____________________________________________________________________\n");
            Console.WriteLine("\tZADANIE 4");
            Zad4();
        }
    }
}
