using Application.Authentication;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Whatsapp_Api.Controllers;

namespace Test
{
    public class AuthControllerTest : IDisposable
    {
        private readonly Mock<IMediator> _mockAuthService;
        public readonly AuthController _controller;


        public AuthControllerTest()
        {
            _mockAuthService = new Mock<IMediator>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Login_With_User_NotFound_Returns_NotFoundResult()
        {
            // Arrange: Crear el comando de autenticación
            var data = new AuthCommand
            {
                Email = "odiseo@gmail.com",
                Password = "12345"
            };

            // Simula que el mediador retorna null para indicar que el usuario no fue encontrado
            _mockAuthService
                .Setup(mediator => mediator.Send(It.IsAny<AuthCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Response)null);  // Simula el escenario de usuario no encontrado

            // Act: Llama al método Login en el controlador
            var result = await _controller.Login(data);

            // Assert: Verifica que el resultado sea un NotFoundResult
            Assert.IsInstanceOf<NotFoundObjectResult>(result);  // Verifica que el resultado sea un 404 NotFound
        }



        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}