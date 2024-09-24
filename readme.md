Aplicación de Chat por Socket
Descripción

Esta es una aplicación de consola desarrollada en C# utilizando sockets y Entity Framework Core. Permite la comunicación en tiempo real entre múltiples clientes a través de un servidor central.
Requisitos
Linux

    Visual Studio Code: https://visualstudio.microsoft.com/es/downloads/
    .NET 8.0 : https://dotnet.microsoft.com/en-us/download
    Opcional: Extensión de VSCode C# Dev Kit: https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit
    SQL Server:
        Docker: https://www.microsoft.com/es-es/sql-server/sql-server-downloads
        Linux: https://www.microsoft.com/es-es/sql-server/sql-server-downloads

Windows

    Visual Studio Community 2022: https://visualstudio.microsoft.com/es/downloads/
    SQL Server para Windows: https://www.microsoft.com/es-es/sql-server/sql-server-downloads

Inicio

    Clonar el repositorio utilizando Git: https://github.com/B-Carcamo/Chat_Socket.git
    Ejecutar el script de la base de datos Chat_Socket.sql y actualizar la configuración de stringConnection en Models/Message.cs
    Ejecutar el programa Server_Chat primero:
        Windows: Ejecutar desde Visual Studio Community o compilar y ejecutar el archivo .exe
        Linux: Ejecutar el comando dotnet run
    Ejecutar varias instancias de Client_Chat:
        Windows: Ejecutar desde Visual Studio Community o compilar y ejecutar el archivo .exe
        Linux: Ejecutar el comando dotnet run
