using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//PORQUE NO PUEDO
namespace TP7_punto1
{
    class Program
    {
        public enum EstadoCivil { Soltero, Casado }
        public enum Genero { Masculino, Femenino }
        public enum Cargo { Auxiliar, Administrativo, Ingeniero, Especialista, Investigador }

        public struct Empleado
        {
            public string nombre;
            public string apellido;
            public DateTime fnac;
            public EstadoCivil estadoCivil;
            public Genero genero;
            public DateTime fing;
            public double sueldoBasico;
            public Cargo cargo;

            public int Antiguedad()
            {
                return (DateTime.Today.Year - fing.Year);
            }
            public int Edad()
            {
                int edad = DateTime.Today.Year - fnac.Date.Year;
                if (fnac.Date > DateTime.Today.AddYears(-edad))
                {
                    edad--;
                }

                return edad;
            }
            public double Salario()
            {
                double adicional;
                if (Antiguedad() < 20)
                {
                    adicional = sueldoBasico * 0.2 * Antiguedad();
                }
                else
                {
                    adicional = sueldoBasico * 0.25;
                }
                if (cargo == Cargo.Ingeniero || cargo == Cargo.Especialista)
                {
                    adicional *= 1.5;
                }
                if (estadoCivil == EstadoCivil.Casado)
                {
                    adicional += 5000;
                }

                return adicional + sueldoBasico;

            }
            public int Jubilacion()
            {
                int restaJub;
                if (genero == Genero.Masculino)
                {
                    restaJub = 65 - Edad(); 
                }
                else
                {
                    restaJub = 60 - Edad();
                }
                return restaJub;
            }

        }




        static void Main(string[] args)
        {
            List<Empleado> Empleados = new List<Empleado>();
            
            Random rand = new Random();

            int analisisEmp = 2;

            for (int i = 0; i < analisisEmp; i++)
            {
                Empleados.Add(CrearEmpleado(rand));
            }

            Mostrar(Empleados.ElementAt(rand.Next(0, analisisEmp)));
            Console.WriteLine("Monto Total acumulado {0}", TotalSueldos(Empleados));



            Console.ReadKey();
        }

        public static Empleado CrearEmpleado(Random rand)
        {
            Empleado empX;
            string[] Nombres = { "Juan", "Jose", "Martin", "Ricardo", "Peter", "Lore", "Nati", "Mirta", "Laura", "Norma" };
            string[] Apellidos = { "Perez", "Pepe", "Lorens", "Pears", "Duncan", "Ginobili", "Parker", "Pérez", "Romero", "Santana" };
            empX.genero = (Genero)rand.Next(0, 2);
            if (empX.genero == Genero.Masculino)
            {
                empX.nombre = Nombres[rand.Next(0, 5)];
            }
            else
            {
                empX.nombre = Nombres[rand.Next(5, 10)];
            }

            empX.apellido = Apellidos[rand.Next(0, 10)];


            DateTime start = new DateTime(1950, 1, 1);

            int rangedate = (DateTime.Today.AddYears(-18) - start).Days;
            empX.fnac = start.AddDays(rand.Next(rangedate));

            rangedate = (DateTime.Today - empX.fnac.AddYears(18)).Days;
            empX.fing = empX.fnac.AddYears(18).AddDays(rand.Next(rangedate));

            empX.sueldoBasico = 15000;
            empX.cargo = (Cargo)rand.Next(0, 5);
            empX.estadoCivil = (EstadoCivil)rand.Next(0, 2);


            return empX;
        }

        public static void Mostrar(Empleado emp)
        {

            Console.WriteLine("\nNombre y apellido: {0}\nGenero: {1}\nEstado civil: {2}\nFecha de Nacimiendo: {3:dd/MM/yyyy}\nFecha de Ingreso: {4:dd/MM/yyyy}\nCargo: {5}\nSueldo basico: {6}\nSalario: {7}\nEdad: {8}\nAntiguedad: {9}\nAños para la Jubilacion: {10}",
            emp.nombre + " " + emp.apellido,
            emp.genero,
            emp.estadoCivil,
            emp.fnac.Date,
            emp.fing.Date,
            emp.cargo,
            emp.sueldoBasico,
            emp.Salario(),
            emp.Edad(),
            emp.Antiguedad(),
            emp.Jubilacion()  );
        }

        public static double TotalSueldos(List<Empleado> Empleados)
        {
            double montoTotal=0;
            Console.WriteLine(Empleados.Last().Salario()); 

            foreach (Empleado emp in Empleados)
            {
                montoTotal += emp.Salario();
                
            } 
            return montoTotal;
        }


    }
}
