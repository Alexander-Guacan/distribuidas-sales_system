using DataAccessLayer;
using Entities;
using BCrypt.Net;

namespace BusinessLogicLayer;

public class UserLogic
{
    //CREAR
    public User Create(User user)
    {
        User? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            // Verificar si el email o nombre de usuario ya existen
            User existingUser = repository.Retrieve<User>(u => u.Email == user.Email || u.UserName == user.UserName);

            if (existingUser == null)
            {
                // Hash de la contraseña antes de guardar (ejemplo usando BCrypt)
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                result = repository.Create(user);
            }
        }

        return result;
    }

    //LEER
    public User RetrieveById(int id)
    {
        User? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            result = repository.Retrieve<User>(u => u.UserId == id);
        }

        return result;
    }

    //ACTUALIZAR
    public bool Update(User userToUpdate)
    {
        bool result = false;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            // Verificar si el email o nombre de usuario ya existen para otro usuario
            User temp = repository.Retrieve<User>(u =>
                (u.Email == userToUpdate.Email || u.UserName == userToUpdate.UserName) &&
                u.UserId != userToUpdate.UserId);

            if (temp == null)
            {
                // Recuperar el usuario actual para conservar los valores que no se deben modificar
                User existingUser = repository.Retrieve<User>(u => u.UserId == userToUpdate.UserId);

                if (existingUser != null)
                {
                    // Actualizar solo los campos permitidos
                    existingUser.UserName = userToUpdate.UserName;
                    existingUser.Email = userToUpdate.Email;
                    existingUser.Password = string.IsNullOrEmpty(userToUpdate.Password)
                        ? existingUser.Password
                        : BCrypt.Net.BCrypt.HashPassword(userToUpdate.Password);
                    existingUser.IsActive = userToUpdate.IsActive;
                    existingUser.LastLogin = userToUpdate.LastLogin;
                    existingUser.FailedLoginAttempts = userToUpdate.FailedLoginAttempts;
                    existingUser.LockoutTime = userToUpdate.LockoutTime;

                    // Guardar los cambios
                    result = repository.Update(existingUser);
                }
            }
        }

        return result;
    }

    //ELIMINAR
    public bool Delete(int id)
    {
        bool result = false;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            var user = RetrieveById(id);
            if (user != null)
            {
                result = repository.Delete(user);
            }
        }

        return result;
    }

    public bool LockUser(int id)
    {
        bool result = false;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            var user = RetrieveById(id);
            if (user != null)
            {
                user.IsActive = false;
                user.LockoutTime = DateTime.Now;
                result = repository.Update(user);
            }
        }

        return result;
    }

    public bool UnlockUser(int id)
    {
        bool result = false;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            var user = RetrieveById(id);
            if (user != null)
            {
                user.IsActive = true;
                user.LockoutTime = null;
                user.FailedLoginAttempts = 0;
                result = repository.Update(user);
            }
        }

        return result;
    }
}
