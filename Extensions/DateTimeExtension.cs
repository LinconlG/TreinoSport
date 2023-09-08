using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreinoSport.Extensions {
    public static class DateTimeExtension {
        public static TimePicker ToTimePicker(this DateTime data) {
            TimePicker picker = new();
            picker.Time = data.TimeOfDay;
            return picker;
        }
    }
}
