using Infotecs.Models.Dto;

namespace Infotecs.Models.Entities;

public class ResultEntity
{
    public Guid Uid { get; set; }
    public Guid FileUid { get; set; }
    public int DateTimeDelta { get; set; }
    public DateTime MinDateTime { get; set; }
    public double AvgExecutionTime { get; set; }
    public decimal AvgValue { get; set; }
    public decimal MedianValue { get; set; }
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public FileEntity? File { get; set; }

    public ResultDto ToDto() =>
        new()
        {
            Uid = Uid,
            FileUid = FileUid,
            DateTimeDelta = DateTimeDelta,
            MinDateTime = MinDateTime,
            AvgExecutionTime = AvgExecutionTime,
            AvgValue = AvgValue,
            MedianValue = MedianValue,
            MinValue = MinValue,
            MaxValue = MaxValue,
        };
}