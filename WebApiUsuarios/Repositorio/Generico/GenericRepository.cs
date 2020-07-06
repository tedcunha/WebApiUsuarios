using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsuarios.Model.Base;
using WebApiUsuarios.Model.Context;
using WebApiUsuarios.Repositorio.Generico.Interface;

namespace WebApiUsuarios.Repositorio.Generico
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MySqlContext _context;
        private DbSet<T> dataset;

        public GenericRepository(MySqlContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public T Alterar(T item)
        {
            try
            {
                var retorno = dataset.SingleOrDefault(u => u.Id == item.Id);
                _context.Entry(retorno).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return item;
        }

        public T Cadastrar(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return item;
        }

        public void Deletar(long Id)
        {
            try
            {
                var retorno = dataset.SingleOrDefault(u => u.Id == Id);
                if (retorno != null)
                {
                    dataset.Remove(retorno);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> Pesquisar()
        {
            return dataset.ToList();
        }

        public T PesquisarPorID(long Id)
        {
            return dataset.SingleOrDefault(u => u.Id == Id);
        }
    }
}
