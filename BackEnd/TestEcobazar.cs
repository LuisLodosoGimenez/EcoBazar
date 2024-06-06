using Xunit;
using Moq;
using backend.Controllers;
using backend.Logica;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

public class TestEcobazar
{
    private readonly Mock<InterfazLogica> _mockLogica;
    private readonly ApiController _controller;

    public TestEcobazar()
    {
        _mockLogica = new Mock<InterfazLogica>();
        _controller = new ApiController(_mockLogica.Object);
    }

    [Fact]
    public async Task RegistrarComprador_DevuelveOk()
    {
        
        var registro = new ApiController.Registro
        {
            TipoRegistro = "Comprador",
            Nombre = "Lucas Gómez",
            NickName = "lucasg",
            Contraseña = "Contraseña1#",
            Email = "lucasg@example.com",
            Edad = 30,
            LimiteGasto = 1000
        };

        
        var result = await _controller.RegistrarComprador(registro);

        
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task RegistrarComprador_DevuelveError()
    {
        
        var registro = new ApiController.Registro
        {
            TipoRegistro = "Comprador",
            Nombre = "Lucas Gómez",
            NickName = "lucasg",
            Contraseña = "Contraseña1#",
            Email = "lucasg@example.com",
            Edad = 30,
            LimiteGasto = 1000
        };

        _mockLogica.Setup(logica => logica.RegistrarComprador(It.IsAny<ApiController.Registro>()))
                   .ThrowsAsync(new System.Exception("Registration error"));

        
        var result = await _controller.RegistrarComprador(registro);

        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task IniciarSesion_DevuelveOk()
    {
        
        var inicioSesion = new ApiController.InicioSesion
        {
            TipoSesion = "Comprador",
            NickName = "lucasg",
            Contraseña = "Contraseña1#"
        };

        var comprador = new Comprador("John Doe", "lucasg", "Contraseña1#", "lucasg@example.com");

        _mockLogica.Setup(logica => logica.IniciarSesion(inicioSesion.NickName, inicioSesion.Contraseña, inicioSesion.TipoSesion))
                   .ReturnsAsync(comprador);

        
        var result = await _controller.IniciarSesion(inicioSesion);

        
        var responseData = Assert.IsType<OkObjectResult>(result).Value;
        var compradorData = responseData!.GetType().GetProperty("Comprador")!.GetValue(responseData, null);
        Assert.Equal(comprador, compradorData);
    }

    [Fact]
    public async Task IniciarSesion_DevuelveError()
    {
        
        var inicioSesion = new ApiController.InicioSesion
        {
            TipoSesion = "Comprador",
            NickName = "lucasg",
            Contraseña = "Contraseña1#"
        };

        _mockLogica.Setup(logica => logica.IniciarSesion(inicioSesion.NickName, inicioSesion.Contraseña, inicioSesion.TipoSesion))
                   .ThrowsAsync(new System.Exception("Login error"));

        
        var result = await _controller.IniciarSesion(inicioSesion);

        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task ObtenerProductorPorCategoría_DevuelveOk()
    {
        
        var categoria = "Electronica";
        var productos = new List<Producto> { new Producto(1000, 10, 5, new Vendedor("Marta López", "martita", "Contraseña2#","martita@gmail.com"), new Articulo("Auriculares", "Electrónica", 10, "Por su salud, no usar con el volumen al máximo", "Depositar en un punto limpio", "España", "Proceso automatizado", "Impacto social positivo e impacto ambiental medio", "Contribuye al ODS 9", new Productor("Daniel Pérez", "daniel123", "Contraseña3#", "daniel@gmail.com"))) };

        _mockLogica.Setup(logica => logica.ObtenerProductosPorCategoria(categoria))
                   .ReturnsAsync(productos);

        
        var result = await _controller.ObtenerProductorPorCategoría(categoria);

        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(productos, okResult.Value);
    }

    [Fact]
    public async Task ObtenerProductorPorCategoría_DevuelveError()
    {
        
        var categoria = "Electronica";

        _mockLogica.Setup(logica => logica.ObtenerProductosPorCategoria(categoria))
                   .ThrowsAsync(new System.Exception("Category error"));

        
        var result = await _controller.ObtenerProductorPorCategoría(categoria);

        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task AñadirProductoACarritoCompra_DevuelveOk()
    {
        
        var carritoCompra = new ApiController.CarritoCompra
        {
            idComprador = 1,
            idProducto = 2
        };

        var comprador = new Comprador("Lucas Gómez", "lucasg", "Contraseña1#", "lucasg@example.com");
        comprador.CarritoCompra.Add(new Producto(1000, 10, 5, new Vendedor("Marta López", "martita", "Contraseña2#","martita@gmail.com"), new Articulo("Auriculares", "Electrónica", 10, "Por su salud, no usar con el volumen al máximo", "Depositar en un punto limpio", "España", "Proceso automatizado", "Impacto social positivo e impacto ambiental medio", "Contribuye al ODS 9", new Productor("Daniel Pérez", "daniel123", "Contraseña3#", "daniel@gmail.com"))));

        _mockLogica.Setup(logica => logica.AñadirProductoACarritoCompra(carritoCompra.idComprador.Value, carritoCompra.idProducto.Value))
                   .ReturnsAsync(comprador);

        
        var result = await _controller.AñadirProductoACarritoCompra(carritoCompra);

        
        var responseData = Assert.IsType<OkObjectResult>(result).Value;
        var carritoData = responseData!.GetType().GetProperty("CarritoCompra")!.GetValue(responseData, null);
        Assert.Equal(comprador.CarritoCompra, carritoData);
    }

    [Fact]
    public async Task AñadirProductoACarritoCompra_DevuelveError()
    {
        
        var carritoCompra = new ApiController.CarritoCompra
        {
            idComprador = 1,
            idProducto = 2
        };

        _mockLogica.Setup(logica => logica.AñadirProductoACarritoCompra(carritoCompra.idComprador.Value, carritoCompra.idProducto.Value))
                   .ThrowsAsync(new System.Exception("Add to cart error"));

        
        var result = await _controller.AñadirProductoACarritoCompra(carritoCompra);

        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
}