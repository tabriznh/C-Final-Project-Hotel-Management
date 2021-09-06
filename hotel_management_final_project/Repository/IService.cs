using System;
using System.Collections.Generic;

namespace hotel_management_final_project.Repository
{
    public interface IService<T>
    {
        List<T> GetAll();
        T Get(int id);
        T Create(T model);
        T Update(int id, T model);
        bool Delete(int id);
    }
}
