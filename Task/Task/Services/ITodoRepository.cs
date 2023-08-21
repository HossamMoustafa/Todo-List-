using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Task.DTOS;
using Task.Models;

namespace Task.Services
{
    public interface ITodoRepository
    {

        //  put header of the function here

        public List<GetTods> GetAll(int pagesize , int pageindex );
        public GetTods GetById(int id);



        public int Delete(int id);
        public int Update(int id, UpdatedDto UpdatedDto);
        void Add(TodoInsertionDto todo);
    }
}
