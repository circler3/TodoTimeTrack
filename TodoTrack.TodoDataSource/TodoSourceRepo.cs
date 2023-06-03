﻿using TodoTrack.Contracts;

namespace TodoTrack.TodoDataSource
{
    /// <summary>
    /// this is temporary use case.
    /// </summary>
    public class TodoSourceRepo : IRepo<TodoItem>
    {
        private readonly TodoDbContext _dbContext;
        public TodoSourceRepo()
        {
            _dbContext = new TodoDbContext();
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }
        //TODO: USE DTO TO MAKE ID IMUTTABLE
        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            item.Id = Guid.NewGuid().ToString();
            _dbContext.TodoItems.Add(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = _dbContext.TodoItems.Find(id);
            if (item == null) return false;
            _dbContext.TodoItems.Remove(item);
            await _dbContext.SaveChangesAsync(true);
            return true;
        }

        public async Task<IQueryable<TodoItem>> GetAsync()
        {
            return await Task.FromResult(_dbContext.TodoItems);
        }

        public async Task<bool> PostNewEntriesAsync(IEnumerable<ProcessPeriod> workFromProcesses)
        {
            _dbContext.ProcessPeriods.AddRange(workFromProcesses);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TodoItem?> UpdateAsync(string id, TodoItem item)
        {
            var target = _dbContext.TodoItems.Find(id);
            if (target == null) return null;
            _dbContext.Entry(target).CurrentValues.SetValues(item);
            await _dbContext.SaveChangesAsync();
            return target;
        }
    }
}