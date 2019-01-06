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
    public class DHospitalDAL:IDAL<BmsHospital>
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataReader reader = null;

        String TABLE_NAME = "dbo.BmsHospital_v1";
        public DHospitalDAL(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public bool Insert(BmsHospital value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("INSERT INTO " + TABLE_NAME +
                 @" ( HospitalName, Address,  City, ContactNo) 
                    VALUES( @HospitalName, @Address, @City, @ContactNo)", connection);

                command.Parameters.Add("@HospitalName", SqlDbType.VarChar, 50);
                command.Parameters.Add("@Address", SqlDbType.VarChar, 50);
                command.Parameters.Add("@City", SqlDbType.VarChar, 20);
                command.Parameters.Add("@ContactNo", SqlDbType.VarChar, 15);
                

                command.Parameters["@HospitalName"].Value = value.HospitalName;
                command.Parameters["@Address"].Value = value.Address;
                command.Parameters["@City"].Value = value.City;
                command.Parameters["@ContactNo"].Value = value.ContactNo;
                

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

        public bool Delete(BmsHospital value)
        {
            bool isSuccess = false;
            try
            {
                command = new SqlCommand("DELETE FROM " + TABLE_NAME +
                     @" WHERE HospitalID = @HospitalID", connection);

                command.Parameters.Add("@HospitalID", SqlDbType.Int);

                command.Parameters["@HospitalID"].Value = value.HospitalID;
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

        public bool Update(BmsHospital value)
        {
            bool isSuccess = false;
            try
            {

                command = new SqlCommand("UPDATE " + TABLE_NAME +
                  @" SET HospitalName = @HospitalName, Address = @Address, City = @City, ContactNo = @ContactNo
                        WHERE HospitalID = @HospitalID", connection);

                
                command.Parameters.Add("@HospitalName", SqlDbType.VarChar, 50);
                command.Parameters.Add("@Address", SqlDbType.VarChar, 50);
                command.Parameters.Add("@City", SqlDbType.VarChar, 20);
                command.Parameters.Add("@ContactNo", SqlDbType.VarChar, 15);


                command.Parameters["@HospitalName"].Value = value.HospitalName;
                command.Parameters["@Address"].Value = value.Address;
                command.Parameters["@City"].Value = value.City;
                command.Parameters["@ContactNo"].Value = value.ContactNo;
                command.Parameters.Add("@HospitalID", SqlDbType.Int);

                command.Parameters["@HospitalID"].Value = value.HospitalID;

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

        public List<BmsHospital> SelectAll()
        {
            List<BmsHospital> list = null;
            try
            {
                list = new List<BmsHospital>();
                command = new SqlCommand("SELECT * FROM " + TABLE_NAME + @"", connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            BmsHospital value = new BmsHospital();
                            value.Address = Convert.ToString(reader["Address"]);
                            value.City = Convert.ToString(reader["City"]);
                            value.ContactNo = Convert.ToString(reader["ContactNo"]);
                            value.HospitalID = Convert.ToInt32(reader["HospitalID"]);
                            value.HospitalName = Convert.ToString(reader["HospitalName"]);


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
