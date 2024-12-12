using System;
using System.Threading.Tasks;
using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer;

public class AuthLogic
{
    private readonly UserLogic _userLogic;
    private readonly FailedLoginAttemptLogic _failedLoginAttemptLogic;

    public AuthLogic()
    {
        _userLogic = new UserLogic();
        _failedLoginAttemptLogic = new FailedLoginAttemptLogic();
    }

   public async Task<bool> ValidateLoginAsync(string email, string password, string ipAddress)
{
    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
    {
        throw new ArgumentException("Email y contraseña no pueden estar vacíos");
    }

    // Intenta recuperar al usuario por el email
    User user = _userLogic.RetrieveByEmail(email);
    
    if (user == null)
    {
        // Si el usuario no existe, registra un intento fallido
        var failedAttempt = new FailedLoginAttempt
        {
            AttemptTime = DateTime.Now,
            IpAddress = ipAddress
        };
        _failedLoginAttemptLogic.Create(failedAttempt);
        
        return false; // El usuario no existe, retorno falso sin más validaciones
    }

    // Si el usuario existe pero la contraseña no es correcta
    if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
    {
        // Registra el intento fallido
        var failedAttempt = new FailedLoginAttempt
        {
            UserId = user.UserId,
            AttemptTime = DateTime.Now,
            IpAddress = ipAddress
        };
        _failedLoginAttemptLogic.Create(failedAttempt);

        // Incrementa el contador de intentos fallidos
        user.FailedLoginAttempts++;
        
        if (user.FailedLoginAttempts >= 5)
        {
            user.IsActive = false;
            user.LockoutTime = DateTime.Now;
        }
        _userLogic.Update(user);

        return false; // Contraseña incorrecta
    }

    // Si la contraseña es correcta, resetea los intentos fallidos
    user.FailedLoginAttempts = 0;
    user.LastLogin = DateTime.Now;
    _userLogic.Update(user);

    return true; // Login exitoso
}

}
