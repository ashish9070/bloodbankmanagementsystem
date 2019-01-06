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
    public class TransactionBAL : IBAL<BmsTransaction>
    {
        BloodTransactionDAL dal = null;

        public TransactionBAL(string connectionString)
        {
            dal = new BloodTransactionDAL(connectionString);
        }

        public bool Add(BmsTransaction value)
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
        public bool Modify(BmsTransaction value)
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
        public bool Remove(BmsTransaction value)
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
        public List<BmsTransaction> GetAll()
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

        private Boolean Valid(BmsTransaction value)
        {
            Boolean isValid = true;
            try
            {
                StringBuilder errorMessage = new StringBuilder();

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
