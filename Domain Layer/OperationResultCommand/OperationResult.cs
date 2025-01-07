using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.OperationResultCommand
{
    public class OperationResult<T>
    {
        public bool IsSuccessfull { get; private set; }
        public string Message { get; private set; }
        public T? Data { get; private set; }
        public string ErrorMessage { get; private set; }

        private OperationResult(bool isSuccessfull, T? data, string errorMessage, string message)
        {
            IsSuccessfull = isSuccessfull;
            Message = message;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static OperationResult<T> Successfull(T data, string message = "Operation successfull")
        {

            return new OperationResult<T>(true, data, null, message);
        }
        public static OperationResult<T> Failure(string errormessage, string message = "Operation failed")
        {

            return new OperationResult<T>(false, default, errormessage, message);
        }



    }
}
