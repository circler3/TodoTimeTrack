﻿using Spectre.Console.Cli;
using Spectre.Console;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TodoTrack.Contracts;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Data;
using System.Text;
using Microsoft.Extensions.Primitives;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TodoTrack.Cli.Commands
{
    /// <summary>
    /// remove a todo item from list of today.
    /// </summary>
    public class RemoveTodoCommand : AsyncCommand<TodoSettings>
    {
        private readonly TodoHolder _todoHolder;

        public RemoveTodoCommand(TodoHolder todoHolder)
        {
            _todoHolder = todoHolder;
        }

        public override async Task<int> ExecuteAsync(CommandContext context, TodoSettings settings)
        {
            try
            {
                List<string> strList = RangeHelper.GetMatchedStringList(settings.IndexString, (await _todoHolder.GetAsync<TodoItem>()).OfType<IEntity>().ToList());
                await _todoHolder.RemoveTodayTodoItemAsync(strList);
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
                throw;
            }
            TableOutputHelper.BuildTable((await _todoHolder.GetAsync<TodoItem>()).Where(w => w.IsToday).ToList(), "Todo Today");
            return 0;
        }
    }
}