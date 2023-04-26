using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IVillaNumberRepository;
using MagicVilla_VillaAPI.Repository.IVillaRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers;

[Route("api/VillaNumberAPI")]
[ApiController]
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
        this._response = new();
        _dbVilla = dbVilla;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetVillaNumbers()
    {
        try
        {
            IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync();
            _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorsMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }

    [HttpGet("{id:int}", Name = "GetVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
    {
        try
        {
            if (id == 0)
            {
                _logger.LogInformation("Get VillaNumber Error with Id: " + id);
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);

            if (villaNumber == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorsMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
    {

        try
        {
            if (await _dbVillaNumber.GetAsync(u => u.VillaNo == createDTO.VillaNo) != null)
            {
                _response.ErrorsMessages = new List<string>() { "Villa Number Already Exists" };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if(await _dbVilla.GetAsync(u => u.Id == createDTO.VillaId) == null)
            {
                _response.ErrorsMessages = new List<string>() { "Villa Id is invalid" };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (createDTO == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDTO);

            await _dbVillaNumber.CreateAsync(villaNumber);

            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorsMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);

            if (villaNumber == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            await _dbVillaNumber.RemoveAsync(villaNumber);

            _response.StatusCode = HttpStatusCode.NoContent;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorsMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }

    [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
    {
        try
        {
            if (updateDTO == null || id != updateDTO.VillaNo)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _dbVilla.GetAsync(u => u.Id == updateDTO.VillaId) == null)
            {
                _response.ErrorsMessages = new List<string>() { "Villa Id is invalid" };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);

            await _dbVillaNumber.UpdateAsync(model);

            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorsMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
}