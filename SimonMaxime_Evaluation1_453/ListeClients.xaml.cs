using System;
using System.Collections.Generic;
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
    /// Interaction logic for ListeClients.xaml
    /// </summary>
    public partial class ListeClients : Window
    {
        public ListeClients()
        {
            InitializeComponent();
            AfficherClients();
        }

        private void AfficherClients()
        {
            using (Evaluation1Entities dbEntities = new Evaluation1Entities())
            {
                listViewDataBase_Clients.ItemsSource = dbEntities.Clients.ToList();
            }
        }
    }

    
}
