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
public class CitaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CitaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
        
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<CitaDto>>>Get1()
    {
        var citas=await _unitOfWork.Citas.GetAllAsync();
        return _mapper.Map<List<CitaDto>>(citas);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CitaDto>>> Getpag([FromQuery] Params CitaParams)
    {
        var Cita = await _unitOfWork.Citas.GetAllAsync(CitaParams.PageIndex,CitaParams.PageSize,CitaParams.Search);
        var lstCitasDto = _mapper.Map<List<CitaDto>>(Cita.registros);
        return new Pager<CitaDto>(lstCitasDto,Cita.totalRegistros,CitaParams.PageIndex,CitaParams.PageSize,CitaParams.Search);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Cita>> Post(CitaDto CitaDto)
    {
        var Cita = _mapper.Map<Cita>(CitaDto);
        this._unitOfWork.Citas.Add(Cita);
        await _unitOfWork.SaveAsync();
        if (Cita == null)
        {
            return BadRequest();
        }
        CitaDto.Id = Cita.Id;
        return CreatedAtAction(nameof(Post), new { id = CitaDto.Id }, CitaDto);
    }
  
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Cita>> Put(int id, [FromBody] CitaDto CitaDto)
    {
        var Cita = _mapper.Map<Cita>(CitaDto);
        if (Cita == null)
        {
            return NotFound();
        }
        _unitOfWork.Citas.Update(Cita);
        await _unitOfWork.SaveAsync();
        return Cita;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var Cita = await _unitOfWork.Citas.GetByIdAsync(id);
        if (Cita == null)
        {
            return NotFound();
        }
        _unitOfWork.Citas.Remove(Cita);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("GeMascotasCita/{motivo}&{fechainicio}&{fechafinal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<CitaxMascotaDto>>>Get2(string motivo, DateTime fechainicio, DateTime fechafinal)
    {
        var citas=await _unitOfWork.Citas.GetMascotasCita(motivo, fechainicio, fechafinal);
        return _mapper.Map<List<CitaxMascotaDto>>(citas);

    }
    [HttpGet("GeMascotasVeterinario/{veterinario}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<CitaxMascotaDto>>>Get3(string veterinario)
    {
        var citas=await _unitOfWork.Citas.GetMascotasVeterinario(veterinario);
        return _mapper.Map<List<CitaxMascotaDto>>(citas);

    }
}
