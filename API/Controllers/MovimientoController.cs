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
    public class MovimientoController:BaseApiController
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public MovimientoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet("GetMovimientos")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MovimientoTotalDto>>>Get1()
    {
        var movimientos=await _unitOfWork.MovimientoMedicamentos.GetMovimientos();
        return _mapper.Map<List<MovimientoTotalDto>>(movimientos);
    }
        [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MovimientoDto>>> Getpag([FromQuery] Params MovimientoParams)
    {
        var Movimiento = await _unitOfWork.MovimientoMedicamentos.GetAllAsync(MovimientoParams.PageIndex,MovimientoParams.PageSize,MovimientoParams.Search);
        var lstMovimientosDto = _mapper.Map<List<MovimientoDto>>(Movimiento.registros);
        return new Pager<MovimientoDto>(lstMovimientosDto,Movimiento.totalRegistros,MovimientoParams.PageIndex,MovimientoParams.PageSize,MovimientoParams.Search);
    }
            [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MovimientoMedicamento>> Post(MovimientoDto MovimientoDto)
    {
        var Movimiento = _mapper.Map<MovimientoMedicamento>(MovimientoDto);
        this._unitOfWork.MovimientoMedicamentos.Add(Movimiento);
        await _unitOfWork.SaveAsync();
        if (Movimiento == null)
        {
            return BadRequest();
        }
        MovimientoDto.Id = Movimiento.Id;
        return CreatedAtAction(nameof(Post), new { id = MovimientoDto.Id }, MovimientoDto);
    }
    /// <summary>
    /// Modificar la informacion de un proveedor, el id debe ser preciso
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MovimientoMedicamento>> Put(int id, [FromBody] MovimientoDto MovimientoDto)
    {
        var Movimiento = _mapper.Map<MovimientoMedicamento>(MovimientoDto);
        if (Movimiento == null)
        {
            return NotFound();
        }
        _unitOfWork.MovimientoMedicamentos.Update(Movimiento);
        await _unitOfWork.SaveAsync();
        return Movimiento;
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
        var Movimiento = await _unitOfWork.MovimientoMedicamentos.GetByIdAsync(id);
        if (Movimiento == null)
        {
            return NotFound();
        }
        _unitOfWork.MovimientoMedicamentos.Remove(Movimiento);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
