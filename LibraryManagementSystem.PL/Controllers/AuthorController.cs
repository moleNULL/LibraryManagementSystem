using AutoMapper;
using LibraryManagementSystem.PL.ViewModels.AuthorViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IMapper _mapper;
    
    public AuthorController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<AuthorViewModel>> Get()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:int}")]
    public async Task<AuthorViewModel> Get(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<int> Add(AuthorViewModel authorViewModel)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:int}")]
    public async Task<bool> Update(AuthorViewModel authorViewModel)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public async Task<bool> Delete(IEnumerable<int> authorIds)
    {
        throw new NotImplementedException();
    }
}