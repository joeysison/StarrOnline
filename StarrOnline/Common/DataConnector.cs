using Dapper;
using StarrOnline.Interface;
using StarrOnline.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace StarrOnline.Common
{
    public class DataConnector : IDataConnection
    {
        public string _errorMessage;
        public bool _IsError;

        public void createPlanType()
        {
            throw new NotImplementedException();
        }

        public void createTransactions(Transactions model)
        {
            throw new NotImplementedException();
        }

        public string GetPremiumAmt(string strSQL, int _ProductType, int _PlanOption, int _PlanType, int NoOfDays)
        {
            string output;
            int _out;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStringData))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@ProductType", _ProductType, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PlanOption", _PlanOption, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@PlanType", _PlanType, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@NoOfDays", NoOfDays, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Premium", DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute(strSQL, parameter, commandType: CommandType.StoredProcedure);

                _out = parameter.Get<int>("@Premium");
            }

            output = _out.ToString("#,##0.00");
            return output;
        }

        public void savePassword(string newPass, int userID)
        {
            try
            {

                using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionStringData))
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@newPassword", newPass, DbType.String, ParameterDirection.Input);
                    parameter.Add("@createdBy", userID, DbType.Int32, ParameterDirection.Input);

                    connection.Execute(DefaultValues.savePassword, parameter, commandType: CommandType.StoredProcedure);

                    _IsError = false;
                    return;
                }

            }
            catch (SqlException sqlex)
            {
                _IsError = true;
                _errorMessage = sqlex.Message;
                return;
            }
        }

        public void cancelData(string COCNo, int ModifiedBy)
        {
            string strSQL = DefaultValues.cancelTransaction;
            
            try{

                using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionStringData))
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@COCNo", COCNo, DbType.String, ParameterDirection.Input);
                    parameter.Add("@ModifiedBy", ModifiedBy, DbType.Int32, ParameterDirection.Input);

                    connection.Execute(strSQL, parameter, commandType: CommandType.StoredProcedure);

                    //var _out = parameter.Get<string>("@CocNO");

                    _IsError = false;
                    return;
                }

            }catch(SqlException sqlex)
            {
                _IsError = true;
                _errorMessage = sqlex.Message;
                return;
            }

        }

        public string saveTransaction(Transaction _transaction)
        {
            string strSQL = DefaultValues.saveTransaction;

            try
            {
                using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionStringData))
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@masterpolicy", _transaction.MASTERPOLICY, DbType.String, ParameterDirection.Input);
                    parameter.Add("@firstname", _transaction.FIRSTNAME, DbType.String, ParameterDirection.Input);
                    parameter.Add("@lastname", _transaction.LASTNAME, DbType.String, ParameterDirection.Input);
                    parameter.Add("@birthdate", _transaction.BIRTHDATE, DbType.Date, ParameterDirection.Input);
                    parameter.Add("@producttype", _transaction.PRODUCTTYPE, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@planoption", _transaction.PLANOPTION, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@plantype", _transaction.PLANTYPE, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@reloc", _transaction.RELOC, DbType.String, ParameterDirection.Input);
                    parameter.Add("@coverage", _transaction.COVERAGE, DbType.String, ParameterDirection.Input);
                    parameter.Add("@issuedate", _transaction.ISSUEDATE, DbType.Date, ParameterDirection.Input);
                    parameter.Add("@traveldatefrom", _transaction.TRAVELDATEFROM, DbType.Date, ParameterDirection.Input);
                    parameter.Add("@traveldateto", _transaction.TRAVELDATETO, DbType.Date, ParameterDirection.Input);
                    parameter.Add("@noofdays", _transaction.NOOFDAYS, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@netpremiums", Convert.ToDouble(_transaction.PREMIUM), DbType.Decimal, ParameterDirection.Input);
                    parameter.Add("@company", _transaction.COMPANY, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@remarks", _transaction.REMARKS, DbType.String, ParameterDirection.Input);
                    parameter.Add("@createdBy", GlobalConfig.userID, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@CocNO", "", DbType.String, direction: ParameterDirection.InputOutput);

                    connection.Execute(strSQL, parameter, commandType: CommandType.StoredProcedure);

                    var _out = parameter.Get<string>("@CocNO");
                    
                    _IsError = false;
                    return _out;
                }
            }
            catch (SqlException sqlex)
            {
                _IsError = true;
                _errorMessage = sqlex.Message;
                return "";
            }
        }

        public List<Transactions> GetTransactions_All(string strSQL)
        {
            List<Transactions> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStringData))
            {
                output = connection.Query<Transactions>(strSQL).ToList();
            }

            return output;
        }

        public List<ProductType> GetProductType(string strSQL)
        {
            List<ProductType> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStringData))
            {
                output = connection.Query<ProductType>(strSQL).ToList();
            }

            return output;
        }

        public List<PlanOption> GetPlanOption(string strSQL)
        {
            List<PlanOption> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStringData))
            {
                output = connection.Query<PlanOption>(strSQL).ToList();
            }

            return output;
        }

        public List<PlanType> GetPlanType(string strSQL)
        {
            List<PlanType> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStringData))
            {
                output = connection.Query<PlanType>(strSQL).ToList();
            }

            return output;
        }

        public List<UserGroupList> GetUserGroupList(string strSQL)
        {
            List<UserGroupList> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStringData))
            {
                output = connection.Query<UserGroupList>(strSQL).ToList();
            }

            return output;
        }


    }
}
