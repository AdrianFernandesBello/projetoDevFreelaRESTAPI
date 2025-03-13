using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTest.Core
{
    public class ProjectTests
    {
        //marcando como teste
        [Fact]
        public void ProjectIsCreated_Start_Sucess()
        {
            // Arrange Instanciando um novo project
            var project = new Project("Title proj", "Description", 1 , 2 , 10000);

            //Act (Ação)START
            project.Start();

            //Assert (Validações) Status do project passa a ser InProgress

            //                  (Valor esperado)         (valor real)
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
            Assert.NotNull(project.StartedAt);

            Assert.True(project.Status == ProjectStatusEnum.InProgress);
            Assert.False(project.StartedAt is null);

        }

        [Fact]
        // Validar se o project caso nao esteja no status created criar uma execeção
        public void ProjectInInvalidState_Start_ThrowsException()
        {
            // Arrange Instanciando um novo project
            var project = new Project("Title proj", "Description", 1, 2, 10000);

            //Act + Assert
            Action? start = project.Start;

            var exception = Assert.Throws<InvalidOperationException>(start);
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);

        }

        //implemnetar testes de Complete(OK), SetIsPayment(Falta), Update(Falta)

        public void ProjectInProgress_Complete_Sucess()
        {
            // Arrange Instanciando um novo project
            var project = new Project("Title proj", "Description", 1, 2, 10000);

            //Act (Ação)
            project.Complete();

            //Assert 
            Assert.Equal(ProjectStatusEnum.Completed, project.Status);
            Assert.True(project.Status == ProjectStatusEnum.Completed);
        }
    }
}
