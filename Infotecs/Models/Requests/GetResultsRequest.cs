namespace Infotecs.Models.Requests;

public class GetResultsRequest
{
    public string? FileName { get; set; }
    public DateTime? MinStartTime { get; set; }
    public DateTime? MaxStartTime { get; set; }
    public decimal? MinAvgValue  { get; set; }
    public decimal?  MaxAvgValue { get; set; }
    public double? MinExecTime  { get; set; }
    public double? MaxExecTime { get; set; }
}