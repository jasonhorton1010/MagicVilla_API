using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IVillaNumberRepository;
using MagicVilla_VillaAPI.Repository.IVillaRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers.v2;

[Route("api/v{version:apiVersion}/VillaNumberAPI")]
[ApiController]
[ApiVersion("2.0")]
public class VillaNumberAPIController : ControllerBase
{
    protected APIResponse _response;
    private readonly ILogger<VillaNumberAPIController> _logger;
    private readonly IMapper _mapper;
    private readonly IVillaNumberRepository _dbVillaNumber;
    private readonly IVillaRepository _dbVilla;


    public VillaNumberAPIController(ILogger<VillaNumberAPIController> logger, ApplicationDbContext db, IMapper mapper, IVillaNumberRepository dbVillaNumbers, IVillaRepository dbVilla)
    {
        _logger = logger;
        _mapper = mapper;
        _dbVillaNumber = dbVillaNumbers;
        _response = new();
        _dbVilla = dbVilla;
    }

    [HttpGet("GetString")]
    public IEnumerable<string> Get()
    {
        return new string[] { "Bhrugen", "DotNetMastery" };
    }
}