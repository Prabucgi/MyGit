using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using TfsCore;

namespace ChangeSetLinkerForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLinkChangeSet_Click(object sender, EventArgs e)
        {
            try
            {
                btnLinkChangeSet.Enabled = false;
                int workItem = Convert.ToInt32(tbWorkItem.Text);
                var ids = new List<int>();
                GetTfsService().LinkChangeSets(workItem, ids);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                btnLinkChangeSet.Enabled = true;
            }
        }

        TfsService GetTfsService()
        {
            var tfsCollection = ConfigurationManager.AppSettings["TfsCollectionUrl"];
            var tfsService = new TfsService(new TfsConnection() { CollectionPath = tfsCollection });
            return tfsService;
        }
    }
}
