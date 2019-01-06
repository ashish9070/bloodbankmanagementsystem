using BMS.DataAccessLayer;
using BMS.DataAccessLayer.ConnectedDAL;
using BMS.Entities;
using BMS.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BMS.BusinessLayer
{
    public class BloodDonorBAL : IBAL<BmsBloodDonor>
    {
        BloodDonorDAL dal = null;

        public BloodDonorBAL(string connectionString)
        {
            dal = new BloodDonorDAL(connectionString);
        }

        public bool Add(BmsBloodDonor value)
        {
            bool isSuccess = false;
            try
            {
                if (Valid(value))
                    isSuccess = dal.Insert(value);
            }
            catch (ValidationException)
            {
                throw;
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
        public bool Modify(BmsBloodDonor value)
        {
            bool isSuccess = false;
            try
            {
                if (Valid(value))
                    isSuccess = dal.Update(value);
            }
            catch (ValidationException)
            {
                throw;
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
        public bool Remove(BmsBloodDonor value)
        {
            bool isSuccess = false;
            try
            {
                isSuccess = dal.Delete(value);
            }
            catch (ValidationException)
            {
                throw;
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
        public List<BmsBloodDonor> GetAll()
        {
            try
            {
                return dal.SelectAll();
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (ConnectedDalException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private Boolean Valid(BmsBloodDonor value)
        {
            Boolean isValid = true;
            try
            {
                StringBuilder errorMessage = new StringBuilder();


                //First Name
                Match matchFirstName = Regex.Match(value.FirstName, "[^a-zA-Z ]*");
                if (matchFirstName.Success == false)
                {
                    isValid = false;
                    errorMessage.Append("First name should contains alphabets and space only").Append(Environment.NewLine);
                }
                else if (value.FirstName == String.Empty)
                {
                    errorMessage.Append("First Name should be provided\n").Append(Environment.NewLine);
                    isValid = false;
                }

                //Last Name
                Match matchLastName = Regex.Match(value.LastName, "[^a-zA-Z ]*");
                if (matchLastName.Success == false)
                {
                    isValid = false;
                    errorMessage.Append("Last name should contains alphabets and space only").Append(Environment.NewLine);
                }
                else if (value.LastName == String.Empty)
                {
                    errorMessage.Append("Last Name should be provided\n").Append(Environment.NewLine);
                    isValid = false;
                }

                // Address
                Match matchAddress = Regex.Match(value.Address, "[^a-zA-Z0-9 ]*");
                if (matchAddress.Success == false)
                {
                    isValid = false;
                    errorMessage.Append("Address should contains alphabets and space only").Append(Environment.NewLine);
                }
                else if (value.Address == String.Empty)
                {
                    errorMessage.Append("Address should be provided\n").Append(Environment.NewLine);
                    isValid = false;
                }

                // City
                Match matchCity = Regex.Match(value.City, "[^a-zA-Z ]*");
                if (matchCity.Success == false)
                {
                    isValid = false;
                    errorMessage.Append("City should contains alphabets and space only").Append(Environment.NewLine);
                }
                else if (value.City == String.Empty)
                {
                    errorMessage.Append("City should be provided\n").Append(Environment.NewLine);
                    isValid = false;
                }

                // Blood Group
                if (value.BloodGroup == String.Empty)
                {
                    errorMessage.Append("Blood group should be provided\n").Append(Environment.NewLine);
                    isValid = false;
                }


                if (!isValid)
                {
                    throw new ValidationException(errorMessage.ToString());
                }
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (ConnectedDalException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return isValid;
        }
    }
}
