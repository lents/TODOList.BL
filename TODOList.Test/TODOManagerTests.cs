using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TODOList.BL;

namespace TODOList.Test
{
    public class TODOManagerTests
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly Fixture _fixture;
        private readonly ToDoManager _toDoManager;

        public TODOManagerTests() {
            _fixture = new Fixture();
            _mockTaskRepository = new Mock<ITaskRepository>();
            _toDoManager = new ToDoManager(_mockTaskRepository.Object);
        }

        [Fact]
        public void CreateTask_ValidInput_AddTaskToRepository()
        {
            //Arrange
            var title = _fixture.Create<string>();
            var description = _fixture.Create<string>();

            //Act
            var task = _toDoManager.CreateTask(title, description);

            //Assert
            task.Should().NotBeNull();
            task.Title.Should().Be(title);
            task.Description.Should().Be(description);
            task.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            _mockTaskRepository.Verify(repo => repo.Add(task), Times.Once);
        }

        [Fact]
        public void CreateTask_TitleIsNull_InvalidArgumentException() {            
            string title = null;
            var description = _fixture.Create<string>();

            Action act = () =>  _toDoManager.CreateTask(title, description);

            act.Should().Throw<ArgumentException>().WithMessage("Title cannot be null or empty. (Parameter 'title')");
        }

        [Fact]
        public void CreateTask_TitleIsEmpty_InvalidArgumentException()
        {
            string title = string.Empty;
            var description = _fixture.Create<string>();

            Action act = () => _toDoManager.CreateTask(title, description);

            act.Should().Throw<ArgumentException>().WithMessage("Title cannot be null or empty. (Parameter 'title')");
        }

        //[Theory, AutoData]
        //public void TestTEST(int first, int second, int sum)
        //{
        //    var fixture = new Fixture().Customize(new AutoMoqCustomization());

        //    var validEntity = fixture.Create<Task>();

        //    var repositoryMock = fixture.Freeze<Mock<ITaskRepository>>();
        //}
    }
}