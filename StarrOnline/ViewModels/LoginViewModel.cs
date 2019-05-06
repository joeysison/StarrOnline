using Caliburn.Micro;
using StarrOnline.Interface;
using System;
using System.Windows;
using StarrOnline.Common;
using System.Data.SqlClient;
using System.Data;
using StarrOnline.Models;
using StarrOnline.ConfigSetup;

namespace StarrOnline.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        #region Property
        #region Username
        private string _username;
        private dataSetup _config;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    NotifyOfPropertyChange(() => Username);
                }
            }
        }
        #endregion
        #region Password
        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    NotifyOfPropertyChange(() => Password);
                }
            }
        }
        #endregion
        #region DBMEthod
        private int getUserID(string vUserName, string vPassword)
        {
            SqlConnection connection = new SqlConnection(GlobalConfig.ConnectionStringData);

            connection.Open();

            SqlCommand objCmd = new SqlCommand();

            int userid;

            try
            {
                objCmd.CommandText = DefaultValues.getUserID;
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Connection = connection;

                objCmd.Parameters.Add(new SqlParameter("@LoginName", SqlDbType.VarChar, 20));
                objCmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 20));
                objCmd.Parameters.Add(new SqlParameter("@iUserID", SqlDbType.Int));
                objCmd.Parameters.Add(new SqlParameter("@iCompanyID", SqlDbType.Int));
                objCmd.Parameters.Add(new SqlParameter("@sPolicyINT", SqlDbType.VarChar,15));
                objCmd.Parameters.Add(new SqlParameter("@sPolicyDOM", SqlDbType.VarChar, 15));
                objCmd.Parameters.Add(new SqlParameter("@sCompanyCode", SqlDbType.VarChar,4));
                objCmd.Parameters.Add(new SqlParameter("@sUserGroup", SqlDbType.VarChar, 10));
                objCmd.Parameters.Add(new SqlParameter("@Logged", SqlDbType.VarChar, 50));

                objCmd.Parameters["@LoginName"].Value = vUserName;
                objCmd.Parameters["@Password"].Value = vPassword;
                objCmd.Parameters["@iUserID"].Direction = ParameterDirection.Output;
                objCmd.Parameters["@iCompanyID"].Direction = ParameterDirection.Output;
                objCmd.Parameters["@sPolicyINT"].Direction = ParameterDirection.Output;
                objCmd.Parameters["@sPolicyDOM"].Direction = ParameterDirection.Output;
                objCmd.Parameters["@sCompanyCode"].Direction = ParameterDirection.Output;
                objCmd.Parameters["@sUserGroup"].Direction = ParameterDirection.Output;
                objCmd.Parameters["@Logged"].Direction = ParameterDirection.Output;

                objCmd.ExecuteNonQuery();

                if (Convert.ToInt32(objCmd.Parameters["@iUserID"].Value) != -1)
                {
                    GlobalConfig.userName = objCmd.Parameters["@Logged"].Value.ToString();
                    GlobalConfig.companyID = Convert.ToInt32(objCmd.Parameters["@iCompanyID"].Value);
                    GlobalConfig.policyNoINT = objCmd.Parameters["@sPolicyINT"].Value.ToString();
                    GlobalConfig.policyNoDOM = objCmd.Parameters["@sPolicyDOM"].Value.ToString();
                    GlobalConfig.userGroup = objCmd.Parameters["@suserGroup"].Value.ToString();
                    GlobalConfig.companyCode = objCmd.Parameters["@sCompanyCode"].Value.ToString();
                }

                userid = Convert.ToInt32(objCmd.Parameters["@iUserID"].Value);
                
                connection.Close();
                objCmd = null;
                connection = null;

                return userid;


            }
            catch (SqlException ex)
            {
                //isError = true;
                //_error = ex.Message;
                MessageBox.Show(ex.Message, "Error: ", MessageBoxButton.OK, MessageBoxImage.Error);

                return -1;
            }
        }
        #endregion
        #endregion Property


        #region MethodXAML
        public LoginViewModel(Interface.INavigationService navigationService, IWindowManager windowManager, ISettingsService SettingService)
            : base(navigationService, windowManager, SettingService)
        {
            //_dBManagerService = dBManagerService;
            //_dBManagerService
            //LoadDBSettings();
            _config = new dataSetup();
            _config.createXMLDBFolder();

            readConfiguration();

        }

        public void readConfiguration()
        {
            try
            {
                GlobalConfig.ConnectionStringData = _config.readSetupTable("DatabaseConnection");
                GlobalConfig.SelectedGDS = _config.readSetupTable("SelectedGDS");
                GlobalConfig.AgentName = _config.readSetupTable("AgentName");
                GlobalConfig.AgentIDAmadeus = _config.readSetupTable("AgentIDAmadeus");
                GlobalConfig.AgentIDSabre = _config.readSetupTable("AgentIDSabre");
                GlobalConfig.FileExport = _config.readSetupTable("exportFile");
                GlobalConfig.SOAReport = _config.readSetupTable("reportSOA");
                GlobalConfig.GRPReport = _config.readSetupTable("reportGRP");
                GlobalConfig.AppName = _config.readSetupTable("AppName");
                GlobalConfig.ApplyFilter = _config.readSetupTable("applyFilter");
                GlobalConfig.ReportFiles = _config.readSetupTable("reportFiles");
                //GlobalConfig.reportData = _config.readSetupTable("report1");


                //_db.InitializeConnection();
            }
            catch (LibraryErrorException e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        public void Login()
        {
            try
            {
                GlobalConfig.userID = getUserID(Username, Password);
    
                if (GlobalConfig.userID == -1)
                {
                    MessageBox.Show("Invalid user name / password", ":: Error :: ", MessageBoxButton.OK, MessageBoxImage.Error);
                    TryClose();
                }else
                {
                    TryClose(true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(App.GetString("CouldntLogIn") + "\r\n\r\n" + ex.Message, App.GetString("Error"), MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion MethodView

    }
}
