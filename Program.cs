using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{

    static int[] numeroPago = new int[10];
    static DateTime[] fecha = new DateTime[10];
    static TimeSpan[] hora = new TimeSpan[10];
    static int[] cedula = new int[10];
    static string[] nombre = new string[10];
    static string[] apellido1 = new string[10];
    static string[] apellido2 = new string[10];
    static int[] numeroCaja = new int[10];
    static int[] tipoServicio = new int[10];
    static string[] numeroFactura = new string[10];
    static decimal[] montoPagar = new decimal[10];
    static decimal[] montoComision = new decimal[10];
    static decimal[] montoDeducido = new decimal[10];
    static decimal[] montoPagaCliente = new decimal[10];
    static decimal[] vuelto = new decimal[10];
    static int indice = 0;

    //Llama a la función menú y valida que la opción digitada sea correcta
    static void Main()
    {
        int opcion;

        do
        {
            MostrarMenuPrincipal();
            Console.Write("Ingrese la opción deseada: ");

            if (int.TryParse(Console.ReadLine(), out opcion))
            {
                ProcesarOpcionMenuPrincipal(opcion);
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un número válido.");
            }
        } while (opcion != 7);
    }
    //Imprime las opciones del menú
    static void MostrarMenuPrincipal()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Clear();
        Console.WriteLine("===Sistema de Pago Servicios Publicos===");
        Console.WriteLine("===         Menu principal           ===");
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("1. Inicializar Vectores");
        Console.WriteLine("2. Realizar Pagos");
        Console.WriteLine("3. Consultar Pagos");
        Console.WriteLine("4. Modificar Pagos");
        Console.WriteLine("5. Eliminar Pagos");
        Console.WriteLine("6. Submenú Reportes");
        Console.WriteLine("7. Salir");
    }

    //Procesa las opciones del menú llamando a las fucniones respectivas
    static void ProcesarOpcionMenuPrincipal(int opcion)
    {
        switch (opcion)
        {
            case 1:
                InicializarVectores();
                break;
            case 2:
                RealizarPagos();
                break;
            case 3:
                ConsultarPagos();
                break;
            case 4:
                ModificarPagos();
                break;
            case 5:
                EliminarPagos();
                break;
            case 6:
                SubmenuReportes();
                break;
            case 7:
                Console.WriteLine("Saliendo del programa. ¡Hasta luego!");
                break;
            default:
                Console.WriteLine("Opción no válida. Por favor, ingrese un número del 1 al 7.");
                break;
        }
    }

    static void InicializarVectores()
    {

        for (int i = 0; i < 10; i++)
        {
            numeroPago[i] = 0;
            fecha[i] = DateTime.MinValue;
            hora[i] = TimeSpan.MinValue;
            cedula[i] = 0;
            nombre[i] = "";
            apellido1[i] = "";
            apellido2[i] = "";
            numeroCaja[i] = 0;
            tipoServicio[i] = 0;
            numeroFactura[i] = "";
            montoPagar[i] = 0;
            montoComision[i] = 0;
            montoDeducido[i] = 0;
            montoPagaCliente[i] = 0;
            vuelto[i] = 0;
        }
        indice = 0;

        Console.WriteLine("Vectores inicializados correctamente. Presione cualquier tecla para continuar.");
        Console.ReadKey();


    }

    //Funcion para ingresar datos de un pago
    static void RealizarPagos()
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        if (indice < 10)
        {
            Random rnd = new Random();
            Console.Clear();
            Console.WriteLine("Ingrese los datos del pago:");


            fecha[indice] = ObtenerFecha("Fecha (YYYY-MM-DD): ");
            hora[indice] = Obtenerhora("Hora (HH:MM): ");

            Console.Write("Cédula: ");
            cedula[indice] = int.Parse(Console.ReadLine());

            Console.Write("Nombre: ");
            nombre[indice] = Console.ReadLine();

            Console.Write("Apellido 1: ");
            apellido1[indice] = Console.ReadLine();

            Console.Write("Apellido 2: ");
            apellido2[indice] = Console.ReadLine();

            numeroCaja[indice] = rnd.Next(1, 4);


            tipoServicio[indice] = ObtenerTipoServicio("Tipo de Servicio (1=Recibo de Luz, 2=Recibo Telefónico, 3=Recibo de Agua): ");

            Console.Write("Número de Factura: ");
            numeroFactura[indice] = Console.ReadLine();

            montoPagar[indice] = ObtenerMonto("Monto a Pagar: ");

            CalcularComision(indice);
            CalcularMontoDeducido(indice);
            montoPagaCliente[indice] = ObtenerMontoPagaCliente(indice);

            vuelto[indice] = CalcularVuelto(indice);

            MostrarDatosPago(indice);
            indice++;
            Console.WriteLine("\n Presione cualquier tecla para volver al menú princial");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("\n Vectores llenos. No se pueden ingresar más pagos.");
            Console.ReadKey();

        }

    }

    static void CalcularComision(int indice)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        switch (tipoServicio[indice])
        {
            case 1: // Recibo de Luz
                montoComision[indice] = montoPagar[indice] * 0.04m; // 4% de comisión
                break;
            case 2: // Recibo Telefónico
                montoComision[indice] = montoPagar[indice] * 0.055m; // 5.5% de comisión
                break;
            case 3: // Recibo de Agua
                montoComision[indice] = montoPagar[indice] * 0.065m; // 6.5% de comisión
                break;
            default:
                Console.WriteLine($"Tipo de servicio desconocido en la transacción {indice + 1}. No se calculó la comisión.");
                break;
        }
    }

    static void CalcularMontoDeducido(int indice)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        montoDeducido[indice] = montoPagar[indice] - montoComision[indice];
    }

    static decimal ObtenerMontoPagaCliente(int indice)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        decimal montoPagaCliente;
        do
        {
            Console.Write("Monto Paga Cliente: ");
            if (!decimal.TryParse(Console.ReadLine(), out montoPagaCliente))
            {
                Console.WriteLine("Por favor, ingrese un monto válido.");
                Console.ReadKey();
                continue;
            }

            if (montoPagaCliente < montoPagar[indice])
            {
                Console.WriteLine("El monto pagado no puede ser menor al monto a pagar.");
            }
            else
            {
                break;
            }
        } while (true);

        return montoPagaCliente;
    }

    static decimal CalcularVuelto(int indice)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        return montoPagaCliente[indice] - montoPagar[indice];
    }

    static void MostrarDatosPago(int indice)
    {

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("===Sistema de Pago Servicios Publicos===");
        Console.WriteLine("===        Datos del Pago            ===");

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"Número de Pago:     {numeroPago[indice]}");
        Console.WriteLine($"Fecha:              {fecha[indice]:yyyy-MM-dd}");
        Console.WriteLine($"Hora:               {fecha[indice]:hh:mm}");
        Console.WriteLine($"Cédula:             {cedula[indice]}");
        Console.WriteLine($"Nombre:             {nombre[indice]}");
        Console.WriteLine($"Apellido1:          {apellido1[indice]}");
        Console.WriteLine($"Apellido2:          {apellido2[indice]}");
        Console.WriteLine($"Número de Caja:     {numeroCaja[indice]}");
        Console.WriteLine($"Tipo de Servicio:   {ObtenerNombreTipoServicio(tipoServicio[indice])}");
        Console.WriteLine($"Número de Factura:  {numeroFactura[indice]}");
        Console.WriteLine($"Monto a pagar:      {montoPagar[indice]:C}");
        Console.WriteLine($"Monto comisión:     {montoComision[indice]:C}");
        Console.WriteLine($"Monto deducido:     {montoDeducido[indice]:C}");
        Console.WriteLine($"Monto paga Cliente: {montoPagaCliente[indice]:C}");
        Console.WriteLine($"Vuelto:             {vuelto[indice]:C}");

    }

    static void ConsultarPagos()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Clear();
        Console.Write("Ingrese el número de pago que desea consultar: ");
        if (int.TryParse(Console.ReadLine(), out int numeroConsulta) && numeroConsulta >= 1 && numeroConsulta <= indice)
        {
            // Verificar si el número de pago está dentro del rango de pagos registrados
            int indiceConsulta = Array.IndexOf(numeroPago, numeroConsulta);

            if (indiceConsulta != -1)
            {
                // El pago se encuentra registrado, mostrar los datos
                MostrarDatosPago(indiceConsulta);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Pago no se encuentra registrado.");
                Console.ReadKey();

            }
        }
        else
        {
            Console.WriteLine("Número de pago inválido. Ingrese un número válido.");
            Console.ReadKey();

        }
    }

    static void ModificarPagos()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("Ingrese el número de pago que desea modificar: ");
        if (int.TryParse(Console.ReadLine(), out int numeroModificacion) && numeroModificacion >= 1 && numeroModificacion <= indice)
        {
            // Verificar si el número de pago está dentro del rango de pagos registrados
            int indiceModificacion = Array.IndexOf(numeroPago, numeroModificacion);

            if (indiceModificacion != -1)
            {
                // El pago se encuentra registrado, mostrar los datos y permitir la modificación
                MostrarDatosPago(indiceModificacion);
                ModificarDatosPago(indiceModificacion);
            }
            else
            {
                Console.WriteLine("Pago no se encuentra registrado.");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Número de pago inválido. Ingrese un número válido.");
            Console.ReadKey();
        }
    }

    static void ModificarDatosPago(int indiceModificacion)
    {

        // Ventana de modificación
        do
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("===Sistema de Pago Servicios Publicos===");
            Console.WriteLine("\n =======Menú de Modificación===");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("1. Modificar Fecha");
            Console.WriteLine("1. Modificar Hora");
            Console.WriteLine("2. Modificar Cédula");
            Console.WriteLine("3. Modificar Nombre");
            Console.WriteLine("4. Modificar Apellido 1");
            Console.WriteLine("5. Modificar Apellido 2");
            Console.WriteLine("6. Modificar Número de caja");
            Console.WriteLine("7. Modificar tipo de servicio");
            Console.WriteLine("8. Modificar número de factura");
            Console.WriteLine("9. Modificar Monto a Pagar");
            Console.WriteLine("10. Regresar al Menú Principal");

            Console.Write("Ingrese la opción deseada: ");
            if (int.TryParse(Console.ReadLine(), out int opcionModificacion))
            {
                ProcesarOpcionModificacion(opcionModificacion, indiceModificacion);
                if (opcionModificacion == 10)
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Por favor,Ingrese un número válido.");
            }
        } while (true);
    }

    static void ProcesarOpcionModificacion(int opcionModificacion, int indiceModificacion)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        switch (opcionModificacion)
        {

            case 1:
                fecha[indiceModificacion] = ObtenerFecha("Nueva Fecha (YYYY-MM-DD): ");

                break;

            case 2:
                hora[indiceModificacion] = Obtenerhora("Nueva Hora (hh:mm): ");

                break;
            case 3:
                Console.Write("\nNueva Cédula: ");
                cedula[indiceModificacion] = int.Parse(Console.ReadLine());
                Console.WriteLine("Cédula guardada satisfactoriamente"); Console.ReadKey();
                break;
            case 4:
                Console.Write("\nNuevo Nombre: ");
                nombre[indiceModificacion] = Console.ReadLine();
                Console.WriteLine("Nombre guardado satisfactoriamente"); Console.ReadKey();
                break;
            case 5:
                Console.Write("\nNuevo Apellido 1: ");
                apellido1[indiceModificacion] = Console.ReadLine();
                Console.WriteLine("Apellido guardad satisfactoriamente"); Console.ReadKey();
                break;
            case 6:
                Console.Write("\nNuevo Apellido 2: ");
                apellido2[indiceModificacion] = Console.ReadLine();
                break;
            case 7:
                numeroCaja[indiceModificacion] = ObtenerNumeroCaja("\nNuevo Número de Caja (1-3): ");
                Console.WriteLine("Número de caja guardada satisfactoriamente"); Console.ReadKey();
                break;
            case 8:
                tipoServicio[indiceModificacion] = ObtenerTipoServicio("\nNuevo Tipo de Servicio (1=Recibo de Luz, 2=Recibo Telefónico, 3=Recibo de Agua): ");
                Console.WriteLine("Tipo de servicio guardado satisfactoriamente"); Console.ReadKey();
                break;
            case 9:
                Console.Write("\nNuevo número de factura: ");
                numeroFactura[indiceModificacion] = Console.ReadLine();
                Console.WriteLine("Factura guardada satisfactoriamente"); Console.ReadKey();
                break;
            case 10:
                montoPagar[indiceModificacion] = ObtenerMonto("\nNuevo monto a pagar: ");
                CalcularComision(indiceModificacion);
                CalcularMontoDeducido(indiceModificacion);
                montoPagaCliente[indiceModificacion] = ObtenerMontoPagaCliente(indiceModificacion);
                vuelto[indiceModificacion] = CalcularVuelto(indiceModificacion);
                break;
            case 11:
                Console.WriteLine("Regresando al Menú Principal...");
                break;
            default:
                Console.WriteLine("Opción no válida. Ingrese un número del 1 al 10");
                break;
        }
    }

    static void EliminarPagos()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("Ingrese el número de pago que desea eliminar: ");
        if (int.TryParse(Console.ReadLine(), out int numeroEliminacion) && numeroEliminacion >= 1 && numeroEliminacion <= indice)
        {
            // Verificar si el número de pago está dentro del rango de pagos registrados
            int indiceEliminacion = Array.IndexOf(numeroPago, numeroEliminacion);

            if (indiceEliminacion != -1)
            {
                // El pago se encuentra registrado, mostrar los datos y preguntar para confirmar la eliminación
                MostrarDatosPago(indiceEliminacion);
                Console.Write("\n¿Está seguro de eliminar el dato? (Si/No): ");
                string respuestaEliminacion = Console.ReadLine().Trim().ToUpper();

                if (respuestaEliminacion == "S")
                {
                    // Eliminar el dato
                    EliminarDatosPago(indiceEliminacion);
                    Console.WriteLine("La información ya fue eliminada.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("La información no fue eliminada.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Pago no se encuentra Registrado.");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Número de pago inválido.Ingrese un número válido.");
        }
    }

    static void EliminarDatosPago(int indiceEliminacion)
    {
        // Mover los datos hacia atrás para llenar el espacio eliminado
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        for (int i = indiceEliminacion; i < indice - 1; i++)
        {
            numeroPago[i] = numeroPago[i + 1];
            fecha[i] = fecha[i + 1];
            hora[i] = hora[i + 1];
            cedula[i] = cedula[i + 1];
            nombre[i] = nombre[i + 1];
            apellido1[i] = apellido1[i + 1];
            apellido2[i] = apellido2[i + 1];
            numeroCaja[i] = numeroCaja[i + 1];
            tipoServicio[i] = tipoServicio[i + 1];
            numeroFactura[i] = numeroFactura[i + 1];
            montoPagar[i] = montoPagar[i + 1];
            montoComision[i] = montoComision[i + 1];
            montoDeducido[i] = montoDeducido[i + 1];
            montoPagaCliente[i] = montoPagaCliente[i + 1];
            vuelto[i] = vuelto[i + 1];
        }

        // Limpiar los últimos datos
        numeroPago[indice - 1] = 0;
        fecha[indice - 1] = DateTime.MinValue;
        hora[indice - 1] = TimeSpan.MinValue;
        cedula[indice - 1] = 0;
        nombre[indice - 1] = "";
        apellido1[indice - 1] = "";
        apellido2[indice - 1] = "";
        numeroCaja[indice - 1] = 0;
        tipoServicio[indice - 1] = 0;
        numeroFactura[indice - 1] = "";
        montoPagar[indice - 1] = 0;
        montoComision[indice - 1] = 0;
        montoDeducido[indice - 1] = 0;
        montoPagaCliente[indice - 1] = 0;
        vuelto[indice - 1] = 0;

        // Decrementar el índice
        indice--;
    }

    static void SubmenuReportes()
    {
        do
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("===Sistema de Pago Servicios Publicos===");
            Console.WriteLine("\nSubmenú Reportes");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("1. Ver todos los Pagos");
            Console.WriteLine("2. Ver pagos por tipo de servicio");
            Console.WriteLine("3. Ver pagos por código de caja");
            Console.WriteLine("4. Ver dinero comisionado por servicios");
            Console.WriteLine("5. Regresar al Menú Principal");

            Console.Write("Ingrese la opción deseada: ");
            if (int.TryParse(Console.ReadLine(), out int opcionReporte))
            {
                ProcesarOpcionReporte(opcionReporte);
                if (opcionReporte == 5)
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Por favor, Ingrese un número válido.");
            }
        } while (true);
    }

    static void ProcesarOpcionReporte(int opcionReporte)
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        switch (opcionReporte)
        {
            case 1:
                VerTodosLosPagos();
                break;
            case 2:
                ReporteVerPagosPorTipoServicio();
                break;
            case 3:
                ReporteVerPagosPorCodigoCaja();
                break;
            case 4:
                VerDineroComisionadoPorServicios();
                break;
            case 5:
                Console.WriteLine("Regresando al Menú Principal.");
                break;
            default:
                Console.WriteLine("Opción no válida. Ingrese un número del 1 al 5.");
                break;
        }
    }

    static void VerTodosLosPagos()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Clear(); // Limpiar la ventana

        Console.WriteLine("Reporte ver todos los pagos");

        // Mostrar todos los pagos
        for (int i = 0; i < indice; i++)
        {
            MostrarDatosPago(i);
            Console.WriteLine("------------------------------");
        }

        Console.WriteLine("\nPresione Enter para regresar al Submenú Reportes.");
        Console.ReadLine();
        Console.Clear(); // Limpiar la ventana después de presionar Enter
    }

    static void ReporteVerPagosPorTipoServicio()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Clear(); // Limpiar la ventana

        Console.WriteLine("Reporte ver pagos por tipo de servicio");

        Console.Write("Ingrese el tipo de servicio que desea ver (1=Recibo de Luz, 2=Recibo Telefónico, 3=Recibo de Agua): ");
        if (int.TryParse(Console.ReadLine(), out int tipoServicioSeleccionado) && tipoServicioSeleccionado >= 1 && tipoServicioSeleccionado <= 3)
        {
            Console.WriteLine($"\nPagos del tipo de servicio: {ObtenerNombreTipoServicio(tipoServicioSeleccionado)}");

            // Mostrar los pagos del tipo de servicio seleccionado
            for (int i = 0; i < indice; i++)
            {
                if (tipoServicio[i] == tipoServicioSeleccionado)
                {
                    MostrarDatosPago(i);
                    Console.WriteLine("------------------------------");
                }
            }
        }
        else
        {
            Console.WriteLine("Tipo de servicio inválido. No se generó el reporte.");
        }

        Console.WriteLine("\nPresione enter para regresar al Submenú Reportes.");
        Console.ReadLine();
        Console.Clear(); // Limpiar la ventana después de presionar Enter
    }

    static void ReporteVerPagosPorCodigoCaja()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Clear(); // Limpiar la ventana

        Console.WriteLine("Reporte Ver Pagos por Código de Caja");

        Console.Write("Ingrese el código de caja que desea ver (1, 2 o 3): ");
        if (int.TryParse(Console.ReadLine(), out int codigoCajaSeleccionado) && codigoCajaSeleccionado >= 1 && codigoCajaSeleccionado <= 3)
        {
            Console.WriteLine($"\nPagos del Código de Caja: {codigoCajaSeleccionado}");

            // Mostrar los pagos del código de caja seleccionado
            for (int i = 0; i < indice; i++)
            {
                if (numeroCaja[i] == codigoCajaSeleccionado)
                {
                    MostrarDatosPago(i);
                    Console.WriteLine("------------------------------");
                }
            }
        }
        else
        {
            Console.WriteLine("Código de caja inválido. No se generó el reporte.");
        }

        Console.WriteLine("\nPresione Enter para regresar al Submenú Reportes.");
        Console.ReadLine();
        Console.Clear(); // Limpiar la ventana después de presionar Enter
    }

    static void VerDineroComisionadoPorServicios()
    {

        Console.Clear(); // Limpiar la ventana

        Console.WriteLine("Reporte Ver Dinero Comisionado por Servicios");

        // Inicializar variables para el resumen
        decimal totalComisionElectricidad = 0;
        int cantidadTransaccionesElectricidad = 0;

        decimal totalComisionTelefono = 0;
        int cantidadTransaccionesTelefono = 0;

        decimal totalComisionAgua = 0;
        int cantidadTransaccionesAgua = 0;

        // Calcular el resumen
        for (int i = 0; i < indice; i++)
        {
            switch (tipoServicio[i])
            {
                case 1: // Recibo de Luz
                    totalComisionElectricidad += montoComision[i];
                    cantidadTransaccionesElectricidad++;
                    break;
                case 2: // Recibo Telefónico
                    totalComisionTelefono += montoComision[i];
                    cantidadTransaccionesTelefono++;
                    break;
                case 3: // Recibo de Agua
                    totalComisionAgua += montoComision[i];
                    cantidadTransaccionesAgua++;
                    break;
                default:
                    Console.WriteLine($"Tipo de servicio desconocido en la transacción {i + 1}. No se incluyó en el resumen.");
                    break;
            }
        }

        // Mostrar el resumen
        Console.WriteLine("\nResumen de Dinero Comisionado por Servicios:");
        MostrarResumenTipoServicio("Recibo de Luz", totalComisionElectricidad, cantidadTransaccionesElectricidad);
        MostrarResumenTipoServicio("Recibo Telefónico", totalComisionTelefono, cantidadTransaccionesTelefono);
        MostrarResumenTipoServicio("Recibo de Agua", totalComisionAgua, cantidadTransaccionesAgua);

        Console.WriteLine("\nPresione enter para regresar al Submenú Reportes.");
        Console.ReadLine();
        Console.Clear(); // Limpiar la ventana después de presionar Enter
    }

    static void MostrarResumenTipoServicio(string nombreServicio, decimal totalComision, int cantidadTransacciones)
    {
        Console.WriteLine($"Servicio: {nombreServicio}");
        Console.WriteLine($"Total Comisionado: {totalComision:C}");
        Console.WriteLine($"Cantidad de Transacciones: {cantidadTransacciones}\n");
    }

    static string ObtenerNombreTipoServicio(int tipoServicio)
    {
        switch (tipoServicio)
        {
            case 1:
                return "Recibo de Luz";
            case 2:
                return "Recibo Telefónico";
            case 3:
                return "Recibo de Agua";
            default:
                return "Desconocido";
        }
    }

    static DateTime ObtenerFecha(string mensaje)
    {
        DateTime fecha;
        do
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine();
            if (!DateTime.TryParse(entrada, out fecha))
            {
                Console.WriteLine("Fecha inválida. Por favor, ingrese una fecha en formato YYYY-MM-DD.");
                Console.WriteLine($"Entrada recibida: '{entrada}'");
            }
            else
            {
                break;
            }
        } while (true);

        return fecha;
    }

    static TimeSpan Obtenerhora(string mensaje)
    {
        TimeSpan hora;
        do
        {
            Console.Write(mensaje);
            if (!TimeSpan.TryParse(Console.ReadLine(), out hora))
            {
                Console.WriteLine("Hora inválida.Por favor, ingrese una hora en formato hh:mm.");
                Console.Clear();
            }
            else
            {
                break;
            }
        } while (true);

        return hora;
    }

    static int ObtenerNumeroCaja(string mensaje)
    {
        int numeroCaja;
        do
        {
            Console.Write(mensaje);
            if (!int.TryParse(Console.ReadLine(), out numeroCaja) || numeroCaja < 1 || numeroCaja > 3)
            {
                Console.WriteLine("Número de caja inválido. Ingrese un número del 1 al 3");
            }
            else
            {
                break;
            }
        } while (true);

        return numeroCaja;
    }

    static int ObtenerTipoServicio(string mensaje)
    {
        int tipoServicio;
        do
        {
            Console.Write(mensaje);
            if (!int.TryParse(Console.ReadLine(), out tipoServicio) || tipoServicio < 1 || tipoServicio > 3)
            {
                Console.WriteLine("Tipo de servicio inválido. Ingrese un número del 1 al 3.");
            }
            else
            {
                break;
            }
        } while (true);

        return tipoServicio;
    }

    static decimal ObtenerMonto(string mensaje)
    {
        decimal monto;
        do
        {
            Console.Write(mensaje);
            if (!decimal.TryParse(Console.ReadLine(), out monto) || monto <= 0)
            {
                Console.WriteLine("Monto inválido.Ingrese un monto mayor a cero.");
            }
            else
            {
                break;
            }
        } while (true);

        return monto;
    }
}

