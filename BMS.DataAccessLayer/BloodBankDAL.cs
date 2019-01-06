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
    public class BloodBankDAL:IDAL<BmsBloodBank>
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;

        String TABLE_NAME = "dbo.BmsBloodBank_v1";
        
        public BloodBankDAL(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public bool Insert(BmsBloodBank value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("INSERT INTO " + TABLE_NAME +
                    @" ( BloodBankName, Address, City, ContactNumber, UserID, Password, SysId, CreationDate) 
                    VALUES( @BloodBankName, @Address, @City, @ContactNumber, @UserID, @Password, @SysId, @CreationDate)", connection);

                command.Parameters.Add("@BloodBankName", SqlDbType.VarChar, 50);
                command.Parameters.Add("@Address", SqlDbType.VarChar, 50);
                command.Parameters.Add("@City", SqlDbType.VarChar, 20);
                command.Parameters.Add("@ContactNumber", SqlDbType.VarChar, 15);
                command.Parameters.Add("@UserID", SqlDbType.VarChar, 50);
                command.Parameters.Add("@Password", SqlDbType.VarChar, 255);
                command.Parameters.Add("@SysId", SqlDbType.VarChar, 128);
                command.Parameters.Add("@CreationDate", SqlDbType.Date);


                command.Parameters["@BloodBankName"].Value = value.BloodBankName;
                command.Parameters["@Address"].Value = value.Address;
                command.Parameters["@City"].Value = value.City;
                command.Parameters["@ContactNumber"].Value = value.ContactNumber;
                command.Parameters["@UserID"].Value = value.UserID;
                command.Parameters["@Password"].Value = value.Password;
                command.Parameters["@SysId"].Value = value.SysId;
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

        public bool Delete(BmsBloodBank value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("DELETE FROM " + TABLE_NAME +
                    @" WHERE BloodBankID = @BloodBankID", connection);

                command.Parameters.Add("@BloodBankID", SqlDbType.Int);

                command.Parameters["@BloodBankID"].Value = value.BloodBankID;
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

        public bool Update(BmsBloodBank value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("UPDATE " + TABLE_NAME +
                    @" SET BloodBankName = @BloodBankName, Address = @Address, City = @City,
                        ContactNumber = @ContactNumber, UserID = @UserID, Password = @Password,
                        SysId = @SysId, CreationDate = @CreationDate
                        WHERE BloodBankID = @BloodBankID", connection);

                command.Parameters.Add("@BloodBankName", SqlDbType.VarChar, 50);
                command.Parameters.Add("@Address", SqlDbType.VarChar, 50);
                command.Parameters.Add("@City", SqlDbType.VarChar, 20);
                command.Parameters.Add("@ContactNumber", SqlDbType.VarChar, 15);
                command.Parameters.Add("@UserID", SqlDbType.VarChar, 50);
                command.Parameters.Add("@Password", SqlDbType.VarChar, 255);
                command.Parameters.Add("@SysId", SqlDbType.VarChar, 128);
                command.Parameters.Add("@CreationDate", SqlDbType.Date);


                command.Parameters["@BloodBankName"].Value = value.BloodBankName;
                command.Parameters["@Address"].Value = value.Address;
                command.Parameters["@City"].Value = value.City;
                command.Parameters["@ContactNumber"].Value = value.ContactNumber;
                command.Parameters["@UserID"].Value = value.UserID;
                command.Parameters["@Password"].Value = value.Password;
                command.Parameters["@SysId"].Value = value.SysId;
                command.Parameters["@CreationDate"].Value = value.CreationDate;

                command.Parameters.Add("@BloodBankID", SqlDbType.Int);

                command.Parameters["@BloodBankID"].Value = value.BloodBankID;

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

        public List<BmsBloodBank> SelectAll()
        {
            List<BmsBloodBank> list = null;
            try
            {
                list = new List<BmsBloodBank>();
                command = new SqlCommand("SELECT * FROM " + TABLE_NAME + @"", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
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
