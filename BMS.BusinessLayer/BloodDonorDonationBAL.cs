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
    public class BloodDonorDonationBAL : IBAL<BmsBloodDonorDonation>
    {
        BloodDonorDonationDAL dal = null;

        public BloodDonorDonationBAL(string connectionString)
        {
            dal = new BloodDonorDonationDAL(connectionString);
        }

        public bool Add(BmsBloodDonorDonation value)
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
        public bool Modify(BmsBloodDonorDonation value)
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
        public bool Remove(BmsBloodDonorDonation value)
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
        public List<BmsBloodDonorDonation> GetAll()
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

        private Boolean Valid(BmsBloodDonorDonation value)
        {
            Boolean isValid = true;
            try
            {
                StringBuilder errorMessage = new StringBuilder();

                if (value.BloodDonationCampID < 0)
                {
                    errorMessage.Append("Blood Donation Camp Id should be non negative\n").Append(Environment.NewLine);
                    isValid = false;
                }

                //BloodDonationDate

                if (value.BloodDonationDate == null)
                {
                    errorMessage.Append("Blood Donation Date should be provided\n");
                    isValid = false;
                }

                //BloodDonationID
                if (value.BloodDonationID < 0)
                {
                    errorMessage.Append("Blood Donation Id should be non negative\n");
                    isValid = false;
                }

                //BloodDonorID
                if (value.BloodDonorID < 0)
                {
                    errorMessage.Append("Blood Donor Id should be non negative\n");
                    isValid = false;
                }




                //BloodInventoryID
                if (value.BloodInventoryID < 0)
                {
                    errorMessage.Append("Blood Onventory Id should be non negative\n");
                    isValid = false;
                }

                //HBCount
                if (value.HBCount < 0)
                {
                    errorMessage.Append("Blood HBCount should be non negative\n");
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
