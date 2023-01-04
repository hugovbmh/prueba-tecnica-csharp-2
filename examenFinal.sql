--2. Indicar la cantidad de Registros por cada Tabla de la Base de Datos.
SELECT COUNT(*) AS 'Cantidad de registros de la tabla CL_CARGOS' FROM CL_CARGOS;
SELECT COUNT(*) AS 'Cantidad de registros de la tabla CL_DEPARTAMENTOS' FROM CL_DEPARTAMENTOS;
SELECT COUNT(*) AS 'Cantidad de registros de la tabla CL_EMPLEADOS' FROM CL_EMPLEADOS;
SELECT COUNT(*) AS 'Cantidad de registros de la tabla CL_LOCALIDAD' FROM CL_LOCALIDAD;
SELECT COUNT(*) AS 'Cantidad de registros de la tabla CL_PAISES' FROM CL_PAISES;
SELECT COUNT(*) AS 'Cantidad de registros de la tabla CL_REGIONES' FROM CL_REGIONES;
GO
--3. Exportar la información de la tabla CL_CARGOS, CL_EMPLEADOS en formato txt y adjuntarlo por correo.(HECHO)
--4. Crear una tabla llamada HM_SBSRCC con los siguientes campos.(HECHO)
CREATE TABLE [dbo].[HM_SBSRCC](
	[PERIODO] [varchar](8) NULL,
	[TIP_DOC_IDE] [varchar](4) NULL,
	[DOC_IDENTIDAD] [varchar](8) NULL,
	[COD_EMPRESA] [int] NULL,
	[SALDO_UTILIZADO] [decimal](10, 2) NULL,
	[SALDO_NO_UTILIZADO] [decimal](10, 2) NULL
) ON [PRIMARY]
GO
SELECT * FROM HM_SBSRCC;
GO
--5. Mostrar los datos de los empleados que tengan como ID_GERENTE = 103 y 200.
SELECT * FROM CL_EMPLEADOS
WHERE ID_GERENTE=103 OR ID_GERENTE=200;
GO
--6. Mostrar los empleados que trabajan en el departamento de ‘IT’ y ‘FINANCE’.
SELECT EMP.NOMBRES + ' ' + EMP.APELLIDOS AS 'Nombre Completo',
EMP.SUELDO AS 'Sueldo', 
DATEDIFF(MONTH, EMP.FECHA_INGRESO, GETDATE()) AS 'Tiempo de servicio del empleado (en meses)' 
FROM CL_EMPLEADOS EMP 
INNER JOIN CL_DEPARTAMENTOS DEP ON EMP.ID_DPTO=DEP.ID_DPTO
WHERE DEP.NOMBRE_DPTO='IT' OR DEP.NOMBRE_DPTO='FINANCE';
GO
--7. Relacionar las tablas Empleado, Cargo y filtrar los que tengan cargo de Programador (IT_PROG) y Contador (FI_ACCOUNT) 
--   cuyos rangos de sueldos son mayores a 5000 y menores igual a 7000. 
SELECT EMP.ID_EMPLEADO, EMP.APELLIDOS + ' ' + EMP.NOMBRES AS 'NOMBRE', EMP.SUELDO,EMP.ID_CARGOS,C.NOMBRE_CARGO
FROM CL_EMPLEADOS EMP 
INNER JOIN CL_CARGOS C ON C.ID_CARGOS=EMP.ID_CARGOS
WHERE EMP.SUELDO > 5000 AND EMP.SUELDO <= 7000 AND (EMP.ID_CARGOS='IT_PROG' OR EMP.ID_CARGOS='FI_ACCOUNT');
GO
--8. Relacionar las tablas Empleado, Departamento, Localidad, País y mostrar el gasto total (suma de sueldo), 
--   por cada departamento en el país de UNITED STATES OF AMERICA
SELECT DEP.NOMBRE_DPTO, SUM(EMP.SUELDO) AS 'GASTO TOTAL', P.NOMBRE_PAIS
FROM CL_EMPLEADOS EMP
INNER JOIN CL_DEPARTAMENTOS DEP ON EMP.ID_DPTO=DEP.ID_DPTO
INNER JOIN CL_LOCALIDAD L ON L.ID_LOCALIDAD=DEP.ID_LOCALIDAD
INNER JOIN CL_PAISES P ON P.ID_PAIS=L.ID_PAIS
WHERE P.NOMBRE_PAIS='UNITED STATES OF AMERICA'
GROUP BY DEP.NOMBRE_DPTO,P.NOMBRE_PAIS;
GO

SELECT * FROM CL_EMPLEADOS;
SELECT * FROM CL_DEPARTAMENTOS;
SELECT * FROM CL_LOCALIDAD;
SELECT * FROM CL_PAISES;
GO

--9. Crear un procedimiento almacenado que retorne una lista de países con el salario promedio, el salario máximo y el salario mínimo 
--   y que el país tenga más de 2 empleados
CREATE PROCEDURE LISTAR_PAISES
	AS
    SELECT P.NOMBRE_PAIS, AVG(EMP.SUELDO) AS 'SALARIO PROMEDIO', MAX(EMP.SUELDO) AS 'SALARIO MAXIMO', MIN(EMP.SUELDO) AS 'SALARIO MINIMO'  
	FROM CL_EMPLEADOS EMP
	INNER JOIN CL_DEPARTAMENTOS DEP ON EMP.ID_DPTO=DEP.ID_DPTO
	INNER JOIN CL_LOCALIDAD L ON L.ID_LOCALIDAD=DEP.ID_LOCALIDAD
	INNER JOIN CL_PAISES P ON P.ID_PAIS=L.ID_PAIS
	GROUP BY P.NOMBRE_PAIS
	HAVING COUNT(EMP.ID_DPTO) > 2;
GO
EXEC LISTAR_PAISES;