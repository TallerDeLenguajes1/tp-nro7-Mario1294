using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_7
{
    


    class Program
    {
        public enum Cargo { Auxiliar, Administrativo, Ingeniero, Especialista, Investigado };
        public enum Genero { Femenino, Masculino };
        public enum EstadoCivil { Soltero, Casado };
        public struct Empleados
        {
            public string Nombre;
            public string Apellido;
            public EstadoCivil estado;
            public DateTime Ingreso;
            public DateTime Nacimiento;
            public Genero genero;
            public double SueldoBasico;
            public Cargo cargo;

            public void MostrarEmpleado()
            {
                Console.WriteLine("Nombre : {0}", Nombre);
                Console.WriteLine("Apellido : {0}", Apellido);
                Console.WriteLine("Fecha de Nacimiento: {0}", Nacimiento.ToLongDateString());
                Console.WriteLine("Estado Civil: {0}", estado);
                Console.WriteLine("Genero: {0}", genero);
                Console.WriteLine("Fecha de Ingreso: {0}", Ingreso.ToLongDateString());
                Console.WriteLine("Sueldo Basico: {0}", SueldoBasico);
                Console.WriteLine("Cargo: {0}", cargo);
            }
        }
        public static int CalcularAnios(DateTime Fecha)
        {
            int anios = DateTime.Today.AddTicks(-Fecha.Ticks).Year - 1;
            return anios;
        }
        public static int Jubilarse(int edad, Genero genero)
        {
            int anios;
            if (genero == (Genero)0)
            {
                anios = edad - 60;
            } else
            {
                anios = edad - 65;
            }
            return anios;
        }
        public static double CalcularSalario(int _antiguedad, double _SueldoBasico, Cargo _cargo, EstadoCivil _estado)
        {

            double salario, adicional;
            Random rnd = new Random();
            int Hijos = rnd.Next(0,5);
            if(_antiguedad <= 20)
            {
                adicional = (0.02 * _SueldoBasico) * _antiguedad;
            }else
            {
                adicional = 0.25 * _SueldoBasico;
            }

            if (_cargo == (Cargo)2 || _cargo == (Cargo)3)
            {
                adicional = adicional + adicional * 0.5;
            }

            if (Hijos > 2 && _estado == (EstadoCivil)1)
            {
                adicional = adicional + 5000;
            }

            salario = _SueldoBasico + adicional;
            return salario;
        }
       
        public static Empleados CrearEmpleado(string _nombre, string _aplellido, EstadoCivil _estado, DateTime _ingreso, DateTime _nacimiento, Genero _genero, double _sueldo, Cargo _cargo)
        {
            Empleados nuevo;
            nuevo.Nombre = _nombre;
            nuevo.Apellido = _aplellido;
            nuevo.estado = _estado;
            nuevo.Ingreso = _ingreso;
            nuevo.Nacimiento = _nacimiento;
            nuevo.genero = _genero;
            nuevo.SueldoBasico = _sueldo;
            nuevo.cargo = _cargo;
 
            return nuevo;
        }
        public static void AgregarEmpleado(List<Empleados>empl, Empleados empleado)
        {
            empl.Add(empleado);
        }
        
        public static void Mostrarempleados(List<Empleados> empleado)
        {
            foreach (Empleados empl in empleado)
            {
                empl.MostrarEmpleado();
                Console.WriteLine("Antiguedad: {0}", CalcularAnios(empl.Ingreso)*(1));
                Console.WriteLine("Edad : {0}" , CalcularAnios(empl.Nacimiento)*(1));
                Console.WriteLine("Años para Jubilarce: {0}", Jubilarse(CalcularAnios(empl.Ingreso), empl.genero)*(-1));
                Console.WriteLine("Salario: {0}", CalcularSalario(CalcularAnios(empl.Ingreso), empl.SueldoBasico, empl.cargo, empl.estado));
                Console.WriteLine("--------------------\n");
            }
        }
        static void Main(string[] args)
        {    
            //Declaro variables
            int dia, mes, anio;
            int cont=0;
            double Salario, MontoTotal = 0;
            Random rnd = new Random();

            //Declaro los enum
            Cargo carg;
            Genero genero;
            EstadoCivil estado;

            //Declaro los nombres y apellidos.
            string[] nombreMas = new string[] { "Juan", "Luis", "Andres", "German", "Mario", "Facundo" };
            string[] nombreFem = new string[] { "Luciana", "Maria", "Florencia", "Yesica", "Pamela", "Ana"};
            string[] apellido = new string[] {"Gomez", "Gonzales", "Medina", "Fernadez", "Rojas" };

            //Creo la lista.
            List<Empleados> Empleados = new List<Empleados>();
            Empleados empleado;

            for (int i = 0; i < 20; i++)
            {
                //Fecha de Nacimineto.
                dia = rnd.Next(1, 31);
                mes = rnd.Next(1, 12);
                anio = rnd.Next(1960, 2001);
                empleado.Nacimiento = new DateTime(anio, mes, dia);

                //Fecha de Ingreso.
                dia = rnd.Next(1, 31);
                mes = rnd.Next(1, 12);
                anio = rnd.Next(1990, 2018);
                empleado.Ingreso = new DateTime(anio, mes, dia);
                
                //Asigno genero.
                genero = (Genero)rnd.Next(0,2);

                //Asigno nombre dependiendo del genero y asigno.
                if(genero == (Genero)0)
                {
                    empleado.Nombre = nombreFem[rnd.Next(0, 6)];
                }
                else
                {
                    empleado.Nombre = nombreMas[rnd.Next(0,6)];
                }
                empleado.Apellido = apellido[rnd.Next(0,5)];

                //Asigno sueldo basico, cargo, estado civil y salario.
                empleado.SueldoBasico = 15000;
                carg  = (Cargo)rnd.Next(0,5);
                estado = (EstadoCivil)rnd.Next(0,2);
                Salario = CalcularSalario(CalcularAnios(empleado.Ingreso), empleado.SueldoBasico, carg, estado);

                //Cargo la estructura con los datos y luego la agrego al final de la lista
                empleado = CrearEmpleado(empleado.Nombre, empleado.Apellido, estado, empleado.Ingreso, empleado.Nacimiento, genero, empleado.SueldoBasico, carg);
                AgregarEmpleado(Empleados, empleado);

                //Calculo Cantidad de empleados y Monto total de lo que se paga en salario
                cont++ ;
                MontoTotal = MontoTotal + Salario;
            }

            //Muestro la Lista con Foreach.
            Mostrarempleados(Empleados);
            Console.WriteLine("\nMonto Total = ${0}", MontoTotal);
            Console.WriteLine("Cantidad de empleados = {0}", cont);

            Console.ReadKey();
        }
    }
}
