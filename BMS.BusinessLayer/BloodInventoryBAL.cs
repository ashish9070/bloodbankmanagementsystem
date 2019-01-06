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
    public class BloodInventoryBAL : IBAL<BmsBloodInventory>
    {
        BloodInventoryDAL dal = null;

        public BloodInventoryBAL(string connectionString)
        {
            dal = new BloodInventoryDAL(connectionString);
        }

        public bool Add(BmsBloodInventory value)
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
        public bool Modify(BmsBloodInventory value)
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
        public bool Remove(BmsBloodInventory value)
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
        public List<BmsBloodInventory> GetAll()
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

        private Boolean Valid(BmsBloodInventory value)
        {
            Boolean isValid = true;
            try
            {
                StringBuilder errorMessage = new StringBuilder();



                // Blood Group
                if (value.BloodGroup == String.Empty)
                {
                    errorMessage.Append("Blood group should be provided\n").Append(Environment.NewLine);
                    isValid = false;
                }

                //Number of Bottles

                if (value.NumberofBottles <= 0)
                {
                    errorMessage.Append("Number of bottles should be greater than 0\n");
                    isValid = false;
                }

                //Expiry Date
                if (value.ExpiryDate == null)
                {
                    errorMessage.Append("Expiry Date should be provided\n");
                    isValid = false;
                }

                //Creation Date
                if (value.CreationDate == null)
                {
                    errorMessage.Append("Creation Date should be provided\n");
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
