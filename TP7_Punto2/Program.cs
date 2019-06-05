using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp7
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

        public int Jubilacion()
        {
            int jub;

            if (genero == Genero.Masculino)
            {
                jub = 65 - Edad();
            }
            else
            {
                jub = 60 - Edad();
            }

            return jub > 0 ? jub : 0;
        }

        public double Salario()
        {
            double adicional;
            if (Antiguedad() < 20)
            {
                adicional = sueldoBasico * 0.02 * Antiguedad();
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
            return sueldoBasico + adicional;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int cant = 5;
            List<Empleado> Empleados = new List<Empleado>();

            for (int i = 0; i < cant; i++)
            {
                Empleados.Add(CargarEmpleado(rnd));
            }

            //Empleados.ForEach(emp => Mostrar(emp));

            Console.WriteLine("\nCantidad de Empleados: {0}\nSalario total: {1}", Empleados.Count, SalarioTotal(Empleados));

            Mostrar(Empleados.ElementAt(rnd.Next(0, cant)));





        }

        public static Empleado CargarEmpleado(Random rnd)
        {
            Empleado emp;
            string[] Nombres = { "Santiago", "Pedro", "Juan", "Luciano", "Martin", "Maria", "Carla", "Luciana", "Cristina", "Alejandra" };
            string[] Apellidos = { "González", "Rodríguez", "Gómez", "Fernández", "López", "Díaz", "Martínez", "Pérez", "Romero", "Sánchez" };

            emp.genero = (Genero)rnd.Next(0, 2);

            if (emp.genero == Genero.Masculino)
            {
                emp.nombre = Nombres[rnd.Next(0, 5)];
            }
            else
            {
                emp.nombre = Nombres[rnd.Next(5, 10)];
            }

            emp.apellido = Apellidos[rnd.Next(0, 10)];

            DateTime start = new DateTime(1940, 1, 1);

            int rangedate = (DateTime.Today.AddYears(-18) - start).Days;
            emp.fnac = start.AddDays(rnd.Next(rangedate));

            rangedate = (DateTime.Today - emp.fnac.AddYears(18)).Days;
            emp.fing = emp.fnac.AddYears(18).AddDays(rnd.Next(rangedate));

            emp.sueldoBasico = 15000;
            emp.cargo = (Cargo)rnd.Next(0, 5);
            emp.estadoCivil = (EstadoCivil)rnd.Next(0, 2);
            return emp;
        }

        public static void Mostrar(Empleado emp)
        {
            Console.WriteLine("\nNombre y apellido: {0}\nGenero: {1}\nEstado civil: {2}\nFecha de Nacimiendo: {3:dd/MM/yyyy}\nFecha de Ingreso: {4:dd/MM/yyyy}\nCargo: {5}\nSueldo basico: {6}\nSalario: {7}\nEdad: {8}\nAntiguedad: {9}\nJubilacion: {10}",
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
                emp.Jubilacion());

        }
        public static double SalarioTotal(List<Empleado> empleados)
        {
            double total = 0;
            foreach (Empleado emp in empleados)
            {
                total += emp.Salario();
            }
            return total;
        }

    }



}