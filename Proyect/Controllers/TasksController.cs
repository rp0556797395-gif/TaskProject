using Microsoft.AspNetCore.Mvc;
using Proyect.Core.Services;
using Proyect.Core;
using System.Collections.Generic;
using Proyect.Core.Models;
using AutoMapper;
using Proyect.Core.DTO;
using Proyect.Service;
using Proyect.ModelDTO;




// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _Taskervice;
        private readonly IMapper _mapper;

        public TasksController(ITaskService context, IMapper _mapper)
        {
            _Taskervice = context;
            _mapper = _mapper;
        }
        // GET: api/<TasksController>
        [HttpGet]
        public async Task<List<TaskDTO>> Get()
        {
            var list =await _Taskervice.GetAllAsync();
            var tasksDTOlist = new List<TaskDTO>();
            tasksDTOlist = _mapper.Map<List<TaskDTO>>(list);
            return tasksDTOlist;

        }

        // GET api/<TasksController>/5
        [HttpGet("{code}")]
        public async Task<ActionResult> Get(int code)
        {
            var t =await _Taskervice.GetByIdAsync(code);
            var cdTO = _mapper.Map<TaskDTO>(t);

            if (t == null)
            {
                return NotFound();
            }
            return Ok(cdTO);
        }

        // POST api/<TasksController>
        [HttpPost]
        public async Task<ActionResult>Post([FromBody] TasksPostModel value)
        {
            var s = new Tasks { Name = value.Name, Category = value.Category };
            var c =await _Taskervice.GetBynameAsync(value.Name);
            if (c == null)
            {
               await _Taskervice.PostAsync(s);
                return Ok(value);
            }
            return Conflict();
        }

        // PUT api/<TasksController>/5
        [HttpPut("{code}")]
        public async Task<ActionResult> Put(int code, [FromBody] TasksPostModel value)
        {
            var s = new Tasks { Name = value.Name, Category = value.Category };
            var c =await _Taskervice.GetByIdAsync(code);
            if (c == null)
            {
                //נתון לויכוח  - האם להחזיר לא נמצא או להחזיר שגיאה בבקשה
                return NotFound();
            }
           await _Taskervice.PutAsync(code, s);
            return Ok(c);

        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{code}")]
        public async Task<ActionResult> Delete(int code)
        {
            var t =await _Taskervice.GetByIdAsync(code);
            if (t == null)
            {
                return BadRequest();
            }
           await _Taskervice.DeleteAsync(code);
            return NoContent();
        }

    }
}
