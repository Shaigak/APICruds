using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Employee;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(EmployeeCreateDto employe)=> await _employeeRepository.CreateAsync(_mapper.Map<Employee>(employe));


        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
           var data = await _employeeRepository.GetAllAsync();

           var mappedData = _mapper.Map<IEnumerable<EmployeeDto>>(data);

           return mappedData;

          //return _mapper.Map<IEnumerable<EmployeeDto>>(await _employeeRepository.GetAllAsync());
        }

        public async Task<EmployeeDto> GetByIdAsync(int? id) => _mapper.Map<EmployeeDto>(await _employeeRepository.GetByIdAsync(id));

        public async Task DeleteAsync(int? id)=> await _employeeRepository.DeleteAsync(await _employeeRepository.GetByIdAsync(id));

        public Task Search(string name, EmployeeSearchDto employee)
        {
            throw new NotImplementedException();
        }


        public async Task UpdateAsync(int id, EmployeeUpdateDto employee)
        {

            var dbEmployee = await _employeeRepository.GetByIdAsync(id);

            _mapper.Map(employee, dbEmployee);

            await _employeeRepository.UpdateAsync(dbEmployee);

        }

    }
}
