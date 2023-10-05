using TreinoSport.Extensions;

namespace TreinoSport.Models {
    public static class ContaStatic {

        public static int GetCodigo() {
            var codigo = Preferences.Get("codigoConta", 0);
            if (codigo == 0) {
                throw new APIException("Ocorreu um erro com a conta, tente fazer o login novamente.", true);
            }
            return codigo;
        }
        public static bool GetIsCT() {
            bool isCT = Preferences.Get("isCT", false);
            string erro = "";
            try {
                erro = Preferences.Get("isCT", "erro");
            }
            catch (Exception) {
                
            }
            
            if (erro == "erro") {
                throw new APIException("Ocorreu um erro com a conta, tente fazer o login novamente.", true);
            }
            return isCT;

        }
        public static string GetEmail() {
            var email = Preferences.Get("email", null);
            return email;
        }
        public static string GetSenha() {
            var senha = Preferences.Get("senha", null);
            return senha;
        }
        public static void SetConta(string email, string senha) {
            Preferences.Set("email", email);
            Preferences.Set("senha", senha);
        }
        public static void Logout() {
            Preferences.Remove("email");
            Preferences.Remove("senha");
            Preferences.Remove("codigoConta");
            Preferences.Remove("isCT");
        }
        public static void SetCodigo(int codigo) {
            Preferences.Set("codigoConta", codigo);
        }
        public static void SetIsCT(bool isCT) {
            Preferences.Set("isCT", isCT);
        }
        public static void SetToken(string token) {
            Preferences.Set("token", token);
        }
        public static string GetToken() {
            var token = Preferences.Get("token", null);
            if (token == null) {
                throw new APIException("Ocorreu um erro com o código, tente novamente.", true);
            }
            return token;
        }
    }
}
