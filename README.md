📜 Sobre el Proyecto
Registro Estudiantes es el backend de una aplicación de gestión académica. La API ha sido diseñada para ser consumida por un frontend (por ejemplo, en Angular), proveyendo todos los endpoints necesarios para administrar estudiantes, materias y profesores de manera eficiente y segura.

El núcleo del proyecto se basa en Arquitectura Limpia (Clean Architecture), lo que garantiza una separación clara de las responsabilidades, alta cohesión y bajo acoplamiento. Esto no solo facilita el mantenimiento y las pruebas, sino que también permite que el sistema evolucione de manera ordenada.

✨ Características
👨‍🎓 Gestión de Estudiantes: Operaciones CRUD (Crear, Leer, Actualizar, Eliminar) completas para los estudiantes.

📚 Registro de Materias: Cada estudiante puede registrar un máximo de 3 materias de un catálogo de 10 disponibles.

⚖️ Sistema de Créditos: Cada materia registrada equivale a 3 créditos académicos.

👨‍🏫 Asignación de Profesores: Un total de 5 profesores, cada uno dictando 2 materias específicas.

🔒 Regla de Negocio Clave: Se implementó una restricción para impedir que un estudiante elija dos materias impartidas por el mismo profesor, asegurando la diversidad académica.

🚀 API Optimizada: El diseño de la API está pensado para un consumo eficiente y rápido desde clientes web modernos.

🔧 Tecnologías Utilizadas
Este proyecto fue construido utilizando un stack de tecnologías moderno y robusto:

Framework: ASP.NET Core 8 Web API

Base de Datos: Microsoft SQL Server

ORM/Micro-ORM: Dapper (para un acceso a datos de alto rendimiento)

Mapeo de Objetos: AutoMapper

Arquitectura: Clean Architecture

Principios de Diseño: SOLID

📊 Guía de Instalación
Para poner en marcha este proyecto en tu entorno local, sigue estos pasos:

Prerrequisitos
Tener instalado el SDK de .NET 8.

Tener acceso a una instancia de SQL Server.

1. Base de Datos
Ejecuta el script ScriptBDStudentLiliana.sql en tu instancia de SQL Server. Esto creará la base de datos RegistroEstudiantes y cargará los datos iniciales de materias y profesores.

Importante: Asegúrate de que el nombre de la base de datos coincida exactamente (RegistroEstudiantes), ya que es el que espera la cadena de conexión.

2. Configuración del Proyecto
Clona el repositorio:

git clone https://github.com/lilipa0916/StudentRegistrationLiliana

Navega al directorio:

cd backend-estudiantes

Configura la cadena de conexión:
Abre el archivo appsettings.json y modifica la sección DefaultConnection con los datos de tu servidor SQL.

"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=RegistroEstudiantes;Trusted_Connection=True;TrustServerCertificate=True;"
}

Reemplaza TU_SERVIDOR por el nombre de tu instancia de SQL Server (ej. localhost, SQLEXPRESS, etc.).

Ejecuta el proyecto:

dotnet run

🚀 Uso
Una vez que el proyecto se esté ejecutando, la API estará disponible en la URL base https://localhost:7299.

Por defecto, ASP.NET Core Web API incluye Swagger/OpenAPI, lo que te proporciona una interfaz de usuario interactiva para explorar y probar todos los endpoints disponibles.

Puedes acceder a la documentación de la API visitando:

➡️ https://localhost:7299/swagger

