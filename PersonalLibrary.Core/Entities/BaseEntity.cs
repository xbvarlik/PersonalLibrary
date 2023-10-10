﻿namespace PersonalLibrary.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public int DeletedBy { get; set; }
}