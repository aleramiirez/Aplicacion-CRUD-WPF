# Proyecto Aplicación CRUD

## Descripción del Proyecto

Este proyecto es una aplicación WPF que gestiona productos y categorías utilizando una base de datos MySQL. La aplicación consta de varias páginas para crear, leer, actualizar, eliminar y visualizar información relacionada con productos. También incluye funcionalidades de inicio de sesión.

## Estructura del Proyecto

El proyecto está dividido en varias partes. A continuación, se proporciona una descripción de cada parte del proyecto:

### Login

La funcionalidad de inicio de sesión se encuentra en el `LoginWindow`. Aquí, el usuario debe ingresar sus credenciales para acceder a las otras partes de la aplicación. Se utiliza un enfoque básico de usuario y contraseña para la autenticación.

### MainWindow

El `MainWindow` actúa como el punto de entrada principal de la aplicación después de iniciar sesión. Desde aquí, el usuario puede acceder a las diferentes secciones de la aplicación, como `CreatePage`, `InfoPage`, `ReadPage`, `UpdatePage`, `HomePage` y `DeletePage`.

### HomePage

La página `HomePage` presenta una visión general de la aplicación, proporcionando enlaces rápidos a las diferentes secciones. Esto puede incluir estadísticas generales, información de resumen, etc.

### CreatePage

La página `CreatePage` permite al usuario crear nuevos productos. Contiene un formulario con campos para el nombre del producto, la categoría y el precio unitario. Al hacer clic en el botón "Crear Producto", se inserta un nuevo registro en la base de datos.

### ReadPage

La página `ReadPage` permite al usuario seleccionar un producto de un ComboBox y ver sus detalles en un DataGrid. También permite seleccionar una categoría para ver todos los productos de esa categoría.

### UpdatePage

La página `UpdatePage` permite al usuario actualizar la información de un producto. Primero selecciona un producto de un ComboBox, luego puede modificar el nombre, categoría y precio unitario antes de hacer clic en "Actualizar Producto".

### DeletePage

La página `DeletePage` permite al usuario seleccionar y eliminar un producto. Contiene un ComboBox para seleccionar el producto y un botón "Eliminar Producto" para confirmar la eliminación.

### InfoPage

La página `InfoPage` muestra información útil como los 5 productos más vendidos, productos sin stock y los 5 productos más caros. Utiliza DataGrids para presentar esta información de manera organizada.

## Uso de la Base de Datos

La aplicación utiliza una base de datos MySQL para almacenar la información de productos y categorías. Asegúrate de configurar correctamente la cadena de conexión en la clase `DataBase.DataBase`.

## Dependencias

- [MySql.Data](https://www.nuget.org/packages/MySql.Data/) - Biblioteca .NET para la comunicación con MySQL.

## Requisitos del Sistema

- .NET Framework 4.7.2 o superior.

## Instalación y Ejecución

1. Clona el repositorio.
2. Configura la cadena de conexión en `DataBase.DataBase`.
3. Abre el proyecto en Visual Studio.
4. Compila y ejecuta la aplicación.

¡Disfruta del proyecto!
