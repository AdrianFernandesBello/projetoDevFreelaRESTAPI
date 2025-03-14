using DevFreela.Application.Command.UpdateProject;
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
            project.Start();

            //Act + Assert
            Action? start = project.Start;

            var exception = Assert.Throws<InvalidOperationException>(start);
            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);

        }

        [Fact]
        public void ProjectInProgress_Complete_Sucess()
        {
            // Arrange Instanciando um novo project
            var project = new Project("Title proj", "Description", 1, 2, 10000);
            project.Start();

            //Act (Ação)
            project.Complete();

            //Assert 
            Assert.Equal(ProjectStatusEnum.Completed, project.Status);
            Assert.True(project.Status == ProjectStatusEnum.Completed);
        }

        [Fact]
        public void ProjectInProgress_SetIsPaymentPending_Sucess()
        {
            var project = new Project("Title proj", "Description", 1, 2, 10000);
            project.Start();

            project.SetPaymentPending();

            Assert.Equal(ProjectStatusEnum.PaymentPending, project.Status);
        }

        [Fact]
        public void RequestProjectUpdate_Update_Sucess()
        {
            var project = new Project("Title proj", "Description", 1, 2, 10000);

            project.Update("Title teste", "Description teste", 20000);

            Assert.Equal("Title teste", project.Title);
            Assert.Equal("Description teste", project.Description);
            Assert.Equal(20000, project.TotalCost);
        }
    }
}
