namespace Infotecs.Models.Dto;

public class ResultDto
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
}