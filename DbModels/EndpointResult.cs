using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebCheckerAPI.Models;

[Table("EndpointResults")]
public class EndpointResult
{
	[Key]
	public Guid EndpointResultId { get; set; }
	public Guid EndpointId { get; set; }
	public DateTime CreatedTime { get; set; }
	public string State { get; set; }
}
