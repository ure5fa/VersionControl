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
                            Percent = double.Parse(line[3]),
                            
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
                            Percent = double.Parse(line[3]),
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
