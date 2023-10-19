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
public class MascotaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public MascotaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MascotaDto>>>Get1()
    {
        var mascotas=await _unitOfWork.Mascotas.GetAllAsync();
        return _mapper.Map<List<MascotaDto>>(mascotas);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MascotaDto>>> Getpag([FromQuery] Params MascotaParams)
    {
        var Mascota = await _unitOfWork.Mascotas.GetAllAsync(MascotaParams.PageIndex,MascotaParams.PageSize,MascotaParams.Search);
        var lstMascotasDto = _mapper.Map<List<MascotaDto>>(Mascota.registros);
        return new Pager<MascotaDto>(lstMascotasDto,Mascota.totalRegistros,MascotaParams.PageIndex,MascotaParams.PageSize,MascotaParams.Search);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Mascota>> Post(MascotaDto MascotaDto)
    {
        var Mascota = _mapper.Map<Mascota>(MascotaDto);
        this._unitOfWork.Mascotas.Add(Mascota);
        await _unitOfWork.SaveAsync();
        if (Mascota == null)
        {
            return BadRequest();
        }
        MascotaDto.Id = Mascota.Id;
        return CreatedAtAction(nameof(Post), new { id = MascotaDto.Id }, MascotaDto);
    }
    /// <summary>
    /// Modificar la informacion de un proveedor, el id debe ser preciso
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Mascota>> Put(int id, [FromBody] MascotaDto MascotaDto)
    {
        var Mascota = _mapper.Map<Mascota>(MascotaDto);
        if (Mascota == null)
        {
            return NotFound();
        }
        _unitOfWork.Mascotas.Update(Mascota);
        await _unitOfWork.SaveAsync();
        return Mascota;
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
        var Mascota = await _unitOfWork.Mascotas.GetByIdAsync(id);
        if (Mascota == null)
        {
            return NotFound();
        }
        _unitOfWork.Mascotas.Remove(Mascota);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("GetMascotasEspecie/{especie}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MascotaxRazaDto>>>Get2(string especie)
    {
        var mascotas=await _unitOfWork.Mascotas.GetMascotasEspecie(especie);
        return _mapper.Map<List<MascotaxRazaDto>>(mascotas);

    }
}
