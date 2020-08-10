using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using HomeHealth.Web.Entities;
using System;
using Xunit;
using FluentAssertions;
using HomeHealth;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Bogus;
using HomeHealth.Web.Data;
using HomeHealth.Web.Data.Tables;
using HomeHealth.Web.Interfaces;
using HomeHealth.Web.Repositories;

namespace HomeHealth.Test.Repositories
{
    public class ServiceRepositoryTests 
    {
        protected DbContextOptions<HomeHealthDbContext> _contextOptions { get; }

        public ServiceRepositoryTests(){
            _contextOptions = new DbContextOptionsBuilder<HomeHealthDbContext>()
                .UseSqlite("FileName=Test.db").Options;
        }


        [Fact]
        public async Task Add_ServiceObjPassed_ProperMethodCalled()
        {
            //arange
            var faker = new Faker("en");
            var newService = new Service{
                ServiceName = faker.Lorem.Word()
            };

            using (var context = new HomeHealthDbContext(_contextOptions))
            {
                context.Database.EnsureCreated();
                
                var ServiceRepository = new Repository<Service>(context);

                ServiceRepository.Add(newService);

                await context.SaveChangesAsync();

                Assert.NotNull(newService.ServiceId);
            
            }
           

            //Act
           

            //Assert
           
        }
    }   
}