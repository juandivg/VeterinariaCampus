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
    public class TratamientoController:BaseApiController
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TratamientoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
        [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<TratamientoDto>>>Get1()
    {
        var Tratamientos=await _unitOfWork.Tratamientos.GetAllAsync();
        return _mapper.Map<List<TratamientoDto>>(Tratamientos);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TratamientoDto>>> Getpag([FromQuery] Params TratamientoParams)
    {
        var Tratamiento = await _unitOfWork.Tratamientos.GetAllAsync(TratamientoParams.PageIndex,TratamientoParams.PageSize,TratamientoParams.Search);
        var lstTratamientosDto = _mapper.Map<List<TratamientoDto>>(Tratamiento.registros);
        return new Pager<TratamientoDto>(lstTratamientosDto,Tratamiento.totalRegistros,TratamientoParams.PageIndex,TratamientoParams.PageSize,TratamientoParams.Search);
    }
            [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Tratamiento>> Post(TratamientoDto TratamientoDto)
    {
        var Tratamiento = _mapper.Map<Tratamiento>(TratamientoDto);
        this._unitOfWork.Tratamientos.Add(Tratamiento);
        await _unitOfWork.SaveAsync();
        if (Tratamiento == null)
        {
            return BadRequest();
        }
        TratamientoDto.Id = Tratamiento.Id;
        return CreatedAtAction(nameof(Post), new { id = TratamientoDto.Id }, TratamientoDto);
    }
 
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Tratamiento>> Put(int id, [FromBody] TratamientoDto TratamientoDto)
    {
        var Tratamiento = _mapper.Map<Tratamiento>(TratamientoDto);
        if (Tratamiento == null)
        {
            return NotFound();
        }
        _unitOfWork.Tratamientos.Update(Tratamiento);
        await _unitOfWork.SaveAsync();
        return Tratamiento;
    }
 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var Tratamiento = await _unitOfWork.Tratamientos.GetByIdAsync(id);
        if (Tratamiento == null)
        {
            return NotFound();
        }
        _unitOfWork.Tratamientos.Remove(Tratamiento);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    }
