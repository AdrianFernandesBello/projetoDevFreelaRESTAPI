using DevFreela.Application.Command.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTest.Fakes;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTest.Application
{
    public class DeleteProjectHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Delete_Sucess_NSubstitute()
        {
            //Arrange
            //var project = new Project("Project A", "Description", 1, 2, 10000);

            var project = FakeDataHelper.CreateFakeProject();

            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(1).Returns(Task.FromResult((Project?)project));
            repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

            var handler = new DeleteProjectHandler (repository);

            var commmand = new DeleteProjectCommand(1);

            //Act
            var result = await handler.Handle(commmand, new CancellationToken());

            //Assert
            Assert.True(result.IsSucess);
            await repository.Received(1).GetById(1);
            await repository.Received(1).Update(Arg.Any<Project>());
        }

        [Fact]
        public async Task ProjectDoesNotExist_Delete_Error_Nsubstitute()
        {
            //arrange
            var repository = Substitute.For<IProjectRepository>();
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

            var handler = new DeleteProjectHandler(repository);

            var commmand = new DeleteProjectCommand(1);

            //act
            var result = await handler.Handle(commmand, new CancellationToken());

            //assert
            Assert.False(result.IsSucess);
            Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);

            await repository.Received(1).GetById(Arg.Any<int>());
            await repository.DidNotReceive().Update(Arg.Any<Project>());
        }
    }
}
