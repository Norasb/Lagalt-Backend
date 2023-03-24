namespace Lagalt_Backend.Models.Dto.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public DateTime DOC { get; set; }
        public string Text { get; set; } = null!;
        public int User { get; set; }
    }
}
