namespace Gallery.Models
{
    public class ArtTechnique
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Painting> Paintings { get; set; }
    }
}
