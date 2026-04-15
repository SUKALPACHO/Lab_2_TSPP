using System;
using System.Collections.Generic;
using Xunit;
using TodoApp;

namespace TodoApp.Tests
{
    public class ItemServiceTests
    {
        // --- 1. ТЕСТИ ДОДАВАННЯ (Task 1) ---

        
        [Fact] // [cite: 32]
        
        public void Add_ValidTitle_ShouldAddItem() // 1. Коректне додавання 
        {
            var service = new ItemService();
            var title = "Купити молоко";

            var item = service.Add(title);

            Assert.NotNull(item); // [cite: 37]
            Assert.Equal(title, item.Title); // [cite: 38]
        }

        [Fact]
        
        public void Add_EmptyTitle_ShouldThrowArgumentException() // 2. Помилка при пустому рядку 
        {
            var service = new ItemService();

            var exception = Assert.Throws<ArgumentException>(() => service.Add("  "));
            Assert.Equal("Title cannot be empty", exception.Message);
        }

        [Fact]
        public void Add_MultipleItems_ShouldHaveUniqueIds() // 3. Перевірка автоінкременту ID
        {
            var service = new ItemService();

            var item1 = service.Add("Задача 1");
            var item2 = service.Add("Задача 2");

            Assert.NotEqual(item1.Id, item2.Id);
            Assert.True(item2.Id > item1.Id);
        }

        // --- 2. ТЕСТИ ВИДАЛЕННЯ (Task 2) ---

        [Fact]
        
        public void Delete_ExistingItem_ShouldRemoveFromList() // 4. Видалення існуючої задачі 
        {
            var service = new ItemService();
            var item = service.Add("Тимчасова задача");

            service.Delete(item.Id);

            Assert.Null(service.GetById(item.Id));
            Assert.Empty(service.GetAll());
        }

        [Fact]
        
        public void Delete_NonExistingItem_ShouldThrowException() // 5. Видалення неіснуючої задачі 
        {
            var service = new ItemService();

            var exception = Assert.Throws<Exception>(() => service.Delete(999));
            Assert.Equal("Todo not found", exception.Message);
        }

        // --- 3. ТЕСТИ СТАТУСУ ТА ПОШУКУ (Task 3) ---

        [Fact]
        
        public void Complete_ValidId_ShouldMarkAsCompleted() // 6. Зміна статусу на виконано 
        {
            var service = new ItemService();
            var item = service.Add("Виконати лабу");

            service.Complete(item.Id);

            Assert.True(item.IsCompleted);
        }

        [Fact]
        
        public void Complete_InvalidId_ShouldThrowException() // 7. Неправильний ID для статусу 
        {
            var service = new ItemService();

            var exception = Assert.Throws<Exception>(() => service.Complete(-1));
            Assert.Equal("Item not found", exception.Message);
        }

        [Fact]
        public void GetCompleted_ShouldReturnOnlyFinishedTasks() // 8. Перевірка фільтрації списку
        {
            var service = new ItemService();
            var item1 = service.Add("Зроблено");
            var item2 = service.Add("В процесі");

            service.Complete(item1.Id);
            var completed = service.GetCompleted();

            Assert.Single(completed);
            Assert.Contains(completed, x => x.Title == "Зроблено");
            Assert.DoesNotContain(completed, x => x.Title == "В процесі");
        }
    }
}