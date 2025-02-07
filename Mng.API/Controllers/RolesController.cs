﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mng.API.Models;
using Mng.Core.DTOs;
using Mng.Core.Entities;
using Mng.Core.Services;
using Mng.Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mng.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        // GET: api/<RoleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
        {
            try
            {
                var roleList = await _roleService.GetRoles();
                var roleListDto = _mapper.Map<IEnumerable<RoleDto>>(roleList);
                return Ok(roleListDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var role = await _roleService.GetRoleById(id);
                var roleDto = _mapper.Map<RoleDto>(role);
                return Ok(roleDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<RoleController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RolePostModel role)
        {
            try
            {
                var roleToAdd = await _roleService.Post(_mapper.Map<Role>(role));
                return Ok(roleToAdd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] RolePostModel role)
        {            
            try
            {
                var updateRole = await _roleService.Put(id, _mapper.Map<Role>(role));
                return Ok(updateRole);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var role =  _roleService.GetRoleById(id);
                if (role is null)
                {
                    return NotFound();
                }
                _roleService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
