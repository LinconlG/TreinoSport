using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.Extensions {
    public static class TaskExtension {

        public static async Task<HttpResponseMessage> TimeoutAfter(this Task<HttpResponseMessage> task, int millisecondsTimeout) {
            if (task == await Task.WhenAny(task, Task.Delay(millisecondsTimeout)))
                return await task;
            else
                return new HttpResponseMessage(System.Net.HttpStatusCode.GatewayTimeout); //throw new TimeoutException("Não houve retorno da API.");
        }
    }
}
