using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTest;

public class CarsControllerUnitTest
{
    [Fact]
    public async void Create_ReturnsOkResult_WhenRequestIsValid()
    {
        //Arrange - Tan�mlamalar�n yap�ld��� par�a
        var mediatorMock = new Mock<IMediator>();
        CreateCarCommand createCarCommand = new(
            "Toyoto", "Corolla", 5000);
        MessageResponse response = new("Ara� ba�ar�yla kaydedildi!");
        CancellationToken cancellationToken = new();

        mediatorMock.Setup(m => m.Send(createCarCommand, cancellationToken))
            .ReturnsAsync(response);

        CarsController carsController = new(mediatorMock.Object);

        //Act - ��lemin yapt�r�l�p sonucun bir de�i�kene at�ld��� par�a
        var result = await carsController.Create(createCarCommand, cancellationToken);

        //Assert - Elde edilen resultun kontrol edildi�i par�a
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

        Assert.Equal(response, returnValue);
        mediatorMock.Verify(m => m.Send(createCarCommand, cancellationToken),
            Times.Once);
    }
}