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
                throw new TimeoutException("Não houve retorno da API.");
        }

        public static bool IsPublicMessageCheck(this Exception exception) {
            try {
                return ((APIException)exception).IsPublicMessage;
            }
            catch (Exception) {
                return false;
            }
        }

        public static async void HandlerException(this Page page, Exception ex) {
            if (ex.IsPublicMessageCheck()) {
                await page.DisplayAlert("Erro", ex.Message, "Ok");
            }
            else {
                await page.DisplayAlert("Erro", "Ocorreu um erro!", "Ok");
            }
        }
    }
}
