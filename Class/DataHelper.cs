using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace pbERP.Class
{
    public static class DataHelper
    {
        public static string ConStrForAll = WebConfigurationManager.ConnectionStrings["DbConStr"].ConnectionString;
        private static string ConnectionString = WebConfigurationManager.ConnectionStrings["DbConStr"].ConnectionString;
        private static SqlConnection con;
        private static SqlCommand cmd;
        private static SqlDataReader reader;
        private static string strSql = "";


        private static SqlConnection _Connection;

        private static SqlConnection Connection
        {
            get
            {
                if (_Connection == null)
                    _Connection = new SqlConnection(ConnectionString);
                return _Connection;
            }
        }

        public static DataSet NewGetFunctionDS(DataTable datatable, string WhereClause)
        {
            string qry = String.Format("SELECT * {0}", datatable.TableName + " " + WhereClause);
            SqlConnection con = new SqlConnection(ConnectionString);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(qry, con);
            adapter.Fill(ds);
            return ds;
        }

        public static SqlConnection getConnection()
        {
            return _Connection;
        }
        private static SqlDataAdapter GetAdapter(string sql)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(
                new SqlCommand(sql, Connection));
            return adapter;
        }

        public static DataSet GetData(string sql)
        {
            DataSet ds = new DataSet();
            GetAdapter(sql).Fill(ds);
            return ds;
        }

        public static DataSet GetTableData(string tableName)
        {
            return GetData(String.Format("SELECT * FROM {0}", tableName));
        }

        public static DataSet GetTableData(DataTable datatable)
        {
            string tableName = datatable.TableName;
            return GetData(String.Format("SELECT * FROM {0}", tableName));
        }

        public static DataSet GetTableData(string tableName, string WhereClause)
        {
            string qry = String.Format("SELECT * FROM {0}", tableName + " " + WhereClause);
            return GetData(qry);
        }
        public static DataSet GetTableData(DataTable datatable, string WhereClause)
        {
            string qry = String.Format("SELECT * FROM {0}", datatable.TableName + " " + WhereClause);
            return GetData(qry);
        }

        public static int UpdateData(DataSet ds, string tableName)
        {
            return UpdateData(ds.Tables[tableName]);
        }

        public static int UpdateData(DataTable dt)
        {
            SqlDataAdapter adapter = GetAdapter(String.Format("SELECT * FROM {0}", dt.TableName));
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            int rowsEffected = adapter.Update(dt);
            return rowsEffected;
        }
        public static int UpdateData(DataTable dt, string tableName)
        {
            SqlDataAdapter adapter = GetAdapter(String.Format("SELECT * FROM {0}", tableName));
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            
            int rowsEffected = adapter.Update(dt);
            return rowsEffected;
        }
        public static DataSet GetDataBySP(string spName)
        {
            SqlCommand cmd = new SqlCommand("[" + spName + "]", _Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }
        public static DataSet GetDataBySP(string spName,String[][] Parameters)
        {
            SqlCommand cmd = new SqlCommand("[" + spName + "]", _Connection);
            for (int i = 0; i < Parameters.Length; i++)
			{
                cmd.Parameters.Add(new SqlParameter(Parameters[i][0],Parameters[i][1]));
                
			}
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ds);
            return ds;
        }

        public static int DeleteAllData(DataTable datatable)
        {
            SqlDataAdapter adapter = GetAdapter(String.Format("SELECT * FROM {0}", datatable.TableName));
            adapter.DeleteCommand = new SqlCommand("DELETE FROM " + datatable.TableName.ToString(), adapter.SelectCommand.Connection);
            adapter.DeleteCommand.Connection.Open();
            int rowsEffected = adapter.DeleteCommand.ExecuteNonQuery();
            adapter.DeleteCommand.Connection.Close();
            return rowsEffected;
        }

        //-----------------New function----------------//

        public static SqlDataReader getReader(string query)
        {
            
            con = new SqlConnection(ConnectionString);
            con.Open();
            cmd = new SqlCommand(query);
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();
            
            return reader;
        }

        public static Int32 getTableMaxId_Sql(string columnName, string tableName)
        {
            Int32 MaxId = 0;
            string strSql = "Select IsNull(Max(" + columnName + "),0)+1 As MaxId From " + tableName;
            con = new SqlConnection(ConnectionString);
            con.Open();
            cmd = new SqlCommand(strSql);
            cmd.CommandText = strSql;
            cmd.Connection = con;
            SqlDataReader reader = cmd.ExecuteReader();

            //for (int i = 0; i > MaxId; i++)
            {

                if (reader.Read())
                {
                    MaxId = (Int32)reader[0];
                }

                return MaxId;
            }
        }

        public static Int32 executeNonQuery(string strQuery)
        {
            int result = 0;
            try
            {
                con = new SqlConnection(ConnectionString);
                cmd = new SqlCommand(strQuery);
                cmd.Connection = con;
                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
                con.Dispose();

            
                return result;
            }
            catch (Exception)
            {

                return result;
            }
        }

        public static Int32 executeNonQuerySP(string query)
        {
            int result = 0;
            con = new SqlConnection(ConnectionString);
            cmd = new SqlCommand(query);
            cmd.Connection = con;
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();

            
            return result;
        }

        public static Int32 executeNonQueryTransaction(List<string> query, int counter)
        {
            int result = 0;
            SqlConnection db = new SqlConnection(ConnectionString);
            SqlTransaction transaction;
            db.Open();
            transaction = db.BeginTransaction();
            try
            {
                for (int i = 0; i < counter; i++)
                {
                    new SqlCommand(query[i], db, transaction).ExecuteNonQuery();
                }
                transaction.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return 0;
            }
            db.Close();
        }

        public static Int32 exeTranWithDetTable(string query, SqlConnection db, SqlTransaction transaction)
        {
            int result = 0;
            try
            {
                new SqlCommand(query, db, transaction).ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static Int32 executeNonQuerySP(string spName, String[][] Parameters)
        {
            con = new SqlConnection(ConnectionString);
            cmd = new SqlCommand("[" + spName + "]");

            for (int i = 0; i < Parameters.Length; i++)
            {
                cmd.Parameters.Add(new SqlParameter(Parameters[i][0], Parameters[i][1]));
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();

            int result = 0;
            return result;
        }

        public static void DataloadToGrid(string strSql,GridView grid)
        {
            DataTable dt = GetData(strSql).Tables[0];
            grid.DataSource = dt;
            grid.DataBind();
        }

        public static void DataLoadToDropDownList(string strQuery, DropDownList ddl, string txtField, string vluField)
        {
            //ddl.Items.Add(new ListItem("", ""));
            DataTable dt = GetData(strQuery).Tables[0];
            ddl.DataSource = dt;
            ddl.DataTextField = txtField;
            ddl.DataValueField = vluField;
            ddl.DataBind();
        }

        public static List<string> getSearchList(string query)
        {

            List<string> ListResult = new List<string>();
            DataTable dt = DataHelper.GetData(query).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListResult.Add(dt.Rows[i][0].ToString());
                }
            }

            return ListResult;
        }
        //getID("ChartOfAccountTypeId", "tblACC_ChartOfAccount", "ChartOfAccountName", "=", txtCredited.Text);
        public static int getID(string ColumnName, string tableName, string WhereColumn, string optr,string WhereColumnValue)
        {
            int getResult=0;
            string query = "Select " + ColumnName + " From " + tableName + " Where " + WhereColumn + optr + "'"+WhereColumnValue+"'";
            DataTable dt = GetData(query).Tables[0];
            if (dt.Rows.Count > 0)
            {
                getResult =int.Parse(dt.Rows[0][0].ToString());
            }

            return getResult;
        }

        // Date Formate Class 
        public static string datetest(string year, string month, string day)
        {
            string y = year.Substring(6, 4);
            string m = month.Substring(3, 2);
            string d = day.Substring(0, 2);
            return y + "-" + m + "-" + d;
        }
        public static string MakeChartOfAccountNo(int ChartOfAccountId, int ChartOfAccountTypeId)
        {
            DataTable dt = new DataTable();
            string ChartOfAccountNo = "";

            dt = DataHelper.GetData("Select IsNull(max(Right(ChartOfAccountNo,4)),0)+1 From tblACC_ChartOfAccount").Tables[0];
            int maxNo = 0;
            if (dt.Rows.Count > 0)
            {
                maxNo = int.Parse(dt.Rows[0][0].ToString());
            }

            dt = DataHelper.GetData("Select ChartOfAccountNo From tblACC_ChartOfAccount Where ChartOfAccountId='" + ChartOfAccountId + "'").Tables[0];
            string AccNo = "";
            if (dt.Rows.Count > 0)
            {
                AccNo = dt.Rows[0][0].ToString();
            }

            if (maxNo.ToString().Length == 1)
            {
                ChartOfAccountNo = AccNo.Substring(0, 2) + "-0" + ChartOfAccountTypeId + "-000" + maxNo;
            }

            if (maxNo.ToString().Length == 2)
            {
                ChartOfAccountNo = AccNo.Substring(0, 2) + "-0" + ChartOfAccountTypeId + "-00" + maxNo;
            }

            if (maxNo.ToString().Length == 3)
            {
                ChartOfAccountNo = AccNo.Substring(0, 2) + "-0" + ChartOfAccountTypeId + "-0" + maxNo;
            }

            if (maxNo.ToString().Length == 4)
            {
                ChartOfAccountNo = AccNo.Substring(0, 2) + "-0" + ChartOfAccountTypeId + "-" + maxNo;
            }

            return ChartOfAccountNo;
        }
    }
}


//SqlConnection db = new SqlConnection(DataHelper.ConStrForAll);
//            SqlTransaction transaction;
//            db.Open();
//            transaction = db.BeginTransaction();
//            try
//            {
//                string query = "";
//                query = "Exec SPInsert 'Sample Request','" + txtSampleRequestId.Text + "','" + txtSampleDescription.Text + "','" + txtSampleRequestQty.Text + "','" + RegisterId + "','" + clnRequestDate.SelectedDate + "','" + JobStatusId + "','" + userID + "','" + EmployeeId + "','" + txtJobNumber.Text + "'";

//                new SqlCommand(query, db, transaction).ExecuteNonQuery();

//                transaction.Commit();
//                lblErrorMsg.Text = "Data Saved Successfully!!!";
//                clearForm();

//            }
//            catch (Exception)
//            {
//                lblErrorMsg.Text = "Error found," + " Data not saved." + " Please try again!!!";
//            }
//            db.Close();