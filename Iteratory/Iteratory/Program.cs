using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Iteratory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListaZakupow listaZakupow = new ListaZakupow();
            listaZakupow.AddProdukt(new Produkt("Jajka", 2.50f, 2));
            listaZakupow.AddProdukt(new Produkt("Banany", 5f, 1));
            listaZakupow.AddProdukt(new Produkt("Ciastka", 7f, 5));
            listaZakupow.AddProdukt(new Produkt("Mleko", 8f, 3));
            
            Console.WriteLine("Lista zakupów po kolei jak ją wprowadziliśmy: ");
            Iterator poKolei = listaZakupow.CreateInSequenceIterator();
            while(poKolei.HasNext()) 
            {
                Console.WriteLine(poKolei.GetNext().ToString());
            }
            Console.WriteLine("\nLista zakupów w kolejności alejek od najbliższej do najdalszej: ");
            Iterator jakNajblizej = listaZakupow.CreateClosestIterator();
            while(jakNajblizej.HasNext()) 
            {
                Console.WriteLine(jakNajblizej.GetNext().ToString());
            }


        }
    }
    interface Iterator 
    {
        Produkt GetNext();
        bool HasNext();
    }
    interface IterableCollection
    {
        Iterator CreateInSequenceIterator();
        Iterator CreateClosestIterator();
    }
    class ListaZakupow: IterableCollection 
    {
        List<Produkt> Zakupy;

        public Iterator CreateClosestIterator()
        {
            return new ClosestIterator(Zakupy);
        }

        public Iterator CreateInSequenceIterator()
        {
            return new InSequenceIterator(Zakupy);
        }
        public ListaZakupow()
        {
            Zakupy = new List<Produkt>();
        }
        public void AddProdukt(Produkt produkt) 
        {
            Zakupy.Add(produkt);
        }
    }
    class InSequenceIterator : Iterator
    {
        List<Produkt> Zakupy;
        int currentProductId = 0;
        public InSequenceIterator(List<Produkt> zakupy)
        {
            Zakupy = zakupy;
        }

        public Produkt GetNext()
        {
            return Zakupy[currentProductId++];
        }

        public bool HasNext()
        {
            if(currentProductId >= Zakupy.Count)
            {
                return false;
            }else 
            {
                return true;
            }
        }
    }
    class ClosestIterator : Iterator
    {
        List<Produkt> Zakupy;
        int currentProductId = 0;
        public ClosestIterator(List<Produkt> zakupy)
        {
            Zakupy = zakupy.OrderBy(produkt => produkt.GetAlejka()).ToList();
            
        }

        public Produkt GetNext()
        {
            return Zakupy.ElementAt(currentProductId++);
        }

        public bool HasNext()
        {
            if (currentProductId >= Zakupy.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    class Produkt 
    {
        private string nazwa;
        private float cena;
        private int alejka;
        public Produkt(string nazwa, float cena, int alejka)
        {
            this.nazwa = nazwa;
            this.cena = cena;
            this.alejka = alejka;
        }
        public int GetAlejka() 
        {
            return this.alejka;
        }
        public override string ToString()
        {
            return nazwa;
        }
    }
}
