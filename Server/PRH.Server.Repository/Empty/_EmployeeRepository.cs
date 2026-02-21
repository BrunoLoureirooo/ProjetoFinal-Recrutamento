//using HR.Contracts;
//using HR.Entities.Models;
//using HR.Repository.Extensions;
//using HR.Shared.RequestFeatures;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace HR.Repository
//{
//    internal sealed class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
//    {
//        public EmployeeRepository(RepositoryContext repositoryContext)
//        : base(repositoryContext)
//        {
//        }

//        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
//        { 
//            //var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
//            //    .OrderBy(e => e.Name)
//            //    .ToListAsync();

//            //return PagedList<Employee>
//            //    .ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);

//            var employees = await FindByCondition(e =>  
//                    e.CompanyId.Equals(companyId), trackChanges)                
                
//                //Paging
//                .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
//                .Take(employeeParameters.PageSize)
                
//                //Filtering
//                .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                
//                //Searching
//                .Search(employeeParameters.SearchTerm)
                
//                //Sorting
//                .Sort(employeeParameters.OrderBy)

//                .ToListAsync();

//            var count = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).CountAsync();

//            return new PagedList<Employee>(employees, count, employeeParameters.PageNumber, employeeParameters.PageSize);
//        }

//        //public async Task<(IEnumerable<Employee>, int)> GetEmployeesPaginatedAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
//        //{
//        //    //var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
//        //    //    .OrderBy(e => e.Name)
//        //    //    .ToListAsync();

//        //    //return PagedList<Employee>
//        //    //    .ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);

//        //    var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
//        //        .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
//        //        .Take(employeeParameters.PageSize)
//        //        .OrderBy(e => e.Name)
//        //        .ToListAsync();

//        //    var count = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).CountAsync();

//        //    return (employees, count);
//        //}

//        public async Task<Employee?> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) =>
//            await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
//                .SingleOrDefaultAsync();

//        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
//        {
//            employee.CompanyId = companyId;
//            Create(employee);
//        }

//        public void DeleteEmployee(Employee employee) => Delete(employee);
//    }
//}
