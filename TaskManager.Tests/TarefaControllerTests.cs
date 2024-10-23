using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.API.Controllers;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

public class TarefaControllerTests
{
    private readonly Mock<ITarefaService> _tarefaServiceMock;
    private readonly TarefaController _controller;

    public TarefaControllerTests()
    {
        _tarefaServiceMock = new Mock<ITarefaService>();
        _controller = new TarefaController(_tarefaServiceMock.Object);
    }

    // Teste para GET: /api/Tarefas (recuperar tarefas pendentes)
    [Fact]
    public async Task GetPendingTarefas_ReturnsOkResult_WithListOfTarefaDtos()
    {
        // Arrange
        var tarefas = new List<Tarefa>
    {
        new Tarefa { Id = 1, Title = "Tarefa teste 1", IsCompleted = false },
        new Tarefa { Id = 2, Title = "Tarefa teste 2", IsCompleted = false }
    };
        _tarefaServiceMock.Setup(service => service.GetTarefasPendentesAsync())
                          .ReturnsAsync(tarefas);

        // Act
        var result = await _controller.GetTarefasPendentes();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTarefas = Assert.IsType<List<TarefaDto>>(okResult.Value); // Expect List<TarefaDto>
        Assert.Equal(2, returnedTarefas.Count);
    }

    // Teste para POST: /api/Tarefas (criar nova tarefa)
    [Fact]
    public async Task CreateTarefa_ValidTarefaDto_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var newTarefaDto = new TarefaDto { Title = "Nova tarefa", IsCompleted = false };
        var newTarefa = new Tarefa { Id = 1, Title = "Nova tarefa", IsCompleted = false };
        _tarefaServiceMock.Setup(service => service.AddTarefaAsync(It.IsAny<Tarefa>()))
                        .Callback<Tarefa>(t => t.Id = 1)
                        .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Create(newTarefaDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.GetById), createdAtActionResult.ActionName);
        var createdTask = Assert.IsType<TarefaDto>(createdAtActionResult.Value);
        Assert.Equal(newTarefaDto.Title, createdTask.Title);
    }

    // Teste para PUT: /api/Tarefas/{id} (atualizar uma tarefa)
    [Fact]
    public async Task UpdateTarefa_ValidTarefaDto_ReturnsNoContent()
    {
        // Arrange
        var tarefa = new Tarefa { Id = 1, Title = "Tarefa original", IsCompleted = false };
        var updatedTarefaDto = new TarefaDto { Id = 1, Title = "Tarefa alterada", IsCompleted = true };

        _tarefaServiceMock.Setup(service => service.GetTarefaByIdAsync(1))
                          .ReturnsAsync(tarefa);
        _tarefaServiceMock.Setup(service => service.UpdateTarefaAsync(It.IsAny<Tarefa>()))
                          .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Update(1, updatedTarefaDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    // Teste para PUT: /api/Tarefas/{id} (tentativa de atualização com Ids diferentes)
    [Fact]
    public async Task UpdateTarefa_TarefaIdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var updatedTarefaDto = new TarefaDto { Id = 3, Title = "Tarefa com ID desconhecido", IsCompleted = true };

        // Act
        var result = await _controller.Update(1, updatedTarefaDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    // Teste para DELETE: /api/Tarefas/{id} (excluir uma tarefa existente)
    [Fact]
    public async Task DeleteTarefa_ValidTarefaId_ReturnsNoContent()
    {
        // Arrange
        var tarefa = new Tarefa { Id = 1, Title = "Tarefa a ser deletada", IsCompleted = false };

        _tarefaServiceMock.Setup(service => service.GetTarefaByIdAsync(tarefa.Id))
                          .ReturnsAsync(tarefa);
        _tarefaServiceMock.Setup(service => service.DeleteTarefaAsync(tarefa.Id))
                          .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(tarefa.Id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _tarefaServiceMock.Verify(service => service.GetTarefaByIdAsync(tarefa.Id), Times.Once);
        _tarefaServiceMock.Verify(service => service.DeleteTarefaAsync(tarefa.Id), Times.Once);
    }

    // Teste para DELETE: /api/Tarefas/{id} (tarefa não encontrada)
    [Fact]
    public async Task DeleteTarefa_TarefaNotFound_ReturnsNotFound()
    {
        // Arrange
        _tarefaServiceMock.Setup(service => service.DeleteTarefaAsync(It.IsAny<int>()))
                        .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(99); // ID inexistente

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
