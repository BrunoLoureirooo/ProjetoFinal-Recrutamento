//using HR.Entities.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Linq.Dynamic.Core;
//using HR.Repository.Extensions.Utility;

//namespace HR.Repository.Extensions
//{
//    public static class RepositoryEmployeeExtensions
//    {
//        public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge)
//            => employees.Where(e => (e.Age >= minAge && e.Age <= maxAge));

//        public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
//        {
//            if (string.IsNullOrWhiteSpace(searchTerm))
//                return employees;

//            var lowerCaseTerm = searchTerm.Trim().ToLower();

//            return employees.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
//        }

//        public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
//        {
//            if (string.IsNullOrWhiteSpace(orderByQueryString))
//                return employees.OrderBy(e => e.Name);
//            string orderQuery = OrderQueryBuilder.CreateOrOrderQuery(orderByQueryString);

//            if (string.IsNullOrWhiteSpace(orderQuery))
//                return employees.OrderBy(e => e.Name);

//            return employees.OrderBy(orderQuery);
//        }
//    }
//}
