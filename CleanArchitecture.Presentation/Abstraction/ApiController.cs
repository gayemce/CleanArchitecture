using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Abstraction;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")] // Tüm kontroller için Authorize kontrolü yapar

public abstract class ApiController : ControllerBase
{
    // Application katmanında ki MedaitR yapısına yani CQRS patterna bir istek atabilmek için
    public readonly IMediator _mediator;

    protected ApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
