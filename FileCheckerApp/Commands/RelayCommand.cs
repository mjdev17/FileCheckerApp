using System;
using System.Windows.Input; // Usado para ICommand en WPF
using Avalonia.Input; // Usado en Avalonia para otros componentes de entrada si es necesario

namespace FileCheckerApp.Commands
{
    // RelayCommand es una implementación básica de ICommand
    public class RelayCommand : ICommand
    {
        // La acción que se ejecuta cuando el comando se invoca
        private readonly Action<object?> _execute;

        // La función que determina si el comando puede ejecutarse (como un check)
        private readonly Func<object?, bool>? _canExecute;

        // Constructor de RelayCommand: recibe la acción a ejecutar y una función opcional para determinar si se puede ejecutar
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute)); // Lanza una excepción si la acción es null
            _canExecute = canExecute;
        }

        // Este método evalúa si el comando puede ejecutarse. 
        // Si _canExecute es null, el comando siempre puede ejecutarse.
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        // Este método ejecuta la acción que se pasó al constructor.
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        // El evento que se dispara cuando el estado de "CanExecute" puede haber cambiado.
        // Esto es importante para que la UI sepa cuándo habilitar o deshabilitar el comando.
        public event EventHandler? CanExecuteChanged
        {
            add
            {
#if NETFRAMEWORK // Solo en WPF (NET Framework o versiones compatibles con WPF)
                CommandManager.RequerySuggested += value; // WPF se encarga de notificar cuando CanExecute cambia
#else
                // En Avalonia no tenemos CommandManager, por lo que este evento no hace nada.
                // Pero si lo necesitas, podrías implementar un mecanismo manual para disparar el evento.
#endif
            }
            remove
            {
#if NETFRAMEWORK // Solo en WPF (NET Framework o versiones compatibles con WPF)
                CommandManager.RequerySuggested -= value; // WPF se encarga de notificar cuando CanExecute cambia
#else
                // En Avalonia no hacemos nada en este caso. Si quieres manejarlo, tendrías que hacerlo manualmente.
#endif
            }
        }
    }
}
