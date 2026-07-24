using Infotecs.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Infotecs.Models.Entities;

public class ValueEntity
{
    public Guid Uid { get; set; }
    public Guid FileUid { get; set; }
    public DateTime Date { get; set; }
    public int ExecutionTime { get; set; }
    public decimal Value { get; set; }
    
    public FileEntity?  File { get; set; }

    public ValueDto ToDto() =>
        new()
        {
            Uid = Uid,
            FileUid = FileUid,
            Date = Date,
            ExecutionTime = ExecutionTime,
            Value = Value
        };
}