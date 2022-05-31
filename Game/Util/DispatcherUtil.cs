using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Game.Util
{
    public static class DispatcherUtil
    {
        public static void Dispatcher(Action acao) =>
            Application.Current.Dispatcher.Invoke(acao, DispatcherPriority.Background);

        public static T Dispatcher<T>(Func<T> acao)
        {
            var result = default(T);
            Application.Current.Dispatcher.Invoke(() => result = acao(), DispatcherPriority.Background);
            return result;
        }

        public static async Task DispatcherAsync(Action acao) =>
                    await Application.Current.Dispatcher.InvokeAsync(acao, DispatcherPriority.Background).Task;
        
        public static async Task<T> DispatcherAsync<T>(Func<T> acao) =>
                    await Application.Current.Dispatcher.InvokeAsync(acao, DispatcherPriority.Background).Task;
    }
}
