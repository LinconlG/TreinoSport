using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.Util {
    public static class Utilidade {



        public static string TratarDayOfWeek(DayOfWeek dia) {
            
            switch (dia) {

                case DayOfWeek.Sunday:
                    return "Domingo";

                case DayOfWeek.Monday:
                    return "Segunda-feira";

                case DayOfWeek.Tuesday:
                    return "Terça-feira";

                case DayOfWeek.Wednesday:
                    return "Quarta-feira";

                case DayOfWeek.Thursday:
                    return "Quinta-feira";

                case DayOfWeek.Friday:
                    return "Sexta-feira";

                case DayOfWeek.Saturday:
                    return "Sábado";

                default: return "-";
            }
        }
    }
}
