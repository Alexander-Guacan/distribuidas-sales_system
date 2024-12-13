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

    public User Login(User userToLogin, string ipAddress)
    {
        User? result;

        if (string.IsNullOrEmpty(userToLogin.Email) || string.IsNullOrEmpty(userToLogin.Password))
        {
            throw new ArgumentException("Email or Password cannot be empty");
        }

        // Intenta recuperar al usuario por el Email
        result = _userLogic.RetrieveByEmail(userToLogin.Email);

        if (result == null)
        {
            // Si el usuario no existe, registra un intento fallido
            var failedAttempt = new FailedLoginAttempt
            {
                AttemptTime = DateTime.Now,
                IpAddress = ipAddress
            };
            _failedLoginAttemptLogic.Create(failedAttempt);
            throw new Exception("Email or Password are incorrect"); // El usuario no existe, retorno falso sin más validaciones
        }

        // Si el usuario existe pero la contraseña no es correcta
        if (!BCrypt.Net.BCrypt.Verify(userToLogin.Password, result.Password))
        {
            // Registra el intento fallido
            var failedAttempt = new FailedLoginAttempt
            {
                UserId = result.UserId,
                AttemptTime = DateTime.Now,
                IpAddress = ipAddress
            };
            _failedLoginAttemptLogic.Create(failedAttempt);

            // Incrementa el contador de intentos fallidos
            result.FailedLoginAttempts++;

            const int maxLoginAttempts = 3;
            if (result.FailedLoginAttempts >= maxLoginAttempts)
            {
                result.IsActive = false;
                result.LockoutTime = DateTime.Now;
                var emailServiceLogic = new EmailServiceLogic();
                emailServiceLogic.SendEmailAsync(result.Email, "Intentos fallidos de inicio de sesión", "Han intentado iniciar sesión múltiples veces en tu cuenta").Wait();
            }

            // Actualiza sin modificar la contraseña
            _userLogic.Update(result, skipPasswordCheck: true);

            throw new Exception("Email or Password are incorrect"); // Contraseña incorrecta
        }

        // Si la contraseña es correcta, resetea los intentos fallidos
        var roleLogic = new RoleLogic();

        if (result.RoleId == null)
        {
            throw new Exception("Role id is null");
        }

        result.Role = roleLogic.RetrieveById((int)result.RoleId);
        result.FailedLoginAttempts = 0;
        result.LastLogin = DateTime.Now;
        _userLogic.Update(result, skipPasswordCheck: true);

        return result; // Login exitoso
    }
}
