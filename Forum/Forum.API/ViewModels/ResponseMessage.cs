using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.API.ViewModels
{
    public class ResponseMessage
    {
        public ResponseMessage()
        {
            Errors = new List<object>();
        }

        public int Code { get; set; }

        public string Message { get; set; }
        
        public object Data { get; set; }

        public IEnumerable<object> Errors { get; set; }
    }
}
