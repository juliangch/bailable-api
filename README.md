# bailable-api
Web API  de Bailable APP


---PASOS PARA PODER EJECUTAR LA SOLUCION---
El proyecto contiene una carpeta llamada sql, en la cual se encuentran los scripts para generar los datos en la base de datos.


1. Crear la base de datos , ejectuando  la query del archivo 1.CrearBaseDeDatos.sql
2. Agregar como owner de la base de datos el siguiento usuario , para poder realizar la conexion con el proyecto  utilizando estas credenciales : User= bailable_user Password= bailable123
3. Luego desde la terminal , ejecutar el comando ```dotnet ef database update``` para aplicar la migracion.
4. Cargar la base de datos , utilizando la query del archivo 2.CargarBaseDeDatos.sql
   
Una vez realizados estos pasos , la aplicacion ya se puede ejecutar sin problema.
