using BMS.Entities;
using BMS.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.DataAccessLayer.ConnectedDAL
{
    public class BloodInventoryDAL:IDAL<BmsBloodInventory>
    {
        SqlConnectionStringBuilder builder = null;
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;

        String TABLE_NAME = "dbo.BmsBloodInventory_v1";

        public BloodInventoryDAL(string connectionString)
        {
            builder = new SqlConnectionStringBuilder(connectionString);
            builder.AsynchronousProcessing = true;

            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public bool Insert(BmsBloodInventory value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("INSERT INTO " + TABLE_NAME +
                  @" ( BloodGroup, NumberofBottles,  BloodBankID, ExpiryDate, CreationDate) 
                    VALUES( @BloodGroup, @NumberofBottles, @BloodBankID, @ExpiryDate,  @CreationDate)", connection);

                command.Parameters.Add("@BloodGroup", SqlDbType.VarChar, 3);
                command.Parameters.Add("@NumberofBottles", SqlDbType.Int);
                command.Parameters.Add("@BloodBankID", SqlDbType.Int);
                command.Parameters.Add("@ExpiryDate", SqlDbType.DateTime);
                command.Parameters.Add("@CreationDate", SqlDbType.DateTime);
                

                command.Parameters["@BloodGroup"].Value = value.BloodGroup;
                command.Parameters["@NumberofBottles"].Value = value.NumberofBottles;
                command.Parameters["@BloodBankID"].Value = value.BloodBankID;
                command.Parameters["@ExpiryDate"].Value = value.ExpiryDate;
                command.Parameters["@CreationDate"].Value = value.CreationDate;
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

        public bool Delete(BmsBloodInventory value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("DELETE FROM " + TABLE_NAME +
                      @" WHERE BloodInventoryID = @BloodInventoryID", connection);

                command.Parameters.Add("@BloodInventoryID", SqlDbType.Int);

                command.Parameters["@BloodInventoryID"].Value = value.BloodInventoryID;
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

        public bool Update(BmsBloodInventory value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("UPDATE " + TABLE_NAME +
                   @" SET BloodGroup = @BloodGroup, NumberofBottles = @NumberofBottles, BloodBankID = @BloodBankID, ExpiryDate = @ExpiryDate, CreationDate = @CreationDate
                        WHERE BloodInventoryID = @BloodInventoryID", connection);

               

                command.Parameters.Add("@BloodGroup", SqlDbType.VarChar, 3);
                command.Parameters.Add("@NumberofBottles", SqlDbType.Int);
                command.Parameters.Add("@BloodBankID", SqlDbType.Int);
                command.Parameters.Add("@ExpiryDate", SqlDbType.DateTime);
                command.Parameters.Add("@CreationDate", SqlDbType.DateTime);


                command.Parameters["@BloodGroup"].Value = value.BloodGroup;
                command.Parameters["@NumberofBottles"].Value = value.NumberofBottles;
                command.Parameters["@BloodBankID"].Value = value.BloodBankID;
                command.Parameters["@ExpiryDate"].Value = value.ExpiryDate;
                command.Parameters["@CreationDate"].Value = value.CreationDate;
               
                command.Parameters.Add("@BloodInventoryID", SqlDbType.Int);
                command.Parameters["@BloodInventoryID"].Value = value.BloodInventoryID;
                int rows = command.ExecuteNonQuery();
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

        //public List<BmsBloodInventory> SelectAll()
        //{
        //    List<BmsBloodInventory> list = null;
        //    try
        //    {
        //        list = new List<BmsBloodInventory>();
        //        command = new SqlCommand("SELECT * FROM " + TABLE_NAME + @"", connection);
                
        //        //AsyncCallback selectAllCallback = new AsyncCallback(selectAllCallback);
        //        command.BeginExecuteReader(new AsyncCallback(MySelectAllCallbackFunction), command);

        //    }
        //    catch (ConnectedDalException)
        //    {
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return list;
        //}

        //public void MySelectAllCallbackFunction(IAsyncResult result)
        //{
        //    try
        //    {
        //        // unbox AsyncState back to SqlCommand
        //        SqlCommand cmd = (SqlCommand)result.AsyncState;

        //        using (SqlDataReader reader = command.EndExecuteReader(result))
        //        {
        //            if (reader != null)
        //            {
        //                while (reader.Read())
        //                {
        //                   // Dispatcher
        //                    BmsBloodInventory value = new BmsBloodInventory();
        //                    value.BloodBankID = Convert.ToInt32(reader["BloodBankID"]);
        //                    value.BloodGroup = Convert.ToString(reader["BloodGroup"]);
        //                    value.BloodInventoryID = Convert.ToInt32(reader["BloodInventoryID"]);
        //                    value.CreationDate = Convert.ToDateTime(reader["CreationDate"]);
        //                    value.ExpiryDate = Convert.ToDateTime(reader["ExpiryDate"]);
        //                    value.NumberofBottles = Convert.ToInt32(reader["NumberofBottles"]);

        //                    list.Add(value);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}

        public List<BmsBloodInventory> SelectAll()
        {
            List<BmsBloodInventory> list = null;
            try
            {
                list = new List<BmsBloodInventory>();
                command = new SqlCommand("SELECT * FROM " + TABLE_NAME + @"", connection);
                IAsyncResult result = command.BeginExecuteReader();
                //int count = 0;
                //while (!result.IsCompleted)
                //{
                //    Debug.WriteLine("Waiting {0}", count);
                //    System.Threading.Thread.Sleep(200);
                //    count += 1;
                //}
                //Debug.WriteLine("Command complete. Affected {0} rows.",
                //    );

                using (SqlDataReader reader = command.EndExecuteReader(result))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            BmsBloodInventory value = new BmsBloodInventory();
                            value.BloodBankID = Convert.ToInt32(reader["BloodBankID"]);
                            value.BloodGroup = Convert.ToString(reader["BloodGroup"]);
                            value.BloodInventoryID = Convert.ToInt32(reader["BloodInventoryID"]);
                            value.CreationDate = Convert.ToDateTime(reader["CreationDate"]);
                            value.ExpiryDate = Convert.ToDateTime(reader["ExpiryDate"]);
                            value.NumberofBottles = Convert.ToInt32(reader["NumberofBottles"]);

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


        public List<BmsBloodInventory> SelectAll2()
        {
            List<BmsBloodInventory> list = null;
            try
            {
                list = new List<BmsBloodInventory>();
                command = new SqlCommand("SELECT * FROM " + TABLE_NAME + @"", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            BmsBloodInventory value = new BmsBloodInventory();
                            value.BloodBankID = Convert.ToInt32(reader["BloodBankID"]);
                            value.BloodGroup = Convert.ToString(reader["BloodGroup"]);
                            value.BloodInventoryID = Convert.ToInt32(reader["BloodInventoryID"]);
                            value.CreationDate = Convert.ToDateTime(reader["CreationDate"]);
                            value.ExpiryDate = Convert.ToDateTime(reader["ExpiryDate"]);
                            value.NumberofBottles = Convert.ToInt32(reader["NumberofBottles"]);

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
