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
    public class DBmsTransactionDAL : IDAL<BmsTransaction>
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;

        String TABLE_NAME = "dbo.BmsTransaction_v1";
        public DBmsTransactionDAL(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public bool Insert(BmsTransaction value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("INSERT INTO " + TABLE_NAME +
                 @" ( HospitalID, BloodInventoryID,  NumberofBottles, CreationDate) 
                    VALUES( @HospitalID, @BloodInventoryID, @NumberofBottles, @CreationDate)", connection);

                command.Parameters.Add("@HospitalID", SqlDbType.Int);
                command.Parameters.Add("@BloodInventoryID", SqlDbType.Int);
                command.Parameters.Add("@NumberofBottles", SqlDbType.Int);
                command.Parameters.Add("@CreationDate", SqlDbType.DateTime);


                command.Parameters["@HospitalID"].Value = value.HospitalID;
                command.Parameters["@BloodInventoryID"].Value = value.BloodInventoryID;
                command.Parameters["@NumberofBottles"].Value = value.NumberofBottles;
                command.Parameters["@CreationDate"].Value = value.CreationDate;
                

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

        public bool Delete(BmsTransaction value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("DELETE FROM " + TABLE_NAME +
                     @" WHERE TransactionID = @TransactionID", connection);

                command.Parameters.Add("@TransactionID", SqlDbType.Int);

                command.Parameters["@TransactionID"].Value = value.TransactionID;
                command.ExecuteNonQuery();
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

        public bool Update(BmsTransaction value)
        {
            bool isSuccess = false;
            try
            {

                command = new SqlCommand("UPDATE " + TABLE_NAME +
                  @" SET HospitalID = @HospitalID, BloodInventoryID = @BloodInventoryID, NumberofBottles = @NumberofBottles, CreationDate = @CreationDate
                        WHERE TransactionID = @TransactionID", connection);


                command.Parameters.Add("@HospitalID", SqlDbType.Int);
                command.Parameters.Add("@BloodInventoryID", SqlDbType.Int);
                command.Parameters.Add("@NumberofBottles", SqlDbType.Int);
                command.Parameters.Add("@CreationDate", SqlDbType.DateTime);


                command.Parameters["@HospitalID"].Value = value.HospitalID;
                command.Parameters["@BloodInventoryID"].Value = value.BloodInventoryID;
                command.Parameters["@NumberofBottles"].Value = value.NumberofBottles;
                command.Parameters["@CreationDate"].Value = value.CreationDate;

                command.Parameters.Add("@TransactionID", SqlDbType.Int);

                command.Parameters["@TransactionID"].Value = value.HospitalID;

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

        public List<BmsTransaction> SelectAll()
        {
            List<BmsTransaction> list = null;
            try
            {
                list = new List<BmsTransaction>();
                command = new SqlCommand("SELECT * FROM " + TABLE_NAME + @"", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            BmsTransaction value = new BmsTransaction();
                            value.BloodInventoryID = Convert.ToInt32(reader["BloodInventoryID"]);
                            value.CreationDate = Convert.ToDateTime(reader["CreationDate"]);
                            value.HospitalID = Convert.ToInt32(reader["HospitalID"]);
                            value.NumberofBottles = Convert.ToInt32(reader["NumberofBottles"]);
                            value.TransactionID = Convert.ToInt32(reader["TransactionID"]);

                            list.Add(value);
                        }
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
