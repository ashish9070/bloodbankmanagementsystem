using BMS.Entities;
using BMS.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.DataAccessLayer.ConnectedDAL
{
    public class DBloodBankDAL:IDAL<BmsBloodBank>
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataAdapter adapter = null;
        DataSet dataSet = null;

        String TABLE_NAME = "dbo.BmsBloodBank_v1";
        
        public DBloodBankDAL(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            adapter = new SqlDataAdapter("SELECT * FROM " + TABLE_NAME, connection);
            dataSet = new DataSet();

            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapter.Fill(dataSet, "VirtualTable");

        }

        public bool Insert(BmsBloodBank value)
        {
            bool isSuccess = false;
            try
            {

                DataRow row = dataSet.Tables["VirtualTable"].NewRow();
                row["BloodBankName"] = value.BloodBankName;
                row["Address"] = value.Address;
                row["City"] = value.City;
                row["ContactNumber"] = value.ContactNumber;
                row["UserID"] = value.UserID;
                row["Password"] = value.Password;
                row["SysId"] = value.SysId;
                row["CreationDate"] = value.CreationDate;


                dataSet.Tables["VirtualTable"].Rows.Add(row);

                //update explicitly
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(dataSet, "VirtualTable");


                isSuccess = true;
            }
            catch (ConnectedDalException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        public bool Delete(BmsBloodBank value)
        {
            bool isSuccess = false;
            try
            {
                DataRow row = dataSet.Tables["VirtualTable"].Rows.Find(value.BloodBankID);
                dataSet.Tables["VirtualTable"].Rows.Remove(row);

                //update explicitly
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(dataSet, "VirtualTable");

                isSuccess = true;
            }
            catch (ConnectedDalException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        public bool Update(BmsBloodBank value)
        {
            bool isSuccess = false;
            try
            {
                DataRow row = dataSet.Tables["VirtualTable"].Rows.Find(value.BloodBankID);
                row["BloodBankName"] = value.BloodBankName;
                row["Address"] = value.Address;
                row["City"] = value.City;
                row["ContactNumber"] = value.ContactNumber;
                row["UserID"] = value.UserID;
                row["Password"] = value.Password;
                row["SysId"] = value.SysId;
                row["CreationDate"] = value.CreationDate;

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(dataSet, "VirtualTable");


                isSuccess = true;
            }
            catch (ConnectedDalException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        public List<BmsBloodBank> SelectAll()
        {
            List<BmsBloodBank> list = null;
            try
            {
                list = new List<BmsBloodBank>();
                DataRowCollection rows = dataSet.Tables["VirtualTable"].Rows;
                if (rows != null)
                {
                    foreach (DataRow reader in rows)
                    {
                        BmsBloodBank value = new BmsBloodBank();
                        value.Address = Convert.ToString(reader["Address"]);
                        value.BloodBankID = Convert.ToInt32(reader["BloodBankID"]);
                        value.BloodBankName = Convert.ToString(reader["BloodBankName"]);
                        value.City = Convert.ToString(reader["City"]);
                        value.ContactNumber = Convert.ToString(reader["ContactNumber"]);
                        value.CreationDate = Convert.ToDateTime(reader["CreationDate"]);
                        value.Password = Convert.ToString(reader["Password"]);
                        value.SysId = Convert.ToString(reader["SysId"]);
                        value.UserID = Convert.ToString(reader["UserID"]);

                        list.Add(value);
                    }
                }
                
            }
            catch (ConnectedDalException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }
    }
}
