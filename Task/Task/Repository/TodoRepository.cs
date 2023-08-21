using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.Immutable;
using Task.DTOS;
using Task.Models;
using Task.Services;

      namespace Task.Repository
        {
    [Authorize]
    public class TodoRepository : ITodoRepository
    {

        //  CruD
        ///  we will inject context in this ctor

        ApplicationDbContext _context;
        public TodoRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }


        ///  index is the page /pagesize number of todos(element ) returned in one page 
        public List<GetTods> GetAll( int pagesize ,  int pageindex)
        {

            pagesize= pagesize<=1 ? 1 : pagesize;
            pageindex= pageindex<=1 ? 1 : (pageindex);


            List<Todo> gettodos = _context.Todos.Include(e=>e.ApplicationUser)
                .Skip((pageindex - 1) * pagesize)
                .Take(pagesize)
                .ToList();

            var returned_todos = gettodos.Select(e=> new GetTods
            {
            
              UserId= e.ApplicationUserId , 
              Title= e.Title,
              Completed= e.Completed,
              id = e.Id,
            
            }).ToList(); 
            
           
            return (returned_todos);
        }

        public GetTods GetById(int id)
        {
           Todo todo = _context.Todos.Include(e=>e.ApplicationUser).SingleOrDefault(x => x.Id == id);
            GetTods gettodos = new GetTods();
            gettodos.Completed = todo.Completed;
            gettodos.Title = todo.Title;
            gettodos.id = todo.Id;
            gettodos.UserId = todo.ApplicationUserId;


            return (gettodos); 


        }

        public void Add(TodoInsertionDto todoInsertionDto)
        {

            var Todo = new Todo { Title = todoInsertionDto.Title, Completed = (bool)todoInsertionDto.Completed
            
            , ApplicationUserId= todoInsertionDto.UserId 

            };

            _context.Todos.Add(Todo);
            _context.SaveChanges();

        }

        public int  Delete(int id)
        {
            Todo? Req_Deleted = _context.Todos.SingleOrDefault(x => x.Id == id);
            if (Req_Deleted is null)
            {
                return 0;
            }

            
            _context.Todos.Remove(Req_Deleted);
            return _context.SaveChanges();


        }

        public int Update(int id, UpdatedDto UpdatedDto)
        {



            Todo? Req_Updated = _context.Todos.SingleOrDefault(x => x.Id == id);
            if(Req_Updated is null )
            {
                return 0;  
            }

            Req_Updated.Title = UpdatedDto.Title;
            Req_Updated.Completed = UpdatedDto.Completed;
            

            return _context.SaveChanges(); /// return 0 or  1 
        }

        
    } 
            }
