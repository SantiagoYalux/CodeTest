using CodeTestTotal.Models;

namespace CodeTestTotal.ViewModel
{
    public class ListClientPetsViewModel
    {
        public int clientID { get; set; }
        public string clientName { get; set; }

        public List<Mascota> pets { get; set; }
        public int cantOrder { get; set; }
    }
}
