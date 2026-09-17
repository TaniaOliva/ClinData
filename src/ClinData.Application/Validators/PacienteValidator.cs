using System.Text.RegularExpressions;
using ClinData.Application.Interfaces;
using ClinData.Application.DTOs.Pacientes;

namespace ClinData.Application.Validators;

public class PacienteValidator
{
  private readonly IPacienteRepository _pacienteRepository;

  public PacienteValidator(IPacienteRepository pacienteRepository)
  {
      _pacienteRepository = pacienteRepository;
  } 
  public async Task<List<string>> CreacionPacienteAsync(CreacionPacienteDto dto)
  {
      var errores = new List<string>();

      if (string.IsNullOrWhiteSpace(dto.Nombre))
      {
          errores.Add("El nombre es obligatorio.");
      }

      if (string.IsNullOrWhiteSpace(dto.Apellido))
      {
          errores.Add("El apellido es obligatorio.");
      }
      if (string.IsNullOrWhiteSpace(dto.Identidad))
      {
          errores.Add("La identidad es obligatoria.");
      }
        else
    {
        var PatronIdentidad = @"^\d{4}-\d{4}-\d{5}$";  
        if (!Regex.IsMatch(dto.Identidad, PatronIdentidad))
        {
            errores.Add("La identidad no es válida el formato debe ser Hondureño: ####-####-#####.");
        }
        else
        {
            var existe = await _pacienteRepository
                .ExisteIdentidadAsync(dto.Identidad);

            if (existe)
            {
                errores.Add(
                    "Ya existe un paciente con esa identidad.");
            }

        }
    }
    return errores;

    }   
}