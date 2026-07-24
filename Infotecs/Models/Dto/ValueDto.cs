namespace Infotecs.Models.Dto;

public class ValueDto
{
    public Guid Uid { get; set; }
    public Guid FileUid { get; set; }
    public DateTime Date { get; set; }
    public int ExecutionTime { get; set; }
    public decimal Value { get; set; }
}