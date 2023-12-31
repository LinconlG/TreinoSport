﻿using CommunityToolkit.Maui.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TreinoSport.Extensions;
using TreinoSport.Util;

namespace TreinoSport.Models
{
    public class DiaDaSemanaDTO {
        public string NomeDia { get; set; }
        public DayOfWeek DiaEnum { get; set; }
        public ObservableCollection<TimePicker> HorariosPicker { get; set; }
        public List<Horario> Horarios { get; set; } = new();

        public List<Horario> ConverterHorario() {
            var horariosDateTime = new List<Horario>();

            for (int i = 0; i < HorariosPicker.Count; i++) {
                var codString = $"{i+1}{(int)DiaEnum}";
                var aux = new Horario() { Codigo = int.Parse(codString), AlunosPresentes = new(), Hora = new(HorariosPicker[i].Time.Ticks) };
                var horarioTemp = Horarios.FirstOrDefault(h => h.Codigo == aux.Codigo && h.Hora == aux.Hora);
                if (horarioTemp != default) {
                    aux.AlunosPresentes = horarioTemp.AlunosPresentes;
                }
                horariosDateTime.Add(aux);
            }
            return horariosDateTime;
        }

        public DiaDaSemana Conversao() {
            var dia = new DiaDaSemana();
            dia.Dia = DiaEnum;
            dia.Horarios = Horarios;
            return dia;
        }

        public DiaDaSemanaDTO() {}
            
        public DiaDaSemanaDTO(DiaDaSemana diaDaSemana) { 
            NomeDia = Utilidade.TratarDayOfWeek(diaDaSemana.Dia);
            DiaEnum = diaDaSemana.Dia;
            HorariosPicker = diaDaSemana.Horarios.ConvertAll(h => h.Hora.ToTimePicker()).ToObservableCollection();
            Horarios = diaDaSemana.Horarios is null ? null : diaDaSemana.Horarios;
            if (Horarios != null) {
                for (int i = 0;i < Horarios.Count;i++) {
                    Horarios[i].HoraString = Horarios[i].Hora.ToString("HH:mm");
                }
            }
        }
    }
}
