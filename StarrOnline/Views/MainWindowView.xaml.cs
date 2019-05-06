using System.Windows;
using System.Windows.Input;
using StarrOnline.Common;
using StarrOnline.ViewModels;
using StarrOnline.Models;
using System;
using System.Windows.Controls;
using System.IO;
using System.Diagnostics;

namespace StarrOnline.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
        }

        //private void Export_Click(object sender, RoutedEventArgs e)
        //{
        //    string oFile = GlobalConfig.ReportFiles;
        //    DataRangeView _paramWindow = new DataRangeView();

        //    _paramWindow.Show();
            
        //    //try
        //    //{
        //    //    //DataGrid dg = dataGrid;
        //    //    dgTransactions.SelectAllCells();
        //    //    dgTransactions.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
        //    //    ApplicationCommands.Copy.Execute(null, dgTransactions);
        //    //    //dgTransactions.UnselectAllCells();
        //    //    String Clipboardresult = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
        //    //    StreamWriter swObj = new StreamWriter(oFile);
        //    //    swObj.WriteLine(Clipboardresult);
        //    //    swObj.Close();
        //    //    Process.Start(oFile);
        //    //}catch(Exception ex)
        //    //{
        //    //    MessageBox.Show(ex.Message);
        //    //    return;
        //    //}
        //}

        private void dgTransactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object item = dgTransactions.SelectedItem;
                string val = (dgTransactions.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;

                GlobalConfig.COCNumber = val;
            }catch
            {
                return;
            }
        }
    }
}
