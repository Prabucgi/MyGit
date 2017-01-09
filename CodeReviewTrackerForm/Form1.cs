using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TfsCore;

namespace CodeReviewTrackerForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                btnGet.Enabled = false;
                List<CheckInDetail> checkInDetails = GetTfsService().GetCheckInDetails(new CheckInRequestParam()
                {
                    Branch = new Branch()
                    { Name = cbBranches.Text },
                    StartDate = dtStart.Value,
                    EndDate = dtEnd.Value
                });
                BindGrid(checkInDetails);
                lblTotal.Text = checkInDetails.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                btnGet.Enabled = true;
            }



        }

        private void BindGrid(List<CheckInDetail> checkInDetails)
        {

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();


            dataGridView1.DataSource = checkInDetails;
            DataGridViewLinkColumn idColumn = new DataGridViewLinkColumn
            {
                DataPropertyName = nameof(CheckInDetail.ChangesetId),
                Name = nameof(CheckInDetail.ChangesetId),
                LinkBehavior = LinkBehavior.AlwaysUnderline
            };
            dataGridView1.Columns.Add(idColumn);
            dataGridView1.CellContentClick -= dataGridView1_CellContentClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(CheckInDetail.Owner),
                Name = nameof(CheckInDetail.Owner)
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(CheckInDetail.AssociatedItemsCount),
                Name = nameof(CheckInDetail.AssociatedItemsCount)
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(CheckInDetail.CodeReviewCount),
                Name = nameof(CheckInDetail.CodeReviewCount)
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(CheckInDetail.UserStoryCount),
                Name = nameof(CheckInDetail.UserStoryCount)
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(CheckInDetail.Date),
                Name = nameof(CheckInDetail.Date)
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(CheckInDetail.Comment),
                Name = nameof(CheckInDetail.Comment)
            });
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "Excel Documents (*.xls)|*.xls",
                FileName = "export.xls"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Helper.ToExcel(dataGridView1, sfd.FileName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                tbBranchStart.Text = "$/42/Heroma/Tmp/IPMF";
                LoadBranches();
                dtStart.Value = DateTime.Today.AddDays(-30);
                dtEnd.Value = DateTime.Today;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void LoadBranches()
        {
            List<Branch> branches = GetTfsService().GetBranches(tbBranchStart.Text);
            cbBranches.DataSource = branches;
            cbBranches.DisplayMember = "Name";
        }

        void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0) return;
            var workItem = dataGridView1.Rows[e.RowIndex].DataBoundItem as CheckInDetail;
            if (workItem == null) return;
            var detail = new AssociatedItemsForm
            {
                WorkItems = workItem.AssociatedWorkItems,
                FormTitle = $"{workItem.ChangesetId} - {workItem.Owner}"
            };


            detail.ShowDialog();

        }
        TfsService GetTfsService()
        {

            var tfsCollection = ConfigurationManager.AppSettings["TfsCollectionUrl"];
            var tfsService = new TfsService(new TfsConnection() { CollectionPath = tfsCollection });
            return tfsService;
        }

        private void btnRefreshBranch_Click(object sender, EventArgs e)
        {
            try
            {
                btnRefreshBranch.Enabled = false;
                LoadBranches();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                btnRefreshBranch.Enabled = true;
            }

        }
    }
    public class Helper
    {
        public static void ToExcel(DataGridView dataGridView, string excelFileName)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dataGridView.Columns.Count; j++)
                sHeaders = sHeaders + Convert.ToString(dataGridView.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dataGridView.Rows[i].Cells.Count; j++)
                    stLine = stLine + Convert.ToString(dataGridView.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            var fs = new FileStream(excelFileName, FileMode.Create);
            var bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }
    }
}
