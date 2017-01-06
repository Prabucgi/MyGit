using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TfsCore;

namespace CodeReviewTrackerForm
{
    public partial class AssociatedItemsForm : Form
    {
        public AssociatedItemsForm()
        {
            InitializeComponent();
            // Load += AssociatedItemsForm_Load1;
        }

        public List<AssociatedWorkItem> WorkItems;
        public string FormTitle;

        private void AssociatedItemsForm_Load1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AssociatedItemsForm_Load(object sender, EventArgs e)
        {
            Text = FormTitle;
            dataGridView1.AutoGenerateColumns = false;



            dataGridView1.DataSource = WorkItems;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                Name = "Id"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Type",
                Name = "Type"
            }); dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Title",
                Name = "Title"
            }); dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "State",
                Name = "State"
            }); dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AssignedTo",
                Name = "AssignedTo"
            });



        }
    }
}
