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
    public class HospitalBAL : IBAL<BmsHospital>
    {
        HospitalDAL dal = null;

        public HospitalBAL(string connectionString)
        {
            dal = new HospitalDAL(connectionString);
        }

        public bool Add(BmsHospital value)
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
        public bool Modify(BmsHospital value)
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
        public bool Remove(BmsHospital value)
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
        public List<BmsHospital> GetAll()
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

        private Boolean Valid(BmsHospital value)
        {
            Boolean isValid = true;
            try
            {
                StringBuilder errorMessage = new StringBuilder();

                // Name
                Match matchName = Regex.Match(value.HospitalName, "[^a-zA-Z ]*");
                if (matchName.Success == false)
                {
                    isValid = false;
                    errorMessage.Append("BmsHospital name should contains alphabets and space only").Append(Environment.NewLine);
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

                // Contact Number
                Match matchNumber = Regex.Match(value.ContactNo, "[7-9][0-9]{9}");
                if (matchNumber.Success == false)
                {
                    isValid = false;
                    errorMessage.Append("Contact Number should contains numbers only starting with 7-9 and have 10 digits.").Append(Environment.NewLine);
                }
                else if (value.ContactNo == String.Empty)
                {
                    errorMessage.Append("Contact Number should be provided\n").Append(Environment.NewLine);
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
