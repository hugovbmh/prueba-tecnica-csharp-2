using ExamenFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ExamenFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult MostrarEmpleados_IT_FINANCE()
        {
            try
            {
                #region obtener registros de tabla empleados

                string cadena = "server=HUGOMH; database=ExamenFinal; user id=sa; password=123456; Encrypt=false;";
                SqlConnection con = new SqlConnection(cadena);
                string query = "SELECT EMP.NOMBRES + ' ' + EMP.APELLIDOS AS 'Nombre Completo',\r\nEMP.SUELDO AS 'Sueldo', \r\nDATEDIFF(MONTH, EMP.FECHA_INGRESO, GETDATE()) AS 'Tiempo de servicio del empleado (en meses)' \r\nFROM CL_EMPLEADOS EMP \r\nINNER JOIN CL_DEPARTAMENTOS DEP ON EMP.ID_DPTO=DEP.ID_DPTO\r\nWHERE DEP.NOMBRE_DPTO='IT' OR DEP.NOMBRE_DPTO='FINANCE';";
                SqlDataAdapter da = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                #endregion

                TempData["MSG"] = "Estos son los registros de empleados IT y FINANCE";

                return View(dt);
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = ex.Message;
                return View();
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}