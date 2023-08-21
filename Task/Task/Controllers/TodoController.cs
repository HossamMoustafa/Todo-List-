using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Task.DTOS;
using Task.Models;
using Task.Repository;
using Task.Services;


namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        ITodoRepository todoRepository;
        public TodoController(ITodoRepository _todoRepository)
        {
            this.todoRepository = _todoRepository;
        }


        [HttpGet]
        public IActionResult Getall(int pagesize=50 , int pageindex=1 )
        {
             
            var todo_List = todoRepository.GetAll(pagesize, pageindex).ToList();

            return Ok(todo_List);

        }

        [HttpPost]
        public IActionResult Insert(TodoInsertionDto todo)

        {
            todoRepository.Add(todo);
            return Ok("created ");
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {

            var todo_List = todoRepository.GetById(id);

            return Ok(todo_List);

        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, UpdatedDto UpdatedDto)
        {


          var x =   todoRepository.Update(id, UpdatedDto);

            if (x==1)

            {
            
                return Ok(UpdatedDto);

            }
          return BadRequest(" error to update record ");



        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            
               var y =   todoRepository.Delete(id);
            if(y==1)
            {
                return Ok(" deleted ");
            }
            return BadRequest(" object is deleted  ");
        }


    }
}
