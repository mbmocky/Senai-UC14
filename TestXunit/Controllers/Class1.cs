using Microsoft.AspNetCore.Mvc;
using APIUC14.Controllers;
using APIUC14.Interfaces;
using APIUC14.Models;
using APIUC14.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace TestXunit.Controllers
{
    public class LoginControllerTest
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {
            //Arrange
            var fakeRepository = new Mock<IUsuarioRepository>();
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            LoginViewModel dadosLogin = new LoginViewModel();

            dadosLogin.Email = "teste.email.com";
            dadosLogin.Senha = "1234";

            var controller = new LoginController(fakeRepository.Object);

            //Act
            var resultado = controller.Login(dadosLogin);

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(resultado);
        }

        [Fact]
        public void LoginController_Retornar_Token()
        {
            //Arrange
            Usuario usuarioRetorno = new Usuario();
            usuarioRetorno.Email = "teste.email.com";
            usuarioRetorno.Senha = "1234";
            usuarioRetorno.Tipo = "0";

            var fakeRepository = new Mock<IUsuarioRepository>();
            fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetorno);

            string issuerValidacao = "chapter.webapi";

            LoginViewModel dadosLogin = new LoginViewModel();
            dadosLogin.Email = "teste.email.com";
            dadosLogin.Senha = "1234";

            var controller = new LoginController(fakeRepository.Object);

            //Act
            OkObjectResult resultado = (OkObjectResult)controller.Login(dadosLogin);

            string token = resultado.Value.ToString().Split(' ')[3];

            var jwtHandler = new JwtSecurityTokenHandler();
            var tokenJwt = jwtHandler.ReadJwtToken(token);

            //Assert
            Assert.Equal(issuerValidacao, tokenJwt.Issuer);
        }
    }
}
