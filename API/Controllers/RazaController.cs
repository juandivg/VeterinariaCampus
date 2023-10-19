using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class RazaController:BaseApiController
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RazaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RazaDto>>>Get1()
    {
        var razas=await _unitOfWork.Razas.GetAllAsync();
        return _mapper.Map<List<RazaDto>>(razas);
    }
       [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<RazaDto>>> Getpag([FromQuery] Params RazaParams)
    {
        var Raza = await _unitOfWork.Razas.GetAllAsync(RazaParams.PageIndex,RazaParams.PageSize,RazaParams.Search);
        var lstRazasDto = _mapper.Map<List<RazaDto>>(Raza.registros);
        return new Pager<RazaDto>(lstRazasDto,Raza.totalRegistros,RazaParams.PageIndex,RazaParams.PageSize,RazaParams.Search);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Raza>> Post(RazaDto razaDto)
    {
        var raza = _mapper.Map<Raza>(razaDto);
        this._unitOfWork.Razas.Add(raza);
        await _unitOfWork.SaveAsync();
        if (raza == null)
        {
            return BadRequest();
        }
        razaDto.Id = raza.Id;
        return CreatedAtAction(nameof(Post), new { id = razaDto.Id }, razaDto);
    }
    /// <summary>
    /// Modificar la informacion de un proveedor, el id debe ser preciso
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Raza>> Put(int id, [FromBody] RazaDto RazaDto)
    {
        var raza = _mapper.Map<Raza>(RazaDto);
        if (raza == null)
        {
            return NotFound();
        }
        _unitOfWork.Razas.Update(raza);
        await _unitOfWork.SaveAsync();
        return raza;
    }
    /// <summary>
    /// Eliminar una paciente por ID
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var raza = await _unitOfWork.Razas.GetByIdAsync(id);
        if (raza == null)
        {
            return NotFound();
        }
        _unitOfWork.Razas.Remove(raza);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("GetRazasxMascotas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RazaxMascotasDto>>>Get2()
    {
        var razas=await _unitOfWork.Razas.GetRazasxMascotas();
        return _mapper.Map<List<RazaxMascotasDto>>(razas);

    }
    }
