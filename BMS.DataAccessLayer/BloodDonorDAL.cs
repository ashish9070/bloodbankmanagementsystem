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
    public class BloodDonorDAL:IDAL<BmsBloodDonor>
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;

        String TABLE_NAME = "dbo.BmsBloodDonor_v1";


        public BloodDonorDAL(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public bool Insert(BmsBloodDonor value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("INSERT INTO " + TABLE_NAME +
                   @" ( FirstName, LastName,  Address, City, MobileNo, BloodGroup, CreationDate) 
                    VALUES( @FirstName, @LastName, @Address, @City, @MobileNo, @BloodGroup, @CreationDate)", connection);

                command.Parameters.Add("@FirstName", SqlDbType.VarChar, 20);
                command.Parameters.Add("@LastName", SqlDbType.VarChar, 20);
                
                command.Parameters.Add("@Address", SqlDbType.VarChar, 50);
                command.Parameters.Add("@City", SqlDbType.VarChar, 20);
                command.Parameters.Add("@MobileNo", SqlDbType.VarChar, 15);
                command.Parameters.Add("@BloodGroup", SqlDbType.VarChar, 3);
                command.Parameters.Add("@CreationDate", SqlDbType.DateTime);


                command.Parameters["@FirstName"].Value = value.FirstName;
                command.Parameters["@LastName"].Value = value.LastName;

                
                command.Parameters["@Address"].Value = value.Address;
                command.Parameters["@City"].Value = value.City;
                command.Parameters["@MobileNo"].Value = value.MobileNo;
                command.Parameters["@BloodGroup"].Value = value.BloodGroup;
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

        public bool Delete(BmsBloodDonor value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("DELETE FROM " + TABLE_NAME +
                      @" WHERE BloodDonorID = @BloodDonorID", connection);

                command.Parameters.Add("@BloodDonorID", SqlDbType.Int);

                command.Parameters["@BloodDonorID"].Value = value.BloodDonorID;
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

        public bool Update(BmsBloodDonor value)
        {
            bool isSuccess = false;
            try
            {

                command = new SqlCommand("UPDATE " + TABLE_NAME +
                   @" SET FirstName = @FirstName, LastName = @LastName, Address = @Address, City = @City,
                        MobileNo = @MobileNo, BloodGroup = @BloodGroup, BloodGroup = @BloodGroup, CreationDate = @CreationDate
                        WHERE BloodDonorID = @BloodDonorID", connection);


               
                command.Parameters.Add("@FirstName", SqlDbType.VarChar, 20);
                command.Parameters.Add("@LastName", SqlDbType.VarChar, 20);

                command.Parameters.Add("@Address", SqlDbType.VarChar, 50);
                command.Parameters.Add("@City", SqlDbType.VarChar, 20);
                command.Parameters.Add("@MobileNo", SqlDbType.VarChar, 15);
                command.Parameters.Add("@BloodGroup", SqlDbType.VarChar, 3);
                command.Parameters.Add("@CreationDate", SqlDbType.DateTime);


                command.Parameters["@FirstName"].Value = value.FirstName;
                command.Parameters["@LastName"].Value = value.LastName;
                command.Parameters["@Address"].Value = value.Address;
                command.Parameters["@City"].Value = value.City;
                command.Parameters["@MobileNo"].Value = value.MobileNo;
                command.Parameters["@BloodGroup"].Value = value.BloodGroup;
                command.Parameters["@CreationDate"].Value = value.CreationDate;

                command.Parameters.Add("@BloodDonorID", SqlDbType.Int);

                command.Parameters["@BloodDonorID"].Value = value.BloodDonorID;

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

        public List<BmsBloodDonor> SelectAll()
        {
            List<BmsBloodDonor> list = null;
            try
            {
                list = new List<BmsBloodDonor>();
                command = new SqlCommand("SELECT * FROM " + TABLE_NAME + @"", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            BmsBloodDonor value = new BmsBloodDonor();
                            value.BloodDonorID = Convert.ToInt32(reader["BloodDonorID"]);
                            value.BloodGroup = Convert.ToString(reader["BloodGroup"]);
                            value.CreationDate = Convert.ToDateTime(reader["CreationDate"]);

                            value.Address = Convert.ToString(reader["Address"]);
                            value.City = Convert.ToString(reader["City"]);
                            value.FirstName = Convert.ToString(reader["FirstName"]);
                            value.LastName = Convert.ToString(reader["LastName"]);
                            value.MobileNo = Convert.ToString(reader["MobileNo"]);


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
