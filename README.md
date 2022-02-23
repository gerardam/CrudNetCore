# CRUD ASP.Net Core MCV
Funciones CRUD (Create, Read, Update y Delete) utilizando diferentes métodos de acceso a BD de SQL Server

<img src="https://lh3.googleusercontent.com/JP2FqMxcDEDyIi4ohHRa1t-ekr9KBCDVHgzhWlBUEZhaZ_ttl3jjXqy3jI4zyQ7AH4CS65g5fKzDSXb6tgmRiYzxf06Rbz5MSHU6OG88ZQV8Y8SauR_GlJ1_dRkU5XLFoOwLdBQD4Q=w2400" alt="Login" />

<b>Opciones de acceso:</b>

* Mediante el EntityFrameworkCore
* Acceso a SP de BD mediante SqlClient y Linq

<b>Para la comunicacion con los SP de la base de datos se realiza mediante:</b>

* Una clase para la definicion de tipos de dato de los parametros
* La declaracion de tipos directamente en los parametros
* Metodos CRUD en clase externa; instanciado desde un controlador

###### Ejemplo de conexion con SP:

        public async Task<IActionResult> Index()
        {
            var opc = new SqlParameter("@Opcion", SqlDbType.TinyInt);
            
            opc.Value = 1;

            return View(await _context.SalonT.FromSqlRaw("exec Salones_SP @Opcion",
                            opc).ToListAsync());
        }


