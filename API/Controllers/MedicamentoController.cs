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
public class MedicamentoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MedicamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
     [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<MedicamentoDto>>>Get1()
    {
        var medicamentos=await _unitOfWork.Medicamentos.GetAllAsync();
        return _mapper.Map<List<MedicamentoDto>>(medicamentos);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MedicamentoDto>>> Getpag([FromQuery] Params MedicamentoParams)
    {
        var Medicamento = await _unitOfWork.Medicamentos.GetAllAsync(MedicamentoParams.PageIndex,MedicamentoParams.PageSize,MedicamentoParams.Search);
        var lstMedicamentosDto = _mapper.Map<List<MedicamentoDto>>(Medicamento.registros);
        return new Pager<MedicamentoDto>(lstMedicamentosDto,Medicamento.totalRegistros,MedicamentoParams.PageIndex,MedicamentoParams.PageSize,MedicamentoParams.Search);
    }
        [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Medicamento>> Post(MedicamentoDto MedicamentoDto)
    {
        var Medicamento = _mapper.Map<Medicamento>(MedicamentoDto);
        this._unitOfWork.Medicamentos.Add(Medicamento);
        await _unitOfWork.SaveAsync();
        if (Medicamento == null)
        {
            return BadRequest();
        }
        MedicamentoDto.Id = Medicamento.Id;
        return CreatedAtAction(nameof(Post), new { id = MedicamentoDto.Id }, MedicamentoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Medicamento>> Put(int id, [FromBody] MedicamentoDto MedicamentoDto)
    {
        var Medicamento = _mapper.Map<Medicamento>(MedicamentoDto);
        if (Medicamento == null)
        {
            return NotFound();
        }
        _unitOfWork.Medicamentos.Update(Medicamento);
        await _unitOfWork.SaveAsync();
        return Medicamento;
    }
 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var Medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);
        if (Medicamento == null)
        {
            return NotFound();
        }
        _unitOfWork.Medicamentos.Remove(Medicamento);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("GetMedicamentosLaboratorio")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<MedicamentoDto>>>Get2(string laboratorio)
    {
        var medicamentos=await _unitOfWork.Medicamentos.GetMedicamentosLaboratorio(laboratorio);
        return _mapper.Map<List<MedicamentoDto>>(medicamentos);

    }
    [HttpGet("GetMedicamentosxPrecio")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<MedicamentoDto>>>Get3(decimal precio)
    {
        var medicamentos=await _unitOfWork.Medicamentos.GetMedicamentosxPrecio(precio);
        return _mapper.Map<List<MedicamentoDto>>(medicamentos);

    }
}
