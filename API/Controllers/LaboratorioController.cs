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
    public class LaboratorioController: BaseApiController
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public LaboratorioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<LaboratorioDto>>>Get1()
    {
        var Laboratorios=await _unitOfWork.Laboratorios.GetAllAsync();
        return _mapper.Map<List<LaboratorioDto>>(Laboratorios);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<LaboratorioDto>>> Getpag([FromQuery] Params LaboratorioParams)
    {
        var Laboratorio = await _unitOfWork.Laboratorios.GetAllAsync(LaboratorioParams.PageIndex,LaboratorioParams.PageSize,LaboratorioParams.Search);
        var lstLaboratoriosDto = _mapper.Map<List<LaboratorioDto>>(Laboratorio.registros);
        return new Pager<LaboratorioDto>(lstLaboratoriosDto,Laboratorio.totalRegistros,LaboratorioParams.PageIndex,LaboratorioParams.PageSize,LaboratorioParams.Search);
    }
            [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Laboratorio>> Post(LaboratorioDto LaboratorioDto)
    {
        var Laboratorio = _mapper.Map<Laboratorio>(LaboratorioDto);
        this._unitOfWork.Laboratorios.Add(Laboratorio);
        await _unitOfWork.SaveAsync();
        if (Laboratorio == null)
        {
            return BadRequest();
        }
        LaboratorioDto.Id = Laboratorio.Id;
        return CreatedAtAction(nameof(Post), new { id = LaboratorioDto.Id }, LaboratorioDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Laboratorio>> Put(int id, [FromBody] LaboratorioDto LaboratorioDto)
    {
        var Laboratorio = _mapper.Map<Laboratorio>(LaboratorioDto);
        if (Laboratorio == null)
        {
            return NotFound();
        }
        _unitOfWork.Laboratorios.Update(Laboratorio);
        await _unitOfWork.SaveAsync();
        return Laboratorio;
    }
 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var Laboratorio = await _unitOfWork.Laboratorios.GetByIdAsync(id);
        if (Laboratorio == null)
        {
            return NotFound();
        }
        _unitOfWork.Laboratorios.Remove(Laboratorio);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    }
