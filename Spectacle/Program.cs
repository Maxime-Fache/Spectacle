namespace Spectacle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // déclaration du nom du spectateur et de la variable permetant de continuer
            string userName = "";
            bool continuer = true;

            // création de 2 dresseurs (nom et nom de son singe)
            Dresseur dresseur1 = new Dresseur("Paul", "Ouistiti");
            Dresseur dresseur2 = new Dresseur("Pierre", "Loulou");

            // annonce du début et saisie du nom à l'utilisateur (spectateur)
            Console.WriteLine("Bienvenu au spectacle de singe! Quel est votre nom?");
            userName = Console.ReadLine();
            Console.WriteLine($"Bonjour {userName}, commençons !");

            // création d'un spectateur
            Spectateur s = new Spectateur(userName);

            // tant que l'utilisateur souhaite continuer, le spectacle continue
            while (continuer)
            {
                // le spectateur réagit au tour des dresseurs de singe
                Console.WriteLine(s.reagirTour(dresseur1.faireJouerTour(), dresseur1.getNomSinge()));
                Console.WriteLine(s.reagirTour(dresseur2.faireJouerTour(), dresseur2.getNomSinge()));

                // demande pour continuer
                Console.WriteLine("voulez vous continuer? y / n");
                string rep = Console.ReadLine();
                if (rep == "y")
                    continuer = true;
                else
                    continuer = false;
            }

            // fin du spectacle
            Console.WriteLine("Merci d'avoir participé au spectacle");
        }
    }

    class Spectateur
    {
        private string nom { get; set; }
        public Spectateur(string _nom)
        {
            this.nom = _nom;
        }

        // reçoit le tour du singe ainsi que son nom et crée la petite phrase d'information
        public string reagirTour(Tour _tourJoue, string _nomSinge)
        {
            if (_tourJoue.getType() != null)
            {
                if (_tourJoue.getType() == "Acrobatique")
                    return $"{nom} applaudit pendant le tour d'acrobatie {_tourJoue.getNom()} de {_nomSinge}";
                else
                    return $"{nom} siffle pendant le tour musical {_tourJoue.getNom()} de {_nomSinge}";
            }
            else { return "pas de tours"; }
        }

    }
    
    class Dresseur
    {
        private string nom { get; set; }
        private string nomSinge { get; set; }
        private Singe singe { get; set; }

        public Dresseur(string _nom, string nomSinge)
        {
            this.nom= _nom;
            this.singe= new Singe(nomSinge);
        }

        // récupère le tour joué du singe
        public Tour faireJouerTour()
        {
            return this.singe.jouerTour();
        }

        // récupère le nom du singe du dresseur
        public string getNomSinge()
        {
            return this.singe.getNom();
        }
    }

    class Singe
    {
        private string nom { get; set; }
        private List<Tour> listeDeTours { get; set; }

        Random rnd = new Random();

        // dans  le constructeur, on construit une liste de 3 tours (nom = tour + n°) et choisis aléatoirement le type du tour
        public Singe(string nom)
        {
            this.nom = nom;
            this.listeDeTours = new List<Tour>();
            for (int i = 0; i < 3; i++)
            {
                if(rnd.Next(1, 11)%2 == 1)
                {
                    listeDeTours.Add(new TourAcrobatique($"tour {i+1}"));
                }
                else
                {
                    listeDeTours.Add(new TourMusical($"tour {i + 1}"));
                }
            }
        }

        // sélectionne aléatoirement un tour dans la liste
        public Tour jouerTour()
        {
            int index = rnd.Next(0, 3);
            return listeDeTours[index];
        }

        // récupère le nom du tour
        public string getNom()
        {
            return this.nom;
        }
    }

    // classe abstraite Tour et 2 classes filles correspondant à un tour d'acrobatie et à un tour de musique
    abstract class Tour
    {
        protected string nom { get; set; }
        private string type { get; }

        public Tour(string _nom, string _type)
        {
            this.nom = _nom;
            this.type = _type;
        }

        // récupère le type
        public string getType()
        {
            return type;
        }
        // récupère le nom
        public string getNom()
        {
            return nom;
        }
    }

    // le constructeur établit le type de tour en fonction de la classe
    class TourAcrobatique : Tour
    {
        public TourAcrobatique(string _nom) : base(_nom, "Acrobatique") {}
    }

    class TourMusical : Tour
    {
        public TourMusical(string _nom) : base(_nom, "Musical"){}
    }
}
