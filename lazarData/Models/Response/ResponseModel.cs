using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.Models.Response
{
    public class ResponseModel<T> where T : class
    {
        public string errorMessage { get; set; }
        public T result { get; set; }
        public ResponseModel() { }
        public ResponseModel(T response) { result = response; }
        public ResponseModel(string message) { errorMessage = message; }
        public ResponseModel(T response, string message) { result = response; errorMessage = message; }
    }
}
