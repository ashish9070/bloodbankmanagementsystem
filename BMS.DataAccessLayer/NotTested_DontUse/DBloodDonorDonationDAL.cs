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
    public class DBloodDonorDonationDAL:IDAL<BmsBloodDonorDonation>
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;

        String TABLE_NAME = "dbo.BmsBloodDonorDonation_v1";



        public DBloodDonorDonationDAL(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public bool Insert(BmsBloodDonorDonation value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("INSERT INTO " + TABLE_NAME +
                  @" ( BloodDonorID, BloodDonationDate,  NumberofBottle, Weight, HBCount, BloodDonationCampID, BloodInventoryID) 
                    VALUES( @BloodDonorID, @BloodDonationDate, @NumberofBottle, @Weight, @HBCount, @BloodDonationCampID, @BloodInventoryID)", connection);

                command.Parameters.Add("@BloodDonorID", SqlDbType.Int);
                command.Parameters.Add("@BloodDonationDate", SqlDbType.DateTime);
                command.Parameters.Add("@NumberofBottle", SqlDbType.Int);
                command.Parameters.Add("@Weight", SqlDbType.Decimal);
                command.Parameters.Add("@HBCount", SqlDbType.Decimal);
                command.Parameters.Add("@BloodDonationCampID", SqlDbType.Int);
                command.Parameters.Add("@BloodInventoryID", SqlDbType.Int);


                command.Parameters["@BloodDonorID"].Value = value.BloodDonorID;
                command.Parameters["@BloodDonationDate"].Value = value.BloodDonationDate;
                command.Parameters["@NumberofBottle"].Value = value.NumberofBottle;
                command.Parameters["@Weight"].Value = value.Weight;
                command.Parameters["@HBCount"].Value = value.HBCount;
                command.Parameters["@BloodDonationCampID"].Value = value.BloodDonationCampID;
                command.Parameters["@BloodInventoryID"].Value = value.BloodInventoryID;

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

        public bool Delete(BmsBloodDonorDonation value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("DELETE FROM " + TABLE_NAME +
                      @" WHERE BloodDonationID = @BloodDonationID", connection);

                command.Parameters.Add("@BloodDonationID", SqlDbType.Int);

                command.Parameters["@BloodDonationID"].Value = value.BloodDonationID;
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

        public bool Update(BmsBloodDonorDonation value)
        {
            bool isSuccess = false;
            try
            {

                command = new SqlCommand("UPDATE " + TABLE_NAME +
                   @" SET BloodDonorID = @BloodDonorID, BloodDonationDate = @BloodDonationDate, NumberofBottle = @NumberofBottle, Weight = @Weight,
                        HBCount = @HBCount, BloodDonationCampID = @BloodDonationCampID, BloodInventoryID = @BloodInventoryID
                        WHERE BloodDonationID = @BloodDonationID", connection);

                
                command.Parameters.Add("@BloodDonorID", SqlDbType.Int);
                command.Parameters.Add("@BloodDonationDate", SqlDbType.DateTime);
                command.Parameters.Add("@NumberofBottle", SqlDbType.Int);
                command.Parameters.Add("@Weight", SqlDbType.Decimal);
                command.Parameters.Add("@HBCount", SqlDbType.Decimal);
                command.Parameters.Add("@BloodDonationCampID", SqlDbType.Int);
                command.Parameters.Add("@BloodInventoryID", SqlDbType.Int);


                command.Parameters["@BloodDonorID"].Value = value.BloodDonorID;
                command.Parameters["@BloodDonationDate"].Value = value.BloodDonationDate;
                command.Parameters["@NumberofBottle"].Value = value.NumberofBottle;
                command.Parameters["@Weight"].Value = value.Weight;
                command.Parameters["@HBCount"].Value = value.HBCount;
                command.Parameters["@BloodDonationCampID"].Value = value.BloodDonationCampID;
                command.Parameters["@BloodInventoryID"].Value = value.BloodInventoryID;

                command.Parameters.Add("@BloodDonationID", SqlDbType.Int);
                command.Parameters["@BloodDonationID"].Value = value.BloodDonationID;


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

        public List<BmsBloodDonorDonation> SelectAll()
        {
            List<BmsBloodDonorDonation> list = null;
            try
            {
                list = new List<BmsBloodDonorDonation>();
                command = new SqlCommand("SELECT * FROM " + TABLE_NAME + @"", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            BmsBloodDonorDonation value = new BmsBloodDonorDonation();
                            value.BloodDonationCampID = Convert.ToInt32(reader["BloodDonationCampID"]);
                            value.BloodDonationDate = Convert.ToDateTime(reader["BloodDonationDate"]);
                            value.BloodDonationID = Convert.ToInt32(reader["BloodDonationID"]);

                            value.BloodDonorID = Convert.ToInt32(reader["BloodDonorID"]);
                            value.BloodInventoryID = Convert.ToInt32(reader["BloodInventoryID"]);
                            value.HBCount = Convert.ToDecimal(reader["HBCount"]);
                            value.NumberofBottle = Convert.ToInt32(reader["NumberofBottle"]);
                            value.Weight = Convert.ToDecimal(reader["Weight"]);

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
