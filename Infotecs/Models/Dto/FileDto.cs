namespace Infotecs.Models.Dto;

public class FileDto
{
    public Guid Uid { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreationTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public List<ValueDto> Values { get; set; }
    public ResultDto? Result { get; set; }
}