using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class VeterinarioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VeterinarioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<VeterinarioDto>>>Get1()
    {
        var veterinarios=await _unitOfWork.Veterinarios.GetAllAsync();
        return _mapper.Map<List<VeterinarioDto>>(veterinarios);
    }
       [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Pager<VeterinarioDto>>> Getpag([FromQuery] Params VeterinarioParams)
    {
        var Veterinario = await _unitOfWork.Veterinarios.GetAllAsync(VeterinarioParams.PageIndex,VeterinarioParams.PageSize,VeterinarioParams.Search);
        var lstVeterinariosDto = _mapper.Map<List<VeterinarioDto>>(Veterinario.registros);
        return new Pager<VeterinarioDto>(lstVeterinariosDto,Veterinario.totalRegistros,VeterinarioParams.PageIndex,VeterinarioParams.PageSize,VeterinarioParams.Search);
    }
            [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Veterinario>> Post(VeterinarioDto VeterinarioDto)
    {
        var Veterinario = _mapper.Map<Veterinario>(VeterinarioDto);
        this._unitOfWork.Veterinarios.Add(Veterinario);
        await _unitOfWork.SaveAsync();
        if (Veterinario == null)
        {
            return BadRequest();
        }
        VeterinarioDto.Id = Veterinario.Id;
        return CreatedAtAction(nameof(Post), new { id = VeterinarioDto.Id }, VeterinarioDto);
    }
   
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Veterinario>> Put(int id, [FromBody] VeterinarioDto VeterinarioDto)
    {
        var Veterinario = _mapper.Map<Veterinario>(VeterinarioDto);
        if (Veterinario == null)
        {
            return NotFound();
        }
        _unitOfWork.Veterinarios.Update(Veterinario);
        await _unitOfWork.SaveAsync();
        return Veterinario;
    }
 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var Veterinario = await _unitOfWork.Veterinarios.GetByIdAsync(id);
        if (Veterinario == null)
        {
            return NotFound();
        }
        _unitOfWork.Veterinarios.Remove(Veterinario);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("GetVeterinariosEspecialidad")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<VeterinarioDto>>>Get2(string especialidad)
    {
        var veterinarios=await _unitOfWork.Veterinarios.GetVeterinariosEspecialidad(especialidad);
        return _mapper.Map<List<VeterinarioDto>>(veterinarios);

    }
}
