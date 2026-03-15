using System.Runtime.Intrinsics.Arm;

namespace ExempluClase
{
    public class FiguraGeometrica
    {
        // data membra privata
        int[] dimensiuniLaturi;

        // proprietati auto-implemented
        public string Denumire { get; set; }
        public int NrLaturi { get; set; }

        // proprietate computed – varianta 1
        public bool EstePoligon
        {
            get
            {
                return NrLaturi >= 3;
            }
        }

        // proprietate computed – varianta 2 (expresie Lambda)
        public bool EstePoligon_v2 => NrLaturi >= 3;

        public void SetDimensiuniLaturi(int[] _dimensiuniLaturi)
        {
            dimensiuniLaturi = new int[_dimensiuniLaturi.Length];
            _dimensiuniLaturi.CopyTo(dimensiuniLaturi, 0);
        }

        public int[] GetDimensiuniLaturi()
        {
            /* returneaza o copie a vectorului, astfel încât utilizatorii acestei 
               clase să nu poata modifica în mod direct conținutul vectorului */
            return (int[])dimensiuniLaturi.Clone();
        }


        //	Constructor fara parametri
        public FiguraGeometrica()
        {
            Console.Write("Introduceti denumirea figurii geometrice: ");
            Denumire = Console.ReadLine(); 

            Console.Write("Introduceti numarul de laturi ale figurii geometrice: ");
            NrLaturi = int.Parse(Console.ReadLine()); 

            int[] laturi = new int[NrLaturi]; 
            for (int i = 0; i < NrLaturi; i++)
            {
                Console.Write($"Introduceti dimensiunea laturii {i + 1}: ");
                laturi[i] = int.Parse(Console.ReadLine());
            }
            SetDimensiuniLaturi(laturi);
        }

        //	Constructor cu parametri
        public FiguraGeometrica(string _denumire, int _nrLaturi)
        {
            Denumire = _denumire;   
            NrLaturi = _nrLaturi;
            Console.WriteLine("Figura este :" + Denumire);
            Console.WriteLine("Numarul de laturi: " + NrLaturi);
            int[] laturi = new int[NrLaturi];
            for (int i = 0; i < NrLaturi; i++)
            {
                Console.Write($"Introduceti dimensiunea laturii {i + 1}: ");
                laturi[i] = int.Parse(Console.ReadLine());
            }
            SetDimensiuniLaturi(laturi);
        }

        public int Perimetru()
        {
            int[] laturi = GetDimensiuniLaturi();
            int Perimetru = 0;
            for (int i = 0; i < NrLaturi; i++)
            {
                int numar = Convert.ToInt32(laturi[i]);
                Perimetru += numar;
            }
            return Perimetru;
        }
        public string TipFigura
        {
            get
            {
                switch (NrLaturi)
                {
                    case 3: return "Triunghi";
                    case 4: return "Patrulater";
                    case 5: return "Pentagon";
                    case 6: return "Hexagon";
                    case 7: return "Heptagon";
                    case 8: return "Octogon";
                    default:
                        if (NrLaturi < 3)
                            return "Nu este poligon";
                        else
                            return $"Poligon cu {NrLaturi} laturi";
                }
            }
        }

        public FiguraGeometrica(string _denumire, int _nrLaturi, bool Joc)
        {
            Denumire = _denumire;
            NrLaturi = _nrLaturi;
            dimensiuniLaturi = new int [NrLaturi];
        }

        public void Joc()
        {
            Console.WriteLine("=== JOC FIGURA GEOMETRICA ===");
            Console.WriteLine("Introduceti numarul de laturi si aflati denumirea figurii!\n");

            string raspuns;
            do
            {
                Console.Write("Introduceti numarul de laturi: ");
                NrLaturi = int.Parse(Console.ReadLine());
                Console.WriteLine($"Figura cu {NrLaturi} laturi este: {TipFigura}\n");
                Console.Write("Doriti sa continuati? (da/nu): ");
                raspuns = Console.ReadLine().ToLower();
                Console.WriteLine();
            } while (raspuns == "da");

            Console.WriteLine("\n=== GENERARE AUTOMATA ===");
            Random rand = new Random();

            for (int i = 0; i < 5; i++)
            {
                NrLaturi = rand.Next(1, 9); 
                Console.WriteLine($"Laturi generate: {NrLaturi} => {TipFigura}");
            }

            Console.ReadKey();
        }
        //	Metoda care returneaza informatiile despre figura geometrica 
        //	sub forma unui sir de caractere
        public string Info()
        {
            if(string.IsNullOrEmpty(Denumire))
            {
                return "FIGURA NESETATA";
            }
            else
            {
                int[] laturi = GetDimensiuniLaturi();
                string laturiStr = string.Join(", ", laturi);
                return $"Denumire: {Denumire},Tip Figura: {TipFigura}, NrLaturi: {NrLaturi}, Laturi: [{laturiStr}],Perimetru: {Perimetru()}";
            }
                
        }

    }
}
