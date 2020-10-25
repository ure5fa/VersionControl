using nepesseg.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nepesseg
{
    public partial class Form1 : Form

    {
        public Form1()

        {
            InitializeComponent();

            Random rng = new Random(1234);
            List<Person> GetPopulation(string csvpath)

            {
                List<Person> population = new List<Person>();
                population = GetPopulation(@"C:\Temp\nép.csv");



                using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))

                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Split(';');
                        population.Add(new Person()
                        {
                            BirthYear = int.Parse(line[0]),
                            Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                            NbrOfChildren = int.Parse(line[2])
                        });
                    }

                }
                for (int year = 2005; year <= 2024; year++)
                {

                    for (int i = 0; i < population.Count; i++)
                    {
                         void SimStep(int Kor, Person person)
                        {
                            
                            if (!person.IsAlive) return;

                            
                            byte Kor = (byte)(year - person.BirthYear);

                            
                            double pDeath = (from x in DeathProbability
                                             where x.Gender == person.Gender && x.Age == age
                                             select x.P).FirstOrDefault();
                            
                            if (rng.NextDouble() <= pDeath)
                                person.IsAlive = false;

                            
                            if (person.IsAlive && person.Gender == Gender.Female)
                            {
                               
                                double pBirth = (from x in BirthProbability
                                                 where x.Age == Kor
                                                 select x.P).FirstOrDefault();
                                
                                if (rng.NextDouble() <= pBirth)
                                {
                                    Person újszülött = new Person();
                                    újszülött.BirthYear = year;
                                    újszülött.NbrOfChildren = 0;
                                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                                    population.Add(újszülött);
                                }
                            }
                        }
                    }

                    int nbrOfMales = (from x in population
                                      where x.Gender == Gender.Male && x.IsAlive
                                      select x).Count();
                    int nbrOfFemales = (from x in population
                                        where x.Gender == Gender.Female && x.IsAlive
                                        select x).Count();
                    Console.WriteLine(
                        string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
                }
                return population;



            }
            List<BirthProbability> GetBirthProbabilities(string csvpath)
            {
                List<BirthProbability> BirthProbability = new List<BirthProbability>();
                BirthProbability = GetBirthProbabilities(@"C:\Temp\születés.csv");
                using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Split(';');
                        BirthProbability.Add(new BirthProbability()
                        {
                            Kor = int.Parse(line[1]),
                            NbrOfChildren = int.Parse(line[2]),
                            pBirth = double.Parse(line[3]),
                            
                        });
                    }
                }

                return BirthProbability;
            }

            List<DeathProbability> GetDeathProbabilities(string csvpath)

            {
                List<DeathProbability> DeathProbability = new List<DeathProbability>();
                DeathProbability = GetDeathProbabilities(@"C:\Temp\halál.csv");
                using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Split(';');
                        DeathProbability.Add(new DeathProbability()
                        {
                            Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                            Kor = int.Parse(line[2]),
                            pDeath = double.Parse(line[3]),
                        });
                    }
                }

                return DeathProbability;
            }

            
            



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Person> Population = new List<Person>();
            List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
            List<DeathProbability> DeathProbabilities = new List<DeathProbability>();


        }

    }
}
