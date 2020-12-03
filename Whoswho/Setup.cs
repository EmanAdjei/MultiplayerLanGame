using System;
using System.Windows.Forms;

namespace Whoswho
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }
        
        // Launches the program in Server Mode
        private void BtnServer_Click(object sender, EventArgs e)
        {
            Hide();
            Server server = new Server();
            server.Closed += (s, args) => Close();
            server.Show();            
        }

        // Launches the program in Client Mode
        private void BtnClient_Click(object sender, EventArgs e)
        { 
            Hide();
            MainMenu menu = new MainMenu();
            menu.Closed += (s, args) => Close();
            menu.Show();            
        }
    }
}
