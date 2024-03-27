using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace WebCheckerAPI.Models;

[Table("Endpoints")]
public class Endpoint
{
	[Key]
	public Guid EndpointId { get; set; }
	public string Url { get; set; }
	public EndpointTime Time { get; set; }
	public DateTime LastRequestDate { get; set; }
}

public enum EndpointTime : short
{
	EveryMinute = 1,
	EveryTwoMinutes = 2,
	EveryFiveMinutes = 3,
	EveryTenMinutes = 4,
	EveryThirtyMinutes = 5,
	EveryHour = 6,
	EveryDay = 7
}
