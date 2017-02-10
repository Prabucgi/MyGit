using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using TfsCore;

namespace MergeCodeForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            try
            {
                verifyInput();
                btnMerge.Enabled = false;
                string sourceBranch = tbSourceBranch.Text.Trim();
                string targetBranch = tbTargetBranch.Text.Trim();
                var changesetIds = new List<int>();
                tbChangeSets.Text.Split(',').ToList().ForEach(cs => changesetIds.Add(Convert.ToInt32(cs)));
                GetTfsService().MergeChangeSets(sourceBranch, targetBranch, changesetIds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                btnMerge.Enabled = true;
            }
        }

        private void verifyInput()
        {
            if (string.IsNullOrEmpty(tbSourceBranch.Text))
                throw new ApplicationException("Source branch required");
            if (string.IsNullOrEmpty(tbTargetBranch.Text))
                throw new ApplicationException("Target branch required");
            if (string.IsNullOrEmpty(tbChangeSets.Text))
                throw new ApplicationException("Changesets required");
        }

        TfsService GetTfsService()
        {
            var tfsCollection = ConfigurationManager.AppSettings["TfsCollectionUrl"];
            var tfsService = new TfsService(new TfsConnection() { CollectionPath = tfsCollection });
            return tfsService;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            tbSourceBranch.Text = "$/42/Heroma/Utv/Dev2013";
            tbTargetBranch.Text = "$/42/Heroma/Utv/Main";
#endif

        }
    }
}
