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
    public class BloodDonationCampDAL:IDAL<BmsBloodDonationCamp>
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;

        String TABLE_NAME = "dbo.BmsBloodDonationCamp_v1";

        public BloodDonationCampDAL(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public bool Insert(BmsBloodDonationCamp value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("INSERT INTO " + TABLE_NAME +
                   @" ( CampName, Address, City, BloodBank, CampStartDate, CampEndDate) 
                    VALUES( @CampName, @Address, @City, @BloodBank, @CampStartDate, @CampEndDate)", connection);

                command.Parameters.Add("@CampName", SqlDbType.VarChar, 50);
                command.Parameters.Add("@Address", SqlDbType.VarChar, 50);
                command.Parameters.Add("@City", SqlDbType.VarChar, 20);
                command.Parameters.Add("@BloodBank", SqlDbType.VarChar, 50);
                command.Parameters.Add("@CampStartDate", SqlDbType.DateTime);
                command.Parameters.Add("@CampEndDate", SqlDbType.DateTime);


                command.Parameters["@CampName"].Value = value.CampName;
                command.Parameters["@Address"].Value = value.Address;
                command.Parameters["@City"].Value = value.City;
                command.Parameters["@BloodBank"].Value = value.BloodBank;
                command.Parameters["@CampStartDate"].Value = value.CampStartDate;
                command.Parameters["@CampEndDate"].Value = value.CampEndDate;
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

        public bool Delete(BmsBloodDonationCamp value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("DELETE FROM " + TABLE_NAME +
                     @" WHERE BloodDonationCampID = @BloodDonationCampID", connection);

                command.Parameters.Add("@BloodDonationCampID", SqlDbType.Int);

                command.Parameters["@BloodDonationCampID"].Value = value.BloodDonationCampID;
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

        public bool Update(BmsBloodDonationCamp value)
        {
            bool isSuccess = false;
            try
            {

                command = new SqlCommand("UPDATE " + TABLE_NAME +
                   @" SET CampName = @CampName, Address = @Address, City = @City,
                        BloodBank = @BloodBank, CampStartDate = @CampStartDate, CampEndDate = @CampEndDate
                        WHERE BloodDonationCampID = @BloodDonationCampID", connection);


                command.Parameters.Add("@CampName", SqlDbType.VarChar, 50);
                command.Parameters.Add("@Address", SqlDbType.VarChar, 50);
                command.Parameters.Add("@City", SqlDbType.VarChar, 20);
                command.Parameters.Add("@BloodBank", SqlDbType.VarChar, 50);
                command.Parameters.Add("@CampStartDate", SqlDbType.DateTime);
                command.Parameters.Add("@CampEndDate", SqlDbType.DateTime);


                command.Parameters["@CampName"].Value = value.CampName;
                command.Parameters["@Address"].Value = value.Address;
                command.Parameters["@City"].Value = value.City;
                command.Parameters["@BloodBank"].Value = value.BloodBank;
                command.Parameters["@CampStartDate"].Value = value.CampStartDate;
                command.Parameters["@CampEndDate"].Value = value.CampEndDate;

                command.Parameters.Add("@BloodDonationCampID", SqlDbType.Int);

                command.Parameters["@BloodDonationCampID"].Value = value.BloodDonationCampID;
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

        public List<BmsBloodDonationCamp> SelectAll()
        {
            List<BmsBloodDonationCamp> list = null;
            try
            {
                list = new List<BmsBloodDonationCamp>();
                command = new SqlCommand("SELECT * FROM " + TABLE_NAME + @"", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            BmsBloodDonationCamp value = new BmsBloodDonationCamp();
                            value.BloodBank = Convert.ToString(reader["BloodBank"]);
                            value.Address = Convert.ToString(reader["Address"]);
                            value.City = Convert.ToString(reader["City"]);
                            value.CampName = Convert.ToString(reader["CampName"]);
                            value.BloodDonationCampID = Convert.ToInt32(reader["BloodDonationCampID"]);

                            value.CampStartDate = Convert.ToDateTime(reader["CampStartDate"]);
                            value.CampEndDate = Convert.ToDateTime(reader["CampEndDate"]);


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
