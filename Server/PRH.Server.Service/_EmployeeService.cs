//using AutoMapper;
//using HR.Contracts;
//using HR.Entities.Exceptions.BadRequest;
//using HR.Entities.Exceptions.NotFound;
//using HR.Entities.Models;
//using HR.Service.Contracts;
//using HR.Shared.DataTransferObjets;
//using HR.Shared.RequestFeatures;
//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HR.Service
//{
//    internal sealed class EmployeeService : IEmployeeService
//    {
//        //Properties
//        private readonly IRepositoryManager _repository;
//        private readonly ILoggerManager _logger;
//        private readonly IMapper _mapper;
//        private readonly IDataShaper<EmployeeDto> _dataShaper;


//        //Constructors
//        public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IDataShaper<EmployeeDto> dataShaper)
//        {
//            _repository = repositoryManager;
//            _logger = logger;
//            _mapper = mapper;
//            _dataShaper = dataShaper;
//        }


//        //Class Functions
//        public async Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
//        {
//            if (!employeeParameters.ValidAgeRange)
//                throw new MaxAgeRangeBadRequestException();

//            await CheckIfCompanyExists(companyId, trackChanges);

//            var employeesWithMetadata = await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges);            

//            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetadata);

//            var shapedData = _dataShaper.ShapeData(employeesDto,employeeParameters.Fields);

//            return (employees: shapedData, metaData: employeesWithMetadata.MetaData);
//        }

//        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
//        {
//            await CheckIfCompanyExists(companyId, trackChanges);

//            var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

//            var employee = _mapper.Map<EmployeeDto>(employeeDb);

//            return employee;
//        }

//        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
//        {
//            await CheckIfCompanyExists(companyId, trackChanges);

//            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

//            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
//            await _repository.SaveAsync();

//            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

//            return employeeToReturn;
//        }

//        public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
//        {
//            await CheckIfCompanyExists(companyId, trackChanges);

//            var employeeForCompany = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

//            _repository.Employee.DeleteEmployee(employeeForCompany);

//            await _repository.SaveAsync();
//        }

//        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges)
//        {
//            await CheckIfCompanyExists(companyId, compTrackChanges);

//            var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);

//            _mapper.Map(employeeForUpdate, employeeEntity);
//            await _repository.SaveAsync();
//        }

//        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
//        {
//            await CheckIfCompanyExists(companyId, compTrackChanges);

//            var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);

//            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

//            return (employeeToPatch, employeeEntity);
//        }

//        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
//        {
//            _mapper.Map(employeeToPatch, employeeEntity);
//            await _repository.SaveAsync();
//        }


//        //Accessory functions
//        private async Task CheckIfCompanyExists(Guid companyId, bool compTrackChanges)
//        {
//            var company = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges);
//            if (company is null)
//                throw new CompanyNotFoundException(companyId);
//        }
//        private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId, Guid id, bool empTrackChanges)
//        {
//            var employeeEntity = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
//            if (employeeEntity is null)
//                throw new EmployeeNotFoundException(id);
//            return employeeEntity;
//        }
//    }
//}
