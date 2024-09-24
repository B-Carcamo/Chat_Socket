# Chat Socket Application

Esta es una aplicación de consola desarrollada en C# utilizando **sockets** y **Entity Framework Core**. Permite la comunicación en tiempo real entre múltiples clientes a través de un servidor central.

## Características

- Comunicación en tiempo real entre múltiples clientes.
- Arquitectura cliente-servidor.
- Uso de **Entity Framework Core** para la persistencia de datos.
- Compatible con entornos **Windows** y **Linux**.
  
## Requisitos

### Requisitos en Linux

- [Visual Studio Code](https://visualstudio.microsoft.com/es/downloads/)
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download)
  
#### Opcional:
- Extensión de VSCode: [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)

#### Opciones de SQL Server:
- [SQL Server Docker o Linux](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)

### Requisitos en Windows

- [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/community/)
- [SQL Server for Windows](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)

## Instalación y Configuración

### Clonar el Repositorio

Clona el repositorio con **Git** utilizando el siguiente comando:
```bash
git clone https://github.com/B-Carcamo/Chat_Socket.git
```
### Configuración de la Base de Datos

1. Ejecuta el script de la base de datos `Chat_Socket.sql` para crear las tablas necesarias.
2. Cambia la configuración del `stringConnection` en el archivo `Models/Message.cs` para que apunte a tu servidor SQLServer.

### Para Ejecutar el Server_Chat y el Client_Chat
### En Windows:
- Corre el proyecto desde **Visual Studio Community** o compila el código y ejecuta el archivo `.exe` generado.

#### En Linux:
- Navega a la carpeta del servidor y ejecuta el comando:
  
  ```bash
  dotnet run