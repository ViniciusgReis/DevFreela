using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
        {
            //Arrange
            var projects = new List<Project>
            {
                new Project("Teste 1", "Descricao teste 1", 1, 2, 10000),
                new Project("Teste 2", "Descricao teste 2", 1, 2, 20000),
                new Project("Teste 3", "Descricao teste 3", 1, 2, 30000)
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            //Act
            var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList);
            Assert.Equal(projects.Count, projectViewModelList.Count); // verifica se o total de projetos retornados na ModelList é o mesmo do passado na base

            projectRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once); // Verifica se o método GetAllAsync foi chamado 1 vez e somente 1 vez.
        }

    }
}
