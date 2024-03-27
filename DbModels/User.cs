using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCheckerAPI.Models;

[Table("Users")]
public class User
{
	[Key]
	public Guid UserId { get; set; }
	public string Username { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Password { get; set; }
	public DateTime CreatedDate { get; set; }
}
