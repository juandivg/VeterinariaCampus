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
    public class TipoMovimientoController:BaseApiController
    {
            private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoMovimientoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
        [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<TipoMovimientoDto>>>Get1()
    {
        var TipoMovimientos=await _unitOfWork.TipoMovimientos.GetAllAsync();
        return _mapper.Map<List<TipoMovimientoDto>>(TipoMovimientos);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoMovimientoDto>>> Getpag([FromQuery] Params TipoMovimientoParams)
    {
        var TipoMovimiento = await _unitOfWork.TipoMovimientos.GetAllAsync(TipoMovimientoParams.PageIndex,TipoMovimientoParams.PageSize,TipoMovimientoParams.Search);
        var lstTipoMovimientosDto = _mapper.Map<List<TipoMovimientoDto>>(TipoMovimiento.registros);
        return new Pager<TipoMovimientoDto>(lstTipoMovimientosDto,TipoMovimiento.totalRegistros,TipoMovimientoParams.PageIndex,TipoMovimientoParams.PageSize,TipoMovimientoParams.Search);
    }
            [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<TipoMovimiento>> Post(TipoMovimientoDto TipoMovimientoDto)
    {
        var TipoMovimiento = _mapper.Map<TipoMovimiento>(TipoMovimientoDto);
        this._unitOfWork.TipoMovimientos.Add(TipoMovimiento);
        await _unitOfWork.SaveAsync();
        if (TipoMovimiento == null)
        {
            return BadRequest();
        }
        TipoMovimientoDto.Id = TipoMovimiento.Id;
        return CreatedAtAction(nameof(Post), new { id = TipoMovimientoDto.Id }, TipoMovimientoDto);
    }
  
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<TipoMovimiento>> Put(int id, [FromBody] TipoMovimientoDto TipoMovimientoDto)
    {
        var TipoMovimiento = _mapper.Map<TipoMovimiento>(TipoMovimientoDto);
        if (TipoMovimiento == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoMovimientos.Update(TipoMovimiento);
        await _unitOfWork.SaveAsync();
        return TipoMovimiento;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var TipoMovimiento = await _unitOfWork.TipoMovimientos.GetByIdAsync(id);
        if (TipoMovimiento == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoMovimientos.Remove(TipoMovimiento);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
