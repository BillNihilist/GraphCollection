using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
namespace Molecules
{
    class Molecule : IDisposable
    {
        //public Atom Root;
        public List<Atom> AllAtoms = new List<Atom>();
        private BondType?[,] matrix;
        private BondType?[][] jaggedArray;
        private string _moleculeName;  //private backing field

        public string MoleculeName
        {
            set
            {
                OnMoleculeNameChanged(_moleculeName, value);
                _moleculeName = value;
                
            }

            get
            {
                return _moleculeName;
            }
        }
        public OnMoleculeNameChangedDelegate OnMoleculeNameChanged;
        


        //public Atom CreateRoot (string name)
        //{
        //    Root = CreateAtom(name);
        //    return Root;
        //}

        public Atom CreateAtom(string name, double atomicMass, PeriodicTable.AtomType atomType)
        {
            var n = (new Atom(name, atomicMass, atomType));
            AllAtoms.Add(n);
            return n;
        }

        public void CreateBondMatrix()
        {
            matrix = new BondType?[AllAtoms.Count, AllAtoms.Count];
            for (int i = 0; i < AllAtoms.Count; i++)
            {
                Atom a1 = AllAtoms[i];

                for (int j = 0; j < AllAtoms.Count; j++)
                {
                    Atom a2 = AllAtoms[j];

                    var bond = a1.Bonds.FirstOrDefault(whatever => whatever.NextAtom == a2);

                    if (bond != null)
                    {
                        matrix[i, j] = bond.TypeOfBond;
                    }
                }

            }
        
        }

        //Create a generic class that allows for all the graph elements to be abstracted. Ienumberable possibility.

        public void CreateBondJaggedArray()
        {
           
            jaggedArray = new BondType?[AllAtoms.Count][];
            for (int i = 0; i < AllAtoms.Count; i++)
            {
                jaggedArray[i] = new BondType?[AllAtoms.Count - (AllAtoms.Count - (i + 1))];
                Atom a1 = AllAtoms[i];

                for (int j = 0; j < AllAtoms.Count - (AllAtoms.Count - (i + 1)); j++)
                {
                    //jaggedArray[j] = new int?[AllAtoms.Count];
                    Atom a2 = AllAtoms[j];

                    var jaggedBond = a1.Bonds.FirstOrDefault(thing => thing.NextAtom == a2);
                    
                    if (jaggedBond != null)
                    {
                        jaggedArray[i][j] = jaggedBond.TypeOfBond;
                    }
                }
            }
        }

        public void PrintJaggedArray()
        {
            Console.Write("       ");
            for (int i = 0; i < AllAtoms.Count; i++)
            {
                Console.Write("{0}  ", AllAtoms[i].myType);
            }

            Console.WriteLine();

            for (int i = 0; i < AllAtoms.Count; i++)
            {
                Console.Write("{0} | [ ", AllAtoms[i].myType);

                for (int j = 0; j < AllAtoms.Count - (AllAtoms.Count - (i + 1)); j++)
                {
                    if (i == j)
                    {
                        Console.Write(" & ");
                    }

                    else if (jaggedArray[i][j] == null)
                    {
                        Console.Write(" . ");
                    }

                    else
                    {
                        Console.Write(" {0} ", jaggedArray[i][j]);
                    }
                }

                Console.Write(" ]\r\n");
            }

            Console.Write("\n");
        }

        public void PrintBondMatrix()
        {
            Console.Write("       ");
            for (int i =0; i < AllAtoms.Count; i++)
            {
                Console.Write("{0}  ", AllAtoms[i].myType);
            }

            Console.WriteLine();

            for (int i = 0; i < AllAtoms.Count; i++)
            {
                Console.Write("{0} | [ ", AllAtoms[i].myType);

                for (int j = 0; j < AllAtoms.Count; j++)
                {
                    if (i == j)
                    {
                        Console.Write(" & ");
                    }

                    else if (matrix [i,j] == null)
                    {
                        Console.Write(" . ");
                    }

                    else
                    {
                        Console.Write(" {0} ", matrix[i, j]);
                    }
                }

                Console.Write(" ]\r\n");
            }

            Console.Write("\n");
        }

        public delegate void OnMoleculeNameChangedDelegate(string OldName, string NewName);

        public void NameMolecule()
        {
            int CarbonCount = 0;
            int HydrogenCount = 0;
            int OxygenCount = 0;
            int CarbonCarbonBonds = 0;
            string Prefix = "";
            string Postfix = "";
            string Iso = "";

            try
            {


                foreach (Atom atom in AllAtoms)
                {
                    if (atom.myType == PeriodicTable.AtomType.H)
                    {
                        HydrogenCount++;
                    }
                    else if (atom.myType == PeriodicTable.AtomType.C)
                    {
                        CarbonCount++;
                    } 
                    else if (atom.myType == PeriodicTable.AtomType.O)
                    {
                        OxygenCount++;
                    }
                }

                Console.WriteLine("Number of Carbons: {0}", CarbonCount);
                Console.WriteLine("Number of Oxygens: {0}", OxygenCount);
                Console.WriteLine("Number of Hydrogens: {0}", HydrogenCount);
                

                if (OxygenCount == 1 && HydrogenCount ==2)
                {
                    Console.WriteLine("Water");
                    
                }

                if (CarbonCount == 1)
                {
                    Prefix = "Meth";
                }
                else if (CarbonCount == 2)
                {
                    Prefix = "Eth";
                }
                else if (CarbonCount == 3)
                {
                    Prefix = "Prop";
                }
                else if (CarbonCount == 4)
                {
                    Prefix = "But";
                }

                if (OxygenCount == 1 && CarbonCount > 0)
                {
                    Postfix = "anol";
                }
                else if (CarbonCount > 0)
                {
                    Postfix = "ane" ;
                }
                

                Console.WriteLine(Iso + Prefix + Postfix);

            }
            catch
            {
                Console.WriteLine("Insert a correct molecule with atomic weights and atom type");

            }
        }

        public void MolecularWeight()
        {
            double Weight = 0;

            for (int i = 0; i < AllAtoms.Count; i++)
            {
                Weight  += AllAtoms[i].AtomicMass;
            }

            Console.WriteLine("The molecular weight is: " + Weight);
        }

        public void Dispose()
        {
            //indicator to check if it's run more than once. Don't dispose of the same thing more than once.
            Console.WriteLine("Resources in memory being disposed of");
        }

        ~Molecule()
        {
            Console.WriteLine("Woo! Garbage collector going");
            Dispose();
        }
    }
}
*/
