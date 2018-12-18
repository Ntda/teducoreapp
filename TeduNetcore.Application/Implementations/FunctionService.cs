using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduNetcore.Application.Interfaces;
using TeduNetcore.Application.ViewModels;
using TeduNetcore.Data.IRepositories;

namespace TeduNetcore.Application.Implementations
{
    public class FunctionService : IFunctionService
    {
        private readonly IFunctionRepository _functionRepository;
        private readonly ILogger<FunctionService> _logger;
        public FunctionService(IFunctionRepository functionRepository, ILogger<FunctionService> logger)
        {
            _functionRepository = functionRepository;
            _logger = logger;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<List<FunctionViewModel>> GetAll()
        {
            IQueryable<FunctionViewModel> function = _functionRepository.FindAll().ProjectTo<FunctionViewModel>();
            return function.ToListAsync();
        }

        public IList<FunctionViewModel> GetPermissionById(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
