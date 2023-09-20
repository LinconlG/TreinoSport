using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.Extensions {
    public class APIException : Exception {
        public string message { get; set; }
        public bool IsPublicMessage { get; set; }

        public APIException(string message, bool isPublicMessage) : base (message) { 
            IsPublicMessage = isPublicMessage;
            this.message = message;
        }

    }
}
