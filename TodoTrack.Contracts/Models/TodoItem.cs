﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace TodoTrack.Contracts
{
    public class TodoItem : IEntity
    {
        [Key]
        public virtual string Id { get; set; }= default!;
        public virtual string Name { get; set; } = string.Empty;
        public virtual string? Description { get; set; }
        public virtual string? Comment { get; set; }
        public virtual Project? Project { get; set; }
        [NotMapped]
        public virtual ICollection<Category>? Categories { get; set; }
        [NotMapped]
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        [NotMapped]
        public virtual ICollection<TodoItem>? SubTodoItems { get; set; }
        public virtual long? CreatedTimestamp { get; set; }
        public virtual long? FinishedTimestamp { get; set; }
        public virtual long? ScheduledBeginTimestamp { get; set; }
        public virtual long? ScheduledDueTimestamp { get; set; }
        public virtual long? LatestWorkTimestamp { get; set; }
        [NotMapped]
        public virtual IList<long>? NotifyTimestamp { get; set; }
        public virtual long? EstimatedDuration { get; set; }
        public virtual string? RepeatCron { get; set; }
        public virtual bool Repeatable { get; set; } = false;
        public virtual TodoStatus Status { get; set; }
        public virtual EisenhowerMatrix Priority { get; set; }
        [NotMapped]
        public virtual ICollection<WorkPeriod>? WorkPeriods { get; set; }
        [NotMapped]
        public virtual ICollection<Attachment>? Attachments { get; set; }

    }
}