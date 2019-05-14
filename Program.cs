using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace sharp_laborai
{
    class Program
    {
        static void Main(string[] args)
        {
            string resultt;
            string vardasPavarde;
            string[] split;
            string[] fileSplit;
            int vidOrMed;
            int Nuskaitymas;
            
            List<Student> people = new List<Student>();

            Console.WriteLine("Suvesti studentus is failo spauskite [1]");
            Console.WriteLine("Suvesti studentus rankiniu budu spauskite [2]");
            Nuskaitymas = int.Parse(Console.ReadLine());
            if (Nuskaitymas == 1)
            {
                string line;          
                StreamReader file = 
                    new StreamReader("../../kursiokai.txt");
                while ((line = file.ReadLine()) != null)
                {
                    
                    line = Regex.Replace(line, @"\s+", " ");
                    fileSplit = line.Split(' ');
                    int stringLenth = fileSplit.Length - 1;
                    int counter = 2;

                    if (fileSplit[0] != "Vardas")
                    {
                        Student student = 
                            new Student
                        {
                            Results = new List<int>()
                        };
                        student.Name = fileSplit[0];
                        student.Surname = fileSplit[1];
                        student.ExamResult = int.Parse(fileSplit[stringLenth]);
                        while (stringLenth > counter)
                        {
                            student.Results.Add(int.Parse(fileSplit[counter]));
                            counter++;
                        }
                        people.Add(student);
                    }
                }
                people = people.OrderBy(x => x.Name).ThenBy(x => x.Surname).ToList();

            }
            else if (Nuskaitymas == 2)
            {
                Console.WriteLine("Iveskite studento varda ir pavarde, palikite tuscia jei nenorite prideti studento");
                vardasPavarde = Console.ReadLine();
                while (vardasPavarde != "")
                {
                    Student student = new Student
                    {
                        Results = new List<int>()
                    };
                    split = vardasPavarde.Split(' ');
                    student.Name = split[0];
                    student.Surname = split[1];
                    Console.WriteLine("Iveskite ND pazymius. (tuscia) baigti ND paz ivedimui");
                    resultt = Console.ReadLine();
                    while (resultt != "")
                    {
                        student.Results.Add(int.Parse(resultt));
                        Console.WriteLine("Iveskite namu darbu pazymi.");
                        resultt = Console.ReadLine();
                    }
                    Console.WriteLine("Iveskite egazimo rezutalta:");
                    student.ExamResult = int.Parse(Console.ReadLine());
                    student.FinalResult = Functions.CalculateFilnalResult(student.Results, student.ExamResult);
                    people.Add(student);

                    Console.WriteLine("Iveskite studento varda ir pavarde, palikite tuscia jei nenorite prideti naujo studento");
                    vardasPavarde = Console.ReadLine();
                }
            }
            Console.WriteLine("Isvesti galutini ivertinima pagal: vidurki [1] ; mediana [2] ; abu [3]");
            vidOrMed = int.Parse(Console.ReadLine());
            Functions.GetResultsToScreen(people, vidOrMed);
            Console.ReadLine();
        }
    }
}
