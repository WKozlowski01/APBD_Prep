using Kolokwium_ORM.DTOs;
using Kolokwium_ORM.Exceptions;
using Kolokwium_ORM.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium_ORM.Controller;

[Route("/api")]
[ApiController]
public class AppController : ControllerBase
{
    private readonly IDbService _dbService;

    public AppController(IDbService dbService)
    {
        _dbService = dbService;
    }



   
    
    
}