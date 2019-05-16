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
            Console.WriteLine("Sugeneruoti 5 studentu sarasus [3]");
            Console.WriteLine("Sugeneruoti ir surusiuoti 1000 studentu sarasus [4]");
            Nuskaitymas = int.Parse(Console.ReadLine());
            //Nuskaitymas = 1;

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
                        Student student = new Student
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
            } else if(Nuskaitymas == 3) {
				Functions.GenerateRandomListFile(10);
				Functions.GenerateRandomListFile(100);
				Functions.GenerateRandomListFile(1000);
				Functions.GenerateRandomListFile(10000);
				Functions.GenerateRandomListFile(100000);
			} else if (Nuskaitymas == 4) {
				
				Functions.SortListAndGenerateFiles(1000);
				
			}

            //Console.WriteLine("Isvesti galutini ivertinima pagal: vidurki [1] ; mediana [2] ; abu [3]");
            //vidOrMed = int.Parse(Console.ReadLine());
            Console.WriteLine("Atlikta. Paspasukite 'Enter'");
            vidOrMed = 3;
                
                Functions.GetResultsToScreen(people, vidOrMed);                
                Console.ReadLine();
            }
        }
    }

