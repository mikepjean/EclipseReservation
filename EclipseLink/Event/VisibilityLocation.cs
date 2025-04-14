using System.ComponentModel.DataAnnotations;

namespace EclipseLink.EventManagement
{
    public class VisibilityLocation
    {
        [Key]
        public int Id { get; set; }
        public string? Location { get; internal set; }
        public string? LocationName { get; internal set; }
    }
}
