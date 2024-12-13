using Entities;

namespace WebAppLayer.Models;

public class UserViewModel
{
    public List<User> Users { get; set; } = new List<User>(); // Lista de User
    public User? UserSearch { get; set; } // User encontrado
    public User? UserEdit { get; set; } // User encontrado
    public string Error { get; set; } = "";// Mensajes de error
}
