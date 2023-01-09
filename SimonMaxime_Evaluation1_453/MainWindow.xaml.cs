using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimonMaxime_Evaluation1_453
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<string> listeProduits { get; set; }
        public ObservableCollection<string> listeCategories { get; set; }
        private Evaluation1Entities dbEntities;
        private Employe employeCommande = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Méthode qui s'exécute quand la fenêtre est chargé
        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AfficherEmployes();
            RemplirCbx();
        }

        // Chargement et remplissage des comboxbox
        private void RemplirCbx()
        {
            using (dbEntities = new Evaluation1Entities())
            {
                // Remplir la liste de Produits
                cbxProduits.ItemsSource = dbEntities.Produits.ToList();
                cbxProduits.DisplayMemberPath = "NomProduit";

                // Remplir la liste de Categories
                cbxCategories.ItemsSource = dbEntities.Categories.ToList();
                cbxCategories.DisplayMemberPath = "NomCategorie";
            }
        }

        private void AfficherEmployes()
        {
            using (dbEntities = new Evaluation1Entities())
            {
                ListViewEmployes.ItemsSource = dbEntities.Employes.ToList();
                if (ListViewEmployes.SelectedIndex > 0)
                {
                    ListViewEmployes.UnselectAll();
                }
                labelSelectionEmployeNom.Content = "Aucun employé sélectionné";
                btnAjouter.IsEnabled = true;
            }
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            textBoxNom.Text = "";
            textBoxPrenom.Text = "";
            textBoxTitre.Text = "";
            textBoxDateNaissance.Text = "";
            textBoxDateEmbauche.Text = "";
            textBoxAdresse.Text = "";
            textBoxTelephone.Text = "";
            textBoxExtension.Text = "";
            textBoxProvince.Text = "";
            textBoxCodePostal.Text = "";
            textBoxPays.Text = "";
            textBoxNotes.Text = "";
            cbxProduits.Text = "";
            cbxCategories.Text = "";
            AfficherEmployes();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            Employe employeSelectionne = ListViewEmployes.SelectedItem as Employe;

            if (employeSelectionne != null)
            {
                afficherMessageErreur("Impossible d'ajouter un employé pendant la modification d'un autre employé.");
                return;
            }

            if (!textBoxDateNaissance.SelectedDate.HasValue)
            {
                afficherMessageErreur("La date de naissance ne peut pas être vide.");
                return;
            }

            if (!textBoxDateEmbauche.SelectedDate.HasValue)
            {
                afficherMessageErreur("La date d'embauche ne peut pas être vide.");
                return;
            }

            if (textBoxNom.Text == "")
            {
                afficherMessageErreur("Le Nom ne peut pas être vide.");
                return;
            }

            if (textBoxPrenom.Text == "")
            {
                afficherMessageErreur("Le Prénom ne peut pas être vide.");
                return;
            }

            Employe nouveauEmploye = new Employe
            {
                Nom = textBoxNom.Text,
                Prenom = textBoxPrenom.Text,
                Titre = textBoxTitre.Text,
                DateDeNaissance = textBoxDateNaissance.SelectedDate.Value,
                DateEmbauche = textBoxDateEmbauche.SelectedDate.Value,
                Adresse = textBoxAdresse.Text,
                Telephone = textBoxTelephone.Text,
                Extension = textBoxExtension.Text,
                Province = textBoxProvince.Text,
                CodePostal = textBoxCodePostal.Text,
                Pays = textBoxPays.Text,
                Notes = textBoxNotes.Text
            };

            if (nouveauEmploye == null)
            {
                afficherMessageErreur("Le nouvel employé à ajouter ne peut être vide.");
                return;
            }

            using (dbEntities = new Evaluation1Entities())
            {
                dbEntities.Employes.Add(nouveauEmploye);
                int resultat = dbEntities.SaveChanges();
                if (resultat <= 0)
                {
                    afficherMessageErreur("Un problème est survenu lors de la sauvegarde de la base de données.\nCode d'erreur: " + resultat.ToString());
                    return;
                }

                // On rafraichit la liste d'employés
                btnEffacer_Click(sender, e);
            }
        }

        private void afficherMessageErreur(string s)
        {
            MessageBoxResult result = MessageBox.Show(s, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            Employe employeSelectionne = ListViewEmployes.SelectedItem as Employe;
            if (employeSelectionne == null)
            {
                afficherMessageErreur("Aucun employé sélectionné ou employé invalide.");
                return;
            }

            using (dbEntities = new Evaluation1Entities())
            {
                Employe employeModifie = dbEntities.Employes.FirstOrDefault(x => x.EmployeID == employeSelectionne.EmployeID);

                if (employeModifie == null)
                {
                    afficherMessageErreur("Impossible de trouver l'employé '" + employeModifie.Prenom + " " + employeModifie.Nom + "' dans la base de données.");
                    return;
                }
                // On vérifie si les 2 employés sont les mêmes
                if (employeSelectionne == employeModifie)
                {
                    afficherMessageErreur("Aucun changement n'a été fait.");
                    return;
                }

                employeModifie.Nom = textBoxNom.Text;
                employeModifie.Prenom = textBoxPrenom.Text;
                employeModifie.Titre = textBoxTitre.Text;
                employeModifie.DateDeNaissance = textBoxDateNaissance.SelectedDate.Value;
                employeModifie.DateEmbauche = textBoxDateEmbauche.SelectedDate.Value;
                employeModifie.Adresse = textBoxAdresse.Text;
                employeModifie.Telephone = textBoxTelephone.Text;
                employeModifie.Extension = textBoxExtension.Text;
                employeModifie.Province = textBoxProvince.Text;
                employeModifie.CodePostal = textBoxCodePostal.Text;
                employeModifie.Pays = textBoxPays.Text;
                employeModifie.Notes = textBoxNotes.Text;

                int resultat = dbEntities.SaveChanges();

                // Si Un problème survient alors on quitte au lieu de update
                if (resultat <= 0)
                {
                    afficherMessageErreur("Un problème est survenu lors de la sauvegarde de la base de données.\nCode d'erreur: " + resultat.ToString());
                    return;
                }

                btnEffacer_Click(sender, e);
                if (employeCommande != null)
                {
                    if (employeCommande.EmployeID == employeModifie.EmployeID)
                    {
                        labelCommandeEmployeNom.Content = employeModifie.Prenom + " " + employeModifie.Nom;
                        employeCommande = employeModifie;
                    }
                }
            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Employe employeSelectionne = ListViewEmployes.SelectedItem as Employe;

            if (employeSelectionne == null)
            {
                afficherMessageErreur("Aucun employé sélectionné ou employé invalide.");
                return;
            }

            using (dbEntities = new Evaluation1Entities())
            {
                Employe employeASupprime = dbEntities.Employes.SingleOrDefault(employe => employe.EmployeID == employeSelectionne.EmployeID);
                if (employeASupprime == null)
                {
                    afficherMessageErreur("Impossible de trouver l'employé '" + employeASupprime.Prenom + " " + employeASupprime.Nom + "' dans la base de données.");
                    return;
                }

                dbEntities.Employes.Remove(employeASupprime);
                int resultat = dbEntities.SaveChanges();
                if (resultat <= 0)
                {
                    afficherMessageErreur("Un problème est survenu lors de la sauvegarde de la base de données.\nCode d'erreur: " + resultat.ToString());
                    return;
                }

                // On rafraichit la liste d'employés
                btnEffacer_Click(sender, e);
            }
        }

        private void listViewDataBase_Commandes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // On ouvre une deuxième fenêtre avec la liste des clients

            // On récupère le ID de la commandes selectionné
            Commande commandeSelectionne = ListViewCommandes.SelectedItem as Commande;
            if (commandeSelectionne == null)
            {
                //afficherMessageErreur("Aucune commande sélectionnée ou commande invalide.");
                return;
            }

            // On va chercher la liste de clients
            IEnumerable<Client> listeClients;
            using (Evaluation1Entities dbEntities = new Evaluation1Entities())
            {
                listeClients = (
                    from c in dbEntities.Clients
                    where c.ClientID == commandeSelectionne.ClientID
                    select c).ToList();
            }

            // On crée la deuxième fenêtre et on lui envoi l'ojet de la commande selectionnée
            ListeClients fenetreClient = new ListeClients(listeClients);

            // On affiche la fenêtre avec l'option .ShowDialog() ce qui empêche de continuer à utiliser la première fenêtre
            fenetreClient.ShowDialog();
        }

        private void listViewDataBase_Employes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Employe employeSelectionne = ListViewEmployes.SelectedItem as Employe;

            if (employeSelectionne == null)
            {
                //afficherMessageErreur("Aucun employé sélectionné ou employé invalide.");
                return;
            }

            // Code utilisant le Linq
            using (dbEntities = new Evaluation1Entities())
            {
                ListViewCommandes.ItemsSource = (
                    from c in dbEntities.Commandes
                    where c.EmployeID == employeSelectionne.EmployeID
                    select c).ToList();

                labelCommandeEmployeNom.Content = employeSelectionne.Prenom + " " + employeSelectionne.Nom;
            }

            employeCommande = employeSelectionne;
        }

        private void listViewDataBase_Employes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employe employeSelectionne = ListViewEmployes.SelectedItem as Employe;

            if (employeSelectionne == null && ListViewEmployes.SelectedIndex > 0)
            {
                afficherMessageErreur("Aucun employé sélectionné ou employé invalide.");
                return;
            }

            if (employeSelectionne != null)
            {
                textBoxNom.Text = employeSelectionne.Nom;
                textBoxPrenom.Text = employeSelectionne.Prenom;
                textBoxTitre.Text = employeSelectionne.Titre;
                textBoxDateNaissance.Text = employeSelectionne.DateDeNaissance.ToString();
                textBoxDateEmbauche.Text = employeSelectionne.DateEmbauche.ToString();
                textBoxAdresse.Text = employeSelectionne.Adresse;
                textBoxTelephone.Text = employeSelectionne.Telephone;
                textBoxExtension.Text = employeSelectionne.Extension;
                textBoxProvince.Text = employeSelectionne.Province;
                textBoxCodePostal.Text = employeSelectionne.CodePostal;
                textBoxPays.Text = employeSelectionne.Pays;
                textBoxNotes.Text = employeSelectionne.Notes;
                labelSelectionEmployeNom.Content = employeSelectionne.Prenom + " " + employeSelectionne.Nom;

                btnAjouter.IsEnabled = false;
            }
        }
    }
}
