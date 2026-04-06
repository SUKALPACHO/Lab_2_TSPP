using Xunit;
using System;
using TodoApp;

namespace TodoApp.Tests
{
    public class TodoItemTests
    {
        
        [Fact]
        public void Add_ValidTitle_ShouldAddItem()
        {
            
            var service = new ItemService();
            var title = "Купити хліб";

           
            var item = service.Add(title);

            
             Assert.NotNull(item); 
             Assert.Equal(title, item.Title); 
        }

        [Fact]
        public void Add_EmptyTitle_ShouldThrowArgumentException()
        {
       
            var service = new ItemService();

           
             var exception = Assert.Throws<ArgumentException>(() => service.Add("")); 
            Assert.Equal("Title cannot be empty", exception.Message);
        }

        
        [Fact]
        public void Delete_ExistingItem_ShouldRemoveTask()
        {
            
            var service = new ItemService();
            var item = service.Add("Задача для видалення");

            
             service.Delete(item.Id); 

            
            var found = service.GetById(item.Id);
            Assert.Null(found); 
        }

        [Fact]
        public void Delete_NonExistingItem_ShouldThrowException()
        {
            
            var service = new ItemService();

           
            var exception = Assert.Throws<Exception>(() => service.Delete(999)); 
            Assert.Equal("Todo not found", exception.Message);
        }

       
        [Fact]
        public void Complete_ValidId_ShouldChangeStatus()
        {
            
            var service = new ItemService();
            var item = service.Add("Нова задача");

            
            service.Complete(item.Id); 

             Assert.True(item.IsCompleted); 
        }

        [Fact]
        public void Complete_InvalidId_ShouldThrowException()
        {
            
            var service = new ItemService();

            
            var exception = Assert.Throws<Exception>(() => service.Complete(-1)); 
            Assert.Equal("Item not found", exception.Message);
        }
    }
}