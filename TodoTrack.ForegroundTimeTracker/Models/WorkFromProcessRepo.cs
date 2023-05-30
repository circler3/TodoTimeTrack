﻿using ForegroundTimeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTrack.Contracts;
using TodoTrack.TodoDataSource;

namespace TodoTImeTrack.ForegroundTimeTracker.Models
{
    public class WorkFromProcessRepo : IWorkFromProcessRepo
    {
        private readonly SQLiteDbContext _context;

        public WorkFromProcessRepo(SQLiteDbContext context)
        {
            _context = context;
        }
        public async Task<bool> PostNewEntriesAsync(IEnumerable<ProcessPeriod> workFromProcesses)
        {
            await _context.Database.EnsureCreatedAsync();
            await _context.WorkFromProcesses.AddRangeAsync(workFromProcesses);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
