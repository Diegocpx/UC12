using System.IdentityModel.Tokens.Jwt;
using ChapterBET9.Controllers;
using ChapterBET9.Interfaces;
using ChapterBET9.Models;
using ChapterBET9.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TesteIntegracao
{
    public class LoginControllerTeste
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {
            //arrange Prepara��o

            var repositoryFake = new Mock<IUsuarioRepository>();

            repositoryFake.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            var controller = new LoginController(repositoryFake.Object);

            LoginViewModel dadosUsuario = new LoginViewModel();
            dadosUsuario.email = "cebola@email.com";
            dadosUsuario.senha = "cebolinha";

            //Act A��o

            var resultado = controller.Login(dadosUsuario);

            //Assert Verifica��o

            Assert.IsType<UnauthorizedObjectResult>(resultado);
        
        }

        [Fact]

        public void LoginController_retornar_token()
        {
            //arrange prepara��o
            Usuario usuarioRetornado = new Usuario();
            usuarioRetornado.Email = "email@email.com";
            usuarioRetornado.Senha = "1234";
            usuarioRetornado.Tipo = "0";
            usuarioRetornado.Id = 1;
            
            
            var repositoryFake = new Mock<IUsuarioRepository>();

            repositoryFake.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetornado);

            LoginViewModel dadosUsuario = new LoginViewModel();
            dadosUsuario.email = "cebola@email.com";
            dadosUsuario.senha = "cebolinha";

            var controller = new LoginController(repositoryFake.Object);

            //act a�ao

            OkObjectResult resultado = (OkObjectResult)controller.Login(dadosUsuario);

            string[] tokenString = resultado.Value.ToString().Split(' ')[3];

            var JwtHandler = new JwtSecurityTokenHandler();
            var tokenJwt = JwtHandler.ReadJwtToken(tokenString);

            //Assert verifica��o
            Assert.Equal("chapter.webapi", resultado.tokenJwt.Issuer);

        }


    }
}