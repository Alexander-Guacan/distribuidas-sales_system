using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer;

public class FailedLoginAttemptLogic
{
    // Crear un registro de intento fallido
    public FailedLoginAttempt Create(FailedLoginAttempt attempt)
    {
        FailedLoginAttempt? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            result = repository.Create(attempt);
        }

        return result;
    }

    // Leer
    public FailedLoginAttempt RetrieveById(int attemptId)
    {
        FailedLoginAttempt? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            result = repository.Retrieve<FailedLoginAttempt>(a => a.AttemptId == attemptId);
        }

        return result;
    }

    // Actualizar
    public bool Update(FailedLoginAttempt attemptToUpdate)
    {
        bool result = false;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            // Recuperar el intento fallido actual
            FailedLoginAttempt existingAttempt = repository.Retrieve<FailedLoginAttempt>(a => a.AttemptId == attemptToUpdate.AttemptId);

            if (existingAttempt != null)
            {
                // Actualizar los campos permitidos
                existingAttempt.AttemptTime = attemptToUpdate.AttemptTime;
                existingAttempt.IpAddress = attemptToUpdate.IpAddress;

                // Guardar los cambios
                result = repository.Update(existingAttempt);
            }
        }

        return result;
    }
}
