using Infotecs.Models.Dto;

namespace Infotecs.Models.Entities;

public class FileEntity
{
    public Guid Uid { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreationTime { get; set; }
    public DateTime UpdateTime { get; set; }
    
    public List<ValueEntity>? Values { get; set; }
    public ResultEntity? Result { get; set; }

    public FileDto ToDto() =>
        new()
        {
            Uid = Uid,
            Name = Name,
            CreationTime = CreationTime,
            UpdateTime = UpdateTime,
            Values = Values?.Select(x => x.ToDto()).ToList(),
            Result = Result?.ToDto()
        };
}