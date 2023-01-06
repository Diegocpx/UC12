using ChapterBet9.Interfaces;
using Moq;

namespace TesteIntegracao
{
    public class LoginControllerTeste
    {
        [Fact]
        public void LoginController_retornar_Usuario_Invalido()
        {
            //arrange - prepara��o
            var repositoryFake = new Mock<IUsuarioRepository>();

            repositoryFake.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);



            //act - a��o


            //Assert - verifica��o
        }
    }
}