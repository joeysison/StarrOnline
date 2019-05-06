using Caliburn.Micro;
using StarrOnline.Common;
using StarrOnline.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace StarrOnline.Views
{
    /// <summary>
    /// Interaction logic for DataRangeView.xaml
    /// </summary>
    public partial class DataRangeView : Window
    {

        public DataRangeView()
        {
            InitializeComponent();           
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string paramStatus = "";
            string paramCluster = "";

            if (cmbStatus.Text.Equals("BOTH"))
            {
                paramStatus = "%";
            }
            else
            {
                paramStatus = cmbStatus.Text;
            }

            if (string.IsNullOrEmpty(cmbGroup.Text))
            {
                paramCluster = "%";
            }
            else
            {
                paramCluster = cmbGroup.Text;
            }

            string query = "Select * from " + GlobalConfig.SOAReport + " where DATEISSUE between '" + dtFrom.SelectedDate.Value.ToString("MM/dd/yyyy") +
                "' and '" + dtTo.SelectedDate.Value.ToString("MM/dd/yyyy") + "' and STATUS like '" + paramStatus + "' and ClusterGrp like '" + paramCluster + "'";
            
            string fileName = GlobalConfig.FileExport + "\\ReportData" + dtFrom.SelectedDate.Value.ToString("MMddyyyy") + "_" + dtTo.SelectedDate.Value.ToString("MMddyyyy") + ".csv";

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(fileName))
            {
                using (SqlConnection conn = new SqlConnection(GlobalConfig.ConnectionStringData))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Connection = conn;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);

                                //Build the CSV file data as a Comma separated string.
                                string csv = string.Empty;

                                foreach (DataColumn column in dt.Columns)
                                {
                                    //Add the Header row for CSV file.
                                    csv += column.ColumnName + ',';
                                }

                                //Add new line.
                                csv += "\r\n";

                                foreach (DataRow row in dt.Rows)
                                {
                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        //Add the Data rows.
                                        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                                    }

                                    //Add new line.
                                    csv += "\r\n";
                                }

                                fs.Write(csv);

                            }
                        }
                    }
                }

                fs.Close();
            }

            MessageBox.Show("Report has been successfully extracted...", GlobalConfig.AppName, MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

    }
}
