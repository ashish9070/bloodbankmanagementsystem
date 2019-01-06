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
    public class BloodDonationCampBAL : IBAL<BmsBloodDonationCamp>
    {
        BloodDonationCampDAL dal = null;

        public BloodDonationCampBAL(string connectionString)
        {
            dal = new BloodDonationCampDAL(connectionString);
        }

        public bool Add(BmsBloodDonationCamp value)
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
        public bool Modify(BmsBloodDonationCamp value)
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
        public bool Remove(BmsBloodDonationCamp value)
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
        public List<BmsBloodDonationCamp> GetAll()
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

        private Boolean Valid(BmsBloodDonationCamp value)
        {
            Boolean isValid = true;
            try
            {
                StringBuilder errorMessage = new StringBuilder();



                // Name
                Match matchName = Regex.Match(value.CampName, "[^a-zA-Z ]*");
                if (matchName.Success == false)
                {
                    isValid = false;
                    errorMessage.Append("BmsCampName should contains alphabets and space only").Append(Environment.NewLine);
                }
                else if (value.CampName == String.Empty)
                {
                    errorMessage.Append("BmsCampName  should be provided\n").Append(Environment.NewLine);
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
                //Blood Bank

                if (value.BloodBank == String.Empty)
                {
                    errorMessage.Append("Blood Bank should be provided\n");
                    isValid = false;
                }
                //Camp Start Date

                if (value.CampStartDate < DateTime.Today)
                {
                    errorMessage.Append("Camp Start Date should be greater than Current Date\n");
                    isValid = false;
                }
                //Camp End Date
                if (value.CampEndDate < value.CampStartDate)
                {
                    errorMessage.Append("Camp End Date should be greater than Camp Start Date\n");
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
